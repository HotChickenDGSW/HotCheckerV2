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

        public MemberCard memberCard
        {
            get;
            set;
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

        public CardViewModel()
        {
            EnterCommand = new DelegateCommand(OnEnter, CanExcute);
        }

        public async void OnEnter()
        {
            await SearchMember(BarcodeData);
            CheckingCardViewVisiblity = Visibility.Collapsed;
            CheckedCardViewVisiblity = Visibility.Visible;
            BarcodeData = string.Empty;
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
                    memberCard = (MemberCard)resp;
                }
            });

        }
    }
}
