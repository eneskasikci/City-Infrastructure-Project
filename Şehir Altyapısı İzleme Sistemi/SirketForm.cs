using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SehirAltyapiIzlemeSistemi
{
    public partial class SirketForm : Form
    {
        public static string conString = @"Data Source=ENES;Initial Catalog=VTYSProjeFinal;Integrated Security=True";
        SqlConnection connection = new SqlConnection(MainForm.conString);

        public SirketForm()
        {
            InitializeComponent();
        }
        public void internetBringToFront()
        {
            internetPanel.BringToFront();
            internetPanel.Visible = true;
        }

        public void asansorBringToFront()
        {
            asansorPanel.BringToFront();
            asansorPanel.Visible = true;
        }

        public void dogalGazBringToFront()
        {
            dogalGazPanel.BringToFront();
            dogalGazPanel.Visible = true;
        }

        public void elektrikBringToFront()
        {
            elektrikPanel.BringToFront();
            elektrikPanel.Visible = true;
        }

        public void suBringToFront()
        {
            suPanel.BringToFront();
            suPanel.Visible = true;
        }

        public void sirketIsmi(string str)
        {
            sirketIsmiLabel.Text = str;
        }

        public void fillAsansorList()
        {
            asansorSaglayiciTextBox.Text = sirketIsmiLabel.Text;
            asansorListView.Items.Clear();
            connection.Open();
            string sql = "EXEC AsansorListele @sirketismi";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sirketismi", sirketIsmiLabel.Text);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString(), 0);
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                item.SubItems.Add(data.GetValue(3).ToString());
                item.SubItems.Add(data.GetValue(4).ToString());
                item.SubItems.Add(data.GetValue(7).ToString());
                item.SubItems.Add(data.GetValue(5).ToString());
                item.SubItems.Add(data.GetValue(6).ToString());
                asansorListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        public void fillElektrikList()
        {
            elektrikSaglayiciTextBox.Text = sirketIsmiLabel.Text;
            elektrikListView.Items.Clear();
            connection.Open();
            string sql = "EXEC ElektrikListele @sirketismi";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sirketismi", sirketIsmiLabel.Text);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().Trim(), 0);
                item.SubItems.Add(data.GetValue(3).ToString().Trim());
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                elektrikListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        public void fillSuList()
        {
            suSaglayiciTextBox.Text = sirketIsmiLabel.Text;
            suListView.Items.Clear();
            connection.Open();
            string sql = "EXEC SuListele @sirketismi";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sirketismi", sirketIsmiLabel.Text);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().Trim(), 0);
                item.SubItems.Add(data.GetValue(3).ToString().Trim());
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                suListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        public void fillDogalGazList()
        {
            dogalGazSaglayiciTextBox.Text = sirketIsmiLabel.Text;
            dogalGazListView.Items.Clear();
            connection.Open();
            string sql = "EXEC DogalGazListele @sirketismi";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sirketismi", sirketIsmiLabel.Text);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().Trim(), 0);
                item.SubItems.Add(data.GetValue(3).ToString().Trim());
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                dogalGazListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        public void fillInternetList()
        {
            internetSaglayiciTextBox.Text = sirketIsmiLabel.Text;
            internetListView.Items.Clear();
            connection.Open();
            string sql = "EXEC InternetListele @sirketismi";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sirketismi", sirketIsmiLabel.Text);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString().Trim(), 0);
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(4).ToString().Trim());
                item.SubItems.Add(data.GetValue(2).ToString());
                item.SubItems.Add(data.GetValue(3).ToString());
                internetListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void altyapiGuncelleButton_Click(object sender, EventArgs e)
        {
            sifreDegistirPanel.Visible = false;
            sidePanel.Height = altyapiGuncelleButton.Height;
            sidePanel.Top = altyapiGuncelleButton.Top;
        }

        private void sifreDegistirButton_Click(object sender, EventArgs e)
        {
            sifreDegistirPanel.Visible = true;
            sidePanel.Height = sifreDegistirButton.Height;
            sidePanel.Top = sifreDegistirButton.Top;
            sifreDegistirPanel.BringToFront();
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
                    binaComboBox.Items.Add(data.GetValue(0).ToString().Trim());
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
        }

        private void ilceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mahalleComboBox.Text = "";
            sokakComboBox.Text = "";
            binaComboBox.Text = "";
        }

        private void mahalleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sokakComboBox.Text = "";
            binaComboBox.Text = "";
        }

        private void sokakComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            binaComboBox.Text = "";
        }

        private void adresIDEkleBEutton_Click(object sender, EventArgs e)
        {
            if(ilComboBox.Text != "" && ilceComboBox.Text != "" && mahalleComboBox.Text != "" && sokakComboBox.Text != "")
            {
                connection.Open();
                string sql = "EXEC AdresIDSec " + ilComboBox.SelectedItem + "," + ilceComboBox.SelectedItem +
                    "," + mahalleComboBox.SelectedItem + "," + sokakComboBox.SelectedItem;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader data = cmd.ExecuteReader();
                string adresID = "";
                while (data.Read())
                    adresID = data.GetValue(0).ToString();
                data.Close();
                connection.Close();

                if (asansorPanel.Visible == true)
                {
                    asansorApartmanNoTextBox.Text = binaComboBox.SelectedItem.ToString();
                    asansorAdresIDTextBox.Text = adresID;
                }
                else if (elektrikPanel.Visible == true)
                {
                    elektrikApartmanNoTextBox.Text = binaComboBox.SelectedItem.ToString();
                    elektrikAdresIDTextBox.Text = adresID;
                }
                else if (suPanel.Visible == true)
                {
                    suApartmanNoTextBox.Text = binaComboBox.SelectedItem.ToString();
                    suAdresIDTextBox.Text = adresID;
                }
                else if (dogalGazPanel.Visible == true)
                {
                    dogalGazApartmanNoTextBox.Text = binaComboBox.SelectedItem.ToString();
                    dogalGazAdresIDTextBox.Text = adresID;
                }
                else if (internetPanel.Visible == true)
                {
                    internetApartmanNoTextBox.Text = binaComboBox.SelectedItem.ToString();
                    internetAdresIDTextBox.Text = adresID;
                }
            }
            else
                MessageBox.Show("Adres boş olamaz");
        }

        private void asansorOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC AsansorEkle @AsansorID, @Marka, @Date, @Kapasite, @EnerjiTuketimi," +
                        "@AdresId, @ApartmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AsansorID", int.Parse(asansorIDTextBox.Text));
                    command.Parameters.AddWithValue("@Marka", asansorMarkaTextBox.Text);
                    command.Parameters.Add("@Date", SqlDbType.Date).Value = muayeneDateTimePicker.Value.Date;
                    command.Parameters.AddWithValue("@Kapasite", int.Parse(kapasiteTextBox.Text));
                    command.Parameters.AddWithValue("@EnerjiTuketimi", enerjiTuketimiTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(asansorAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@ApartmanNo", int.Parse(asansorApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", asansorSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Asansör başarıyla eklendi");
                }
                connection.Close();
                fillAsansorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void asansorListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (asansorListView.SelectedItems.Count > 0)
            {
                asansorIDTextBox.Text = asansorListView.SelectedItems[0].SubItems[0].Text;
                asansorMarkaTextBox.Text = asansorListView.SelectedItems[0].SubItems[1].Text.Trim();
                muayeneDateTimePicker.Text = asansorListView.SelectedItems[0].SubItems[2].Text;
                kapasiteTextBox.Text = asansorListView.SelectedItems[0].SubItems[3].Text;
                enerjiTuketimiTextBox.Text = asansorListView.SelectedItems[0].SubItems[4].Text.Trim();
                asansorSaglayiciTextBox.Text = asansorListView.SelectedItems[0].SubItems[5].Text.Trim();
                asansorAdresIDTextBox.Text = asansorListView.SelectedItems[0].SubItems[6].Text;
                asansorApartmanNoTextBox.Text = asansorListView.SelectedItems[0].SubItems[7].Text;
            }
        }

        private void asansorGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC AsansorGuncelle @AsansorID, @Marka, @Date, @Kapasite, @EnerjiTuketimi," +
                        "@AdresId, @ApartmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AsansorID", int.Parse(asansorIDTextBox.Text));
                    command.Parameters.AddWithValue("@Marka", asansorMarkaTextBox.Text);
                    command.Parameters.Add("@Date", SqlDbType.Date).Value = muayeneDateTimePicker.Value.Date;
                    command.Parameters.AddWithValue("@Kapasite", int.Parse(kapasiteTextBox.Text));
                    command.Parameters.AddWithValue("@EnerjiTuketimi", enerjiTuketimiTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(asansorAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@ApartmanNo", int.Parse(asansorApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", asansorSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Asansör başarıyla güncellendi");
                }
                connection.Close();
                fillAsansorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void asansorSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC AsansorSil " + asansorIDTextBox.Text;
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Asansör başarıyla silindi");
                }
                connection.Close();
                fillAsansorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void asansorAraButton_Click(object sender, EventArgs e)
        {
            asansorListView.Items.Clear();
            connection.Open();
            string sql = "EXEC AsansorAra " + asansorIDTextBox.Text;
            SqlCommand cmd2 = new SqlCommand(sql, connection);
            SqlDataReader data = cmd2.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString(), 0);
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                item.SubItems.Add(data.GetValue(3).ToString());
                item.SubItems.Add(data.GetValue(4).ToString());
                item.SubItems.Add(data.GetValue(7).ToString());
                item.SubItems.Add(data.GetValue(5).ToString());
                item.SubItems.Add(data.GetValue(6).ToString());
                asansorListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void asansorIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (asansorIDTextBox.Text == "")
                fillAsansorList();
        }

        private void elektrikAraButton_Click(object sender, EventArgs e)
        {
            elektrikListView.Items.Clear();
            connection.Open();
            string sql = "EXEC ElektrikAra " + elektrikAltyapiNoTextBox.Text;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString(), 0);
                item.SubItems.Add(data.GetValue(3).ToString());
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                elektrikListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void elektrikAltyapiNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (elektrikAltyapiNoTextBox.Text == "")
                fillElektrikList();
        }

        private void elektrikOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC ElektrikEkle @AltyapiNo, @AdresID, @ApartmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", elektrikAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AdresID", int.Parse(elektrikAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@ApartmanNo", int.Parse(elektrikApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", elektrikSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Elektrik başarıyla eklendi");
                }
                connection.Close();
                fillElektrikList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void elektrikListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (elektrikListView.SelectedItems.Count > 0)
            {
                elektrikAltyapiNoTextBox.Text = elektrikListView.SelectedItems[0].SubItems[0].Text;
                elektrikSaglayiciTextBox.Text = elektrikListView.SelectedItems[0].SubItems[1].Text;
                elektrikAdresIDTextBox.Text = elektrikListView.SelectedItems[0].SubItems[2].Text;
                elektrikApartmanNoTextBox.Text = elektrikListView.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void elektrikGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC ElektrikGuncelle @AltyapiNo, @AdresID, @ApartmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", elektrikAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AdresID", int.Parse(elektrikAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@ApartmanNo", int.Parse(elektrikApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", elektrikSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Elektrik başarıyla güncellendi");
                }
                connection.Close();
                fillElektrikList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void elektrikSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC ElektrikSil " + elektrikAltyapiNoTextBox.Text;
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Elektrik başarıyla silindi");
                }
                connection.Close();
                fillElektrikList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void suAraButton_Click(object sender, EventArgs e)
        {
            suListView.Items.Clear();
            connection.Open();
            string sql = "EXEC SuAra " + suAltyapiNoTextBox.Text;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString(), 0);
                item.SubItems.Add(data.GetValue(3).ToString());
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                suListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void suAltyapiNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (suAltyapiNoTextBox.Text == "")
                fillSuList();
        }

        private void suOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC SuEkle @AltyapiNo, @AdresId, @AprtmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", suAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(suAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@AprtmanNo", int.Parse(suApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", suSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Su başarıyla eklendi");
                }
                connection.Close();
                fillSuList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void suGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC SuGuncelle @AltyapiNo, @AdresId, @AprtmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", suAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(suAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@AprtmanNo", int.Parse(suApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", suSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Su başarıyla güncellendi");
                }
                connection.Close();
                fillSuList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void suSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC SuSil " + suAltyapiNoTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Su başarıyla silindi");
                }
                connection.Close();
                fillSuList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void suListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suListView.SelectedItems.Count > 0)
            {
                suAltyapiNoTextBox.Text = suListView.SelectedItems[0].SubItems[0].Text;
                suSaglayiciTextBox.Text = suListView.SelectedItems[0].SubItems[1].Text;
                suAdresIDTextBox.Text = suListView.SelectedItems[0].SubItems[2].Text;
                suApartmanNoTextBox.Text = suListView.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void dogalGazListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dogalGazListView.SelectedItems.Count > 0)
            {
                dogalGazAltyapiNoTextBox.Text = dogalGazListView.SelectedItems[0].SubItems[0].Text;
                dogalGazSaglayiciTextBox.Text = dogalGazListView.SelectedItems[0].SubItems[1].Text;
                dogalGazAdresIDTextBox.Text = dogalGazListView.SelectedItems[0].SubItems[2].Text;
                dogalGazApartmanNoTextBox.Text = dogalGazListView.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void dogalGazAltyapiNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (dogalGazAltyapiNoTextBox.Text == "")
                fillDogalGazList();
        }

        private void dogalGazAraButton_Click(object sender, EventArgs e)
        {
            dogalGazListView.Items.Clear();
            connection.Open();
            string sql = "EXEC DogalGazAra " + dogalGazAltyapiNoTextBox.Text;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString(), 0);
                item.SubItems.Add(data.GetValue(3).ToString());
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                dogalGazListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void dogalGazOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC DogalGazEkle @AltyapiNo, @AdresId, @AprtmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", dogalGazAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(dogalGazAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@AprtmanNo", int.Parse(dogalGazApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", dogalGazSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Doğal Gaz başarıyla eklendi");
                }
                connection.Close();
                fillDogalGazList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void dogalGazGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC DogalGazGuncelle @AltyapiNo, @AdresId, @AprtmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", dogalGazAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(dogalGazAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@AprtmanNo", int.Parse(dogalGazApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", dogalGazSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Doğal Gaz başarıyla güncellendi");
                }
                connection.Close();
                fillDogalGazList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void dogalGazSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC DogalGazSil " + dogalGazAltyapiNoTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Doğal Gaz başarıyla silindi");
                }
                connection.Close();
                fillDogalGazList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void internetListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internetListView.SelectedItems.Count > 0)
            {
                internetAltyapiNoTextBox.Text = internetListView.SelectedItems[0].SubItems[0].Text;
                internetAltyapiTipiTextBox.Text = internetListView.SelectedItems[0].SubItems[1].Text;
                internetSaglayiciTextBox.Text = internetListView.SelectedItems[0].SubItems[2].Text;
                internetAdresIDTextBox.Text = internetListView.SelectedItems[0].SubItems[3].Text;
                internetApartmanNoTextBox.Text = internetListView.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void internetAltyapiNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (internetAltyapiNoTextBox.Text == "")
                fillInternetList();
        }

        private void internetAraButton_Click(object sender, EventArgs e)
        {
            internetListView.Items.Clear();
            connection.Open();
            string sql = "EXEC InternetAra " + internetAltyapiNoTextBox.Text;
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                ListViewItem item = new ListViewItem(data.GetValue(0).ToString(), 0);
                item.SubItems.Add(data.GetValue(1).ToString());
                item.SubItems.Add(data.GetValue(4).ToString());
                item.SubItems.Add(data.GetValue(2).ToString());
                item.SubItems.Add(data.GetValue(3).ToString());
                internetListView.Items.Add(item);
            }
            data.Close();
            connection.Close();
        }

        private void internetOlusturButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC InternetEkle @AltyapiNo, @AltyapiTipi, @AdresId, @AprtmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", internetAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AltyapiTipi", internetAltyapiTipiTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(internetAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@AprtmanNo", int.Parse(internetApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", internetSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Internet başarıyla eklendi");
                }
                connection.Close();
                fillInternetList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void internetGuncelleButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC InternetGuncelle @AltyapiNo, @AltyapiTipi, @AdresId, @AprtmanNo, @Saglayici";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@AltyapiNo", internetAltyapiNoTextBox.Text);
                    command.Parameters.AddWithValue("@AltyapiTipi", internetAltyapiTipiTextBox.Text);
                    command.Parameters.AddWithValue("@AdresId", int.Parse(internetAdresIDTextBox.Text));
                    command.Parameters.AddWithValue("@AprtmanNo", int.Parse(internetApartmanNoTextBox.Text));
                    command.Parameters.AddWithValue("@Saglayici", internetSaglayiciTextBox.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Internet başarıyla güncellendi");
                }
                connection.Close();
                fillInternetList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void internetSilButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    string sql = "EXEC InternetSil " + internetAltyapiNoTextBox.Text;
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Internet başarıyla silindi");
                }
                connection.Close();
                fillInternetList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }
    }
}