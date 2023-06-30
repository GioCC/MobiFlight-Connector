using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MobiFlight;

namespace System
{
    public static class ComboBoxHelper
    {
        static public bool SetSelectedItem(ComboBox comboBox, string value)
        {
            if (comboBox.FindStringExact(value) != -1)
            {
                comboBox.SelectedIndex = comboBox.FindStringExact(value);
                return true;
            }
            return false;
        }

        static public bool SetSelectedItemByIndex(ComboBox comboBox, int index)
        {
            if (comboBox.Items.Count > index)
            {
                comboBox.SelectedIndex = index;
                return true;
            }
            return false;
        }

        static public bool SetSelectedItemByPart(ComboBox comboBox, string value)
        {
            foreach (object item in comboBox.Items)
            {
                if ((item.ToString()).Contains(value))
                {
                    comboBox.SelectedIndex = comboBox.FindStringExact(item.ToString());
                    return true;
                }
            }

            return false;
        }

        static public bool SetSelectedItemByValue(ComboBox comboBox, string value)
        {
            foreach (object item in comboBox.Items)
            {
                if ((item.ToString()) == value)
                {
                    comboBox.SelectedItem = item;
                    return true;
                }
            }
            return false;
        }

        static public bool BindMobiFlightFreePins(ComboBox comboBox, List<MobiFlightPin> Pins, String CurrentPin, bool analogOnly = false)
        {
            // This function assigns to a ComboBox the supplied list of all currently free pins,
            // plus the specified one marked as free.
            // Required because, in a selection list for a device signal, the already assigned pin
            // must be in the list in order to be selectable.
            
            if (Pins == null) return false;
            // Deep-clone list as 'Used' list
            List<MobiFlightPin> UsablePins = Pins.ConvertAll(pin => new MobiFlightPin(pin));
            // Mark current pin as free
            if (UsablePins.Exists(x => x.Pin == byte.Parse(CurrentPin)))
                UsablePins.Find(x => x.Pin == byte.Parse(CurrentPin)).Used = false;

            if (analogOnly == true)
            {
                UsablePins = UsablePins.FindAll(x => x.isAnalog == true);
            }

            // Assign the all-free list to the combobox
            comboBox.DataSource = UsablePins.FindAll(x => x.Used == false);
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Pin";

            // Restore the original item selection
            comboBox.SelectedValue = byte.Parse(CurrentPin);
            
            return false;
        }
        static public void reassignPin(string newPin, List<MobiFlightPin> pinList, ref string currentPin)
        {
            // This function updates the config data (currentPin) with the new value passed.
            // It does not deal with the ComboBox value set update; this has to be done by the caller.
            // The assignment flags in the "base" pin list are accordingly updated (the current pin no. 
            // is marked as free and the new one as used)
            try {
                byte nBefore = byte.Parse(currentPin);
                byte nAfter = byte.Parse(newPin);
                if (currentPin != newPin) {
                    // Pin 0 is used for the stepper; but Pin 0 is not a correct Pin for the Mega. (?)
                    MobiFlightPin p;
                    p = pinList.Find(x => x.Pin == nBefore);
                    if (p != null) p.Used = false;
                    p = pinList.Find(x => x.Pin == nAfter);
                    if (p != null) p.Used = true;
                }
            }
            catch (Exception ex) {
                Log.Instance.log($"Pin reassignment from {currentPin} to {newPin} went wrong: {ex.Message}", LogSeverity.Error);
            }
            // now confirm assignment of the new value in the configuration data
            currentPin = newPin;
        }
        static public void freePin(List<MobiFlightPin> pinList, string currentPin)
        {
            // This function removes "currentPin" from the free pin list.
            byte nBefore = byte.Parse(currentPin);
            try {
                MobiFlightPin pin = pinList.Find(x => x.Pin == nBefore);
                if (pin != null)
                    pin.Used = false;
            }
            catch (Exception ex) {
                Log.Instance.log($"Release of pin {currentPin} went wrong: {ex.Message}", LogSeverity.Error);
            }
        }
        static public void reservePin(List<MobiFlightPin> pinList, ref string newPin)
        {
            // This function tries to reserve the specified pin as newly occupied.
            // If it is no longer available (or ""), the first free one will be assigned.
            // If no more pins are available, an empty string is returned.
            try {
                MobiFlightPin p = null;
                if (newPin != "") {
                    byte newPinNo = byte.Parse(newPin);
                    p = pinList.Find(x => x.Pin == newPinNo);
                    if (p == null) throw new Exception("Nonexistent pin number");
                }
                if (p != null && !p.Used) {
                    p.Used = true;
                    newPin = p.Pin.ToString();
                } else {
                    p = pinList.Find(x => x.Used == false);
                    if(p == null)  throw new ArgumentOutOfRangeException();
                }
                newPin = p.Pin.ToString();
            }
            catch (ArgumentOutOfRangeException ex) {
                MessageBox.Show(i18n._tr("uiMessageNotEnoughPinsMessage"),
                                i18n._tr("uiMessageNotEnoughPinsHint"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                newPin = "";
            }
            catch (Exception ex) {
                Log.Instance.log($"Pin assignment to {newPin} went wrong: {ex.Message}", LogSeverity.Error);
            }
        }

    }
}
