using MyBank.Helpers;
using System;

namespace MyBank.Models
{
    public class VipClient : IBankItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
    }
}
