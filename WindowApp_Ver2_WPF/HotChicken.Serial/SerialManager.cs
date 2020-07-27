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
        private StopBits stopBits
        {
            get;
            set;
        }
        private Parity parity
        {
            get;
            set;
        }

        private SerialPort serialPort
        {
            get;
            set;
        }

        public SerialDataReceivedEventHandler SerialDataReceivedEventHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 시리얼통신을 위한 객체의 생성자 기본설정을 할 수 있음 
        /// stopBits->0 1 2 3(1.5)
        /// parity->0 1(홀수) 2(짝수) 3(Mark) 4(Space)
        /// </summary>
        /// <param name="name">port의 이름</param>
        /// <param name="baudRate">통신속도</param>
        /// <param name="dataBits">데이터비트</param>
        /// <param name="stopBits">stop비트 설정</param>
        /// <param name="parity">페리티비트 설정</param>
        public SerialManager(string name, int baudRate, int dataBits, int stopBits, int parity)
        {
            portName = name;
            this.baudRate = baudRate;
            this.dataBits = dataBits;
            this.stopBits = (StopBits)stopBits;
            this.parity = (Parity)parity;
        }

        /// <summary>
        /// 시리얼통신을 위한 객체의 생성자 특정 keyword가 들어가는 포트만 연결하기위해
        /// ConnectSomePort와 함께 쓰는 생성자
        /// stopBits->0 1 2 3(1.5)
        /// parity->0 1(홀수) 2(짝수) 3(Mark) 4(Space)
        /// </summary>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        public SerialManager(int baudRate, int dataBits, int stopBits, int parity)
        {
            this.baudRate = baudRate;
            this.dataBits = dataBits;
            this.stopBits = (StopBits)stopBits;
            this.parity = (Parity)parity;
        }

        public void GetSerialPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        public async Task ConnectSomePort(string keyword)
        {
            portName = ports.Where(x => x.Contains(keyword) == true).FirstOrDefault();
            await ConnectSerialPort();
        }

        public async Task ConnectSerialPort()
        {
            await Task.Run(() =>
            {
                serialPort = new SerialPort();
                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;
                serialPort.DataBits = dataBits;
                serialPort.Parity = parity;
                serialPort.Open();
            });
        }

        public void SetDataReceivedEvent(SerialDataReceivedEventHandler method)
        {
            SerialDataReceivedEventHandler += method;
        }

        public void DeleteDataReceivedEvent(SerialDataReceivedEventHandler method)
        {
            SerialDataReceivedEventHandler -= method;
        }

        public async Task SendData(string data)
        {
            await Task.Run(() =>
            {
                serialPort.Write(data);
            });
        }
    }
}
