using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace DocFormer.Templates
{
   public class Formats
    {
       public float MarginConverter(float cm)
        {
            try
            {
                //"1 дюйм = 2,54 см"
                float multipiller = 0.3937007874015748F;
                float cmtoinch = cm * multipiller;
                //1 одно значение отсупа в классе Docx = 1/72 of an inch.
                return cmtoinch * 72;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public float TableSizeConverter(float cm)
        {
            try
            {
                //"100 = 3,53 см"
                float multipiller = 28.328611898017F;
                return cm * multipiller;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Formatting TNR12
        {
            get
            {
                var t = new Formatting();
                t.Size = 12;
                t.FontFamily = new Font("Times New Roman");
                return t;
            }
        }
        public Formatting TNR12Bold
        {
            get
            {
                var t = new Formatting();
                t.Size = 12;
                t.FontFamily = new Font("Times New Roman");
                t.Bold = true;
                return t;
            }
        }

        public Formatting TNR12Italic
        {
            get
            {
                var t = new Formatting();
                t.Size = 12;
                t.FontFamily = new Font("Times New Roman");
                t.Italic = true;
                return t;
            }
        }

        public Formatting TNR8Italic
        {
            get
            {
                var t = new Formatting();
                t.Size = 8;
                t.FontFamily = new Font("Times New Roman");
                t.Italic = true;
                return t;
            }
        }
    }
}
