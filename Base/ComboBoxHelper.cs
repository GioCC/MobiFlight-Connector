using System;
using System.Linq;
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
        static public void reassignPin(ComboBox comboBox, List<MobiFlightPin> pinList, ref string signalPin)
        {
            // This function updates the config data (signalPin) with the new value read from the ComboBox.
            // At the same time:
            // - the assignment flags in the "base" pin list are accordingly updated (the current pin no. is marked as free
            //   and the new one as used)
            // - an updated pin list is associated to the ComboBox
            string after = comboBox.SelectedItem.ToString();
            byte nBefore = byte.Parse(signalPin);
            byte nAfter = byte.Parse(after);
            try {
                if (signalPin != after) {
                    // Pin 0 is used for the stepper.
                    // But Pin 0 is not a correct Pin for the Mega.
                    if (pinList.Find(x => x.Pin == nBefore)!=null)
                        pinList.Find(x => x.Pin == nBefore).Used = false;
                    if (pinList.Find(x => x.Pin == nAfter)!=null)
                        pinList.Find(x => x.Pin == nAfter).Used = true;
                }
            }
            catch (Exception ex) {
                Log.Instance.log($"Pin reassignment from {signalPin} to {after} went wrong: {ex.Message}", LogSeverity.Error);
            }
            // now confirm assignment of the new value in the configuration data
            signalPin = after;
            
            //ComboBoxHelper.BindMobiFlightFreePins(comboBox, pinList, after);
            // the function above has rebuilt its datasource, therefore the ComboBox selection must be restored:
            //comboBox.SelectedValue = nAfter;
        }
        static public void reassignChannelPin(ComboBox comboBox, List<MobiFlightPin> pinList, ref string oldPin, ref string oldChannel)
        {
            // Update for encoders on Mux or ShiftReg (must consider also mux/shifter channel)
            //
            // Version of reassignPin(), but including "channel" sub-parameter, for all devices that can 
            // - from SignalPin, extract the Pin part (-> nBefore) and the Channel part
            //   (depending on whether 'SignalPin' is like "28#7" (mux on pin 28, ch #7) or "MyDevice#7";
            //   more probably the latter, which is much more readable)

            byte pinBefore, chBefore;
            byte pinAfter,  chAfter;

            pinBefore = byte.Parse(oldPin);
            chBefore =  byte.Parse(oldChannel);

            var newPinParts = comboBox.SelectedItem.ToString().Split('#');
            if (newPinParts.Count() == 1) {
                // Standard pin
                pinAfter = byte.Parse(newPinParts[1]);
                chAfter = 255; 
            } else {
                // Pin with channel
                String newPin = "";//pinOfInputMultiplexer(newPinParts[1]);
                pinAfter = byte.Parse(newPin); ;
                chAfter = byte.Parse(newPinParts[2]);
            }
            if (!byte.TryParse(oldChannel, out chBefore)) chBefore = 255;
            if(!byte.TryParse(newPinParts[2], out chAfter)) chAfter = 255;
            
            try {
                if (pinBefore != pinAfter) {
                    // Different pin
                    if (chBefore == 255) {
                        // Std pin: free pinBefore
                        if (pinList.Find(x => x.Pin == pinBefore) != null) {
                            pinList.Find(x => x.Pin == pinBefore).Used = false;
                        }
                    } else {
                        // ... Free chBefore on pinBefore
                    }
                    if (chAfter == 255) {
                        // Std pin: occupy pinAfter
                        if (pinList.Find(x => x.Pin == pinAfter) != null) {
                            pinList.Find(x => x.Pin == pinAfter).Used = true;
                        }
                    } else {
                        // ... occupy chAfter on pinAfter
                    }
                } else {
                    // Same pin
                    if (chBefore != chAfter) {
                        // ... Free chBefore and occupy chAfter on that pin
                    }
                }
            }
            catch (Exception ex) {
                Log.Instance.log($"Pin reassignment from {pinBefore} to {pinAfter} went wrong: {ex.Message}", LogSeverity.Error);
                if(chBefore != 255 || chAfter != 255) {
                    Log.Instance.log($"(channel from {chBefore} to {chAfter})", LogSeverity.Error);
                }
            }
            // now confirm assignment of the new value in the configuration data
            oldPin = ;
            oldChannel = ;
        }

    }
}
