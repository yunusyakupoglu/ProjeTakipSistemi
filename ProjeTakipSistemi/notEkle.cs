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
    public partial class notEkle : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
        SqlCommand cmd;
        public notEkle()
        {
            InitializeComponent();
            label4.Text = GirisPaneli.user;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
            cmd = new SqlCommand();
            cmd.Connection = con;


            //Hataları engellemeye yönelik olarak, tüm veritabanı işlemlerini try-catch blokları arasında yapmaya özen gösterin.
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command1 = new SqlCommand("select id from tblPersonel where kullaniciAdi = '" + label4.Text + "'", con);
                int proje = (int)command1.ExecuteScalar();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string Konular = "insert into notlar(BAŞLIK,KATEGORİ,İÇERİK,personelID) values (@BAŞLIK,@KATEGORİ,@İÇERİK,@personelID)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand kod = new SqlCommand(Konular, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                kod.Parameters.AddWithValue("@BAŞLIK", textBox1.Text);
                kod.Parameters.AddWithValue("@KATEGORİ", comboBox1.Text);
                kod.Parameters.AddWithValue("@İÇERİK", richTextBox1.Text);
                kod.Parameters.AddWithValue("@personelID", proje);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                kod.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Not Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kategoriEkle ekle = new kategoriEkle();
            ekle.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kategoriSil sil = new kategoriSil();
            sil.Show();
        }

        private void notEkle_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand command = new SqlCommand("SELECT id from tblPersonel where kullaniciAdi ='" + GirisPaneli.user + "'");
            SqlDataReader dr;

            command.Connection = con;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                label4.Text = dr["id"].ToString();
            }
            dr.Close();
            dr.Dispose();

            string strSQL = string.Format("select * from notlarKategori where personelID='"+label4.Text+"'");
            SqlDataAdapter adp = new SqlDataAdapter(strSQL, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][1].ToString());
            }
            con.Close();
        }
    }
}
