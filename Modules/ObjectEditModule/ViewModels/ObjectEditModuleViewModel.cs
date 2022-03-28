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

namespace DocFormer.Modules.ObjectEditModule.ViewModels
{
    public class ObjectEditModuleViewModel : PropertyChangedRealization
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IContainer container { get; set; }
        IEventAggregator eventAggreagtor;
        ICollections Collections;
        public ObjectEditModuleViewModel(ICollections _collections, IEventAggregator events)
        {

            Collections = _collections;
            eventAggreagtor = events;
            Obj = new ObservableCollection<Core.Models.Objects>();
            CommandsInitialization();
            LoadFromSQLComplete();

        }

        private void LoadFromSQLComplete()
        {
            try
            {
                Collections.SyncCollection(Obj);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        public ObservableCollection<Objects> Obj
        {
            get
            {
                return this._Obj;
            }
            set
            {
                if (this.Obj != value)
                {
                    this._Obj = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Objects> _Obj { get; set; }

        #region Добавление нового объекта или редактирование старого
        public Objects AddItem
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
        private Objects _AddItem { get; set; }

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
                WindowTitle = "Добавить новый объект";
                EditWindowIsOpen = true;
                AddItem = new Objects();
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
                WindowTitle = "Редактировать объект";
                EditWindowIsOpen = true;
                AddItem = Obj.Where(i => i.Id == (Guid)id).FirstOrDefault();
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
                    Collections.SyncCollection(Obj);
                    EditWindowIsOpen = false;
                    eventAggreagtor.GetEvent<ObjectsUpdateEvent>().Publish();
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
                    Collections.SyncCollection(Obj);
                    DeleteWindowIsOpen = false;
                    eventAggreagtor.GetEvent<ObjectsUpdateEvent>().Publish();
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
