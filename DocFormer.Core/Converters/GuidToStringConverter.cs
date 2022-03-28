using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace DocFormer.Core.Converters
{
    public class GuidToStringConverter : MarkupExtension, IValueConverter
    {
        public Guid guid
        {
            get
            {
                return _guid;
            }

            set
            {
                _guid = value;
            }
        }
        private Guid _guid = Guid.Empty;
        public Dictionary<Guid, string> dictionary
        {
            get
            {
                return _dictionary;
            }
            set
            {
                _dictionary = value;
            }
        }
        private Dictionary<Guid, string> _dictionary = new Dictionary<Guid, string>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (guid != null && dictionary != null)
            {
                if (guid != Guid.Empty)
                {

                    if (dictionary.ContainsKey(guid))
                        return dictionary[guid];
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

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
