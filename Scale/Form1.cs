using ScaleManagment.Components;
using ScaleManagment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TereziEla;

namespace ScaleManagment
{
    public partial class Form1 : Form
    {
        private object label1;
        public ListView listView;
        private TextBox txtSearch;

        public object FlatAppearance { get; private set; }

        public Form1()
        {
            InitializeComponent();


            using (var db = new AppDbContext())
            {
               
                // İstifadəçiləri oxumaq
                //var cards = db.Cards.ToList();

                //var users = db.Users.ToList();
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSearch = new TextBox();
            txtSearch.Text = "Axtarış edin";
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Location = new Point(20, 20);
            txtSearch.Width = 200;

            // Event-lər əlavə olunur
            txtSearch.GotFocus += RemoveText;
            txtSearch.LostFocus += AddText;


            // İstifadəçini sil Button (sağ yuxarı)
            Button btnDelete = new Button();
            btnDelete.Text = "İstifadəçini sil";
            btnDelete.Size = new Size(120, 25);
            btnDelete.Location = new Point(scaleInfoContent.Width - 250, 7);
            scaleInfoContent.Controls.Add(btnDelete);

            btnDelete.Click += new EventHandler(btnDelete_Click);

            // Yeni istifadəçi Button (sağ yuxarıda, delete-in yanında)
            Button btnNew = new Button();
            btnNew.Text = "Yeni istifadəçi";

            btnNew.Size = new Size(120, 25);
            btnNew.BackColor = Color.Orange;
            btnNew.Location = new Point(scaleInfoContent.Width - 125, 7);
            scaleInfoContent.Controls.Add(btnNew);

            btnNew.Click += new EventHandler(btnNew_Click);



            // ListView (orta hissədə)
            if (listView == null)
            {
                listView = new ListView();
                listView.View = View.Details;
                listView.FullRowSelect = true;
                listView.GridLines = true;
                listView.Size = new Size(1150, 730);
                listView.Location = new Point(10, 40);

                listView.Columns.Add("İstifadəçi adı", 570);
                listView.Columns.Add("Yaradılma tarixi", 570);

                scaleInfoContent.Controls.Add(listView);

                string connectionString = "Data Source=DESKTOP-IQB2C7N\\SQLEXPRESS;Initial Catalog=Qeydiyyatdb;User ID=sa;Password=Scale123+-;Encrypt=True;TrustServerCertificate=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select*from dbo.tblDatas";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = reader["Istifadeci adi"].ToString();
                        DateTime date = Convert.ToDateTime(reader["Yaradilma tarixi"]);
                        //listView = new ListView();
                        ListViewItem item = new ListViewItem(name);
                        item.SubItems.Add(date.ToString("dd.MM.yyyy HH:mm"));
                        listView.Items.Add(item);
                    }

                    reader.Close();
                }
            }


        }

