using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    [Serializable]
    public class ObservableKeyValuePair<TKey, TValue> : 
                                                        INotifyPropertyChanged
    {
        public ObservableKeyValuePair(TKey k, TValue v)
        {
            this.Key = k;
            this.Value = v;
        }
        public ObservableKeyValuePair()
        {
        }
        #region properties
        private TKey key;
        private TValue value;

        public TKey Key
        {
            get { return key; }
            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        public TValue Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append('[');
            if (Key != null)
            {
                s.Append(Key.ToString());
            }
            s.Append(", ");
            if (Value != null)
            {
                s.Append(Value.ToString());
            }
            s.Append(']');
            return s.ToString();
        }
        #endregion

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
