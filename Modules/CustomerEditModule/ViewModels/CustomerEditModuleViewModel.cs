using Autofac;
using DocFormer.Core;
using DocFormer.Core.EventsAggregator;
using DocFormer.Core.Interfaces;
using DocFormer.Core.Models;
using NLog;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.CustomerEditModule.ViewModels
{
    public class CustomerEditModuleViewModel : PropertyChangedRealization
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IContainer container { get; set; }
        ICollections Collections;
        IEventAggregator eventAggregator;
        public CustomerEditModuleViewModel(ICollections _collections, IEventAggregator events)
        {

            Collections = _collections;
            eventAggregator = events;
            customers = new ObservableCollection<Core.Models.Customers>();
            CommandsInitialization();
            EventsInitialization();
            LoadFromSQLComplete();

           
        }

    
        private void OrganizationTableUpdate()
        {
            try
            {
                var o = Collections.Organizations.ToDictionary(k => k.Id, v => v.Name);
                o.Add(Guid.Empty, "не определено");
                organizationsType = new Dictionary<Guid, string>();
                organizationsType = o;
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
                Collections.SyncCollection(customers);
                customersType = Collections.CustomerType;
                OrganizationTableUpdate();
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }


        public ObservableCollection<Customers> customers
        {
            get
            {
                return this._customers;
            }
            set
            {
                if (this.customers != value)
                {
                    this._customers = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Customers> _customers { get; set; }


        #region Тип субъекта
        public Dictionary<Guid, string> customersType
        {
            get
            {
                return this._customersType;
            }
            set
            {
                if (this.customersType != value)
                {
                    this._customersType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Dictionary<Guid, string> _customersType { get; set; }
        #endregion

        #region Организация
        /// <summary>
        /// Словарь с организациями
        /// </summary>
        public Dictionary<Guid, string> organizationsType
        {
            get
            {
                return this._organizationsType;
            }
            set
            {
                if (this.organizationsType != value)
                {
                    this._organizationsType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Dictionary<Guid, string> _organizationsType { get; set; }
        #endregion

        #region Объекты
        /// <summary>
        /// Словарь с объектами
        /// </summary>
        public Dictionary<Guid, string> objectsType
        {
            get
            {
                return this._objectsType;
            }
            set
            {
                if (this.objectsType != value)
                {
                    this._objectsType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Dictionary<Guid, string> _objectsType { get; set; }
        #endregion


        #region Добавление новой организации или редактирование старой
        public Customers AddItem
        {
            get
            {
                return this._AddItem;
            }
            set
            {
                if (this.AddItem != value)
                {
                    this._AddItem = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Customers _AddItem { get; set; }

        public string SaveMessage
        {
            get
            {
                return this._SaveMessage;
            }
            set
            {
                if (this.SaveMessage != value)
                {
                    this._SaveMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _SaveMessage { get; set; }

        #endregion

        #region Добавление и редактирование документа
        public bool EditWindowIsOpen
        {
            get
            {
                return this._EditWindowIsOpen;
            }
            set
            {
                if (this.EditWindowIsOpen != value)
                {
                    this._EditWindowIsOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool _EditWindowIsOpen { get; set; }
        public string WindowTitle
        {
            get
            {
                return this._WindowTitle;
            }
            set
            {
                if (this.WindowTitle != value)
                {
                    this._WindowTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _WindowTitle { get; set; }
        private void Add()
        {
            try
            {
                SaveMessage = "";
                WindowTitle = "Добавить новый субъект";
                EditWindowIsOpen = true;
                AddItem = new Customers();
                AddItem.Id = Guid.NewGuid();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                SaveMessage = ex.Message;
            }
        }

        private void Edit(object id)
        {
            try
            {
                SaveMessage = "";
                WindowTitle = "Редактировать субъект";
                AddItem = customers.Where(i => i.Id == (Guid)id).FirstOrDefault();
                EditWindowIsOpen = true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                SaveMessage = ex.Message;
            }
        }

        private void Save()
        {
            try
            {
                SaveMessage = Collections.InsertOrUpdate(AddItem);
                if (!SaveMessage.StartsWith("Ошибка!"))
                {
                    Collections.SyncCollection(customers);
                    EditWindowIsOpen = false;
                    eventAggregator.GetEvent<CustomerUpdateEvent>().Publish();
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }
        #endregion


        #region Удаление документа

        public bool DeleteWindowIsOpen
        {
            get
            {
                return this._DeleteWindowIsOpen;
            }
            set
            {
                if (this.DeleteWindowIsOpen != value)
                {
                    this._DeleteWindowIsOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool _DeleteWindowIsOpen { get; set; }

        public string DeleteMessage
        {
            get
            {
                return this._DeleteMessage;
            }
            set
            {
                if (this.DeleteMessage != value)
                {
                    this._DeleteMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _DeleteMessage { get; set; }

        private void Delete(object id)
        {
            try
            {
                DeleteMessage = "";
                WindowTitle = "Удалить запись";
                DeleteWindowIsOpen = true;
                ///Удалить запись
                AddItem = customers.Where(i => i.Id == (Guid)id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }
        private void AcceptDelete()
        {
            DeleteMessage = Collections.Delete(AddItem);
            if (!DeleteMessage.StartsWith("Ошибка!"))
            {
                Collections.SyncCollection(customers);
                DeleteWindowIsOpen = false;
                eventAggregator.GetEvent<CustomerUpdateEvent>().Publish();
            }
        }
        private void RejectDelete()
        {
            DeleteWindowIsOpen = false;
        }
        #endregion




        #region Commands
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<object> DeleteCommand { get; set; }
        public DelegateCommand DeleteAcceptCommand { get; set; }
        public DelegateCommand DeleteRejectCommand { get; set; }


        private void CommandsInitialization()
        {
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand<object>(Edit);
            SaveCommand = new DelegateCommand(Save);

            DeleteCommand = new DelegateCommand<object>(Delete);
            DeleteAcceptCommand = new DelegateCommand(AcceptDelete);
            DeleteRejectCommand = new DelegateCommand(RejectDelete);
        }

        public void EventsInitialization()
        {
            eventAggregator.GetEvent<OrganizationsUpdateEvent>().Subscribe(OrganizationTableUpdate);
        }
        #endregion
    }
}
