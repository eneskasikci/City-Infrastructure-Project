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


namespace SehirAltyapiIzlemeSistemi
{
    public partial class AdminForm : Form
    {
        SqlConnection connection = new SqlConnection(MainForm.conString);
        int mov;
        int movX;
        int movY;
        public AdminForm()
        {
            InitializeComponent();
            fillAdresList();
        }
        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }
        public void adminName(string str)
        {
            adminNameLabel.Text = str;
        }

        private void fillAdresList()
        {
            adresListView.Items.Clear();
            connection.Open();
            string sql = "EXEC AdresListele";
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataReader data = cmd2.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().TrimEnd(), 0);
                item.SubItems.Add(data.GetValue(1).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(2).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(3).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(4).ToString().TrimEnd());
                adresListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void fillBinaList()
        {
            binaListView.Items.Clear();
            connection.Open();
            string sql = "EXEC BinaListele";
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataReader data = cmd2.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().TrimEnd(), 0);
                item.SubItems.Add(data.GetValue(1).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(2).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(3).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(4).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(5).ToString().TrimEnd());
                binaListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void fillSirketList()
        {
            sirketListView.Items.Clear();
            connection.Open();
            string sql = "EXEC SirketListele";
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataReader data = cmd2.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().TrimEnd(), 0);
                item.SubItems.Add(data.GetValue(1).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(2).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(3).ToString().TrimEnd());
                item.SubItems.Add(data.GetValue(4).ToString().TrimEnd());
                sirketListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sirketEkleButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = sirketEkleButton.Height;
            sidePanel.Top = sirketEkleButton.Top;
            sirketEklePanel.BringToFront();
            fillSirketList();
        }

        private void binaEkleButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = binaEkleButton.Height;
            sidePanel.Top = binaEkleButton.Top;
            binaEklePanel.BringToFront();
            fillBinaList();
        }

        private void adresListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adresListView.SelectedItems.Count > 0)
            {
                adresAdresIDTextBox.Text = adresListView.SelectedItems[0].SubItems[0].Text;
                adresIlTextBox.Text = adresListView.SelectedItems[0].SubItems[1].Text;
                adresIlceTextBox.Text = adresListView.SelectedItems[0].SubItems[2].Text;
                adresMahalleTextBox.Text = adresListView.SelectedItems[0].SubItems[3].Text;
                adresSokakTextBox.Text = adresListView.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void adresEkleButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = adresEkleButton.Height;
            sidePanel.Top = adresEkleButton.Top;
            adresEklePanel.BringToFront();
        }

        private void adresOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {

                    string sql = "EXEC AdresEkle " +
                        adresAdresIDTextBox.Text + "," +
                        adresIlTextBox.Text + "," +
                        adresIlceTextBox.Text + "," +
                        adresMahalleTextBox.Text + "," +
                        adresSokakTextBox.Text;
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Adres başarıyla eklendi");
                }
                connection.Close();
                fillAdresList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void adresGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {

                    string sql = "EXEC AdresGuncelle '@AdresID', '@Il', '@Ilce', '@Mahalle', '@Sokak'";
                        //adresAdresIDTextBox.Text + "," +
                        //adresIlTextBox.Text + "," +
                        //adresIlceTextBox.Text + "," +
                        //adresMahalleTextBox.Text + "," +
                        //adresSokakTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AdresID", adresAdresIDTextBox.Text);
                    command.Parameters.AddWithValue("@Il", adresIlTextBox.Text);
                    command.Parameters.AddWithValue("@Ilce", adresIlceTextBox.Text);
                    command.Parameters.AddWithValue("@Mahalle", adresMahalleTextBox.Text);
                    command.Parameters.AddWithValue("@Sokak", adresSokakTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Adres başarıyla güncellendi");
                }
                connection.Close();
                fillAdresList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void adresSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC AdresSil " + adresAdresIDTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Adres başarıyla silindi");
                }
                connection.Close();
                fillAdresList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void binaOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC BinaEkle " +
                        binaApartmanNoTextBox.Text + "," +
                        binaEnerjiKimligiTextBox.Text + "," +
                        binaYanginMerdiveniTextBox.Text + "," +
                        binaDaireSayisiTextBox.Text + "," +
                        binaKatSayisiTextBox.Text + "," +
                        binaAdresIDTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bina başarıyla eklendi");
                }
                connection.Close();
                fillBinaList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void binaGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {

                    string sql = "EXEC BinaGuncelle " +
                        binaApartmanNoTextBox.Text + "," +
                        binaEnerjiKimligiTextBox.Text + "," +
                        binaYanginMerdiveniTextBox.Text + "," +
                        binaDaireSayisiTextBox.Text + "," +
                        binaKatSayisiTextBox.Text + "," +
                        binaAdresIDTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bina başarıyla güncellendi");
                }
                connection.Close();
                fillBinaList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void binaSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC BinaSil " + binaApartmanNoTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bina başarıyla silindi");
                }
                connection.Close();
                fillBinaList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void binaListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (binaListView.SelectedItems.Count > 0)
            {
                binaApartmanNoTextBox.Text = binaListView.SelectedItems[0].SubItems[0].Text;
                binaEnerjiKimligiTextBox.Text = binaListView.SelectedItems[0].SubItems[1].Text;
                binaYanginMerdiveniTextBox.Text = binaListView.SelectedItems[0].SubItems[2].Text;
                binaDaireSayisiTextBox.Text = binaListView.SelectedItems[0].SubItems[3].Text;
                binaKatSayisiTextBox.Text = binaListView.SelectedItems[0].SubItems[4].Text;
                binaAdresIDTextBox.Text = binaListView.SelectedItems[0].SubItems[5].Text;
            }
        }

        private void sirketOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC SirketEkle " +
                        sirketSicilNoTextBox.Text + "','" +
                        sirketIsimTextBox.Text + "','" +
                        sirketHizmetTextBox.Text + "','" +
                        sirketMailTextBox.Text + "','" +
                        sirketSifreTextBox.Text + "'";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Şirket başarıyla eklendi");
                }
                connection.Close();
                fillSirketList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void sirketGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC SirketGuncelle '" +
                        sirketSicilNoTextBox.Text + "', '" +
                        sirketIsimTextBox.Text + "', '" +
                        sirketHizmetTextBox.Text+ "', '" +
                        sirketMailTextBox.Text + "', '" +
                        sirketSifreTextBox.Text + "'";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Şirket başarıyla güncellendi");
                }
                connection.Close();
                fillSirketList();
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
}

        private void sirketSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC SirketSil " + sirketSicilNoTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Şirket başarıyla silindi");
                }
                connection.Close();
                fillSirketList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void sirketListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sirketListView.SelectedItems.Count > 0)
            {
                sirketSicilNoTextBox.Text = sirketListView.SelectedItems[0].SubItems[0].Text;
                sirketIsimTextBox.Text = sirketListView.SelectedItems[0].SubItems[1].Text;
                sirketHizmetTextBox.Text = sirketListView.SelectedItems[0].SubItems[2].Text;
                sirketMailTextBox.Text = sirketListView.SelectedItems[0].SubItems[3].Text;
                sirketSifreTextBox.Text = sirketListView.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void ilComboBox_Enter(object sender, EventArgs e)
        {
            ilComboBox.Items.Clear();
            connection.Open();
            string sql = "EXEC İlSec";
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataReader data = cmd2.ExecuteReader();
            while (data.Read())
                ilComboBox.Items.Add(data.GetValue(0).ToString().Trim());
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
                    ilceComboBox.Items.Add(data.GetValue(0).ToString().Trim());
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
                    mahalleComboBox.Items.Add(data.GetValue(0).ToString().Trim());
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
                    sokakComboBox.Items.Add(data.GetValue(0).ToString().Trim());
                data.Close();
                connection.Close();
            }
        }

        private void ilComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceComboBox.Text = "";
            mahalleComboBox.Text = "";
            sokakComboBox.Text = "";
        }

        private void ilceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mahalleComboBox.Text = "";
            sokakComboBox.Text = "";
        }

        private void mahalleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sokakComboBox.Text = "";
        }

        private void adresIDEkleBEutton_Click(object sender, EventArgs e)
        {
            connection.Open();
            string sql = "EXEC AdresIDSec " + ilComboBox.SelectedItem + "," + ilceComboBox.SelectedItem +
                "," + mahalleComboBox.SelectedItem + "," + sokakComboBox.SelectedItem;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
                binaAdresIDTextBox.Text = data.GetValue(0).ToString();
            data.Close();
            connection.Close();
        }

        private void moveAdminPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void moveAdminPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void moveAdminPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }
    }
}