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
    public partial class MFLedSegmentPanel : UserControl
    {
        private MobiFlight.Config.LedModule ledModule;
        private List<MobiFlightPin> pinList;    // COMPLETE list of pins (includes status)
        private bool initialized = false;
        
        public event EventHandler Changed;

        public MFLedSegmentPanel()
        {
            InitializeComponent();
            mfPin1ComboBox.Items.Clear();
            mfPin2ComboBox.Items.Clear();
            mfPin3ComboBox.Items.Clear();
            if (Parent != null) mfIntensityTrackBar.BackColor = Parent.BackColor;
        }

        public MFLedSegmentPanel(MobiFlight.Config.LedModule ledModule, List<MobiFlightPin> Pins) : this()
        {
            pinList = Pins; // Keep pin list stored
                            // Since the panel is synced whenever a new device is selected, the free/used list won't change
                            // (because of changes elsewhere) as long as we remain in this panel, so we can keep it stored
                            // for the lifetime of the panel.

            this.ledModule = ledModule;
            textBox1.Text = ledModule.Name;
            mfIntensityTrackBar.Value = ledModule.Brightness;
            ComboBoxHelper.SetSelectedItem(mfNumModulesComboBox, ledModule.NumModules);

            switch(ledModule.ClsPin) {
                case MobiFlight.Config.LedModule.MODEL_TM1637_4D:
                    mfDisplayTypeComboBox.SelectedIndex = 1;
                    break;
                case MobiFlight.Config.LedModule.MODEL_TM1637_6D:
                    mfDisplayTypeComboBox.SelectedIndex = 2;
                    break;
                default:
                    mfDisplayTypeComboBox.SelectedIndex = 0;
                    break;
            }

            //displayLedTypeMAX.Checked = true;
            //if (ledModule.ClsPin == MobiFlight.Config.LedModule.MODEL_TM1637_4D) { displayLedTypeTM4.Checked = true; }
            //else
            //if (ledModule.ClsPin == MobiFlight.Config.LedModule.MODEL_TM1637_6D) { displayLedTypeTM6.Checked = true; }
            
            UpdateFreePinsInDropDowns();

            initialized = true;
        }
        
        private bool isMax()
        { 
            //return displayLedTypeMAX.Checked == true; 
            return mfDisplayTypeComboBox.SelectedIndex == 0; 
        }
        private void setNonPinValues()
        {
            ledModule.Name = textBox1.Text;
            ledModule.Brightness = (byte)(mfIntensityTrackBar.Value);
            ledModule.NumModules = isMax()? mfNumModulesComboBox.Text : "1";
        }
        private void setMAXMode(String mode) // bool MAXmode)
        {
            var MAXmode = (mode == MobiFlight.Config.LedModule.MODEL_MAX72xx);
            mfPin2ComboBox.Visible = MAXmode;
            mfNumModulesComboBox.Visible = MAXmode;
            mfPin2Label.Visible = MAXmode;
            numberOfModulesLabel.Visible = MAXmode;
            label3.Visible = MAXmode;
        }
        private void changeMAXMode(String mode)
        {
            setMAXMode(mode);
            var MAXmode = (mode == MobiFlight.Config.LedModule.MODEL_MAX72xx);
            if (MAXmode) {
                // First try and see if the "old" pin is still available, otherwise assign the first free one
                if (mfPin2ComboBox.SelectedIndex == -1) {
                    mfPin2ComboBox.SelectedIndex = 0;
                    ReassignFreePinsInDropDowns(mfPin2ComboBox);
                }
                ledModule.ClsPin = mfPin2ComboBox.SelectedItem.ToString();
                ComboBoxHelper.reservePin(pinList, ref ledModule.ClsPin);
            } else {
                ComboBoxHelper.freePin(pinList, ledModule.ClsPin);
                ledModule.ClsPin = mode;
                ReassignFreePinsInDropDowns(mfPin2ComboBox);
                // UpdateFreePinsInDropDowns();
            }
            if (Changed != null)
                Changed(ledModule, new EventArgs());
        }
        private void UpdateFreePinsInDropDowns()
        {
            bool exInitialized = initialized;
            initialized = false;    // inhibit value_Changed events
            ComboBoxHelper.BindMobiFlightFreePins(mfPin1ComboBox, pinList, ledModule.DinPin);
            ComboBoxHelper.BindMobiFlightFreePins(mfPin3ComboBox, pinList, ledModule.ClkPin);
            ComboBoxHelper.BindMobiFlightFreePins(mfPin2ComboBox, pinList, ledModule.ClsPin);
            initialized = exInitialized;
        }

        private void ReassignFreePinsInDropDowns(ComboBox comboBox)
        {
            bool exInitialized = initialized;
            initialized = false;    // inhibit value_Changed events

            // First update the one that is changed
            // Here, the config data (ledModule.XXXPin) is updated with the new value read from the changed ComboBox;
            var newPin = comboBox.SelectedItem.ToString();
            if (comboBox == mfPin1ComboBox) { ComboBoxHelper.reassignPin(newPin, pinList, ref ledModule.DinPin); } else
            if (comboBox == mfPin2ComboBox) { ComboBoxHelper.reassignPin(newPin, pinList, ref ledModule.ClsPin); } else
            if (comboBox == mfPin3ComboBox) { ComboBoxHelper.reassignPin(newPin, pinList, ref ledModule.ClkPin); }
            // then the others are updated too 
            UpdateFreePinsInDropDowns();

            initialized = exInitialized;
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (!initialized) return;
            if(sender == mfPin1ComboBox || sender == mfPin2ComboBox || sender == mfPin3ComboBox) { 
                ReassignFreePinsInDropDowns(sender as ComboBox);
            }
            setNonPinValues();
            if (Changed != null)
                Changed(ledModule, new EventArgs());
        }

        private void displayLedTypeMAX_CheckedChanged(object sender, EventArgs e)
        {
            //if (!(sender as RadioButton).Checked) return;
            //// Retrieve previous pin and try to reinstate it
            //ledModule.ClsPin = mfPin2ComboBox.SelectedItem.ToString(); 
            //setMAXMode(MobiFlight.Config.LedModule.MODEL_MAX72xx);
        }
        private void displayLedTypeTM4_CheckedChanged(object sender, EventArgs e)
        {
            //if (!(sender as RadioButton).Checked) return;
            //setMAXMode(MobiFlight.Config.LedModule.MODEL_TM1637_4D);
        }
        private void displayLedTypeTM6_CheckedChanged(object sender, EventArgs e)
        {
            //if (!(sender as RadioButton).Checked) return;
            //setMAXMode(MobiFlight.Config.LedModule.MODEL_TM1637_6D);
        }

        private void mfDisplayTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = (sender as ComboBox).SelectedIndex;
            string newMode = "";
            switch (selection) {
                case 0:
                    newMode = MobiFlight.Config.LedModule.MODEL_MAX72xx;
                    break;
                case 1:
                    newMode = MobiFlight.Config.LedModule.MODEL_TM1637_4D;
                    break;
                case 2:
                    newMode = MobiFlight.Config.LedModule.MODEL_TM1637_6D;
                    break;

            }
            if (!initialized) {
                setMAXMode(newMode);
            } else { 
                changeMAXMode(newMode);
            }
        }
    }
}
