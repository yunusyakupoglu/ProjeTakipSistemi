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
    public partial class List : Form
    {
        Label kullanicid1 = new Label();
        public List()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
            SqlCommand cmd;
            SqlDataReader dr;
            cmd = new SqlCommand("SELECT * FROM tblPersonel", con);
            con.Open();
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["kullaniciAdi"].ToString() == GirisPaneli.user && dr["parola"].ToString() == GirisPaneli.pass && dr["yetki"].ToString() == "Admin")
                {
                    panel1.BackgroundImage = Properties.Resources._1;
                    label13.Text = "A D M İ N";
                }
                else if (dr["kullaniciAdi"].ToString() == GirisPaneli.user && dr["parola"].ToString() == GirisPaneli.pass && dr["yetki"].ToString() == "Üye")
                {
                    panel1.BackgroundImage = Properties.Resources._2;
                    label13.Text = "Ü Y E";
                    
                    kullanicid1.Name = "labellllll";
                    kullanicid1.Text = "labellllll";

                    Point labelKonum = new Point(407, 60);
                    kullanicid1.Location = labelKonum;
                    kullanicid1.TextChanged += kullanicid1_TextChanged;
                    kullanicid1.Visible = false;
                    this.Controls.Add(kullanicid1);
                }
            }
            con.Close();
            con.Dispose();
        }

        private void kullanicid1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "CONVERT(personelID, 'System.String')LIKE '" + kullanicid1.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ProjeTakipSistemi;Integrated Security=true");
        SqlDataReader dr;

        private void button3_Click(object sender, EventArgs e)
        {
            İcerik admin = new İcerik();
            admin.btnGuncelle.Enabled = false;
            admin.btnGuncelle.Visible = false;
            admin.Show();
        }

        DataTable dt;
        private void uyeList_Load(object sender, EventArgs e)
        {
            con.Open();

            SqlDataAdapter adp = new SqlDataAdapter("select * from tblProjeGiris", con);
            dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[5].Visible = false;

            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 10);

            label1.Text = GirisPaneli.user;

            SqlCommand command = new SqlCommand("SELECT id from tblPersonel where kullaniciAdi ='" + GirisPaneli.user + "'");

            command.Connection = con;

            dr = command.ExecuteReader();

            while (dr.Read())
            {
                kullanicid.Text = dr["id"].ToString();
                kullanicid1.Text = dr["id"].ToString();
            }
            dr.Close();
            dr.Dispose();

        }

        private void uyeList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                DialogResult durum = MessageBox.Show("Proje kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                //Kullanıcıya silme onayı penceresi açıp, verdiği cevabı durum değişkenine aktardık.
                if (DialogResult.Yes == durum) // Eğer kullanıcı Evet seçeneğini seçmişse, veritabanından kaydı silecek kodlar çalışır.
                {
                    SqlCommand command = new SqlCommand("delete from tblProjeGiris where id ='" + dataGridView1.SelectedRows[i].Cells["id"].Value.ToString() + "'", con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            İcerik proje = new İcerik();
            proje.btnKydt.Enabled = false;
            proje.btnKydt.Visible = false;
            proje.isAdiTXT.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            /*
                tblProjeGiris Çekme
             */

            SqlCommand command = new SqlCommand("SELECT * from tblProjeGiris where id ='" + dataGridView1. CurrentRow.Cells["id"].Value.ToString() + "'");

            command.Connection = con;

            dr = command.ExecuteReader();

            while (dr.Read())
            {
                proje.ihaleTuruCMB.Text = dr["ihaleTuru"].ToString();
                proje.yukleniciTXT.Text = dr["yuklenici"].ToString();
                proje.label9.Text = dr["id"].ToString();
            }
            dr.Close();
            dr.Dispose();
            

            /*
                tblKontrolTeskilati Çekme
             */

            SqlCommand command1 = new SqlCommand("Select * from tblKontrolTeskilati1 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader kt;
            command1.Connection = con;
            kt = command1.ExecuteReader();
            while (kt.Read())
            {
                proje.k1AD.Text = kt["ad"].ToString();
                proje.k1SOYAD.Text = kt["soyad"].ToString();
                proje.comboBox1.Text = kt["meslek"].ToString();
            }
            kt.Close();
            kt.Dispose();

            SqlCommand command2 = new SqlCommand("Select * from tblKontrolTeskilati2 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader kt1;
            command2.Connection = con;
            kt1 = command2.ExecuteReader();
            while (kt1.Read())
            {
                proje.textBox8.Text = kt1["ad"].ToString();
                proje.textBox6.Text = kt1["soyad"].ToString();
                proje.comboBox3.Text = kt1["meslek"].ToString();
            }
            kt1.Close();
            kt1.Dispose();

            SqlCommand command3 = new SqlCommand("Select * from tblKontrolTeskilati3 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader kt2;
            command3.Connection = con;
            kt2 = command3.ExecuteReader();
            while (kt2.Read())
            {
                proje.textBox11.Text = kt2["ad"].ToString();
                proje.textBox9.Text = kt2["soyad"].ToString();
                proje.comboBox4.Text = kt2["meslek"].ToString();
            }
            kt2.Close();
            kt2.Dispose();

            SqlCommand command4 = new SqlCommand("Select * from tblKontrolTeskilati4 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader kt3;
            command4.Connection = con;
            kt3 = command4.ExecuteReader();
            while (kt3.Read())
            {
                proje.textBox14.Text = kt3["ad"].ToString();
                proje.textBox12.Text = kt3["soyad"].ToString();
                proje.comboBox5.Text = kt3["meslek"].ToString();
            }
            kt3.Close();
            kt3.Dispose();

            SqlCommand command5 = new SqlCommand("Select * from tblGorevlendirmeOnayTarihi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader got;
            command5.Connection = con;
            got = command5.ExecuteReader();
            while (got.Read())
            {
                proje.dateTimePicker1.Text = got["gorevlendirmeOnayTarihi"].ToString();
            }
            got.Close();
            got.Dispose();

            SqlCommand command6 = new SqlCommand("Select * from tblIhaleBilgileri where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader ib;
            command6.Connection = con;
            ib = command6.ExecuteReader();
            while (ib.Read())
            {
                proje.textBox15.Text = ib["ihaleKayitNo"].ToString();
                proje.dateTimePicker2.Text = ib["ihaleTarihi"].ToString();
            }
            ib.Close();
            ib.Dispose();

            SqlCommand command7 = new SqlCommand("Select * from tblIsyeriTeslimTarihi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader itt;
            command7.Connection = con;
            itt = command7.ExecuteReader();
            while (itt.Read())
            {
                proje.dateTimePicker3.Text = itt["isyeriTeslimTarihi"].ToString();
            }
            itt.Close();
            itt.Dispose();

            SqlCommand command8 = new SqlCommand("Select * from tblOnayliProjeTeslimTarihi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader opt;
            command8.Connection = con;
            opt = command8.ExecuteReader();
            while (opt.Read())
            {
                proje.dateTimePicker4.Text = opt["onayliProjeTeslimTarihi"].ToString();
            }
            opt.Close();
            opt.Dispose();

            SqlCommand command9 = new SqlCommand("Select * from tblSozlesmeBilgileri where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader sb;
            command9.Connection = con;
            sb = command9.ExecuteReader();
            while (sb.Read())
            {
                proje.dateTimePicker5.Text = sb["sozlesmeTarihi"].ToString();
                proje.textBox16.Text = sb["sozlesmeBedeli"].ToString();
                proje.textBox17.Text = sb["isinSuresi"].ToString();
                proje.dateTimePicker6.Text = sb["isBitimTarihi"].ToString();
            }
            sb.Close();
            sb.Dispose();

            SqlCommand command10 = new SqlCommand("Select * from tblSureUzatimiveKesifArtisi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader su;
            command10.Connection = con;
            su = command10.ExecuteReader();
            while (su.Read())
            {
                proje.textBox1.Text = su["nedeni"].ToString();
                proje.textBox2.Text = su["ekSozlesmeBedeli"].ToString();
                proje.textBox3.Text = su["verilenSure"].ToString();
                proje.dateTimePicker25.Text = su["ekSureliIsBitimTarihi"].ToString();
                proje.dateTimePicker26.Text = su["kesifArtisiTarihi"].ToString();
                proje.textBox4.Text = su["toplamBedel"].ToString();
                proje.textBox5.Text = su["yuzde"].ToString();
                proje.textBox32.Text = su["kdvDahilToplamBedel"].ToString();
            }
            su.Close();
            su.Dispose();

            SqlCommand command11 = new SqlCommand("Select * from tblCezalar1 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader c1;
            command11.Connection = con;
            c1 = command11.ExecuteReader();
            while (c1.Read())
            {
                proje.textBox23.Text = c1["neden"].ToString();
                proje.textBox22.Text = c1["bedel"].ToString();
            }
            c1.Close();
            c1.Dispose();

            SqlCommand command12 = new SqlCommand("Select * from tblCezalar2 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader c2;
            command12.Connection = con;
            c2 = command12.ExecuteReader();
            while (c2.Read())
            {
                proje.textBox25.Text = c2["neden"].ToString();
                proje.textBox24.Text = c2["bedel"].ToString();
            }
            c2.Close();
            c2.Dispose();

            SqlCommand command13 = new SqlCommand("Select * from tblCezalar3 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader c3;
            command13.Connection = con;
            c3 = command13.ExecuteReader();
            while (c3.Read())
            {
                proje.textBox21.Text = c3["neden"].ToString();
                proje.textBox20.Text = c3["bedel"].ToString();
            }
            c3.Close();
            c3.Dispose();

            SqlCommand command14 = new SqlCommand("Select * from tblKesinti where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader ksnt;
            command14.Connection = con;
            ksnt = command14.ExecuteReader();
            while (ksnt.Read())
            {
                proje.textBox38.Text = ksnt["elektrik"].ToString();
                proje.textBox37.Text = ksnt["su"].ToString();
                proje.textBox34.Text = ksnt["diğer"].ToString();
            }
            ksnt.Close();
            ksnt.Dispose();

            SqlCommand command15 = new SqlCommand("Select * from tblTeminatMektubuBilgileriSozlesmeGeregi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader tsg;
            command15.Connection = con;
            tsg = command15.ExecuteReader();
            while (tsg.Read())
            {
                proje.dateTimePicker7.Text = tsg["sonGecerlilikTarihi"].ToString();
                proje.textBox18.Text = tsg["sozlesmeBedeli"].ToString();
            }
            tsg.Close();
            tsg.Dispose();

            SqlCommand command16 = new SqlCommand("Select * from tblTeminatMektubuBilgileriEkSozlesmeGeregi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader teg;
            command16.Connection = con;
            teg = command16.ExecuteReader();
            while (teg.Read())
            {
                proje.dateTimePicker8.Text = teg["sonGecerlilikTarihi"].ToString();
                proje.textBox19.Text = teg["ilaveSozlesmeBedeli"].ToString();
            }
            teg.Close();
            teg.Dispose();

            SqlCommand command17 = new SqlCommand("Select * from tblAllRiskSozlesmeGeregi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader tasg;
            command17.Connection = con;
            tasg = command17.ExecuteReader();
            while (tasg.Read())
            {
                proje.dateTimePicker11.Text = tasg["sonGecerlilikTarihi"].ToString();
                proje.textBox26.Text = tasg["bedel"].ToString();
            }
            tasg.Close();
            tasg.Dispose();

            SqlCommand command18 = new SqlCommand("Select * from tblAllRiskYuklenicininVerdigi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader tayv;
            command18.Connection = con;
            tayv = command18.ExecuteReader();
            while (tayv.Read())
            {
                proje.dateTimePicker12.Text = tayv["sonGecerlilikTarihi"].ToString();
                proje.textBox27.Text = tayv["bedel"].ToString();
            }
            tayv.Close();
            tayv.Dispose();

            SqlCommand command19 = new SqlCommand("Select * from tblHakedis1 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader h1;
            command19.Connection = con;
            h1 = command19.ExecuteReader();
            while (h1.Read())
            {
                proje.dateTimePicker13.Text = h1["hakedisTarihi"].ToString();
                proje.dateTimePicker10.Text = h1["itibarTarihi"].ToString();
                proje.textBox28.Text = h1["bedel"].ToString();
                proje.textBox29.Text = h1["fiyatFarki"].ToString();
                proje.textBox30.Text = h1["toplamHakedis"].ToString();
                proje.textBox31.Text = h1["kdvDahiltoplamHakedis"].ToString();
            }
            h1.Close();
            h1.Dispose();

            SqlCommand command20 = new SqlCommand("Select * from tblHakedis2 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader h2;
            command20.Connection = con;
            h2 = command20.ExecuteReader();
            while (h2.Read())
            {
                proje.dateTimePicker27.Text = h2["hakedisTarihi"].ToString();
                proje.dateTimePicker14.Text = h2["itibarTarihi"].ToString();
                proje.textBox43.Text = h2["bedel"].ToString();
                proje.textBox42.Text = h2["fiyatFarki"].ToString();
                proje.textBox41.Text = h2["toplamHakedis"].ToString();
                proje.textBox40.Text = h2["kdvDahiltoplamHakedis"].ToString();
            }
            h2.Close();
            h2.Dispose();

            SqlCommand command21 = new SqlCommand("Select * from tblHakedis3 where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader h3;
            command21.Connection = con;
            h3 = command19.ExecuteReader();
            while (h3.Read())
            {
                proje.dateTimePicker29.Text = h3["hakedisTarihi"].ToString();
                proje.dateTimePicker28.Text = h3["itibarTarihi"].ToString();
                proje.textBox49.Text = h3["bedel"].ToString();
                proje.textBox48.Text = h3["fiyatFarki"].ToString();
                proje.textBox47.Text = h3["toplamHakedis"].ToString();
                proje.textBox46.Text = h3["kdvDahiltoplamHakedis"].ToString();
            }
            h3.Close();
            h3.Dispose();

            SqlCommand command22 = new SqlCommand("Select * from tblIsProgamiBildirimiSozlesmeGeregi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader isg;
            command22.Connection = con;
            isg = command22.ExecuteReader();
            while (isg.Read())
            {
                proje.dateTimePicker16.Text = isg["sozlesmeGeregiSonTarih"].ToString();
                proje.dateTimePicker17.Text = isg["YuklenicininBildirdigiTarih"].ToString();
            }
            isg.Close();
            isg.Dispose();

            SqlCommand command23 = new SqlCommand("Select * from tblIsProgramiBildirimiSureUzatimi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader isu;
            command23.Connection = con;
            isu = command23.ExecuteReader();
            while (isu.Read())
            {
                proje.dateTimePicker15.Text = isu["sozlesmeGeregiSonTarih"].ToString();
                proje.dateTimePicker18.Text = isu["yuklenicininBildirdigiTarih"].ToString();
            }
            isu.Close();
            isu.Dispose();

            SqlCommand command24 = new SqlCommand("Select * from tblTeknikPersonelBildirimi where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader tpb;
            command24.Connection = con;
            tpb = command24.ExecuteReader();
            while (tpb.Read())
            {
                proje.dateTimePicker22.Text = tpb["sozlesmeGeregiSonTarih"].ToString();
                proje.dateTimePicker20.Text = tpb["YuklenicininBildirdigiTarih"].ToString();
                proje.dateTimePicker21.Text = tpb["PersonelDegisimTarihi"].ToString();
                proje.dateTimePicker19.Text = tpb["YuklenicininBildirdigiPersonelDegisimTarihi"].ToString();
            }
            tpb.Close();
            tpb.Dispose();

            SqlCommand command25 = new SqlCommand("Select * from tblKabulTarihleri where pid ='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'");
            SqlDataReader kbl;
            command25.Connection = con;
            kbl = command25.ExecuteReader();
            while (kbl.Read())
            {
                proje.dateTimePicker24.Text = kbl["geciciKabulTarihi"].ToString();
                proje.dateTimePicker23.Text = kbl["kesinKabulTarihi"].ToString();
            }
            kbl.Close();
            kbl.Dispose();


            con.Close();


            proje.Show();
        }

        
    }
}
