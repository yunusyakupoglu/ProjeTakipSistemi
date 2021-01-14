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
    public partial class Personelİşlemleri : Form
    {
        public Personelİşlemleri()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OM6H7IM; Initial Catalog=ProjeTakipSistemi;User Id=yunus;password=1234;");
        SqlCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;

            //Hataları engellemeye yönelik olarak, tüm veritabanı işlemlerini try-catch blokları arasında yapmaya özen gösterin.
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string Konular = "insert into tblPersonel(AD,SOYAD,kullaniciAdi,parola,ePosta,yetki) values (@AD,@SOYAD,@kullaniciAdi,@parola,@ePosta,@yetki)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand kod = new SqlCommand(Konular, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                kod.Parameters.AddWithValue("@AD", isim.Text);
                kod.Parameters.AddWithValue("@SOYAD", soyisim.Text);
                kod.Parameters.AddWithValue("@kullaniciAdi", kadi.Text);
                kod.Parameters.AddWithValue("@parola", parola.Text);
                kod.Parameters.AddWithValue("@ePosta", eposta.Text);
                kod.Parameters.AddWithValue("@yetki", yetki.Text);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                kod.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Personel Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Form3_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter adp = new SqlDataAdapter("select * from tblPersonel", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Personelİşlemleri frm = new Personelİşlemleri();
            frm.Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                pid.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                isim.Text = dataGridView1.Rows[e.RowIndex].Cells["AD"].FormattedValue.ToString();
                soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["SOYAD"].FormattedValue.ToString();
                kadi.Text = dataGridView1.Rows[e.RowIndex].Cells["kullaniciAdi"].FormattedValue.ToString();
                parola.Text = dataGridView1.Rows[e.RowIndex].Cells["parola"].FormattedValue.ToString();
                eposta.Text = dataGridView1.Rows[e.RowIndex].Cells["ePosta"].FormattedValue.ToString();
                yetki.Text = dataGridView1.Rows[e.RowIndex].Cells["yetki"].FormattedValue.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("DELETE from tblPersonel where id = @id", con);
            cmd.Parameters.AddWithValue("@id", pid.Text);

            try
            {
                DialogResult durum = MessageBox.Show("Personel kaydını silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

                if (DialogResult.Yes == durum)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt silinemedi.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;


            //Hataları engellemeye yönelik olarak, tüm veritabanı işlemlerini try-catch blokları arasında yapmaya özen gösterin.
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string Konular = "update tblPersonel set AD=@AD,SOYAD=@SOYAD,kullaniciAdi=@kullaniciAdi,parola=@parola,ePosta=@ePosta,yetki=@yetki where id='" + pid.Text + "'";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand kod = new SqlCommand(Konular, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                kod.Parameters.AddWithValue("@AD", isim.Text);
                kod.Parameters.AddWithValue("@SOYAD", soyisim.Text);
                kod.Parameters.AddWithValue("@kullaniciAdi", kadi.Text);
                kod.Parameters.AddWithValue("@parola", parola.Text);
                kod.Parameters.AddWithValue("@ePosta", eposta.Text);
                kod.Parameters.AddWithValue("@yetki", yetki.Text);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                kod.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Personel Kaydı Güncellendi.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
    }
}
