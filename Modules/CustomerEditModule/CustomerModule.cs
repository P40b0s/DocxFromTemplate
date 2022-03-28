using DocFormer.Modules.CustomerEditModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.CustomerEditModule
{
    public class CustomerModule : IModule
    {
        IRegionManager _regionManager;
        public CustomerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("CustomerEditModule", typeof(CustomerEditModuleView));
        }
    }
}
