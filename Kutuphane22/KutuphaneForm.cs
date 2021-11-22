using Kutuphane22.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane22
{
    public partial class KutuphaneForm : Form
    {
        private readonly Kullanici kullanici;
        KutuphaneYoneticisi kutuphaneYoneticisi;

        public KutuphaneForm(Kullanici kullanici)
        {
            InitializeComponent();
            this.kullanici = kullanici;
            VerileriOku();
            TurleriEkle();
            DataGuncelle();//Bagis Yaptıktan sonra kitap ödünç aldıktan sonra DataGuncelle metodunu kullancam kod tekrarı yapmamak için metodlaştırıyorum.
        }

        private void DataGuncelle()
        {
            //TODO
            dgvKitaplar.DataSource = null;
            dgvKitaplar.DataSource = kutuphaneYoneticisi.Kitaplar;
            dgvKitaplar.Columns[0].Visible = false;
            dgvKitaplar.Columns[7].Visible = false;
        }

        private void TurleriEkle()
        {
            cmbTurler.Items.Add("Hepsi");
            foreach (var item in Enum.GetValues(typeof(KitapTur)))
            {
                cmbTurler.Items.Add(item);
            }
            cmbTurler.SelectedIndex = 0;//ilk değer seçili olsun
                                        //cmbTurler.SelectedIndex = -1; //seçili olmasını istemiyorsa
        }

        private void VerileriOku()
        {
            try
            {
                //Varsa Oku
                string json = File.ReadAllText("veriKutuphane.json");
                kutuphaneYoneticisi = JsonConvert.DeserializeObject<KutuphaneYoneticisi>(json);
            }
            catch (Exception)
            {
                kutuphaneYoneticisi = new KutuphaneYoneticisi();
                //Okuyamazsan oluştur.
            }
        }

        private void tsmiCikisYap_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiKitapOduncAl_Click(object sender, EventArgs e)
        {

            Kitap arananKitap = (Kitap)dgvKitaplar.SelectedRows[0].DataBoundItem;
            arananKitap.OduncAlinmaTarihi = DateTime.Now.AddDays(8);
            kullanici.OduncAlinanKitaplar.Add(arananKitap);
            kutuphaneYoneticisi.Kitaplar.Remove(arananKitap);
            DataGuncelle();
        }

        private void dgvKitaplar_MouseClick(object sender, MouseEventArgs e)
        {
            //Sağ click olduysa
            if (e.Button == MouseButtons.Right)
            {
                var position = dgvKitaplar.HitTest(e.X, e.Y).RowIndex;//satırlarda sağ click olduğunda göstermek için. Gri alanda sağ click olursa açılmıcak.
                if (position >= 0)
                {
                    contextMenuStrip1.Show(dgvKitaplar, new Point(e.X, e.Y));
                    dgvKitaplar.Rows[position].Selected = true;//sağ click yaptığımda dgvKitaplardaki seçimi değiştirmek için.
                }
            }
        }

        private void KutuphaneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VerileriKaydet();
        }

        private void VerileriKaydet()
        {
            string json = JsonConvert.SerializeObject(kutuphaneYoneticisi);
            File.WriteAllText("veriKutuphane.json", json);
        }

        private void tsmiHesabim_Click(object sender, EventArgs e)
        {
            HesabimForm hesabimForm = new HesabimForm(kullanici, kutuphaneYoneticisi);
            hesabimForm.ShowDialog();
            DataGuncelle();
        }

        private void tsmiBagisYap_Click(object sender, EventArgs e)
        {
            BagisForm bagisForm = new BagisForm(kutuphaneYoneticisi);
            bagisForm.ShowDialog();
            DataGuncelle();
        }

        private void cmbTurler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTurler.SelectedIndex > 0)
            {
                KitapTur secili = (KitapTur)cmbTurler.SelectedItem;
                dgvKitaplar.DataSource = kutuphaneYoneticisi.Kitaplar.Where(x => x.KitapTur == secili).ToList();
            }
            if (cmbTurler.SelectedIndex == 0)
                DataGuncelle();
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            if (txtArama.Text == "")
                DataGuncelle();
            string arananKelime = txtArama.Text;
            dgvKitaplar.DataSource = kutuphaneYoneticisi.Kitaplar.Where(x => x.Ad.Contains(arananKelime)).ToList();
        }

        private void tsmiKitapImhaEt_Click(object sender, EventArgs e)
        {
            Kitap kitap = (Kitap)dgvKitaplar.SelectedRows[0].DataBoundItem;
            kutuphaneYoneticisi.KitapImha(kitap);
            DataGuncelle();
        }
    }
}
