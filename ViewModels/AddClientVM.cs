using MyBank.Db.DataJob;
using MyBank.Helpers;

namespace MyBank.ViewModels
{
    public class AddClientVM
    {
        public RelayCommand AddClientCommand { get; set; }
        public AddClientVM() =>
        AddClientCommand = new(o => BankItemsOperator.AddBankItem(o as IBankItem),
                           o => { return o != null && o is IBankItem; });
    }
}