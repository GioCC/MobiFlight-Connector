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
        private string model = MobiFlight.Config.LedModule.MODEL_MAX72xx;
        private string exMAXClsPin = "";

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
            // Keep pin list stored
            // Since the panel is synced whenever a new device is selected, the free/used list won't change
            // (because of changes elsewhere) as long as we remain in this panel, so we can keep it stored
            // for the lifetime of the panel.
            pinList = Pins;

            this.ledModule = ledModule;
            textBox1.Text = ledModule.Name;
            mfIntensityTrackBar.Value = ledModule.Brightness;
            
            // Set display model
            // Changing "mfDisplayTypeComboBox.SelectedIndex" below also triggers status value changes
            switch (ledModule.NumModules) {
                case MobiFlight.Config.LedModule.MODEL_TM1637_4D:
                    mfDisplayTypeComboBox.SelectedIndex = 1;
                    break;
                case MobiFlight.Config.LedModule.MODEL_TM1637_6D:
                    mfDisplayTypeComboBox.SelectedIndex = 2;
                    break;
                default:
                    mfDisplayTypeComboBox.SelectedIndex = 0;
                    // This is an actual value: also sync corresponding CB.
                    // When uninitialized, this doesn't trigger changes.
                    ComboBoxHelper.SetSelectedItem(mfNumModulesComboBox, ledModule.NumModules);
                    break;
            }
            UpdateFreePinsInDropDowns();
            initialized = true;
        }
        private bool isMax()
        { 
            return (model == MobiFlight.Config.LedModule.MODEL_MAX72xx); 
        }
        private void setMAXModeAppearance(String newMode) // bool MAXmode)
        {
            var MAXmode = (newMode == MobiFlight.Config.LedModule.MODEL_MAX72xx);
            mfPin2ComboBox.Visible = MAXmode;
            mfNumModulesComboBox.Visible = MAXmode;
            mfPin2Label.Visible = MAXmode;
            numberOfModulesLabel.Visible = MAXmode;
            label3.Visible = MAXmode;
        }
        private void changeMAXMode(String newMode)
        {
            // Adjust appearance
            setMAXModeAppearance(newMode);
            
            // Deal with pin reassignment
            if(initialized) {
                if (newMode == MobiFlight.Config.LedModule.MODEL_MAX72xx) {
                    // First try and see if the "old" pin is still available, otherwise assign the first free one
                    ledModule.ClsPin = ((exMAXClsPin != "") ? exMAXClsPin : "");
                    ComboBoxHelper.assignPin(pinList, ref ledModule.ClsPin);
                } else {
                    if(exMAXClsPin == "") exMAXClsPin = ledModule.ClsPin;
                    // "freePin()" is the first half part only of "ReassignFreePinsInDropDowns()"
                    ComboBoxHelper.freePin(pinList, ledModule.ClsPin);
                    // Make sure to use a neutral value
                    ledModule.ClsPin = "255";
                }
                UpdateFreePinsInDropDowns();

                // Set identity marker
                ledModule.NumModules = newMode;
                if (Changed != null)
                    Changed(ledModule, new EventArgs());
            }

            model = newMode;            
        }
        private void UpdateFreePinsInDropDowns()
        {
            bool exInitialized = initialized;
            initialized = false;    // inhibit value_Changed events
            ComboBoxHelper.BindMobiFlightFreePins(mfPin1ComboBox, pinList, ledModule.DinPin);
            if(isMax()) ComboBoxHelper.BindMobiFlightFreePins(mfPin2ComboBox, pinList, ledModule.ClsPin);
            ComboBoxHelper.BindMobiFlightFreePins(mfPin3ComboBox, pinList, ledModule.ClkPin);
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
            // Pin settings
            if (!initialized) return;
            if(sender == mfPin1ComboBox || sender == mfPin2ComboBox || sender == mfPin3ComboBox) { 
                ReassignFreePinsInDropDowns(sender as ComboBox);
            }
            // Non-pin settings
            ledModule.Name = textBox1.Text;
            ledModule.Brightness = (byte)(mfIntensityTrackBar.Value);
            ledModule.NumModules = isMax() ? mfNumModulesComboBox.Text : model;

            if (Changed != null)
                Changed(ledModule, new EventArgs());
        }

        private void mfDisplayTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = (sender as ComboBox).SelectedIndex;
            string newMode = "";
            switch (selection) {
                case 0:
                    newMode = MobiFlight.Config.LedModule.MODEL_MAX72xx; break;
                case 1:
                    newMode = MobiFlight.Config.LedModule.MODEL_TM1637_4D; break;
                case 2:
                    newMode = MobiFlight.Config.LedModule.MODEL_TM1637_6D;break;
            }
            if(newMode != "") changeMAXMode(newMode);
        }
    }
}
