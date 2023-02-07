using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MobiFlight.Config
{
    public class Encoder : BaseDevice
    {
        [XmlAttribute]
        public String PinLeft = "1";
        [XmlAttribute]
        public String PinRight = "2";
        [XmlAttribute]
        public String EncoderType = "0";
        [XmlAttribute]
        public String PinLeftSel = "-1";
        [XmlAttribute]
        public String PinRightSel = "-2";

        const ushort _paramCount = 6;

        public Encoder() { Name = "Encoder"; _type = DeviceType.Encoder; }

        override public String ToInternal()
        {
            return base.ToInternal() + Separator
                 + PinLeft + Separator
                 + PinRight + Separator
                 + EncoderType + Separator
                 + Name + Separator
                 + PinLeftSel + Separator
                 + PinRightSel + End;
        }

        override public bool FromInternal(String value)
        {
            if (value.Length == value.IndexOf(End) + 1) value = value.Substring(0, value.Length - 1);
            String[] paramList = value.Split(Separator);
            var paramCnt = paramList.Count();
            if ((paramCnt != _paramCount + 1) 
                && (paramCnt != (_paramCount-2) + 1)   // Compatibility with old definition
                )
            {
                throw new ArgumentException("Param count does not match. " + paramList.Count() + " given, " + _paramCount + " expected");
            }

            PinLeft = paramList[1];
            PinRight = paramList[2];
            EncoderType = paramList[3];
            Name = paramList[4];
            PinLeftSel =  (paramCnt > 4 ? paramList[5] : "-1");
            PinRightSel = (paramCnt > 4 ? paramList[6] : "-2");

            return true;
        }
        public override bool Equals(object obj)
        {
            Encoder other = obj as Encoder;
            if (other == null)
            {
                return false;
            }

            return this.Name == other.Name
                && this.PinLeft == other.PinLeft
                && this.PinRight == other.PinRight
                && this.PinLeftSel == other.PinLeftSel
                && this.PinRightSel == other.PinRightSel
                && this.EncoderType == other.EncoderType
                && this.Type == other.Type;
        }

        public override string ToString()
        {
            return Type + ":" + Name 
                + " PinLeft:" + PinLeft + " PinRight:" + PinRight 
                + " PinLeftSel:" + PinLeftSel + " PinRightSel:" + PinRightSel
                + " EncoderType:" + EncoderType;
        }
    }
}