using Autofac;
using DocFormer.Core;
using DocFormer.Core.ErrorsValidation;
using DocFormer.Core.EventsAggregator;
using DocFormer.Core.Interfaces;
using DocFormer.Core.Models;
using NLog;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.ActSelectorModule.ViewModels
{
    public class ActSelectorModuleViewModel : PropertyChangedRealization, INotifyDataErrorInfo
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static Autofac.IContainer container { get; set; }
        ICollections Collections;
        IEventAggregator eventAggregator;
        IVControl APS;
        public ActSelectorModuleViewModel(ICollections _collections, IEventAggregator events, IVControl _APS)
        {
            Collections = _collections;
            eventAggregator = events;
            APS = _APS;
            Organizations = new ObservableCollection<Core.Models.Organizations>();
            Objects = new ObservableCollection<Core.Models.Objects>();
            actsProperties = new ActsProperties();
            CommandsInitialization();
            EventsInitialization();
            LoadFromSQLComplete();
        }

    
        private void OrganizationTableUpdate()
        {
            try
            {
                Collections.SyncCollection(Organizations);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        private void ObjectsTableUpdate()
        {
            try
            {
                Collections.SyncCollection(Objects);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        private void LoadFromSQLComplete()
        {
            try
            {
                OrganizationTableUpdate();
                ObjectsTableUpdate();
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        public ActsProperties actsProperties
        {
            get
            {
                return this._actsProperties;
            }
            set
            {
                if (this.actsProperties != value)
                {
                    this._actsProperties = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ActsProperties _actsProperties { get; set; }

        public ObservableCollection<Objects> Objects
        {
            get
            {
                return this._Objects;
            }
            set
            {
                if (this.Objects != value)
                {
                    this._Objects = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Objects> _Objects { get; set; }


        public ObservableCollection<Organizations> Organizations
        {
            get
            {
                return this._Organizations;
            }
            set
            {
                if (this.Organizations != value)
                {
                    this._Organizations = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Organizations> _Organizations { get; set; }

     
        public string ErrorMessage
        {
            get
            {
                return this._ErrorMessage;
            }
            set
            {
                if (this.ErrorMessage != value)
                {
                    this._ErrorMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ErrorMessage { get; set; }

        #region Прогрессбар
        public double progressValue
        {
            get
            {
                return this._progressValue;
            }
            set
            {
                if (this.progressValue != value)
                {
                    this._progressValue = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private double _progressValue { get; set; }

        public double progressMaximum
        {
            get
            {
                return this._progressMaximum;
            }
            set
            {
                if (this.progressMaximum != value)
                {
                    this._progressMaximum = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private double _progressMaximum { get; set; }
        #endregion

        #region Commands
        public DelegateCommand GenerateAPSAndSOUEActsCommand { get; set; }
        public DelegateCommand GenerateTESTCommand { get; set; }

        private void CommandsInitialization()
        {
            GenerateAPSAndSOUEActsCommand = new DelegateCommand(GenerateAPSAndSOUEActs);
            GenerateTESTCommand = new DelegateCommand(GenerateTEST);
        }

        public void EventsInitialization()
        {
            eventAggregator.GetEvent<OrganizationsUpdateEvent>().Subscribe(OrganizationTableUpdate);
            eventAggregator.GetEvent<ObjectsUpdateEvent>().Subscribe(ObjectsTableUpdate);
        }
        #endregion


        #region Формирование актов АПС и СОУЭ

        private void GenerateTEST()
        {
            APS.TEST();
        }

        private void GenerateAPSAndSOUEActs()
        {
            ErrorMessage = string.Empty;
            var customers = Collections.Customers;
            try
            {
                var zak = customers.Where(c => c.Type == IdIdentification.CustomerType.Заказчик);
                if (zak.Count() == 0)
                {
                    ErrorMessage += "Заказчик не определен!" + Environment.NewLine;
                }

                var podr = customers.Where(c => c.Type == IdIdentification.CustomerType.Подрядчик);
                if (podr.Count() == 0)
                {
                    ErrorMessage += "Подрядчик не определен!" + Environment.NewLine;
                }

                var tnadzor = customers.Where(c => c.Type == IdIdentification.CustomerType.Технадзор);
                if (tnadzor.Count() == 0)
                {
                    ErrorMessage += "Технический надзор не определен!" + Environment.NewLine;
                }

                var creator = customers.Where(c => c.Type == IdIdentification.CustomerType.Составитель);
                if (creator.Count() == 0)
                {
                    ErrorMessage += "Составитель не определен!" + Environment.NewLine;
                }
                var eksp = customers.Where(c => c.Type == IdIdentification.CustomerType.Экспуатирующая);
                if (eksp.Count() == 0)
                {
                    ErrorMessage += "Эксплуатирующая организация не определена!" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(actsProperties.ChNumber))
                {
                    ErrorMessage += "Шифр проекта не определен!" + Environment.NewLine;
                }

                if (actsProperties.Object == null)
                {
                    ErrorMessage += "Объект не выбран!" + Environment.NewLine;
                }
                var project = customers.Where(c => c.Type == IdIdentification.CustomerType.Проектировщик);
                if (project.Count() == 0)
                {
                    ErrorMessage += "Проектная организация не выбрана!" + Environment.NewLine;
                }
                if (!string.IsNullOrEmpty(ErrorMessage))
                   throw new Exception(ErrorMessage);

               var aps = Collections.DocTemplates.Where(t => t.Type == Core.Enums.TemplatesActTypeEnum.АПС);
               foreach(var act in aps)
                {
                    var actnames = Collections.DocNames.Where(d => d.Id == act.Name).FirstOrDefault();
                    actsProperties.ActType = actnames.Name;
                    actsProperties.ActType_S = actnames.SingleName;
                    APS.GenerateActs(actsProperties, act);
                }

                var soue = Collections.DocTemplates.Where(t => t.Type == Core.Enums.TemplatesActTypeEnum.СОУЭ);
                foreach (var act in soue)
                {
                    var actnames = Collections.DocNames.Where(d => d.Id == act.Name).FirstOrDefault();
                    actsProperties.ActType = actnames.Name;
                    actsProperties.ActType_S = actnames.SingleName;
                    APS.GenerateActs(actsProperties, act);
                }

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }


           
        }
        #endregion

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

        public async void Validation([CallerMemberName]string propertyName = "")
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    //switch (propertyName)
                    //{
                    //    default:
                    //        {
                    //            return;
                    //        }
                    //    case "OrganName":
                    //        {
                    //            if (string.IsNullOrEmpty(OrganName))
                    //            {
                    //                AddError(Errors.ORGAN_ERROR, propertyName);
                    //            }
                    //            else
                    //            {
                    //                RemoveError(Errors.ORGAN_ERROR, propertyName);
                    //            }
                    //            break;
                    //        }
                    //}
                }
                catch (System.Exception ex)
                {
                    logger.Fatal(ex);
                }

            });
        }

        #endregion
    }
}
