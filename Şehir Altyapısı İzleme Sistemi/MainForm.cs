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
using System.Net.Mail;

namespace SehirAltyapiIzlemeSistemi
{
    public partial class MainForm : Form
    {
        public static string conString = @"Data Source=ENES;Initial Catalog = VTYSProjeFinal; Integrated Security = True";
        SqlConnection connection = new SqlConnection(MainForm.conString);

        int mov;
        int movX;
        int movY;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }

        private void panelLoginButton_Click(object sender, EventArgs e)
        {
            string sirket_isim = "";
            string sirket_sifre = "";
            string sirket_hizmet = "";
            connection.Open();
            string sql = "EXEC SirketSec " + usernameTextBox.Text;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                sirket_isim = data.GetValue(1).ToString().Trim();
                sirket_hizmet = data.GetValue(2).ToString().Trim();
                sirket_sifre = data.GetValue(4).ToString().Trim();
            }
            data.Close();

            string admin_name = "";
            string admin_password = "";
            string sql2 = "EXEC AdminSec " + usernameTextBox.Text;
            SqlCommand command2 = new SqlCommand(sql2, connection);
            SqlDataReader data2 = command2.ExecuteReader();

            while (data2.Read())
            {
                admin_name = data2.GetValue(0).ToString().Trim();
                admin_password = data2.GetValue(1).ToString().Trim();
            }
            data.Close();
            connection.Close();

