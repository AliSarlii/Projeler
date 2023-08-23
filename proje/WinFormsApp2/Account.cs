using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    internal class Account
    {
        public string isim { get; set; }
        public string soy_isim { get; set; }
        public string KullaniciAdi { get;set;}
        public string Sifre { get; set; }   
        
        public bool Yönetici { get; set; }
    }
}
