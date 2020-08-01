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

        public CardViewModel()
        {
            EnterCommand = new DelegateCommand(OnEnter);
        }

        public async void OnEnter()
        {
            await SearchMember(BarcodeData);
            await Task.Run(() =>
            {
                CheckingCardViewVisiblity = Visibility.Collapsed;
                CheckedCardViewVisiblity = Visibility.Visible;
                BarcodeData = string.Empty;
            });
            await Task.Delay(2000);
            ChangeScreenEventHandler?.Invoke();
            await Task.Run(() =>
            {
                CheckingCardViewVisiblity = Visibility.Visible;
                CheckedCardViewVisiblity = Visibility.Collapsed;
            });

        }

        public bool CanExcute()
        {
            if(BarcodeData.Length >= 8 && BarcodeData.Length <= 10)
            {
                return true;
            }
            BarcodeData = string.Empty;
            return false;
        }

        public async Task SearchMember(string cardId)
        {
            await Task.Run(() =>
            {
                var resp = MemberManager.FindMemberByCardId(cardId);
                if (resp != null)
                {
                    Count++;
                    MemberCard = (MemberCard)resp;
                }
            });

        }
    }
}
