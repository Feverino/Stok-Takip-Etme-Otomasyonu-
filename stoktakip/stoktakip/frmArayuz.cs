using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stoktakip
{
    public partial class frmArayuz : Form
    {
        public frmArayuz()
        {
            InitializeComponent();
        }
        
        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmArayuz_Load(object sender, EventArgs e)
        {

        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelArka_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadUserControl(UserControl uc)
        {
            // Sağ taraftaki PanelArka paneline UserControl yükleme
            PanelArka.Controls.Clear();  // Eski içeriği temizle
            uc.Dock = DockStyle.Fill;    // UserControl'ü panelin boyutuna uydur
            PanelArka.Controls.Add(uc);  // UserControl'ü panele ekle
        }

        // Kontrol Paneli Butonunun Click Olayı
        private void pictureBox1_Click(object sender, EventArgs e)
        {            
            KontrolPaneliUserControl kontrolPaneliUC = new KontrolPaneliUserControl();
            LoadUserControl(kontrolPaneliUC);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            AnaMenuUserControl1 anamenuUC = new AnaMenuUserControl1();
            LoadUserControl(anamenuUC);
        }
        // Ürünlerim Butonunun Click Olayı
        private void btnUrunlerim_Click(object sender, EventArgs e)
        {            
            UrunlerimUserControl musteriislemleriUC = new UrunlerimUserControl();
            LoadUserControl(musteriislemleriUC);
        }

        private void btnUrunIslemleri_Click(object sender, EventArgs e)
        {
            UrunIslemleriUserControl urunislemleriUC = new UrunIslemleriUserControl();
            LoadUserControl(urunislemleriUC);
        }
        
        private void btnMusteriIslemleri_Click(object sender, EventArgs e)
        {
            MusteriIslemleriUserControl urunlerimUC = new MusteriIslemleriUserControl();
            LoadUserControl(urunlerimUC);
        }

        private void btnUrunSatisi_Click(object sender, EventArgs e)
        {
            UrunSatısıUserControl urunsatisiUC = new UrunSatısıUserControl();
            LoadUserControl(urunsatisiUC);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Diğer butonlar için de benzer şekilde Click olayları eklenir.

    }
}
