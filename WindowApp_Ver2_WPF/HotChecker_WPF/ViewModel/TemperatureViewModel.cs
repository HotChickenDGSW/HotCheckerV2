using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChecker_WPF.Model;
using HotChicken.Serial;
using Prism.Mvvm;

namespace HotChecker_WPF.ViewModel
{
    public class TemperatureViewModel : BindableBase
    {
        SerialManager serialManager = new SerialManager(9600, 8, 1, 0);

        private Temperature _temperature = new Temperature();
        public Temperature Temperature
        {
            get=>_temperature;
            set=>SetProperty(ref _temperature, value);
        }

        public TemperatureViewModel()
        {
            SerialInit();
        }

        private async void SerialInit()
        {
            serialManager.GetSerialPorts();
            serialManager.DataReceivedEventHandler  += SerialManager_DataReceivedEventHandler;
            await serialManager.ConnectSomePort("");
        }

        public void SerialManager_DataReceivedEventHandler(DateTime date, string data)
        {
            Temperature.Date = date;
            Temperature.Temperture = double.Parse(data);
        }

        //TODO : 바인딩 이름바꾸기, 시간대 설정값 모듈만들기, 서버와 이야기해서 아침점심저녁 정해진 시간대 받아오는 API 요청
    }
}
