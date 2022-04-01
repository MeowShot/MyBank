using MyBank.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
#nullable disable
namespace MyBank.Helpers
{
    public class ClientConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue && values[0].ToString().Length > 0)
            {
                try
                {
                    values[0] = values[0].ToString();
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
            if (values[1] != null && values[1] != DependencyProperty.UnsetValue && values[1].ToString().Length > 0)
            {
                try
                {
                    values[1] = values[1].ToString();
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
            if (values[2] != null && values[2] != DependencyProperty.UnsetValue && values[2].ToString().Length > 0)
            {
                try
                {
                    values[2] = values[2].ToString();
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
            if (values[3] != null && values[3] != DependencyProperty.UnsetValue && values[3].ToString().Length > 0)
            {
                try
                {
                    values[3] = values[3].ToString();
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
            return values[2].ToString() switch
            {
                "Common" => new CommonClient
                {
                    Name = values[0] as string,
                    LastName = values[1] as string,
                    Organization = values[3] as string,
                    Id = Guid.NewGuid()
                },
                "Vip" => new VipClient
                {
                    Name = values[0] as string,
                    LastName = values[1] as string,
                    Organization = values[3] as string,
                    Id = Guid.NewGuid()
                },
                _ => null,
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
