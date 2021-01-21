using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ProjeTakipSistemi
{
    public partial class kategoriEkle : Form
    {
        public kategoriEkle()
        {
            InitializeComponent();
            label2.Text = GirisPaneli.user;
        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");

        private void button1_Click(object sender, EventArgs e)
        {
                
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command1 = new SqlCommand("select id from tblPersonel where kullaniciAdi = '" + label2.Text + "'",con);
                int proje = (int)command1.ExecuteScalar();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string Konular = "insert into notlarKategori(KATEGORİ,personelID) values (@KATEGORİ,@personelID)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand kod = new SqlCommand(Konular, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                kod.Parameters.AddWithValue("@KATEGORİ", textBox1.Text);
                kod.Parameters.AddWithValue("@personelID", proje);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                kod.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Kategori Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void kategoriEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
