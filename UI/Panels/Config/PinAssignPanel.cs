using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels.Config
{
    public partial class PinAssignPanel : UserControl
    {
        byte _byte = 1;
        List<MobiFlightPin> _pinList;

        public PinAssignPanel(List<MobiFlightPin> pinList)
        {
            InitializeComponent();
            rebuild(pinList);
        }

        public void rebuild(List<MobiFlightPin> pinList)
        {
            _pinList = pinList;
            _build(pinList);
        }

        private void _build(List<MobiFlightPin> pinList)
        {
            flpBase.Controls.Clear();
            _pinList?.ForEach(pin => _addPinBox(pin));

        }

        private void _addPinBox(MobiFlightPin pin)
        {
            CheckBox c = new CheckBox();
            flpBase.Controls.Add(c);
        }

        [Description("Set the Byte"), Category("Default")] 
        public byte Byte
        {
            get { return _byte; }
            set
            {
                _byte = value;
                Label.Text = "Byte " + _byte;
                for (int i = 0; i != 8; i++)
                {
                    flpBase.Controls["checkBox" + i].Text = ((_byte-1) * 8 + i).ToString();
                }
            }
        }

        public byte Value
        {
            get { 
                byte result = 0;
                for (int i = 0; i != 8; i++)
                {
                    result += (Byte)(
                                ((flpBase.Controls["checkBox" + i] as CheckBox).Checked ? 1 : 0) << i
                              );
                }
                return result;
            }
            set
            {
                for (int i = 0; i != 8; i++)
                {
                    (flpBase.Controls["checkBox" + i] as CheckBox).Checked = ((value & 1) == 1);
                    value = (byte) (value >> 1);
                }
            }
        }
    }
}
