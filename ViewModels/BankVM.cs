using MyBank.Db.DataJob;
using MyBank.Helpers;
using MyBank.Models;
using MyBank.Views;
using System;
using System.Collections.ObjectModel;

namespace MyBank.ViewModels
{
    public class BankVM : BaseVM
    {
        #region public members 
        public static ObservableCollection<Bill> BillsToShow { get; set; }
        public ObservableCollection<CommonClient> CommonsToShow { get; set; }
        public ObservableCollection<VipClient> VipsToShow { get; set; }
        public DateTime NowDate
        {
            get { return _nowDate; }
            set
            {
                _nowDate = value; OnPropertyChanged("NowDate");
                BillConverter.Actual = NowDate;
            }
        }
        public static Guid ClientForNewBill { get; set; }
        public IBankItem Selected
        {
            get => selected; set
            {
                selected = value;
                SelectedOperate(Selected);
            }
        }
        #endregion
        #region private
        private DateTime _nowDate = DateTime.UtcNow;
        private IBankItem selected;
        #endregion
        #region commands
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand DateChanging { get; set; }
        public RelayCommand ToAddBillWindow { get; set; }
        public RelayCommand ToAddClientWindow { get; set; }
        public RelayCommand ToTransferCommand { get; set; }
        #endregion
        public BankVM()
        {
            BillsToShow = new();
            FillGrids();
            CommandsActivate();
        }
        #region methods
        void FillGrids()
        {
            CommonsToShow = BankItemsOperator.Context.Commons.Local.ToObservableCollection();
            VipsToShow = BankItemsOperator.Context.Vips.Local.ToObservableCollection();
        }
        /// <summary>
        /// Look bills of concrete client
        /// </summary>
        public void BillsRefresh()
        {
            if (Selected != null)
            {
                BillsToShow.Clear();
                var Search = BankItemsOperator.GetBills(Selected.Id);
                foreach (var item in Search) { BillsToShow.Add(item); }
            }
        }
        void CommandsActivate()
        {
            ToTransferCommand = new(x => new TransferView().Show(), x => true);
            ToAddClientWindow = new(x => new AddClientView().Show(), x => true);
            DeleteItem = new(x =>
            {
                if (Selected is Bill)
                {
                    BankItemsOperator.DeleteBankItem(Selected);
                    BillsToShow.Remove(Selected as Bill);
                }
                else
                { BankItemsOperator.DeleteBankItem(Selected); }
            },
                x => true);
            DateChanging = new(x =>
            {
                NowDate = NowDate.AddMonths(1);
                BankItemsOperator.DeposMonthlyGrowth(NowDate);
                BillsRefresh();
            }, x => true);
            ToAddBillWindow = new(x =>
            {
                new AddBillView().Show();
            }, x => Selected != null && Selected is VipClient or CommonClient);
        }
        /// <summary>
        /// Preparing to transfer.
        /// </summary>
        /// <param name="Selected"></param>
        void SelectedOperate(IBankItem Selected)
        {
            if (Selected is CommonClient or VipClient)
            {
                ClientForNewBill = Selected.Id;
                BillsRefresh();
            }
        }
        #endregion
    }
}
