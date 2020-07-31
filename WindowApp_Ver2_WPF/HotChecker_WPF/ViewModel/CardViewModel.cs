using HotChecker_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChicken.Member;
using System.Windows;
using Prism.Mvvm;
using System.Configuration;
using System.Windows.Input;
using Prism.Commands;
using System.Windows.Media;

namespace HotChecker_WPF.ViewModel
{
    public class CardViewModel : BindableBase
    {

        private MemberCard _memberCard;
        public MemberCard MemberCard
        {
            get=>_memberCard;
            set=>SetProperty(ref _memberCard, value);
        }

        private string _cardGuideMsg = "바코드리더기에 카드를 인식시켜주세요!";
        public string CardGuideMsg
        {
            get => _cardGuideMsg;
            set => SetProperty(ref _cardGuideMsg, value);
        }

        private int _count = 1;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private Visibility _checkingCardViewVisiblity = Visibility.Visible;
        public Visibility CheckingCardViewVisiblity
        {
            get => _checkingCardViewVisiblity;
            set => SetProperty(ref _checkingCardViewVisiblity, value);
        }

        private Visibility _checkedCardViewVisiblity = Visibility.Collapsed;
        public Visibility CheckedCardViewVisiblity
        {
            get => _checkedCardViewVisiblity;
            set => SetProperty(ref _checkedCardViewVisiblity, value);
        }

        private bool _textBoxIsEnable = true;
        public bool TextBoxIsEnable
        {
            get => _textBoxIsEnable;
            set => SetProperty(ref _textBoxIsEnable, value);
        }

        private string _barcodeData = string.Empty;
        public string BarcodeData
        {
            get => _barcodeData;
            set => SetProperty(ref _barcodeData, value);
        }

        public ICommand EnterCommand
        {
            get;
            set;
        }

        public delegate void ChangeScreenEvent();
        public event ChangeScreenEvent ChangeScreenEventHandler;


        public delegate void TextboxFocusEvent();
        public event TextboxFocusEvent TextBoxFocusEventHandler;

        MediaPlayer mediaPlayerCheck = new MediaPlayer();
        MediaPlayer mediaPlayerPlzReCheck = new MediaPlayer();

        public CardViewModel()
        {
            Init();


        }

        private void Init()
        {
            mediaPlayerCheck.Open(new Uri("../Assets/check.mp3"));
            mediaPlayerPlzReCheck.Open(new Uri("../Assets/plzReCheckCard.mp3"));
            EnterCommand = new DelegateCommand(OnEnter);
        }

        public async void OnEnter()
        {

            if (await SearchMember(BarcodeData))//barcode찍었을때
            {
                mediaPlayerCheck.Play();
                await SerialCommunicator.serialManager.SendData("4");
                await Task.Run(() =>//화면전환
                {
                    CheckingCardViewVisiblity = Visibility.Collapsed;
                    CheckedCardViewVisiblity = Visibility.Visible;
                    BarcodeData = string.Empty;
                });
                await Task.Delay(2000);//2초기다림
                ChangeScreenEventHandler?.Invoke();//카드->체온으로 컨트롤 체인지 이벤트
                await Task.Run(() =>//화면전환이 이루어지고 카드체크는 원래 상태로 복구
                {
                    CheckingCardViewVisiblity = Visibility.Visible;
                    CheckedCardViewVisiblity = Visibility.Collapsed;
                });
            }
            else
            {
                mediaPlayerPlzReCheck.Play();
                CardGuideMsg = "등록되지 않은 사용자입니다. 다시 시도해주세요!";
                TextBoxIsEnable = false;
                await Task.Delay(2000);//2초기다림
                CardGuideMsg = "바코드리더기에 카드를 인식시켜주세요!";
                TextBoxIsEnable = true;
                TextBoxFocusEventHandler?.Invoke();
            }



        }

        //public bool CanExcute()
        //{
        //    if(BarcodeData.Length >= 8 && BarcodeData.Length <= 10)
        //    {
        //        return true;
        //    }
        //    BarcodeData = string.Empty;
        //    return false;
        //}

        public async Task<bool> SearchMember(string cardId)
        {
            bool res = true;
            await Task.Run(() =>
            {
                // TODO : 시리얼 데이터 타이밍 넣기
                var resp = MemberManager.FindMemberByCardId(cardId);
                if (resp != null)
                {
                    Count++;
                    MemberCard = (MemberCard)resp;
                    
                }
                else
                {
                    res = false;
                    
                }
            });
            return res;

        }
    }
}
