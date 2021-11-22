using Kutuphane22.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane22
{
    public partial class HesabimForm : Form
    {
        private readonly Kullanici _kullanici;
        private readonly KutuphaneYoneticisi _kutuphaneYoneticisi;
        Kitap kitap;

        public HesabimForm(Kullanici kullanici, KutuphaneYoneticisi kutuphaneYoneticisi)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _kutuphaneYoneticisi = kutuphaneYoneticisi;
            KullaniciBilgileriniGetir();
            KitaplariGuncelle();
        }

        private void KitaplariGuncelle()
        {
            dgvOduncAlinanKitaplar.DataSource = null;
            dgvOduncAlinanKitaplar.DataSource = _kullanici.OduncAlinanKitaplar;
            dgvOduncAlinanKitaplar.Columns[0].Visible = false;
            dgvOduncAlinanKitaplar.Columns[7].Visible = false;
        }

        private void KullaniciBilgileriniGetir()
        {

            lblAdSoyad.Text += _kullanici.AdSoyad;
            lblID.Text += _kullanici.Id;
            lblParola.Text += _kullanici.Parola;
            lblKullaniciAdi.Text += _kullanici.KullaniciAdi;
        }

        private void dgvOduncAlinanKitaplar_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvOduncAlinanKitaplar.SelectedRows.Count > 0)
                 kitap = (Kitap)dgvOduncAlinanKitaplar.SelectedRows[0].DataBoundItem;
            DateTime oduncAlmaTarihi = (DateTime)kitap.OduncAlinmaTarihi;
            dtpSonTeslimTarihi.Value = oduncAlmaTarihi.AddDays(14);
        }

        private void btnKitapTeslimEt_Click(object sender, EventArgs e)
        {
            _kullanici.OduncAlinanKitaplar.Remove(kitap);
            _kutuphaneYoneticisi.KitapTeslim(kitap);
            KitaplariGuncelle();
        }
    }
}
