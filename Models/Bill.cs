using BankEF.CommandsConverters;
using MyBank.Helpers;
using System;

namespace MyBank.Models
{
    public class Bill : IProp, IBankItem
    {
        private bool isLocked;
        private decimal money;
        public Guid Owner { get; set; }
        public bool IsLocked
        {
            get => isLocked; set
            {
                isLocked = value; OnPropertyChanged("IsLocked");
            }
        }
        public decimal Money
        {
            get => money; set
            {
                money = value; OnPropertyChanged("Money");
            }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid Id { get; set; }
    }
}
