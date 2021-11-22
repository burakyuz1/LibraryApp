using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane22.Data
{
    public class KutuphaneYoneticisi
    {
        public KutuphaneYoneticisi()
        {
            Kitaplar = new List<Kitap>();
        }
        public List<Kitap> Kitaplar { get; set; }

        public void KitapBagis(Kitap kitap,int adet)
        {
            for (int i = 0; i < adet; i++)
            {
                Kitaplar.Add(kitap);
            }
        }
        public void KitapTeslim(Kitap kitap)
        {
            
            Kitaplar.Add(kitap);
        }
        public void KitapImha(Kitap kitap)
        {
            Kitaplar.Remove(kitap);
        }
    }
}
