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
    public partial class Notlarım : Form
    {

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
        SqlCommand cmd;
        public Notlarım()
        {
            InitializeComponent();
        }

        private void Notlarım_Load(object sender, EventArgs e)
        {
            //label4.Text = GirisPaneli.user;
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

            string strSQL = string.Format("select * from notlarKategori where personelID='"+label4.Text+"' ");
            SqlDataAdapter adp = new SqlDataAdapter(strSQL, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][1].ToString());
            }
            con.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            kategoriEkle kategori = new kategoriEkle();
            kategori.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kategoriSil sil = new kategoriSil();
            sil.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notEkle ekle = new notEkle();
            ekle.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string strSQL = string.Format("select * from notlar where KATEGORİ='{0}'", comboBox1.SelectedItem);
            SqlDataAdapter adp = new SqlDataAdapter(strSQL, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i][1].ToString());
            }
            con.Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            con.Open();
            string strSQL = string.Format("select * from notlar where BAŞLIK='{0}'", listBox1.SelectedItem);
            SqlDataAdapter adp = new SqlDataAdapter(strSQL, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            richTextBox1.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                richTextBox1.AppendText(dt.Rows[i][2].ToString());
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string strSQL = "update notlar set BAŞLIK=@BAŞLIK,KATEGORİ=@KATEGORİ,İÇERİK=@İÇERİK where BAŞLIK=@BAŞLIK";
                // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                SqlCommand komut = new SqlCommand(strSQL, con);
                //Sorgumuzu ve conmizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@BAŞLIK", listBox1.Text);
                komut.Parameters.AddWithValue("@İÇERİK", richTextBox1.Text);
                komut.Parameters.AddWithValue("@KATEGORİ", comboBox1.Text);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Not Bilgileri Güncellendi.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
            cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                con.Open();
                string secmeSorgusu = "SELECT * from notlar where BAŞLIK=@BAŞLIK";
                SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, con);
                secmeKomutu.Parameters.AddWithValue("@BAŞLIK", listBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);
                SqlDataReader dr = secmeKomutu.ExecuteReader();
                if (dr.Read()) //Datareader herhangi bir okuma yapabiliyorsa aşağıdaki kodlar çalışır.
                {
                    string baslik = dr["BAŞLIK"].ToString();
                    dr.Close();
                    //Datareader ile okunan müşteri ad ve soyadını isim değişkenine atadım.
                    //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
                    DialogResult durum = MessageBox.Show(baslik + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                    //Kullanıcıya silme onayı penceresi açıp, verdiği cevabı durum değişkenine aktardık.
                    if (DialogResult.Yes == durum) // Eğer kullanıcı Evet seçeneğini seçmişse, veritabanından kaydı silecek kodlar çalışır.
                    {
                        string silmeSorgusu = "DELETE from notlar where BAŞLIK=@BAŞLIK";
                        //musterino parametresine bağlı olarak müşteri kaydını silen sql sorgusu
                        SqlCommand delete = new SqlCommand(silmeSorgusu, con);
                        delete.Parameters.AddWithValue("@BAŞLIK", listBox1.Text);
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
    }
}
