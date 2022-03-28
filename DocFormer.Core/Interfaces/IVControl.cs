using DocFormer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Interfaces
{
    public interface IVControl
    {
        void TEST();
        void GenerateActs(ActsProperties ap, DocumentsTemplates template);
        void АктПриемкиОборудования_АПС(ActsProperties ap, bool IsNeedTable = false);
        void АктПриемкиОборудования_СОУЭ(ActsProperties ap, bool IsNeedTable = false);
        void АктПриемкиСмонтированногоОборудования_АПС(ActsProperties ap, bool IsNeedTable = false);
        void АктПриемкиСмонтированногоОборудования_СОУЭ(ActsProperties ap, bool IsNeedTable = false);
        void АктПриемаВЭксплуатацию_АПС(ActsProperties ap, bool IsNeedTable = false);
        void АктПриемаВЭксплуатацию_СОУЭ(ActsProperties ap, bool IsNeedTable = false);
        void АктОбОкончанииПНР_АПС(ActsProperties ap, bool IsNeedTable = false);
        void АктОбОкончанииПНР_СОУЭ(ActsProperties ap, bool IsNeedTable = false);
        void АктСкрытыхРабот_АПС(ActsProperties ap, bool IsNeedTable = false);
        void АктСкрытыхРабот_СОУЭ(ActsProperties ap, bool IsNeedTable = false);
        void ВедомостьОборудования_АПС(ActsProperties ap, bool IsNeedTable = false);
        void ВедомостьОборудования_СОУЭ(ActsProperties ap, bool IsNeedTable = false);
        void ПротоколЗаземления(ActsProperties ap, bool IsNeedTable = false);
        void ТитульныйЛист(ActsProperties ap);
        void Перечень(ActsProperties ap);

    }
}
