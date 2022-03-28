using Autofac;
using DocFormer.Core;
using DocFormer.Core.Interfaces;
using DocFormer.Templates.APS;
using DocFormer.Views;
using Prism.Autofac;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DocFormer
{
    class BootStrapper : AutofacBootstrapper
    {
        /// <summary>Creates the shell or main window of the application.</summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        /// <summary>Initializes the shell.</summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(Modules.CustomerEditModule.CustomerModule));
            catalog.AddModule(typeof(Modules.OrganizationEditModule.OrganizationModule));
            catalog.AddModule(typeof(Modules.ObjectEditModule.ObjectModule));
            catalog.AddModule(typeof(Modules.TechnologyEditModule.TechnologyModule));
            catalog.AddModule(typeof(Modules.ActSelectorModule.SelectorModule));
            base.InitializeModules();
        }



        /// <summary>
        /// Creates the <see cref="T:Autofac.ContainerBuilder" /> that will be used to create the default container.
        /// </summary>
        /// <returns>A new instance of <see cref="T:Autofac.ContainerBuilder" />.</returns>
        protected override ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(Collections)).As<ICollections>().SingleInstance();
            builder.RegisterType(typeof(VControl)).As<IVControl>();

            return builder;
        }
    }
}
