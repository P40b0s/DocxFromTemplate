using DocFormer.Modules.OrganizationEditModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Modules.OrganizationEditModule
{
    public class OrganizationModule : IModule
    {
        IRegionManager _regionManager;
        public OrganizationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("OrganizationEditModule", typeof(OrganizationEditModuleView));
        }
    }
}
