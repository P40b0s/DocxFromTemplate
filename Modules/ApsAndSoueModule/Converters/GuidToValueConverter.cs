using DocFormer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DocFormer.Modules.ActSelectorModule.Converters
{
    public class GuidToValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null)
            {
                var guid = (Guid)values[0];
                var d = (Dictionary<Guid, string>)values[1];
                if (guid != Guid.Empty)
                {
                   
                        if(d.ContainsKey(guid))
                            return d[guid];
                        else
                            return "Не определено";
                }
                else
                {
                    return "Не определено";
                }
            }
            else
            {
                return "Не определено";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
