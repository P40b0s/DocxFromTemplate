using DocFormer.Modules.TechnologyEditModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.TechnologyEditModule
{
    public class TechnologyModule : IModule
    {
        IRegionManager _regionManager;
        public TechnologyModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("TechnologyEditModule", typeof(TechnologyEditModuleView));
        }
    }
}
