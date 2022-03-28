using DocFormer.Modules.ObjectEditModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.ObjectEditModule
{
    public class ObjectModule : IModule
    {
        IRegionManager _regionManager;
        public ObjectModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ObjectEditModule", typeof(ObjectEditModuleView));
        }
    }
}
