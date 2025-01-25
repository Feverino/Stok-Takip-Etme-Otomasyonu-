using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stoktakip
{
    public partial class frmGirişEkranı : Form
    {
        public frmGirişEkranı()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=FEVER\\SQLEXPRESS;Initial Catalog=Stok_Takip;Integrated Security=True");


        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select Username,Password from Users WHERE Username=@p1 AND Password = @p2", connection);
            command.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
            command.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                lblHata.Text = " ";
                frmArayuz frm = new frmArayuz();
                frm.Show();

                this.Hide();
            }
            else
            {

                lblHata.Font = new Font(lblHata.Font, FontStyle.Bold);
                lblHata.Text = "Kullanıcı Adı veya Şifre Yanlış!";



            }
            connection.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmŞifremiUnuttum frm = new frmŞifremiUnuttum();
            frm.ShowDialog();
            
        }

        private void chkSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSifreGoster.CheckState == CheckState.Checked)
            {
                txtSifre.UseSystemPasswordChar = true;
            }
            else if (chkSifreGoster.CheckState==CheckState.Unchecked)
            {
                txtSifre.UseSystemPasswordChar = false;
            }
        }

        private void lblHata_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmGirişEkranı_Load(object sender, EventArgs e)
        {

        }
    }
}
