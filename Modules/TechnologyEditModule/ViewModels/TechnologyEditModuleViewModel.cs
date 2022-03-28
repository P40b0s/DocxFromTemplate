using Autofac;
using DocFormer.Core;
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
using System.Windows.Data;

namespace DocFormer.Modules.TechnologyEditModule.ViewModels
{

    public class TechnologyEditModuleViewModel : PropertyChangedRealization
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IContainer container { get; set; }
        IEventAggregator eventAggreagtor;
        ICollections Collections;
        public TechnologyEditModuleViewModel(ICollections _collections, IEventAggregator events)
        {

            Collections = _collections;
            eventAggreagtor = events;
            Tech = new ObservableCollection<TechnologyModel>();
            CommandsInitialization();
            LoadFromSQLComplete();
        }

        private void LoadFromSQLComplete()
        {
            try
            {
                Collections.SyncCollection(Tech);
                technologyType = Collections.TechnologyType;
                technologyType.Add(Guid.Empty, "Не определено");
                technologyCountType = Collections.CountType;
                technologyCountType.Add(Guid.Empty, "Не определено");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        public ObservableCollection<TechnologyModel> Tech
        {
            get
            {
                return this._Tech;
            }
            set
            {
                if (this.Tech != value)
                {
                    this._Tech = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TechnologyModel> _Tech { get; set; }

        #region Тип оборудования
        /// <summary>
        /// Словарь с объектами
        /// </summary>
        public Dictionary<Guid, string> technologyType
        {
            get
            {
                return this._technologyType;
            }
            set
            {
                if (this.technologyType != value)
                {
                    this._technologyType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Dictionary<Guid, string> _technologyType { get; set; }

        #endregion

        #region Единица измерения оборудования

        /// <summary>
        /// Словарь с объектами
        /// </summary>
        public Dictionary<Guid, string> technologyCountType
        {
            get
            {
                return this._technologyCountType;
            }
            set
            {
                if (this.technologyCountType != value)
                {
                    this._technologyCountType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Dictionary<Guid, string> _technologyCountType { get; set; }

        #endregion


        #region Добавление нового объекта или редактирование старого
        public TechnologyModel AddItem
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
        private TechnologyModel _AddItem { get; set; }

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
                WindowTitle = "Добавить новое оборудование";
                EditWindowIsOpen = true;
                AddItem = new TechnologyModel();
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
                WindowTitle = "Редактировать оборудование";
                EditWindowIsOpen = true;
                AddItem = Tech.Where(i => i.Id == (Guid)id).FirstOrDefault();
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
                    Collections.SyncCollection(Tech);
                    EditWindowIsOpen = false;
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
                AddItem = Tech.Where(i => i.Id == (Guid)id).FirstOrDefault();
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
                    Collections.SyncCollection(Tech);
                    DeleteWindowIsOpen = false;
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
