using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjeTakipSistemi
{
    public partial class GirisPaneli : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        bool drmcontrol = false;
        public GirisPaneli()
        {
            InitializeComponent();
        }

        public static string user;
        public static string yetki;
        public static string pass;
        private void GirisButton_Click(object sender, EventArgs e)
        {
            user = txtUser.Text;
            pass = txtPass.Text;
            con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
            cmd = new SqlCommand("SELECT * FROM tblPersonel", con);

            con.Open();
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["kullaniciAdi"].ToString() == txtUser.Text && dr["parola"].ToString() == txtPass.Text && dr["yetki"].ToString() == "Admin")
                {
                    //List icrk = new List();
                    //icrk.Show();
                    List admin = new List();
                    admin.Show();
                    this.Hide();
                    drmcontrol = true;
                    break;

                }
                else if (dr["kullaniciAdi"].ToString() == txtUser.Text && dr["parola"].ToString() == txtPass.Text && dr["yetki"].ToString() == "Üye")
                {
                    List admin = new List();
                    admin.Show();
                    this.Hide();
                    drmcontrol = true;
                    break;
                }
                else if (txtUser.Text == "" || txtPass.Text == "")
                {
                    MessageBox.Show("Kullanıcı adı ve şifre boş geçilemez.");
                    drmcontrol = true;
                    break;
                }

                drmcontrol = false;


            }
            if (drmcontrol == false)
            {
                MessageBox.Show("Böyle bir kullanıcı bulunamadı.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ePosta posta = new ePosta();
            posta.Show();
        }
    }
}