            if (passwordTextBox.Text == admin_password)
            {
                AdminForm adminform = new AdminForm();
                adminform.adminName(admin_name);
                adminform.Show();
            }
            else if (passwordTextBox.Text == sirket_sifre)
            {
                SirketForm sirketform = new SirketForm();
                sirketform.sirketIsmi(sirket_isim);
                if (sirket_hizmet == "Elektrik")
                {
                    sirketform.elektrikBringToFront();
                    sirketform.fillElektrikList();
                }
                else if (sirket_hizmet == "Asansör")
                {
                    sirketform.asansorBringToFront();
                    sirketform.fillAsansorList();
                }
                else if (sirket_hizmet == "Su")
                {
                    sirketform.suBringToFront();
                    sirketform.fillSuList();
                }
                else if (sirket_hizmet == "Doğalgaz")
                {
                    sirketform.dogalGazBringToFront();
                    sirketform.fillDogalGazList();
                }   
                else if (sirket_hizmet == "İnternet")
                {
                    sirketform.internetBringToFront();
                    sirketform.fillInternetList();
                }
                sirketform.Show();
            }
            else
            {
                messageLabel.ForeColor = Color.Red;
                messageLabel.Text = "Incorrect Username or Password";
            }
        }

        private void usernameTextBox_Enter(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "Username")
                usernameTextBox.Text = "";
        }

        private void usernameTextBox_Leave(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "")
                usernameTextBox.Text = "Username";
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "Password")
                passwordTextBox.Text = "";
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
                passwordTextBox.Text = "Password";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sorgulamaButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = sorgulamaButton.Height;
            sidePanel.Top = sorgulamaButton.Top;
            adresPanel.BringToFront();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = loginButton.Height;
            sidePanel.Top = loginButton.Top;
            loginPanel.BringToFront();
        }

        private void ilComboBox_Enter(object sender, EventArgs e)
        {
            ilComboBox.Items.Clear();
            connection.Open();
            string sql = "EXEC İlSec";
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataReader data = cmd2.ExecuteReader();

            while (data.Read())
            {
                ilComboBox.Items.Add(data.GetValue(0).ToString().Trim());
            }
            data.Close();
            connection.Close();
        }

        private void ilceComboBox_Enter(object sender, EventArgs e)
        {
            if (ilComboBox.Text != "")
            {
                ilceComboBox.Items.Clear();
                connection.Open();
                string sql = "EXEC İlceSec " + ilComboBox.SelectedItem;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    ilceComboBox.Items.Add(data.GetValue(0).ToString().Trim());
                }
                data.Close();
                connection.Close();
            }   
        }

        private void mahalleComboBox_Enter(object sender, EventArgs e)
        {
            if (ilceComboBox.Text != "" && ilComboBox.Text != "")
            {
                mahalleComboBox.Items.Clear();
                connection.Open();
                string sql = "EXEC MahalleSec " + ilComboBox.SelectedItem + "," + ilceComboBox.SelectedItem;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    mahalleComboBox.Items.Add(data.GetValue(0).ToString().Trim());
                }
                data.Close();
                connection.Close();
            }    
        }

        private void sokakComboBox_Enter(object sender, EventArgs e)
        {
            if (mahalleComboBox.Text != "" && ilceComboBox.Text != "" && ilComboBox.Text != "")
            {
                sokakComboBox.Items.Clear();
                connection.Open();
                string sql = "EXEC SokakSec " + ilComboBox.SelectedItem + "," +
                    ilceComboBox.SelectedItem + "," + mahalleComboBox.SelectedItem;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    sokakComboBox.Items.Add(data.GetValue(0).ToString().Trim());
                }
                data.Close();
                connection.Close();
            }   
        }

        private void ilComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceComboBox.Text = "";
            mahalleComboBox.Text = "";
            sokakComboBox.Text = "";
            binaComboBox.Text = "";
            daireComboBox.Text = "";
        }

        private void ilceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mahalleComboBox.Text = "";
            sokakComboBox.Text = "";
            binaComboBox.Text = "";
            daireComboBox.Text = "";
        }

        private void mahalleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sokakComboBox.Text = "";
            binaComboBox.Text = "";
            daireComboBox.Text = "";
        }

        private void sokakComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            binaComboBox.Text = "";
            daireComboBox.Text = "";
        }

        private void binaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            daireComboBox.Text = "";
        }

        private void binaComboBox_Enter(object sender, EventArgs e)
        {
            if (sokakComboBox.Text != "" && mahalleComboBox.Text != "" && ilceComboBox.Text != "" && ilComboBox.Text != "")
            {
                binaComboBox.Items.Clear();
                connection.Open();
                string sql = "EXEC BinaSec " + ilComboBox.SelectedItem + "," + ilceComboBox.SelectedItem +
                    "," + mahalleComboBox.SelectedItem + "," + sokakComboBox.SelectedItem;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    binaComboBox.Items.Add(data.GetValue(0).ToString().Trim());
                }
                data.Close();
                connection.Close();
            }    
        }

        private void daireComboBox_Enter(object sender, EventArgs e)
        {
            if (binaComboBox.Text != "" && sokakComboBox.Text != "" && mahalleComboBox.Text != "" && ilceComboBox.Text != "" && ilComboBox.Text != "")
            {
                daireComboBox.Items.Clear();
                connection.Open();
                string sql = "EXEC DaireSayısıSorgula " + ilComboBox.SelectedItem + "," + ilceComboBox.SelectedItem +
                    "," + mahalleComboBox.SelectedItem + "," + sokakComboBox.SelectedItem + "," + binaComboBox.SelectedItem;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    for (int i = 0; i < Int16.Parse(data.GetValue(0).ToString()); i++)
                        daireComboBox.Items.Add(i + 1).ToString();
                }
                data.Close();
                connection.Close();
            }   
        }

        private void clearAndSize()
        {
            asansorLabel.Text = "";
            enerjiKimligiLabel.Text = "";
            yanginMerdivenLabel.Text = "";
            elektrikListBox.Items.Clear();
            dogalGazListBox.Items.Clear();
            suListBox.Items.Clear();
            internetListBox.Items.Clear();
            elektrikGroupBox.Size = new System.Drawing.Size(509, 100);
            dogalGazGroupBox.Size = new System.Drawing.Size(509, 100);
            suGroupBox.Size = new System.Drawing.Size(509, 100);
            internetGroupBox.Size = new System.Drawing.Size(509, 131);
        }

        private void sorgulaButton_Click(object sender, EventArgs e)
        {
            clearAndSize();
            if (binaComboBox.Text != "" && sokakComboBox.Text != "" && mahalleComboBox.Text != ""
                && ilceComboBox.Text != "" && ilComboBox.Text != "")
            {
                string AdresID = "";
                connection.Open();
                string sql = "EXEC AdresIDSec " + ilComboBox.SelectedItem + "," + ilceComboBox.SelectedItem +
                    "," + mahalleComboBox.SelectedItem + "," + sokakComboBox.SelectedItem;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader data = command.ExecuteReader();
                while (data.Read())
                    AdresID = data.GetValue(0).ToString();
                data.Close();
                connection.Close();
                connection.Open();
                sql = string.Format("EXEC BinaElektrikSorgula " + AdresID + "," + binaComboBox.SelectedItem);
                SqlCommand cmd = new SqlCommand(sql, connection);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    elektrikListBox.Items.Add("Sağlayıcı: " + data.GetValue(1).ToString().Trim());
                    elektrikListBox.Items.Add("Altyapı Numarası: #" + data.GetValue(0).ToString().Trim());
                    elektrikListBox.Items.Add("-----------------------------------------------------");
                }
                data.Close();
                connection.Close();
                connection.Open();
                sql = string.Format("EXEC BinaSuSorgula " + AdresID + "," + binaComboBox.SelectedItem);
                cmd = new SqlCommand(sql, connection);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    suListBox.Items.Add("Sağlayıcı: " + data.GetValue(1).ToString().Trim());
                    suListBox.Items.Add("Altyapı Numarası: #" + data.GetValue(0).ToString().Trim());
                    suListBox.Items.Add("-----------------------------------------------------");
                }
                data.Close();
                connection.Close();

                connection.Open();
                sql = string.Format("EXEC BinaDogalgazSorgula " + AdresID + "," + binaComboBox.SelectedItem);
                cmd = new SqlCommand(sql, connection);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    dogalGazListBox.Items.Add("Sağlayıcı: " + data.GetValue(1).ToString().Trim());
                    dogalGazListBox.Items.Add("Altyapı Numarası: #" + data.GetValue(0).ToString().Trim());
                    dogalGazListBox.Items.Add("-----------------------------------------------------");
                }
                data.Close();
                connection.Close();

                connection.Open();
                sql = string.Format("EXEC BinaInternetSorgula " + AdresID + "," + binaComboBox.SelectedItem);
                cmd = new SqlCommand(sql, connection);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    internetListBox.Items.Add("Sağlayıcı: " + data.GetValue(1).ToString().Trim());
                    internetListBox.Items.Add("Altyapı Numarası: #" + data.GetValue(0).ToString().Trim());
                    internetListBox.Items.Add("Altyapı Tipi: " + data.GetValue(2).ToString().Trim());
                    internetListBox.Items.Add("-----------------------------------------------------");
                }
                data.Close();
                connection.Close();

                connection.Open();
                sql = string.Format("EXEC BinaOzellikleriSorgula " + AdresID + "," + binaComboBox.SelectedItem);
                cmd = new SqlCommand(sql, connection);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    yanginMerdivenLabel.Text = data.GetValue(0).ToString().Trim();
                    enerjiKimligiLabel.Text = data.GetValue(1).ToString().Trim();
                }
                if (yanginMerdivenLabel.Text == "" || enerjiKimligiLabel.Text == "")
                {
                    if (yanginMerdivenLabel.Text == "" && enerjiKimligiLabel.Text == "")
                    {
                        yanginMerdivenLabel.Text = "Yok";
                        enerjiKimligiLabel.Text = "Yok";
                    }
                    else if (yanginMerdivenLabel.Text == "")
                        yanginMerdivenLabel.Text = "Yok";
                    else
                        enerjiKimligiLabel.Text = "Yok";
                }
                data.Close();
                connection.Close();

                connection.Open();
                sql = string.Format("Select * FROM  asansorvaryokfonksiyonu(' " + AdresID + "','" + binaComboBox.SelectedItem) + "')";
               
                cmd = new SqlCommand(sql, connection);
                data = cmd.ExecuteReader();
                while (data.Read())
                asansorLabel.Text = data.GetValue(0).ToString();
                data.Close();
                connection.Close();
            }
        }

        private void parolaButon_Click(object sender, EventArgs e)
        {
            string sirket_mail = "";
            string sirket_isim = "";
            string sirket_hizmet = "";
            string sirket_sifre = "";
            string sirket_sicilno = "";
            connection.Open();
            string sql = "EXEC SirketSec " + usernameTextBox.Text;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {

                sirket_sicilno = data.GetValue(0).ToString().Trim();
                sirket_isim = data.GetValue(1).ToString().Trim();
                sirket_hizmet = data.GetValue(2).ToString().Trim();
                sirket_mail = data.GetValue(3).ToString().Trim();
                sirket_sifre = data.GetValue(4).ToString().Trim();
            }
            data.Close();
            connection.Close();

            if (sirket_mail == "")
            {
                MessageBox.Show("Kullanıcı bulunamadı. Lütfen kullanıcı adını kontrol ediniz.");
                return;
            }

            int length = 8;
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            string yenisifre = str_build.ToString();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("ooplab2_23@outlook.com", "BoburshohEnesMurat");
            MailMessage eposta = new MailMessage();
            eposta.From = new MailAddress("ooplab2_23@outlook.com");
            eposta.To.Add(sirket_mail);
            eposta.Subject = "Şehir Altyapısı Şifre Servisi";
            eposta.Body = "Yeni şifreniz:" + yenisifre;

            int sent = 0;

            try
            {
                smtp.Send(eposta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderilirken bir sorun oluştu." + ex.Message);
                sent = -1;
            }

            if (sent == 0)
            {
                MessageBox.Show("Şifreniz gönderildi. Mail adresinizi kontrol ediniz.");
                try
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        string sql1 = "EXEC SirketGuncelle '" +
                            sirket_sicilno + "', '" +
                            sirket_isim + "', '" +
                            sirket_hizmet + "', '" +
                            sirket_mail + "', '" +
                            yenisifre + "'";
                        SqlCommand command1 = new SqlCommand(sql1, connection);
                        command1.ExecuteNonQuery();
                    }
                    MessageBox.Show("Sifre Güncellendi.");
                    connection.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Şifre güncellenirken bir sorun oluştu. Lütfen tekrar deneyiniz.");
                    connection.Close();
                }
            }
        }

        private void moveformPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void moveformPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void moveformPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }
    }
}