using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Net.Mail;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;

namespace stoktakip
{
    public partial class frmŞifremiUnuttum : Form
    {
        public frmŞifremiUnuttum()
        {
            InitializeComponent();
        }

        public bool MailGonder(string aliciMail, string konu, string icerik)
        {
            try
            {
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress("depomatikbusiness@gmail.com");  // Gönderen e-posta adresi
                ePosta.To.Add(aliciMail); // Alıcı e-posta adresi
                ePosta.Subject = konu;  // Başlık
                ePosta.Body = icerik;   // İçerik

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential("depomatikbusiness@gmail.com", "ggvb arci csck mipg"); // Uygulama şifresi
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;

                // Mail gönderme işlemi
                smtp.Send(ePosta);
                return true;
            }
            catch (Exception ex)
            {
                // Eğer hata olursa, hata mesajını ekranda göster
                MessageBox.Show("Mail gönderme hatası: " + ex.Message);
                return false;
            }
        }


        private void btnGonder_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=FEVER\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True";

            try
            {
                // Kullanıcı maili üzerinden sorgu yapıyoruz
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    string sorgu = "SELECT Password, Mail FROM Users WHERE Mail = @Mail";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@Mail", txtMail.Text.Trim());

                        SqlDataReader oku = komut.ExecuteReader();
                        if (oku.Read()) // Eğer kullanıcı bulunduysa
                        {
                            string sifre = oku["Password"].ToString(); // Şifreyi al
                            string aliciMail = oku["Mail"].ToString(); // Maili al

                            // Veritabanından aldığımız bilgilerle mail gönder
                            bool mailDurumu = MailGonder(aliciMail, "Uygulama Şifre Hatırlatma", $"Merhaba,\n\nŞifreniz: {sifre}");

                            if (mailDurumu)
                            {
                                lblHata.Visible = true;
                                lblHata.ForeColor = Color.Green;
                                lblHata.Text = "Şifreniz e-posta adresinize gönderildi.";
                            }
                            else
                            {
                                lblHata.Visible = true;
                                lblHata.ForeColor = Color.Red;
                                lblHata.Text = "Mail gönderilirken hata oluştu.";
                            }
                        }
                        else
                        {
                            lblHata.Visible = true;
                            lblHata.ForeColor = Color.Red;
                            lblHata.Text = "Girdiğiniz e-posta adresi sistemde bulunamadı.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblHata.Visible = true;
                lblHata.ForeColor = Color.Red;
                lblHata.Text = "Hata Oluştu: " + ex.Message;
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtEposta_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        
        private Image normalImage;
        private Image hoverImage;

        private void frmŞifremiUnuttum_Load(object sender, EventArgs e)
        {
            normalImage = Image.FromFile(@"C:\Users\BATUHAN\Desktop\stoktakip\mailgonder.png");
            hoverImage = Image.FromFile(@"C:\Users\BATUHAN\Desktop\stoktakip\mailgonder2.png");


            btnGonder.Image = normalImage;
            btnGonder.SizeMode = PictureBoxSizeMode.StretchImage;


            btnGonder.MouseEnter += btnGonder_MouseEnter;
            btnGonder.MouseLeave += btnGonder_MouseLeave;

        }
        private void btnGonder_MouseEnter(object sender, EventArgs e)
        {
            btnGonder.Image = hoverImage;
        }

        private void btnGonder_MouseLeave(object sender, EventArgs e)
        {
            btnGonder.Image = normalImage;
        }
        

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            frmGirişEkranı frm = new frmGirişEkranı();
            frm.ShowDialog();
            this.Close();
        }
    }
}