        private void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Axtarış edin";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void RemoveText(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Axtarış edin")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
        }

        public void AddUserToListView(string username)
        {
            if (listView == null)
            {
                MessageBox.Show("ListView hələ yaradılmayıb!");
                return;
            }

            string date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            ListViewItem item = new ListViewItem(username);
            item.SubItems.Add(date);
            listView.Items.Add(item);
        }







        private void btnNew_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm(this); // <-- this = Form1 obyekti
            addUserForm.ShowDialog();
        }



        public void AddToList(string kartNo, string ad, string soyad,
                         string avtoNo, string marka,
                         string mensubiyyet, string status, string grade)
        {
            ListViewItem item = new ListViewItem(""); // checkbox üçün boş sütun

            item.SubItems.Add(kartNo);
            item.SubItems.Add(ad + " " + soyad);
            item.SubItems.Add(avtoNo);
            item.SubItems.Add(marka);
            item.SubItems.Add(mensubiyyet);
            item.SubItems.Add(status);
            item.SubItems.Add(grade);

            listView.Items.Add(item);
        }





        private void button2_Click(object sender, EventArgs e)
        {

            scaleInfoContent.Controls.Clear();

            // Əsas Layout
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.Dock = DockStyle.Fill;
            tbl.ColumnCount = 4;
            tbl.RowCount = 3;
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));

            Font labelFont = new Font("Segoe UI", 9, FontStyle.Regular);
            Font textFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Bir helper funksiya yazırıq ki, label+textbox düzülüşünü asan yaradaq
            Control CreateField(string label, string value)
            {
                Panel p = new Panel { Dock = DockStyle.Fill };
                Label l = new Label { Text = label, Dock = DockStyle.Top, Font = labelFont, AutoSize = true };
                TextBox t = new TextBox { Text = value, Dock = DockStyle.Bottom, Font = textFont };
                p.Controls.Add(t);
                p.Controls.Add(l);
                return p;
            }

            // 1-ci sıra
            tbl.Controls.Add(CreateField("Tərəzi", "Azermining Group3"), 0, 0);
            tbl.Controls.Add(CreateField("Tarix/Saat", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")), 1, 0);
            tbl.Controls.Add(CreateField("Avtomobil nömrəsi", "77JB459"), 2, 0);
            tbl.Controls.Add(CreateField("Avtomobil modeli", "BMW"), 3, 0);

            // 2-ci sıra
            tbl.Controls.Add(CreateField("Şirkət", "AMG"), 0, 1);
            tbl.Controls.Add(CreateField("Sürücü", "Mərəh Mərəh"), 1, 1);
            tbl.Controls.Add(CreateField("Kart ID", "553695947"), 2, 1);
            tbl.Controls.Add(CreateField("Yükün növü", "Low quality gold"), 3, 1);

            // 3-cü sıra → RFID və düymələr
            Panel bottomPanel = new Panel { Dock = DockStyle.Fill };

            Label lblRfid = new Label
            {
                Text = "RFID status: ",
                ForeColor = Color.Black,
                Font = labelFont,
                AutoSize = true,
                Location = new Point(12, 12)
            };
            Label lblOxuyucuyabagli = new Label
            {
                Text = " ● Oxuyucuya bağlı",
                ForeColor = Color.Green,
                Font = labelFont,
                AutoSize = true,
                Location = new Point(lblRfid.Right+3,lblRfid.Top)
            };
            bottomPanel.Controls.Add(lblRfid);
            bottomPanel.Controls.Add(lblOxuyucuyabagli);


            RadioButton toggle = new RadioButton
            {
                Appearance = Appearance.Button,
                Text = "Avtomatik",
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Width = 100,
                Height = 30,
                Location = new Point(lblOxuyucuyabagli.Right+190,lblOxuyucuyabagli.Top)
            };

            toggle.FlatAppearance.BorderSize = 0;

            // Event
            toggle.CheckedChanged += (s, args) =>
            {
                if (toggle.Checked)
                {
                    toggle.BackColor = Color.Green;
                    toggle.ForeColor = Color.White;
                    toggle.Text = "ON";
                }
                else
                {
                    toggle.BackColor = Color.Red;
                    toggle.ForeColor = Color.White;
                    toggle.Text = "OFF";
                }
            };

            // Formaya əlavə et
            bottomPanel.Controls.Add(toggle);



            //toggle.BackColor = toggle.Checked ? Color.Gold : Color.LightGray;

            // Düymələr
            Button btnBagla = new Button { Text = "Bağla", ForeColor = Color.Red, FlatStyle = FlatStyle.Flat, Location = new Point(515, 15), Width = 50 };
            Button btnAc = new Button { Text = "Aç", ForeColor = Color.Green, FlatStyle = FlatStyle.Flat, Location = new Point(580, 15), Width = 30 };
            Button btnTara = new Button { Text = "Tara", FlatStyle = FlatStyle.Flat,  Location = new Point(600, 15), Width = 60 };
            btnTara.FlatAppearance.BorderSize = 0;
            Button btnTesdiqla = new Button { Text = "Təsdiqlə", BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(660, 15), Width = 80 };
            Button btnOxucu = new Button { Text = "Oxucuya bağlan", BackColor = Color.Goldenrod, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(750, 15), Width = 100 };

            bottomPanel.Controls.AddRange(new Control[] { btnBagla, btnAc, btnTara, btnTesdiqla, btnOxucu });

            tbl.Controls.Add(bottomPanel, 0, 2);
            tbl.SetColumnSpan(bottomPanel, 4);

            // Panel2-yə əlavə et
            scaleInfoContent.Controls.Add(tbl);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scaleInfoContent.Controls.Clear();

            // Əsas Layout
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Üst hissə
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Orta hissə (cədvəl)
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Alt hissə

            // --- ÜST HİSSƏ (Search + Export düyməsi)
            Panel topPanel = new Panel { Dock = DockStyle.Fill };

            TextBox txtSearch = new TextBox
            {
                //PlaceholderText = "Axtarış edin",
                Location = new Point(10, 8),

                Width = 200
            };

            Button btnExport = new Button
            {
                Text = "File export",
                Anchor = AnchorStyles.Right,
                Location = new Point(600, 6),
                Width = 100
            };

            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnExport);

            // --- ORTA HİSSƏ (ListView)
            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                CheckBoxes = true
            };

            // Sütunlar
            listView.Columns.Add("Giriş çəkisi", 80);
            listView.Columns.Add("Çıxış çəkisi", 80);
            listView.Columns.Add("Ümumi çəki", 80);
            listView.Columns.Add("Giriş tarixi", 100);
            listView.Columns.Add("Çıxış tarixi", 100);
            listView.Columns.Add("Kart", 80);
            listView.Columns.Add("Grade", 100);
            listView.Columns.Add("Post", 140);
            listView.Columns.Add("Maşın nömrəsi", 100);

            string connectionString = "Data Source=DESKTOP-IQB2C7N\\SQLEXPRESS;Initial Catalog=erp_azmaind;User ID=sa;Password=Scale123+-;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "select*from dbo.gates";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string giris = reader["weight_in"].ToString();
                    string cixis = reader["weight_out"].ToString();
                    string umumiceki = reader["weight_total"].ToString();

                    DateTime girisTarixi = Convert.ToDateTime(reader["data_in"]);
                    DateTime cixisTarixi = Convert.ToDateTime(reader["data_out"]);

                    string kart = reader["card"].ToString();
                    string grade = reader["sort"].ToString();
                    string post = reader["post"].ToString();
                    string masin = reader["carnumber"].ToString();

                    ListViewItem item = new ListViewItem(giris);
                    item.SubItems.Add(cixis);
                    item.SubItems.Add(umumiceki);
                    item.SubItems.Add(girisTarixi.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(cixisTarixi.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(kart);
                    item.SubItems.Add(grade);
                    item.SubItems.Add(post);
                    item.SubItems.Add(masin);

                    listView.Items.Add(item);
                }

                reader.Close();
            }
            // Demo datalar (test üçün)



            // --- ALT HİSSƏ (Sütun sayı + səhifələmə)
            Panel bottomPanel = new Panel { Dock = DockStyle.Fill };

            Label lblCount = new Label
            {
                Text = "Sətir sayı: " + listView.Items.Count,
                Location = new Point(10, 10),
                AutoSize = true
            };

            // Səhifələmə düymələri
            Button btnPrev = new Button { Text = "<", Location = new Point(300, 5), Width = 40 };
            Button btnPage1 = new Button { Text = "1", Location = new Point(350, 5), Width = 40 };
            Button btnPage2 = new Button { Text = "2", Location = new Point(400, 5), Width = 40 };
            Button btnNext = new Button { Text = ">", Location = new Point(450, 5), Width = 40 };

            bottomPanel.Controls.Add(lblCount);
            bottomPanel.Controls.AddRange(new Control[] { btnPrev, btnPage1, btnPage2, btnNext });

            // Əlavə et Layout-a
            mainLayout.Controls.Add(topPanel, 0, 0);
            mainLayout.Controls.Add(listView, 0, 1);
            mainLayout.Controls.Add(bottomPanel, 0, 2);

            // Panel2-yə əlavə et
            scaleInfoContent.Controls.Add(mainLayout);
        }



        private void button4_Click(object sender, EventArgs e)
        {
            scaleInfoContent.Controls.Clear();

            // Əsas Layout
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Üst hissə
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Orta hissə (cədvəl)
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Alt hissə

            // --- ÜST HİSSƏ (Search + Export düyməsi)
            Panel topPanel = new Panel { Dock = DockStyle.Fill };

            TextBox txtSearch = new TextBox
            {
                //PlaceholderText = "Axtarış edin",
                Location = new Point(10, 8),
                Width = 200
            };

            Button btnExport = new Button
            {
                Text = "File export",
                Anchor = AnchorStyles.Right,
                Location = new Point(600, 6),
                Width = 100
            };

            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnExport);

            // --- ORTA HİSSƏ (ListView)
            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                CheckBoxes = true
            };

            // Sütunlar
            listView.Columns.Add("Giriş çəkisi", 80);
            listView.Columns.Add("Çıxış çəkisi", 80);
            listView.Columns.Add("Ümumi çəki", 80);
            listView.Columns.Add("Giriş tarixi", 110);
            listView.Columns.Add("Çıxış tarixi", 110);
            listView.Columns.Add("Kart", 80);
            listView.Columns.Add("Grade", 100);
            listView.Columns.Add("Post", 120);
            listView.Columns.Add("Maşın nömrəsi", 100);

            string connectionString = "Data Source=DESKTOP-IQB2C7N\\SQLEXPRESS;Initial Catalog=erp_azmaind;User ID=sa;Password=Scale123+-;Encrypt=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "select*from dbo.gates";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string giris = reader["weight_in"].ToString();
                    string cixis = reader["weight_out"].ToString();
                    string umumiceki = reader["weight_total"].ToString();

                    DateTime girisTarixi = Convert.ToDateTime(reader["data_in"]);
                    DateTime cixisTarixi = Convert.ToDateTime(reader["data_out"]);

                    string kart = reader["card"].ToString();
                    string grade = reader["sort"].ToString();
                    string post = reader["post"].ToString();
                    string masin = reader["carnumber"].ToString();

                    ListViewItem item = new ListViewItem(giris);
                    item.SubItems.Add(cixis);
                    item.SubItems.Add(umumiceki);
                    item.SubItems.Add(girisTarixi.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(cixisTarixi.ToString("dd.MM.yyyy HH:mm"));
                    item.SubItems.Add(kart);
                    item.SubItems.Add(grade);
                    item.SubItems.Add(post);
                    item.SubItems.Add(masin);

                    listView.Items.Add(item);
                }

                reader.Close();
            }
            // --- ALT HİSSƏ (Sütun sayı + səhifələmə)
            Panel bottomPanel = new Panel { Dock = DockStyle.Fill };

            Label lblCount = new Label
            {
                Text = "Sətir sayı: " + listView.Items.Count,
                Location = new Point(10, 10),
                AutoSize = true
            };

            // Səhifələmə düymələri
            Button btnPrev = new Button { Text = "<", Location = new Point(300, 5), Width = 40 };
            Button btnPage1 = new Button { Text = "1", Location = new Point(350, 5), Width = 40 };
            Button btnPage2 = new Button { Text = "2", Location = new Point(400, 5), Width = 40 };
            Button btnNext = new Button { Text = ">", Location = new Point(450, 5), Width = 40 };

            bottomPanel.Controls.Add(lblCount);
            bottomPanel.Controls.AddRange(new Control[] { btnPrev, btnPage1, btnPage2, btnNext });

            // Əlavə et Layout-a
            mainLayout.Controls.Add(topPanel, 0, 0);
            mainLayout.Controls.Add(listView, 0, 1);
            mainLayout.Controls.Add(bottomPanel, 0, 2);

            // Panel2-yə əlavə et
            scaleInfoContent.Controls.Add(mainLayout);
        }

        private void cardManagerClick(object sender, EventArgs e)
        {
            CardManager.Init(scaleInfoContent);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            // panel2 təmizlə
            scaleInfoContent.Controls.Clear();
            scaleInfoContent.AutoScroll = true;

            //
            // === Yuxarı filter paneli ===
            //
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 50;


            Button btnExport = new Button()
            {
                Text = "Export",
                Location = new Point(450, 10),
                Width = 80
            };

            Button btnDelete = new Button()
            {
                Text = "Reysi silmək",
                Location = new Point(540, 10),
                Width = 100
            };

            Button btnEdit = new Button()
            {
                Text = "Dəyişiklik",
                Location = new Point(650, 10),
                Width = 90
            };

            Button btnNew = new Button()
            {
                Text = "+ Yeni reys",
                Location = new Point(750, 10),
                BackColor = Color.Gold,
                Width = 100
            };


            topPanel.Controls.Add(btnExport);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(btnEdit);
            topPanel.Controls.Add(btnNew);

            //
            // === Statistik göstəricilər paneli ===
            //
            Panel statsPanel = new Panel();
            statsPanel.Dock = DockStyle.Top;
            statsPanel.Height = 150;

            int x = 10, y = 10;
            void AddStat(string title, string value)
            {
                Label lbl = new Label();
                lbl.Text = title;
                lbl.Location = new Point(x, y);
                lbl.AutoSize = true;

                TextBox txt = new TextBox();
                txt.Text = value;
                txt.Location = new Point(x, y + 20);
                txt.Width = 160;
                txt.ReadOnly = true;
                txt.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                statsPanel.Controls.Add(lbl);
                statsPanel.Controls.Add(txt);

                x += 180;
                if (x > 700)
                {
                    x = 10;
                    y += 60;
                }
            }

            AddStat("Yerinə yetirilən reyslərin sayı", "33242");
            AddStat("Ümumi daşınan yükün çəkisi, ton", "24245");
            AddStat("High quality gold reyslərin sayı", "786");
            AddStat("High quality gold çəkisi, ton", "342");
            AddStat("Medium quality gold reyslərin sayı", "3425");
            AddStat("Medium quality gold çəkisi, ton", "2452");
            AddStat("Low quality gold reyslərin sayı", "3425");
            AddStat("Low quality gold çəkisi, ton", "2452");
            AddStat("Waste reyslərin sayı", "2452");
            AddStat("Waste çəkisi, ton", "2452");
            AddStat("Tamamlanmamış reyslərin sayı", "2452");

            //
            // === Aşağı ListView (Cədvəl) ===
            //
            ListView listView = new ListView();
            listView.Dock = DockStyle.Fill;
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.CheckBoxes = true;

            // Sütunlar
            listView.Columns.Add("ID", 50);
            listView.Columns.Add("Kart", 80);
            listView.Columns.Add("Avto markası", 80);
            listView.Columns.Add("Avto nişanı", 80);
            listView.Columns.Add("Yüklü çəki", 70);
            listView.Columns.Add("Yüklü tarix", 110);
            listView.Columns.Add("Boş çəki", 70);
            listView.Columns.Add("Boş tarix", 100);
            listView.Columns.Add("Xammal çəki", 70);
            listView.Columns.Add("Xammal növü", 90);
            listView.Columns.Add("Tərəzi", 110);

            // Məlumat nümunəsi
            string[,] data =
            {
        {"01", "452525", "BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "Medium", "Azermining2"},
        {"02", "2454252", "BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "High quality gold", "Azermining2"},
        {"03", "2454555", "BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "Low quality gold", "Azermining2"},
        {"04", "534535",  "BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "Medium", "Azermining2"},
        {"05", "3453465", "BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "Medium", "Azermining2"},
        {"06", "46546846","BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "High quality gold", "Azermining2"},
        {"07", "7475466", "BMW", "77JB456", "45353", "18.01.2025 06:59", "45353", "18.01.2025 06:59", "45353", "Low quality gold", "Azermining2"},
    };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                ListViewItem item = new ListViewItem(data[i, 0]);
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    item.SubItems.Add(data[i, j]);
                }
                listView.Items.Add(item);
            }

            //
            // === Panel2-yə əlavə et ===
            //
            scaleInfoContent.Controls.Add(listView);
            scaleInfoContent.Controls.Add(statsPanel);
            scaleInfoContent.Controls.Add(topPanel);
        }

        internal void AddUserToListView(string userName, string creationDate)
        {
            throw new NotImplementedException();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
