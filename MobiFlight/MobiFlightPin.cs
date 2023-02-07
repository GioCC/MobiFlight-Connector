using System;
using System.Xml.Serialization;

namespace MobiFlight
{
    public class MobiFlightPin
    {
        [XmlAttribute]
        public byte Pin { get; set; }
        [XmlAttribute]
        public bool isAnalog = false;
        [XmlAttribute]
        public bool isPWM = false;
        [XmlAttribute]
        public bool isI2C = false;

        // Should this be serialized? Its validity depends on the configuration
        [XmlAttribute]
        public byte Channel { get; set; } = 255;
        // Should derived pins be a subclass?    

        // This is internal state and shouldn't be serialized to/from the .board.xml files
        public bool Used = false;

        private string name = null;

        [XmlAttribute]
        public string Name
        {
            get { return name != null ? name : Pin.ToString(); }
            set { name = value; }
        }

        public MobiFlightPin()
        {
        }

        public MobiFlightPin(MobiFlightPin pin)
        {
            Pin = pin.Pin;
            isAnalog = pin.isAnalog;
            isPWM = pin.isPWM;
            isI2C = pin.isI2C;
            Used = pin.Used;
            Name = pin.Name;
        }

        public bool ShouldSerializeName()
        {
            return !string.IsNullOrEmpty(name);
        }

        public bool isDerived()
        {
            return Channel != 255;
        }

        public override String ToString()
        {
            return Pin.ToString();
        }
    }
}
