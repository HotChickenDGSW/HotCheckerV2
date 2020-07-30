using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using HotChecker_WPF.Model;
using HotChicken.Serial;
using Prism.Mvvm;

namespace HotChecker_WPF.ViewModel
{
    public class TemperatureViewModel : BindableBase
    {
        MediaPlayer mediaPlayer = new MediaPlayer();

        private Temperature _temperature = new Temperature();
        public Temperature Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private Visibility _checkingTemperatureViewVisiblity = Visibility.Visible;
        public Visibility CheckingTemperatureViewVisiblity
        {
            get => _checkingTemperatureViewVisiblity;
            set => SetProperty(ref _checkingTemperatureViewVisiblity, value);
        }

        private Visibility _checkedTemperatureViewVisiblity = Visibility.Collapsed;
        public Visibility CheckedTemperatureViewVisiblity
        {
            get => _checkedTemperatureViewVisiblity;
            set => SetProperty(ref _checkedTemperatureViewVisiblity, value);
        }

        public delegate void ChangeScreenEvent();
        public event ChangeScreenEvent ChangeScreenEventHandler;

        public TemperatureViewModel()
        {
            SerialInit();
        }

        private async void SerialInit()
        {
            SerialCommunicator.serialManager.GetSerialPorts();
            SerialCommunicator.serialManager.DataReceivedEventHandler += SerialManager_DataReceivedEventHandler;
            await SerialCommunicator.serialManager.ConnectSomePort("");
            await SerialCommunicator.serialManager.SendData("5");
        }

        public async void SerialManager_DataReceivedEventHandler(DateTime date, string data)
        {
            if (data.Contains("."))
            {

                await SetData(date, data);
                await Task.Run(() =>
                {
                    CheckingTemperatureViewVisiblity = Visibility.Collapsed;
                    CheckedTemperatureViewVisiblity = Visibility.Visible;
                });
                mediaPlayer.Play();
                await Task.Delay(2000);
                ChangeScreenEventHandler?.Invoke();
                await Task.Run(() =>
                {
                    CheckingTemperatureViewVisiblity = Visibility.Visible;
                    CheckedTemperatureViewVisiblity = Visibility.Collapsed;

                });
            }
            else
            {
                await SerialCommunicator.serialManager.SendData("1");
            }


        }



        private async Task SetData(DateTime date, string data)
        {
            await Task.Run(() =>
            {
                Temperature.Date = date;
                Temperature.Temperture = double.Parse(data);
            });
        }
        //TODO : 바인딩 이름바꾸기, 시간대 설정값 모듈만들기, 서버와 이야기해서 아침점심저녁 정해진 시간대 받아오는 API 요청
    }
}
