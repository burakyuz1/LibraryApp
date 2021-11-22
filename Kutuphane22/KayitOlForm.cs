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
    public partial class KayitOlForm : Form
    {
        private readonly KullaniciYoneticisi kullaniciYoneticisi;

        public KayitOlForm(KullaniciYoneticisi kullaniciYoneticisi)
        {
            InitializeComponent();
            this.kullaniciYoneticisi = kullaniciYoneticisi;
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            string adSoyad = txtAdSoyad.Text.Trim();
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string parola = txtParola.Text;
            if (ParolaDogrula() || kullaniciYoneticisi.KullaniciVarmi(kullaniciAdi) || string.IsNullOrEmpty(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Hatalı İşlem!");
                return;
            }
            kullaniciYoneticisi.KayitOlma(adSoyad, kullaniciAdi, parola);
            MessageBox.Show("Kayıt İşlemi Başarıyla Gerçekleştirilmiştir.");
            Close();
        }

        private void txtParola_TextChanged(object sender, EventArgs e)
        {
            if (ParolaDogrula())
            {
                lblEslesme.Text = "Parola eşleşmiyor!";
                lblEslesme.ForeColor = Color.Red;
            }
            else
            {
                lblEslesme.Text = "Parola başarılı";
                lblEslesme.ForeColor = Color.Green;
            }
        }

        private bool ParolaDogrula()
        {
            return (txtParola.Text != txtParolaTekrar.Text) || string.IsNullOrEmpty(txtParola.Text) || string.IsNullOrEmpty(txtParolaTekrar.Text);
        }

        private void txtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if (kullaniciYoneticisi.KullaniciVarmi(txtKullaniciAdi.Text.Trim()) || string.IsNullOrEmpty(txtKullaniciAdi.Text))
            {
                lblEslesme.Text = "Kullanıcı Adı Uygun Değil!";
                lblEslesme.ForeColor = Color.Red;
            }
            else
            {
                lblEslesme.Text = "Kullanıcı Adı Uygun!";
                lblEslesme.ForeColor = Color.Green;
            }
        }
    }
}
