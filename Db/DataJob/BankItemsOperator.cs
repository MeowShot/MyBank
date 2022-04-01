using MyBank.Helpers;
using MyBank.Models;
using System;
using System.Linq;
using System.Windows;

namespace MyBank.Db.DataJob
{
        /// <summary>
    /// Logic responsible for changing data in the database
    /// </summary>
    public static class BankItemsOperator
    {
        public static BankContext Context { get; set; } = new();
        public static void DeleteBankItem(IBankItem item)
        {
            switch (item.GetType().Name)
            {
                case "CommonClient":
                    Context.Commons.Remove(item as CommonClient);
                    break;
                case "VipClient":
                    Context.Vips.Remove(item as VipClient);
                    break;
                case "Bill":
                    Context.Bills.Remove(item as Bill);
                    break;
                default:
                    break;
            }
            Context.SaveChanges();
        }
        /// <summary>
        /// This method checks type of item.
        /// Then adds it to the appropriate dbset.
        /// Contains an additional check for CommonClient.
        /// </summary>
        /// <param name="item"></param>
        public static void AddBankItem(IBankItem item)
        {
            switch (item.GetType().Name)
            {
                case "CommonClient":
                    Context.Commons.Add(item as CommonClient);
                    break;
                case "VipClient":
                    Context.Vips.Add(item as VipClient);
                    break;
                case "Bill":
                    Bill BillToAdd = item as Bill;
                    var Client = Context.Commons.FirstOrDefault(x => x.Id == BillToAdd.Owner);
                    if (Client != null && Context.Commons.FirstOrDefault(x =>
                        x.Id == BillToAdd.Owner).GetType().Name ==
                    "CommonClient")
                    {
                        if (Client.MaxDepositsCount > 0)
                        {
                            Context.Bills.Add(item as Bill);
                            Context.Commons.FirstOrDefault(x => x.Id == BillToAdd.Owner).MaxDepositsCount--;
                        }
                        else { MessageBox.Show("the deposit creation limit for this client has been exceeded"); }
                    }
                    else
                    {
                        Context.Bills.Add(item as Bill);
                    }
                    break;
                default:
                    break;
            }
            try
            {
                Context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Некорректный ввод, либо такая запись уже была создана");
            }
        }
        /// <summary>
        /// Simple transfer between bills    
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sum"></param>
        public static void BillTransfer(Bill from, Bill to, decimal sum)
        {
            if (from.Money >= sum)
            {
                switch (from.Money)
                {
                    case > 0:
                        if (!from.IsLocked || !to.IsLocked)
                        {
                            Context.Bills.FirstOrDefault(x => x == from).Money -= sum;
                            Context.Bills.FirstOrDefault(x => x == to).Money += sum;
                            Context.SaveChanges();
                        }
                        else { MessageBox.Show("One of the bills is locked"); break; }
                        break;
                    default: MessageBox.Show("Bill is empty"); break;
                }
            }
            else
            {
                MessageBox.Show("not enough money!!!");
            }
        }
        /// <summary>
        /// This imitates % like at some real bank
        /// 12% per month
        /// </summary>
        /// <param name="CheckDate"></param>
        public static void DeposMonthlyGrowth(DateTime CheckDate)
        {

            foreach (var item in Context.Bills)
            {

                if (item.EndTime >= CheckDate)
                { item.IsLocked = true; }
                if (!item.IsLocked)
                {
                    item.Money += item.Money * 0.12M;
                }
            }
            Context.SaveChanges();
        }
        public static IQueryable<Bill> GetBills(Guid OwnerId)
        {
            return Context.Bills.Where(x => x.Owner == OwnerId);
        }
    }
}
