using BankEF.CommandsConverters;
using MyBank.Helpers;
using System;

namespace MyBank.Models
{
    public class CommonClient : IProp, IBankItem
    {
        private int maxDepositsCount = 5;
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public int MaxDepositsCount
        {
            get => maxDepositsCount; set
            {
                maxDepositsCount = value; OnPropertyChanged("MaxDepositsCount");
            }
        }
        public Guid Id { get; set; }
    }
}
