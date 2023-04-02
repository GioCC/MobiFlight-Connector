using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels.Settings.Device
{
    public partial class MFEncoderPanel : UserControl
    {

        private MobiFlight.Config.Encoder encoder;
        private List<MobiFlightPin> pinList;    // COMPLETE list of pins (includes status)
        private bool initialized = false;

        public event EventHandler Changed;

        public MFEncoderPanel()
        {
            InitializeComponent();
            mfLeftPinComboBox.Items.Clear();
            mfRightPinComboBox.Items.Clear();
            //... if there is any mux defined:
            bool anyMuxDefined = false;
            cbPin1IsMux.Enabled = cbPin2IsMux.Enabled = anyMuxDefined;
            //... populate cbMuxPin1 and cbMuxPin2 with mux name list

        }

        public MFEncoderPanel(MobiFlight.Config.Encoder encoder, List<MobiFlightPin> Pins) : this()
        {
            pinList = Pins;
            this.encoder = encoder;
            ComboBoxHelper.SetSelectedItemByIndex(mfEncoderTypeComboBox, int.Parse(encoder.EncoderType));
            textBox1.Text = encoder.Name;

            UpdateFreePinsInDropDown(0);
            UpdateFreePinsInDropDown(1);
            // Default standard selected values, next pins available
            // No difference whether it's a pin or channel value
            mfLeftPinComboBox.SelectedValue = byte.Parse(encoder.PinLeft);
            mfRightPinComboBox.SelectedValue = byte.Parse(encoder.PinRight);
            //SuspendLayout();   // "!initialized" is used instead

            //if LEFT pin is on mux:
            if(isOnMux(encoder, 0)) {
                //... cbPin1IsMux.Checked = ([cbMuxPin1 name] != "");
                //... cbMuxPin1.SelectedItem = [cbMuxPin1 name]
            }

            // if RIGHT pin is on mux:
            if (isOnMux(encoder, 1)) {
                //... cbPin2IsMux.Checked = ([cbMuxPin2 name] != "");
                //... cbMuxPin2.SelectedItem = [cbMuxPin2 name]
            }
            //ResumeLayout();   // "!initialized" is used instead
            initialized = true;
        }

        private bool isOnMux(MobiFlight.Config.Encoder encoder, int pinNo)
        {
            int res;
            int.TryParse((pinNo == 0 ? encoder.PinLeftSel : encoder.PinRightSel), out res);
            return res >= 0;
        }

        private void setNonPinValues()
        {
            encoder.EncoderType = mfEncoderTypeComboBox.SelectedIndex.ToString();
            encoder.Name = textBox1.Text;
        }
        private void UpdateFreePinsInDropDown(int which)
        {
            bool exInitialized = initialized;
            initialized = false;    // inhibit value_Changed events
            
            if(which == 0) { 
                if (isOnMux(encoder, 0)) {
                    //... fill mfLeftPinComboBox with values 0...7 (or 0..15) depending on mux type
                    mfLeftPinComboBox.SelectedValue = byte.Parse(encoder.PinLeft);

                } else {
                    ComboBoxHelper.BindMobiFlightFreePins(mfLeftPinComboBox, pinList, encoder.PinLeft);
                }
            } else { 
                if (isOnMux(encoder, 1)) {
                    //... fill mfRightPinComboBox with values 0...7 (or 0..15) depending on mux type
                    mfLeftPinComboBox.SelectedValue = byte.Parse(encoder.PinRight);
                } else {
                    ComboBoxHelper.BindMobiFlightFreePins(mfRightPinComboBox, pinList, encoder.PinRight);
                }
            }
            initialized = exInitialized;
        }

        private void ReassignFreePinsInDropDowns(ComboBox comboBox)
        {
            // This is used for pin changes when the mux attachment status does NOT change;
            // the other case is handled curremtly in event methods cbPin<n>IsMux_CheckedChanged()

            bool exInitialized = initialized;
            initialized = false;    // inhibit value_Changed events

            //... TODO: operation is different if the mux attachment status changes!
           
            // First update the one that is changed
            // Here, the config data (encoder.XXXPin) is updated with the new value read from the changed ComboBox;
            if (comboBox == mfLeftPinComboBox) {
                if (isOnMux(encoder, 0)) {
                    //... should not check overlap, thus [free + re-occupy] is not required?
                } else { 
                    ComboBoxHelper.reassignPin(comboBox, pinList, ref encoder.PinLeft);
                    // ReassignChannelPin is probably not necessary?
                    // ComboBoxHelper.reassignChannelPin(mfLeftPinComboBox, pinList, ref encoder.PinLeft, ref encoder.PinLeftSel); 
                }
                // update the other too 
                UpdateFreePinsInDropDown(1);
            } else
            if (comboBox == mfRightPinComboBox) {
                if (isOnMux(encoder, 1)) {
                    //... should not check overlap, thus [free + re-occupy] is not required?
                } else { 
                    ComboBoxHelper.reassignPin(comboBox, pinList, ref encoder.PinRight);
                    // ComboBoxHelper.reassignChannelPin(mfRightPinComboBox, pinList, ref encoder.PinRight, ref encoder.PinRightSel); 
                }
                // update the other too 
                UpdateFreePinsInDropDown(0);
            }

            initialized = exInitialized;
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (!initialized) return;
            //... TODO; reassign (= free + re-occupy) only makes sense for non-mux pins?
            ReassignFreePinsInDropDowns(sender as ComboBox);
            setNonPinValues();
            if (Changed != null)
                Changed(encoder, new EventArgs());
        }

        private void cbPin1IsMux_CheckedChanged(object sender, EventArgs e)
        {
            if (!initialized) return;

            bool newStatus = (sender as CheckBox).Checked;
            if (newStatus == true) {
                // Was non-mux: free old pin
                MobiFlightPin newPin = pinList.Find(x => x.Pin == Byte.Parse(encoder.PinLeft));
                if (newPin != null) {
                    newPin.Used = false;
                } else {
                    // what can we do?
                }
                //....repopulate pin list: fill mfLeftPinComboBox with values 0...7 (or 0..15) depending on mux type
                mfLeftPinComboBox.SelectedIndex = 0;
                encoder.PinLeft = mfLeftPinComboBox.SelectedItem.ToString();  // First channel (supposed to be "0")
                cbMuxPin1.Enabled = true;
                cbMuxPin1.SelectedIndex = 0;
                //encoder.PinLeftSel = [input pin of selected mux];
            } else {
                // Was mux: nothing to free there;
                // Find first free pin in pinList and assign it:
                MobiFlightPin newPin = pinList.Find(x => x.Used == false);
                if(newPin != null) {
                    encoder.PinLeft = newPin.Pin.ToString();
                    encoder.PinLeftSel = "-1";
                    newPin.Used = true;
                } else {
                    // what can we do?
                }
                // Repopulate pin list
                ComboBoxHelper.BindMobiFlightFreePins(mfLeftPinComboBox, pinList, encoder.PinLeft);
            }
            UpdateFreePinsInDropDown(1);
        }

        private void cbPin2IsMux_CheckedChanged(object sender, EventArgs e)
        {
            //... see other pin
        }
    }
}
