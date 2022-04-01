using MyBank.Models;
using MyBank.ViewModels;
using MyBank.Views;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyBank.Helpers
{
    class BillConverter : IMultiValueConverter
    {
        public static DateTime Actual { get; set; } = DateTime.UtcNow;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue && values[0].ToString().Length > 0)
            {
                try
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(values[0].ToString(), "[^0-9]"))
                    {
                        MessageBox.Show("Only numbers!!!");
                        //delete letter
                        (values[3] as AddBillView).Money.Text = (values[3] as AddBillView).Money.Text.Remove(values[0].ToString().Length - 1);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            //if null command disabled
            else
            {
                return null;
            }
            if (values[1] != null && values[1] != DependencyProperty.UnsetValue && values[1].ToString().Length > 0)
            {
                try
                {
                    //Try convert date to srtring
                    values[1] = DateTime.ParseExact(values[1].ToString(), "dd/MM/yyyy", null);
                }
                catch (Exception)
                {
                    //same
                    return null;
                }
            }
            else
            {
                return null;
            }
            if (values[2] != null && values[2] != DependencyProperty.UnsetValue && values[2].ToString().Length > 0
                && values[2] != DependencyProperty.UnsetValue)
            {
                if (DateTime.ParseExact(values[2].ToString(), "dd/MM/yyyy", null) < Actual)
                {
                    MessageBox.Show("earlier than now?");
                    return null;
                }
                try
                {
                    values[2] = DateTime.ParseExact(values[2].ToString(), "dd/MM/yyyy", null);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return new Bill()
            {
                Owner = BankVM.ClientForNewBill,
                Money = decimal.Parse(values[0].ToString()),
                IsLocked = false,
                StartTime = DateTime.Parse(values[1].ToString()),
                EndTime = DateTime.Parse(values[2].ToString()),
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

