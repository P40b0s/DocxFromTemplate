using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core
{
    public class IdIdentification
    {
        public class CustomerType
        {
            public static readonly Guid Заказчик = new Guid("2ebfc392-620a-4215-b9d1-39afc56c18d0");
            public static readonly Guid Подрядчик = new Guid("f071118d-e4d0-49f2-b749-e2ae910246fb");
            public static readonly Guid Генподрядчик = new Guid("85419288-2969-441a-b41c-3d0062b75d99");
            public static readonly Guid Застройщик = new Guid("86773804-712f-4c52-a031-f20e27cb8f4c");
            public static readonly Guid Проектировщик = new Guid("055ba475-cc0c-4639-82a6-866b5afbe10c");
            public static readonly Guid Экспуатирующая = new Guid("574b1c37-5f08-4fc8-9a68-d59367a791f0");
            public static readonly Guid Технадзор = new Guid("1445d887-8f7e-4574-8129-8a84d919e1a1");
            public static readonly Guid Составитель = new Guid("00ffa6a0-f0bc-4ced-a72d-c475b94f69fa");
        }

        public class TechnologyType
        {
            public static readonly Guid СредстваАПС = new Guid("0117e6b1-5b52-46f4-aeed-98a51d84fb6f");
            public static readonly Guid СредстваСОУЭ = new Guid("022341cd-9045-4e38-9e50-b0107ef5a5ee");

        }
    }
}
