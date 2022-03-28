using DocFormer.Core.ErrorsValidation;
using DocFormer.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public abstract class Base : PropertyChangedRealization ,INotifyDataErrorInfo, IBase
    {
        public Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if (this.Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Guid _Id { get; set; }

        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                if (this.IsSelected != value)
                {
                    this._IsSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool _IsSelected { get; set; }

        public virtual void Update(ICustomers c){}
        public virtual void Update(IObjects obj){}
        public virtual void Update(IOrganizations org){}
        public virtual void Update(ITechnologyModel tech){}

        #region Модель валидации данных
        [field: NonSerialized]
        protected ConcurrentDictionary<string, ObservableCollection<CustomErrorType>> errorsDictionary = new ConcurrentDictionary<string, ObservableCollection<CustomErrorType>>();
        public void AddError(CustomErrorType error, [CallerMemberName]string propertyName = "")
        {
            if (errorsDictionary != null && !string.IsNullOrEmpty(propertyName) && error != null)
            {
                if (!errorsDictionary.ContainsKey(propertyName))
                {
                    errorsDictionary[propertyName] = new ObservableCollection<CustomErrorType>();
                }
                if (!errorsDictionary[propertyName].Contains(error))
                {
                    errorsDictionary[propertyName].Insert(0, error);
                    OnPropertyErrorsChanged(propertyName);

                }
            }
        }

        public void RemoveError(CustomErrorType error, [CallerMemberName]string propertyName = "")
        {
            try
            {
                if (errorsDictionary != null && !string.IsNullOrEmpty(propertyName) && error != null)
                {
                    if (errorsDictionary.ContainsKey(propertyName) && errorsDictionary[propertyName].Contains(error))
                    {
                        errorsDictionary[propertyName].Remove(error);
                        if (errorsDictionary[propertyName].Count == 0)
                        {
                            ObservableCollection<CustomErrorType> ce = new ObservableCollection<CustomErrorType>();
                            errorsDictionary.TryRemove(propertyName, out ce);
                        }
                        OnPropertyErrorsChanged(propertyName);
                    }
                }
            }
            catch (System.Exception ex)
            {
             
            }
        }
        #endregion

        #region INotifyDataErrorInfo
        [field: NonSerialized]
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnPropertyErrorsChanged([CallerMemberName]string propertyName = "")
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                getCriticalErrorsCount();
            }

        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            ObservableCollection<CustomErrorType> errors = new ObservableCollection<CustomErrorType>();
            if (propertyName != null)
            {
                errorsDictionary.TryGetValue(propertyName, out errors);
                return errors;
            }

            else
                return null;
        }

        public bool HasErrors
        {
            get
            {
                try
                {
                    if (errorsDictionary != null)
                    {
                        return errorsDictionary.Any();
                    }
                    else return false;
                }
                catch { }
                return false;
            }
        }

        #region Мои методы для отсеивания ошибок      
        private void getCriticalErrorsCount()
        {
            try
            {
                int count = 0;
                foreach (var t in errorsDictionary)
                {
                    int c = t.Value.Where(d => d.MessageErrorType == ErrorType.ERROR).Count();
                    count += c;
                }
                CriticalErrorsCount = count;
            }
            catch { }
        }

        private int _CriticalErrorsCount { get; set; }
        public int CriticalErrorsCount
        {
            get
            {
                return this._CriticalErrorsCount;
            }
            set
            {
                this._CriticalErrorsCount = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        public virtual void PropertyTriggers([CallerMemberName]string propertyName = "")
        {

        }
        public virtual void Validation([CallerMemberName]string propertyName = "")
        {

        }

        #endregion

    }
}
