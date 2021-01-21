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

namespace ProjeTakipSistemi
{
    public partial class kategoriSil : Form
    {
        public kategoriSil()
        {
            InitializeComponent();
        }
        SqlConnection con= new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
        SqlCommand cmd;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string secmeSorgusu = "SELECT * from notlarKategori where KATEGORİ=@KATEGORİ";
                SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, con);
                secmeKomutu.Parameters.AddWithValue("@KATEGORİ", listBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);
                SqlDataReader dr = secmeKomutu.ExecuteReader();
                if (dr.Read()) //Datareader herhangi bir okuma yapabiliyorsa aşağıdaki kodlar çalışır.
                {
                    string adi = dr["KATEGORİ"].ToString();
                    dr.Close();
                    //Datareader ile okunan müşteri ad ve soyadını isim değişkenine atadım.
                    //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
                    DialogResult durum = MessageBox.Show(adi + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                    //Kullanıcıya silme onayı penceresi açıp, verdiği cevabı durum değişkenine aktardık.
                    if (DialogResult.Yes == durum) // Eğer kullanıcı Evet seçeneğini seçmişse, veritabanından kaydı silecek kodlar çalışır.
                    {
                        string silmeSorgusu = "DELETE from notlarKategori where KATEGORİ=@KATEGORİ";
                        //musterino parametresine bağlı olarak müşteri kaydını silen sql sorgusu
                        SqlCommand delete = new SqlCommand(silmeSorgusu, con);
                        delete.Parameters.AddWithValue("@KATEGORİ", listBox1.Text);
                        delete.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Silindi...");
                        //Silme işlemini gerçekleştirdikten sonra kullanıcıya mesaj verdik.
                    }
                }
                else
                    MessageBox.Show("Kayıt Bulunamadı.");
                con.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void kategoriSil_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand command = new SqlCommand("SELECT id from tblPersonel where kullaniciAdi ='" + GirisPaneli.user + "'");
            SqlDataReader dr1;

            command.Connection = con;
            dr1 = command.ExecuteReader();
            while (dr1.Read())
            {
                label1.Text = dr1["id"].ToString();
            }
            dr1.Close();
            dr1.Dispose();

            cmd = new SqlCommand("select * from notlarKategori where personelID='"+label1.Text+"'", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                listBox1.Items.Add(read["KATEGORİ"]);

            }
            con.Close();
            read.Close();
        }
    }
}
