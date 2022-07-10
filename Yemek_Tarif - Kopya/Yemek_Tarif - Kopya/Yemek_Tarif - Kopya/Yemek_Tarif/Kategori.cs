using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yemek_Tarif.Models;

namespace Yemek_Tarif
{
    public class Kategori
    {

        public IEnumerable<tbl_Yemekler> Deger1 { get; set; }
        public IEnumerable<tbl_Kategoriler> Deger2 { get; set; }

    }
}