using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChicken.Serial
{
    public class SerialManager
    {
       
        private string[] ports
        {
            get;
            set;
        }
        private string portName
        {
            get;
            set;
        }
        private int baudRate
        {
            get;
            set;
        }
        private int dataBits
        {
            get;
            set;
        }
        private StopBits StopBits
        {
            get;
            set;
        }
        private Parity Parity
        {
            get;
            set;
        }
        public SerialDataReceivedEventHandler SerialDataReceivedEventHandler
        {
            get;
            set;
        }
        
        public void GetSerialPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        public void ConnectSomePort()
        {

        }
    }
}
