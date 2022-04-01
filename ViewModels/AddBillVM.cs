using MyBank.Db.DataJob;
using MyBank.Helpers;
using MyBank.Models;

namespace MyBank.ViewModels
{
    public class AddBillVM : BaseVM
    {
        public RelayCommand AddBillCommand { get; set; }
        public AddBillVM() => AddBillCommand = new(o =>
                {
                    BankItemsOperator.AddBankItem(o as Bill);
                    BankVM.BillsToShow.Add(o as Bill);
                }, o => { return o != null && o is Bill; });
    }
}
