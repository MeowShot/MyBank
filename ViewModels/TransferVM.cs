using MyBank.Db.DataJob;
using MyBank.Helpers;
using MyBank.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyBank.ViewModels
{ 
    public class TransferVM : BaseVM
    {
        public ObservableCollection<Bill> Bills { get; set; }
        public RelayCommand TransferCommnad { get; set; }
        public Bill BillFrom { get; set; }
        public Bill BillTo { get; set; }
        public string SumFromBox { get; set; }
        public decimal SumToTransfer { get; set; }
        public TransferVM()
        {
            TransferCommnad = new(x =>
            {
                if (CheckSum())
                    BankItemsOperator.BillTransfer(BillFrom, BillTo, SumToTransfer);
                Close(x);
            }, x => BillFrom != null && BillTo != null);
            Bills = BankItemsOperator.Context.Bills.Local.ToObservableCollection();
        }
        bool CheckSum()
        {
            try
            {
                SumToTransfer = decimal.Parse(SumFromBox);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }

    }
}
