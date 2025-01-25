using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace stoktakip
{
    public partial class UrunIslemleriUserControl : UserControl
    {
        public UrunIslemleriUserControl()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FEVER\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True;Encrypt=True;TrustServerCertificate=true");
        bool durum;

        private void markakontrol()
        {
            durum = true;
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(txtMarka.Text))
            {
                MessageBox.Show("Kategori ve Marka alanları boş olamaz.");
                durum = false;
                return;
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text == read["kategori"].ToString() && txtMarka.Text == read["marka"].ToString())
                {
                    durum = false;
                    break;
                }
            }
            baglanti.Close();
        }

        private void kategorigetir()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT kategori FROM kategoribilgileri", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                comboBox1.Items.Clear(); // Önceden eklenmiş öğeleri temizle
                while (read.Read())
                {
                    comboBox1.Items.Add(read["kategori"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Çekme Hatası");
            }
            finally
            {
                baglanti.Close();
            }
        }
        bool durum1;

        private void kategorikontrol()
        {
            durum1 = true;
            baglanti.Open();
            SqlCommand komu = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komu.ExecuteReader();
            while (read.Read())
            {
                if (txtKategoriOlusturma.Text == read["kategori"].ToString() || txtKategoriOlusturma.Text == "")
                {
                    durum1 = false;
                }
            }
            baglanti.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            markakontrol();
            if (durum == true)
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO markabilgileri (kategori, marka) VALUES (@kategori, @marka)", baglanti);
                    komut.Parameters.AddWithValue("@kategori", comboBox1.Text);
                    komut.Parameters.AddWithValue("@marka", txtMarka.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Marka Eklendi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Veri Ekleme Hatası");
                }
                finally
                {
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Böyle Bir Kategori Ve Marka Var.", "Uyarı");
            }

            txtMarka.Text = "";
            comboBox1.SelectedIndex = -1;  // ComboBox seçim indeksini sıfırla
        }

        private void kategorigetir2() // burada
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboKategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void UrunIslemleriUserControl_Load_1(object sender, EventArgs e)
        {
            kategorigetir();
            kategorigetir2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kategorikontrol();
            if (durum1 == true)
            {
                baglanti.Open();
                SqlCommand komu = new SqlCommand("insert into kategoribilgileri(kategori) values('" + txtKategoriOlusturma.Text + "')", baglanti);
                komu.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kategori Eklendi");
            }
            else
            {
                MessageBox.Show("Böyle Bir Kategori Zaten Bulunuyor.");
            }
            txtKategoriOlusturma.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        //burdan sonra
        bool durum3;

        private void barkodkontrol()
        {
            durum3 = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barkodno"].ToString() || txtBarkodNo.Text == "")
                {
                    durum3 = false;
                }
            }
            baglanti.Close();

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum3 == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into urun(barkodno,kategori,marka,urunadi,miktari,alisfiyati,satisfiyati,tarih) values(@barkodno,@kategori,@marka,@urunadi,@miktari,@alisfiyati,@satisfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
                komut.Parameters.AddWithValue("@marka", comboMarka.Text);
                komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktarı.Text));
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlışFiyatı.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatışFiyatı.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Ürün Eklendi");
            }
            else
            {
                MessageBox.Show("Böyle Bir Barkod No Bulunmaktadır.", "Uyarı");
            }

            comboMarka.Items.Clear();
            foreach (Control item in this.Controls)
            {
                if (item is System.Windows.Forms.TextBox)
                {
                    item.Text = "";
                }
                if (item is System.Windows.Forms.ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktari=miktari+'" + int.Parse(Miktarıtxt.Text) + "' where barkodno='" + BarkodNotxt.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in this.Controls)
            {
                if (item is System.Windows.Forms.TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Mevcut Miktara Ekleme Yapıldı.");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktari=miktari-'" + int.Parse(Miktarıtxt.Text) + "' where barkodno='" + BarkodNotxt.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in this.Controls)
            {
                if (item is System.Windows.Forms.TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Mevcut Miktardan Eksiltme Yapıldı.");
        }

        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text == "")
            {
                lblMiktari.Text = "";
                foreach (Control item in this.Controls)
                {
                    if (item is System.Windows.Forms.TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno like '" + BarkodNotxt.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                Kategoritxt.Text = read["kategori"].ToString();
                Markatxt.Text = read["marka"].ToString();
                ÜrünAdıtxt.Text = read["urunadi"].ToString();
                lblMiktari.Text = read["miktari"].ToString();
                AlışFiyatıtxt.Text = read["alisfiyati"].ToString();
                SatışFiyatıtxt.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri where kategori = '" + comboKategori.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
