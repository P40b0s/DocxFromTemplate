using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocFormer.Templates.APS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DocFormer.Core;
using DocFormer.Core.Models;
using DocFormer.Core.Interfaces;

namespace DocFormer.Templates.APS.Tests
{
    [TestClass()]
    public class VControlTest
    {
        private IContainer Container;
        public VControlTest()
        {
            getContainer();
            getOrganizations();
        }

        private void getContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(Collections)).As<ICollections>().SingleInstance();
            builder.RegisterType(typeof(VControl)).As<IVControl>();
            Container = builder.Build(Autofac.Builder.ContainerBuildOptions.IgnoreStartableComponents);
        }
        [TestMethod()]
        public void GenerateDocxTest()
        {
            //VControl vc = new VControl();
            //vc.GenerateDocx();
        }


        [TestMethod()]
        public void t()
        {
            var vc = Container.Resolve<IVControl>();
            vc.TEST();
        }



        #region Акт приемки оборудования
        [TestMethod()]
        public void АктПриемкиОборудования_АПС()
        {
            //Формируем тестовый объект
            var customers = getCustomers();
            var obj = getObject();
            var tech = getListTech(Core.IdIdentification.TechnologyType.СредстваАПС);
            var ap = getActsProperties();


            var vc = Container.Resolve<IVControl>();
            vc.АктПриемкиОборудования_АПС(ap);
        }

        #endregion

        [TestMethod()]
        public void АктПриемкиОборудования_СОУЭ()
        {
            var customers = getCustomers();
            var obj = getObject();
            var tech = getListTech(Core.IdIdentification.TechnologyType.СредстваСОУЭ);
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.АктПриемкиОборудования_СОУЭ(ap);
        }

        [TestMethod()]
        public void АктПриемкиСмонтированногоОборудования_АПС()
        {
            var customers = getCustomers();
            var obj = getObject();
            var tech = getListTech(Core.IdIdentification.TechnologyType.СредстваАПС);
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.АктПриемкиСмонтированногоОборудования_АПС(ap);
        }

        [TestMethod()]
        public void АктПриемкиСмонтированногоОборудования_СОУЭ()
        {
            var customers = getCustomers();
            var obj = getObject();
            var tech = getListTech(Core.IdIdentification.TechnologyType.СредстваСОУЭ);
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.АктПриемкиСмонтированногоОборудования_СОУЭ(ap);
        }

        [TestMethod()]
        public void АктПриемаВЭксплуатацию_АПС_СОУЭ()
        {
            var customers = getCustomers();
            var obj = getObject();
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.АктПриемаВЭксплуатацию_АПС(ap);
            vc.АктПриемаВЭксплуатацию_СОУЭ(ap);
        }

        [TestMethod()]
        public void АктОбОкончанииПНР()
        {
            var customers = getCustomers();
            var obj = getObject();
            var ap = getActsProperties();


            var vc = Container.Resolve<IVControl>();

            vc.АктОбОкончанииПНР_АПС(ap);
            vc.АктОбОкончанииПНР_СОУЭ(ap);
        }

        [TestMethod()]
        public void АктСкрытыхРабот()
        {
            var customers = getCustomers();
            var obj = getObject();
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.АктСкрытыхРабот_АПС(ap);
            vc.АктСкрытыхРабот_СОУЭ(ap);
        }

        [TestMethod()]
        public void ВедомостьСмонтированогоОборудования()
        {
            var customers = getCustomers();
            var obj = getObject();
            var techAps = getListTech(Core.IdIdentification.TechnologyType.СредстваАПС);
            var techSO = getListTech(Core.IdIdentification.TechnologyType.СредстваСОУЭ);
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.ВедомостьОборудования_АПС(ap);
            vc.ВедомостьОборудования_СОУЭ(ap);
        }

        [TestMethod()]
        public void ТитульныйЛист()
        {
            var customers = getCustomers();
            var obj = getObject();
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.ТитульныйЛист(ap);
        }

        [TestMethod()]
        public void ПротоколЗаземления()
        {
            var customers = getCustomers();
            var obj = getObject();
            var ap = getActsProperties();

            var vc = Container.Resolve<IVControl>();
            vc.ПротоколЗаземления(ap);
        }







        #region Тестовый список пользователей

        private void getOrganizations()
        {
            var collections = Container.Resolve<ICollections>();
            Organizations alp = new Organizations();
            alp.Id = new Guid("0267b951-ca23-49b0-8e91-f58a0e74b1f3");
            alp.Name = "ООО «Альпина Пласт»";
            collections.Organizations.Add(alp);

            Organizations mis = new Organizations();
            mis.Id = new Guid("5b7a5655-24d4-40b2-8399-2c51cb690f03");
            mis.Name = "ООО «МИС»";
            mis.LicenseNumber = "№ 83874- 4342";
            mis.LicenseValidityPeriod = "Бессрочная";
            mis.LicenseIssued = "Выдана пупкиным П.П.";
            mis.Address = "3 Улица строителей дом 5";
            collections.Organizations.Add(mis);

            Organizations misI = new Organizations();
            misI.Id = new Guid("621b8977-3a49-4328-8318-180e8cb9c16f");
            misI.Name = "ООО «МИС Инжинеринг»";
            misI.LicenseNumber = "№ 83874- 4342";
            misI.LicenseValidityPeriod = "Бессрочная";
            misI.LicenseIssued = "Выдана пупкиным П.П.";
            misI.Address = "3 Улица строителей дом 5";
            collections.Organizations.Add(misI);

            Organizations eks = new Organizations();
            eks.Id = new Guid("a21b8977-3a49-4328-8318-180e8cb9c16a");
            eks.Name = "ООО «Рога и Копыта»";
            collections.Organizations.Add(eks);
        }

        private ActsProperties getActsProperties()
        {
            ActsProperties ap = new ActsProperties();
            ap.SignDate = DateTime.Now.Year;
            ap.WorkStartYear = DateTime.Now.Year;
            ap.WorkEndYear = DateTime.Now.Year;
            ap.ChNumber = "№31234123412134";
            return ap;
        }

        private List<Customers> getCustomers()
        {

            List<Customers> c = new List<Customers>();

            Customers zakazchik = new Customers();
            zakazchik.Id = new Guid();
            zakazchik.FIO = "Плакунов М.А.";
            zakazchik.Post = "инженер-электроник";
            zakazchik.Organization = new Guid("0267b951-ca23-49b0-8e91-f58a0e74b1f3");
            zakazchik.Type = new Guid("981EA0FA-970C-4596-B802-F286C81B150E");
            c.Add(zakazchik);

            Customers podr = new Customers();
            podr.Id = new Guid();
            podr.FIO = "Тябликов В.В.";
            podr.Post = "начальник электромонтажного участка";
            podr.Organization = new Guid("5b7a5655-24d4-40b2-8399-2c51cb690f03");
            podr.Type = new Guid("3A27D4E8-1F94-4C70-9CDD-2AE3531E74DD");
            c.Add(podr);

            Customers eksp = new Customers();
            eksp.Id = new Guid();
            eksp.FIO = "Никулин В.В.";
            eksp.Post = "великий нехочуха";
            eksp.Organization = new Guid("a21b8977-3a49-4328-8318-180e8cb9c16a");
            eksp.Type = new Guid("F0C9DDE8-89C5-4BA5-8F21-6AFEB03FC08B");
            c.Add(eksp);

            Customers proj = new Customers();
            proj.Id = new Guid();
            proj.FIO = "Махмуд Д.Д.";
            proj.Post = "великий хочуха";
            proj.Organization = new Guid("621b8977-3a49-4328-8318-180e8cb9c16f");
            proj.Type = new Guid("C0C9DDE8-89C5-4BA5-8D21-6A9EB03FC08B");
            c.Add(proj);

            Customers tech = new Customers();
            tech.Id = new Guid();
            tech.FIO = "Надзоров Т.Т.";
            tech.Post = "Надзиратель";
            tech.Organization = new Guid("0267b951-ca23-49b0-8e91-f58a0e74b1f3");
            tech.Type = new Guid("52553976-E22C-4435-AF79-DE544FB2BA30");
            c.Add(tech);

            Customers creator = new Customers();
            creator.Id = new Guid();
            creator.FIO = "Аверин А.С.";
            creator.Post = "Инженер ПТО";
            creator.Type = new Guid("AEED06C0-3895-4C71-BA63-5DE9A7EB0053");
            c.Add(creator);

            return c;
        }




        private List<TechnologyModel> getListTech(Guid type)
        {
            List<TechnologyModel> tech = new List<TechnologyModel>();
            TechnologyModel t = new TechnologyModel();
            t.TechnologyName = "Адресный приемно-контрольный охранно-пожарный прибор";
            t.TechnologyMark = "Рубеж-20П";
            t.TechnologyCreator = "ООО «КБПА»";
            t.Count = "1";
            t.CountType = new Guid("3a8fb6bc-10a3-459d-9c63-5bd428b4be6b");
            t.TechnologyType = type;
            tech.Add(t);
            tech.Add(t);
            tech.Add(t);
            tech.Add(t);
            tech.Add(t);

            return tech;
        }

        private Objects getObject()
        {
            Objects obj = new Objects();
            obj.ObjectName = "\"Альпина Пласт\" Офисно-производственно-складские помещения";
            obj.ObjectAdresse = "Московская обл., г. Клин, Ленинградское шоссе, 88км, стр. 103";
            return obj;
        }


        #endregion





        [TestMethod()]
        public void FullGenerate()
        {
            //VControl vc = new VControl();
            //vc.АктПриемкиОборудования_АПС();
            //vc.АктПриемкиОборудования_СОУЭ();
            //vc.АктПриемкиСмонтированногоОборудования_АПС();
            //vc.АктПриемкиСмонтированногоОборудования_СОУЭ();
        }




    }
}