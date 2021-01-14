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
using System.Data.Sql;
using System.Net;
using System.Net.Mail;

namespace ProjeTakipSistemi
{
    public partial class ePosta : Form
    {
        public ePosta()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-OM6H7IM; Initial Catalog=ProjeTakipSistemi;User Id=yunus;password=1234;");
            cmd = new SqlCommand("SELECT * FROM tblPersonel where kullaniciAdi='"+username.Text.ToString()+"' and ePosta='"+email.Text.ToString()+"'", con);
            con.Open();
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                try
                {
                    if (con.State==ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SmtpClient smtpserver = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    String tarih = DateTime.Now.ToLongDateString();
                    String mailadresin = ("subuyapiisleridairebaskanligi@gmail.com");
                    String sifre = ("Projesubu54");
                    String smptsrvr = "smtp.gmail.com";
                    String kime = (oku["ePosta"].ToString());
                    String konu = ("Şifre Hatırlatma Maili!");
                    String yaz = ("Sayın " + oku["AD"] + " " + oku["SOYAD"] + "\n" + "Bizden " + tarih + " Tarihinde şifre hatırlatma talebinde bulundunuz." + "\n" + "Kullanıcı Adınız: " + oku["kullaniciAdi"].ToString() + "\n"+"Parolanız: "+oku["parola"].ToString()+"\nİyi Günler Dileriz...");
                    smtpserver.Credentials = new NetworkCredential(mailadresin, sifre);
                    smtpserver.Port = 587;
                    smtpserver.Host = smptsrvr;
                    smtpserver.EnableSsl = true;
                    mail.From = new MailAddress(mailadresin);
                    mail.To.Add(kime);
                    mail.Subject = konu;
                    mail.Body = yaz;
                    smtpserver.Send(mail);
                    DialogResult bilgi = new DialogResult();
                    bilgi = MessageBox.Show("Şifreniz mail adresinize gönderilmiştir.");
                    this.Hide();
                }
                catch (Exception Hata)
                {

                    MessageBox.Show("Mail Gönderme Hatası!", Hata.Message);
                }

            }
        }
    }
}
