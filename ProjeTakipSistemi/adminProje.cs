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
    public partial class adminProje : Form
    {
        public adminProje()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OM6H7IM; Initial Catalog=ProjeTakipSistemi;User Id=yunus;password=1234;");
        SqlCommand cmd = new SqlCommand();

        

        private void programHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.Show();
        }

        private void personelİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personelİşlemleri form3 = new Personelİşlemleri();
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                    tblProjeGiris Ekleme
                 */
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand command1 = new SqlCommand("select id from tblPersonel where kullaniciAdi = '" + label101.Text + "'", con);
                int proje = (int)command1.ExecuteScalar();
                string tblProjeGiris = "insert into tblProjeGiris(guncellemeTarihi,projeAd,yuklenici,ihaleTuru,personelID) values (@guncellemeTarihi,@projeAd,@yuklenici,@ihaleTuru,@personelID);Select cast(scope_identity() as int)";
                SqlCommand command = new SqlCommand(tblProjeGiris, con);
                command.Parameters.AddWithValue("@guncellemeTarihi", dateTimePicker9.Value);
                command.Parameters.AddWithValue("@projeAd", isAdiTXT.Text);
                command.Parameters.AddWithValue("@yuklenici", yukleniciTXT.Text);
                command.Parameters.AddWithValue("@ihaleTuru", ihaleTuruCMB.Text);
                command.Parameters.AddWithValue("@personelID", proje);
                command.ExecuteNonQuery();

                SqlCommand command2 = new SqlCommand("select id from tblProjeGiris where projeAd = '" + label6.Text + "'", con);
                int proje1 = (int)command2.ExecuteScalar();



                /*
                    tblKontrolTeskilati Ekleme
                 */

                string tblKontrolTeskilati = "insert into tblKontrolTeskilati(ad,soyad,meslek,pid) values (@ad,@soyad,@meslek,@pid);Select cast(scope_identity() as int)";
                SqlCommand command3 = new SqlCommand(tblKontrolTeskilati, con);
                command3.Parameters.AddWithValue("@ad", textBox55.Text);
                command3.Parameters.AddWithValue("@soyad", textBox54.Text);
                command3.Parameters.AddWithValue("@meslek", comboBox6.Text);
                command3.Parameters.AddWithValue("@pid", proje1);
                command3.ExecuteNonQuery();

                string tblKontrolTeskilati1 = "insert into tblKontrolTeskilati(ad,soyad,meslek,pid) values (@ad,@soyad,@meslek,@pid);Select cast(scope_identity() as int)";
                SqlCommand command4 = new SqlCommand(tblKontrolTeskilati1, con);
                command4.Parameters.AddWithValue("@ad", textBox53.Text);
                command4.Parameters.AddWithValue("@soyad", textBox52.Text);
                command4.Parameters.AddWithValue("@meslek", comboBox2.Text);
                command4.Parameters.AddWithValue("@pid", proje1);
                command4.ExecuteNonQuery();

                string tblKontrolTeskilati2 = "insert into tblKontrolTeskilati(ad,soyad,meslek,pid) values (@ad,@soyad,@meslek,@pid);Select cast(scope_identity() as int)";
                SqlCommand command5 = new SqlCommand(tblKontrolTeskilati2, con);
                command5.Parameters.AddWithValue("@ad", textBox51.Text);
                command5.Parameters.AddWithValue("@soyad", textBox50.Text);
                command5.Parameters.AddWithValue("@meslek", comboBox1.Text);
                command5.Parameters.AddWithValue("@pid", proje1);
                command5.ExecuteNonQuery();

                string tblKontrolTeskilati3 = "insert into tblKontrolTeskilati(ad,soyad,meslek,pid) values (@ad,@soyad,@meslek,@pid);Select cast(scope_identity() as int)";
                SqlCommand command6 = new SqlCommand(tblKontrolTeskilati3, con);
                command6.Parameters.AddWithValue("@ad", textBox57.Text);
                command6.Parameters.AddWithValue("@soyad", textBox56.Text);
                command6.Parameters.AddWithValue("@meslek", comboBox7.Text);
                command6.Parameters.AddWithValue("@pid", proje1);
                command6.ExecuteNonQuery();

                /*
                    tblGorevlendirmeOnayTarihi Ekleme
                 */

                string tblGorevlendirmeOnayTarihi = "insert into tblGorevlendirmeOnayTarihi(gorevlendirmeOnayTarihi,pid) values (@gorevlendirmeOnayTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command7 = new SqlCommand(tblGorevlendirmeOnayTarihi, con);
                command7.Parameters.AddWithValue("@gorevlendirmeOnayTarihi", dateTimePicker1.Value);
                command7.Parameters.AddWithValue("@pid", proje1);
                command7.ExecuteNonQuery();

                /*
                    tblIhaleBilgileri Ekleme
                 */

                string tblIhaleBilgileri = "insert into tblIhaleBilgileri(ihaleKayitNo,ihaleTarihi,pid) values (@ihaleKayitNo,@ihaleTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command8 = new SqlCommand(tblIhaleBilgileri, con);
                command8.Parameters.AddWithValue("@ihaleKayitNo", textBox15.Text);
                command8.Parameters.AddWithValue("@ihaleTarihi", dateTimePicker2.Value);
                command8.Parameters.AddWithValue("@pid", proje1);
                command8.ExecuteNonQuery();

                /*
                    tblIsyeriTeslimTarihi Ekleme
                 */

                string tblIsyeriTeslimTarihi = "insert into tblIsyeriTeslimTarihi(isyeriTeslimTarihi,pid) values (@isyeriTeslimTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command9 = new SqlCommand(tblIsyeriTeslimTarihi, con);
                command9.Parameters.AddWithValue("@isyeriTeslimTarihi", dateTimePicker3.Value);
                command9.Parameters.AddWithValue("@pid", proje1);
                command9.ExecuteNonQuery();

                /*
                    tblOnayliProjeTeslimTarihi Ekleme
                 */

                string tblOnayliProjeTeslimTarihi = "insert into tblOnayliProjeTeslimTarihi(onayliProjeTeslimTarihi,pid) values (@onayliProjeTeslimTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command10 = new SqlCommand(tblOnayliProjeTeslimTarihi, con);
                command10.Parameters.AddWithValue("@onayliProjeTeslimTarihi", dateTimePicker4.Value);
                command10.Parameters.AddWithValue("@pid", proje1);
                command10.ExecuteNonQuery();

                /*
                    tblSozlesmeBilgileri Ekleme
                 */

                string tblSozlesmeBilgileri = "insert into tblSozlesmeBilgileri(sozlesmeTarihi,sozlesmeBedeli,isinSuresi,isBitimTarihi,pid) values (@sozlesmeTarihi,@sozlesmeBedeli,@isinSuresi,@isBitimTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command11 = new SqlCommand(tblSozlesmeBilgileri, con);
                command11.Parameters.AddWithValue("@sozlesmeTarihi", dateTimePicker5.Value);
                command11.Parameters.AddWithValue("@sozlesmeBedeli", textBox16.Text);
                command11.Parameters.AddWithValue("@isinSuresi", textBox17.Text);
                command11.Parameters.AddWithValue("@isBitimTarihi", dateTimePicker6.Value);
                command11.Parameters.AddWithValue("@pid", proje1);
                command11.ExecuteNonQuery();

                /*
                    tblSureUzatimiVeKesifArtisi Ekleme
                 */

                string tblSureUzatimiVeKesifArtisi = "insert into tblSureUzatimiVeKesifArtisi(nedeni,ekSozlesmeBedeli,verilenSure,ekSureliIsBitimtarihi,kesifArtisiTarihi,toplamBedel,yuzde,kdvDahilToplamBedel,pid) values (@nedeni,@ekSozlesmeBedeli,@verilenSure,@ekSureliIsBitimtarihi,@kesifArtisiTarihi,@toplamBedel,@yuzde,@kdvDahilToplamBedel,@pid);Select cast(scope_identity() as int)";
                SqlCommand command12 = new SqlCommand(tblSureUzatimiVeKesifArtisi, con);
                command12.Parameters.AddWithValue("@nedeni", textBox1.Text);
                command12.Parameters.AddWithValue("@ekSozlesmeBedeli", textBox2.Text);
                command12.Parameters.AddWithValue("@verilenSure", textBox3.Text);
                command12.Parameters.AddWithValue("@ekSureliIsBitimTarihi", dateTimePicker25.Value);
                command12.Parameters.AddWithValue("@kesifArtisiTarihi", dateTimePicker26.Value);
                command12.Parameters.AddWithValue("@toplamBedel", textBox4.Text);
                command12.Parameters.AddWithValue("@yuzde", textBox5.Text);
                command12.Parameters.AddWithValue("@kdvDahilToplamBedel", textBox32.Text);
                command12.Parameters.AddWithValue("@pid", proje1);
                command12.ExecuteNonQuery();

                /*
                    tblKesinti Ekleme
                 */

                string tblKesinti = "insert into tblKesinti(elektrik,su,diğer,pid) values (@elektrik,@su,@diğer,@pid);Select cast(scope_identity() as int)";
                SqlCommand command19 = new SqlCommand(tblKesinti, con);
                command19.Parameters.AddWithValue("@elektrik", textBox38.Text);
                command19.Parameters.AddWithValue("@su", textBox37.Text);
                command19.Parameters.AddWithValue("@diğer", textBox34.Text);
                command19.Parameters.AddWithValue("@pid", proje1);
                command19.ExecuteNonQuery();

                /*
                    tblCezalar Ekleme
                 */

                string tblCezalar = "insert into tblCezalar(neden,bedel,pid) values (@neden,@bedel,@pid);Select cast(scope_identity() as int)";
                SqlCommand command15 = new SqlCommand(tblCezalar, con);
                command15.Parameters.AddWithValue("@neden", textBox23.Text);
                command15.Parameters.AddWithValue("@bedel", textBox22.Text);
                command15.Parameters.AddWithValue("@pid", proje1);
                command15.ExecuteNonQuery();

                string tblCezalar1 = "insert into tblCezalar(neden,bedel,pid) values (@neden,@bedel,@pid);Select cast(scope_identity() as int)";
                SqlCommand command17 = new SqlCommand(tblCezalar1, con);
                command17.Parameters.AddWithValue("@neden", textBox25.Text);
                command17.Parameters.AddWithValue("@bedel", textBox24.Text);
                command17.Parameters.AddWithValue("@pid", proje1);
                command17.ExecuteNonQuery();

                string tblCezalar2 = "insert into tblCezalar(neden,bedel,pid) values (@neden,@bedel,@pid);Select cast(scope_identity() as int)";
                SqlCommand command18 = new SqlCommand(tblCezalar2, con);
                command18.Parameters.AddWithValue("@neden", textBox21.Text);
                command18.Parameters.AddWithValue("@bedel", textBox20.Text);
                command18.Parameters.AddWithValue("@pid", proje1);
                command18.ExecuteNonQuery();

                /*
                    tblTSozlesmeGeregi Ekleme
                 */

                string tblTSozlesmeGeregi = "insert into tblTeminatMektubuBilgileriSozlesmeGeregi(sonGecerlilikTarihi,sozlesmeBedeli,pid) values (@sonGecerlilikTarihi,@sozlesmeBedeli,@pid);Select cast(scope_identity() as int)";
                SqlCommand command20 = new SqlCommand(tblTSozlesmeGeregi, con);
                command20.Parameters.AddWithValue("@sonGecerlilikTarihi", dateTimePicker7.Value);
                command20.Parameters.AddWithValue("@sozlesmeBedeli", textBox18.Text);
                command20.Parameters.AddWithValue("@pid", proje1);
                command20.ExecuteNonQuery();

                /*
                    tblTEkSozlesmeGeregi Ekleme
                 */

                string tblTEkSozlesmeGeregi = "insert into tblTeminatMektubuBilgileriEkSozlesmeGeregi(sonGecerlilikTarihi,ilaveSozlesmeBedeli,pid) values (@sonGecerlilikTarihi,@ilaveSozlesmeBedeli,@pid);Select cast(scope_identity() as int)";
                SqlCommand command21 = new SqlCommand(tblTEkSozlesmeGeregi, con);
                command21.Parameters.AddWithValue("@sonGecerlilikTarihi", dateTimePicker8.Value);
                command21.Parameters.AddWithValue("@ilaveSozlesmeBedeli", textBox19.Text);
                command21.Parameters.AddWithValue("@pid", proje1);
                command21.ExecuteNonQuery();

                /*
                    tblAllRiskSozlesmeGeregi Ekleme
                 */

                string tblasg = "insert into tblAllRiskSozlesmeGeregi(sonGecerlilikTarihi,bedel,pid) values (@sonGecerlilikTarihi,@bedel,@pid);Select cast(scope_identity() as int)";
                SqlCommand command22 = new SqlCommand(tblasg, con);
                command22.Parameters.AddWithValue("@sonGecerlilikTarihi", dateTimePicker11.Value);
                command22.Parameters.AddWithValue("@bedel", textBox26.Text);
                command22.Parameters.AddWithValue("@pid", proje1);
                command22.ExecuteNonQuery();

                /*
                    tblAllRiskYuklenicininVerdigi Ekleme
                 */

                string tblayv = "insert into tblAllRiskYuklenicininVerdigi(sonGecerlilikTarihi,bedel,pid) values (@sonGecerlilikTarihi,@bedel,@pid);Select cast(scope_identity() as int)";
                SqlCommand command23 = new SqlCommand(tblayv, con);
                command23.Parameters.AddWithValue("@sonGecerlilikTarihi", dateTimePicker12.Value);
                command23.Parameters.AddWithValue("@bedel", textBox27.Text);
                command23.Parameters.AddWithValue("@pid", proje1);
                command23.ExecuteNonQuery();

                /*
                    tblHakedis Ekleme
                 */

                string hakedis = "insert into tblHakedis(hakedisTarihi,itibarTarihi,bedel,fiyatFarki,toplamHakedis,kdvDahilToplamHakedis,pid) values (@hakedisTarihi,@itibarTarihi,@bedel,@fiyatFarki,@toplamHakedis,@kdvDahilToplamHakedis,@pid);Select cast(scope_identity() as int)";
                SqlCommand command24 = new SqlCommand(hakedis, con);
                command24.Parameters.AddWithValue("@hakedisTarihi", dateTimePicker13.Value);
                command24.Parameters.AddWithValue("@itibarTarihi", dateTimePicker10.Value);
                command24.Parameters.AddWithValue("@bedel", textBox28.Text);
                command24.Parameters.AddWithValue("@fiyatFarki", textBox29.Text);
                command24.Parameters.AddWithValue("@toplamHakedis", textBox30.Text);
                command24.Parameters.AddWithValue("@kdvDahilToplamHakedis", textBox31.Text);
                command24.Parameters.AddWithValue("@pid", proje1);
                command24.ExecuteNonQuery();

                string hakedis1 = "insert into tblHakedis(hakedisTarihi,itibarTarihi,bedel,fiyatFarki,toplamHakedis,kdvDahilToplamHakedis,pid) values (@hakedisTarihi,@itibarTarihi,@bedel,@fiyatFarki,@toplamHakedis,@kdvDahilToplamHakedis,@pid);Select cast(scope_identity() as int)";
                SqlCommand command25 = new SqlCommand(hakedis1, con);
                command25.Parameters.AddWithValue("@hakedisTarihi", dateTimePicker27.Value);
                command25.Parameters.AddWithValue("@itibarTarihi", dateTimePicker14.Value);
                command25.Parameters.AddWithValue("@bedel", textBox43.Text);
                command25.Parameters.AddWithValue("@fiyatFarki", textBox42.Text);
                command25.Parameters.AddWithValue("@toplamHakedis", textBox41.Text);
                command25.Parameters.AddWithValue("@kdvDahilToplamHakedis", textBox40.Text);
                command25.Parameters.AddWithValue("@pid", proje1);
                command25.ExecuteNonQuery();

                string hakedis2 = "insert into tblHakedis(hakedisTarihi,itibarTarihi,bedel,fiyatFarki,toplamHakedis,kdvDahilToplamHakedis,pid) values (@hakedisTarihi,@itibarTarihi,@bedel,@fiyatFarki,@toplamHakedis,@kdvDahilToplamHakedis,@pid);Select cast(scope_identity() as int)";
                SqlCommand command26 = new SqlCommand(hakedis2, con);
                command26.Parameters.AddWithValue("@hakedisTarihi", dateTimePicker29.Value);
                command26.Parameters.AddWithValue("@itibarTarihi", dateTimePicker28.Value);
                command26.Parameters.AddWithValue("@bedel", textBox49.Text);
                command26.Parameters.AddWithValue("@fiyatFarki", textBox48.Text);
                command26.Parameters.AddWithValue("@toplamHakedis", textBox47.Text);
                command26.Parameters.AddWithValue("@kdvDahilToplamHakedis", textBox46.Text);
                command26.Parameters.AddWithValue("@pid", proje1);
                command26.ExecuteNonQuery();

                /*
                    tblIsProgramiBildirimiSozlesmeGeregi Ekleme
                 */

                string tblispbsg = "insert into tblIsProgamiBildirimiSozlesmeGeregi(sozlesmeGeregiSonTarih,YuklenicininBildirdigiTarih,pid) values (@sozlesmeGeregiSonTarih,@YuklenicininBildirdigiTarih,@pid);Select cast(scope_identity() as int)";
                SqlCommand command27 = new SqlCommand(tblispbsg, con);
                command27.Parameters.AddWithValue("@sozlesmeGeregiSonTarih", dateTimePicker16.Value);
                command27.Parameters.AddWithValue("@YuklenicininBildirdigiTarih", dateTimePicker17.Value);
                command27.Parameters.AddWithValue("@pid", proje1);
                command27.ExecuteNonQuery();

                /*
                    tblIsProgramiBildirimiSureUzatimi
                 */

                string tblipbsu = "insert into tblIsProgramiBildirimiSureUzatimi(sozlesmeGeregiSonTarih,yuklenicininBildirdigiTarih,pid) values (@sozlesmeGeregiSonTarih,@yuklenicininBildirdigiTarih,@pid);Select cast(scope_identity() as int)";
                SqlCommand command28 = new SqlCommand(tblipbsu, con);
                command28.Parameters.AddWithValue("@sozlesmeGeregiSonTarih", dateTimePicker15.Value);
                command28.Parameters.AddWithValue("@YuklenicininBildirdigiTarih", dateTimePicker18.Value);
                command28.Parameters.AddWithValue("@pid", proje1);
                command28.ExecuteNonQuery();

                /*
                    tblTeknikPersonelBildirimi Ekleme
                 */

                string tpb = "insert into tblTeknikPersonelBildirimi(sozlesmeGeregiSonTarih,yuklenicininBildirdigiTarih,PersonelDegisimTarihi,YuklenicininBildirdigiPersonelDegisimTarihi,pid) values (@sozlesmeGeregiSonTarih,@yuklenicininBildirdigiTarih,@PersonelDegisimTarihi,@YuklenicininBildirdigiPersonelDegisimTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command29 = new SqlCommand(tpb, con);
                command29.Parameters.AddWithValue("@sozlesmeGeregiSonTarih", dateTimePicker22.Value);
                command29.Parameters.AddWithValue("@yuklenicininBildirdigiTarih", dateTimePicker20.Value);
                command29.Parameters.AddWithValue("@PersonelDegisimTarihi", dateTimePicker21.Value);
                command29.Parameters.AddWithValue("@YuklenicininBildirdigiPersonelDegisimTarihi", dateTimePicker19.Value);
                command29.Parameters.AddWithValue("@pid", proje1);
                command29.ExecuteNonQuery();

                /*
                    tblKabulTarihleri Ekleme
                 */

                string kt = "insert into tblKabulTarihleri(geciciKabulTarihi,kesinKabulTarihi,pid) values (@geciciKabulTarihi,@kesinKabulTarihi,@pid);Select cast(scope_identity() as int)";
                SqlCommand command30 = new SqlCommand(kt, con);
                command30.Parameters.AddWithValue("@geciciKabulTarihi", dateTimePicker24.Value);
                command30.Parameters.AddWithValue("@kesinKabulTarihi", dateTimePicker23.Value);
                command30.Parameters.AddWithValue("@pid", proje1);
                command30.ExecuteNonQuery();

                MessageBox.Show("kaydedildi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void adminProje_Load(object sender, EventArgs e)
        {
            label101.Text = GirisPaneli.user;
        }

        private void isAdiTXT_TextChanged(object sender, EventArgs e)
        {
            label6.Text = isAdiTXT.Text;
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            label5.Text = textBox38.Text;
        }

        private void adminProje_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
