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

namespace DocFormer.Modules.OrganizationEditModule.ViewModels
{
    public class OrganizationEditModuleViewModel : PropertyChangedRealization
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IContainer container {get;set;}
        IEventAggregator eventAggreagtor;
        ICollections Collections;
        public OrganizationEditModuleViewModel(ICollections _collections, IEventAggregator events)
        {
           
            Collections = _collections;
            eventAggreagtor = events;
            Org = new ObservableCollection<Core.Models.Organizations>();
            CommandsInitialization();
            LoadFromSQLComplete();

        }

        private void LoadFromSQLComplete()
        {
            try
            {
                Collections.SyncCollection(Org);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        public ObservableCollection<Organizations> Org
        {
            get
            {
                return this._Org;
            }
            set
            {
                if (this.Org != value)
                {
                    this._Org = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Organizations> _Org { get; set; }

        #region Добавление новой организации или редактирование старой
        public Organizations AddItem
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
        private Organizations _AddItem { get; set; }

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
                WindowTitle = "Добавить новую организацию";
                EditWindowIsOpen = true;
                AddItem = new Organizations();
                AddItem.Id = Guid.NewGuid();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        private void Edit(object id)
        {
            try
            {
                SaveMessage = "";
                WindowTitle = "Редактировать организацию";
                EditWindowIsOpen = true;
                AddItem = Org.Where(i => i.Id == (Guid)id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        private void Save()
        {
            try
            {
                SaveMessage = Collections.InsertOrUpdate(AddItem);
                if (!SaveMessage.StartsWith("Ошибка!"))
                {
                    Collections.SyncCollection(Org);
                    EditWindowIsOpen = false;
                    eventAggreagtor.GetEvent<OrganizationsUpdateEvent>().Publish();
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
                AddItem = Org.Where(i => i.Id == (Guid)id).FirstOrDefault();
                var c = Collections.Customers.Where(cust => cust.Organization == AddItem.Id);
                if (c.Count() > 0)
                {
                    DeleteMessage = "С данной организацией связанны: ";
                    foreach (var customer in c)
                    {
                        DeleteMessage += Environment.NewLine + customer.FIO;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }
        private void AcceptDelete()
        {
            try
            {
                DeleteMessage = Collections.Delete(AddItem);
                if (!DeleteMessage.StartsWith("Ошибка!"))
                {
                    Collections.SyncCollection(Org);
                    DeleteWindowIsOpen = false;
                    eventAggreagtor.GetEvent<OrganizationsUpdateEvent>().Publish();
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
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
            //eventsAggregator.GetEvent<SqlLoadComplete>().Subscribe(LoadFromSQLComplete);
        }
        #endregion

    }
}
