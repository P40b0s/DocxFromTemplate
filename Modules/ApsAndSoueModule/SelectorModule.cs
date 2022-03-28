using DocFormer.Modules.ActSelectorModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.ActSelectorModule
{
    public class SelectorModule : IModule
    {
        IRegionManager _regionManager;
        public SelectorModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ActSelectorModule", typeof(ActSelectorModuleView));
        }
    }
}
