using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScaleManagment.Components;
using ScaleManagment.Data;

namespace ScaleManagment
{
    public partial class Form1 : Form
    {
        private object label1;

        public Form1()
        {
            InitializeComponent();


            using (var db = new AppDbContext())
            {
               
                // İstifadəçiləri oxumaq
                var cards = db.Cards.ToList();

                var users = db.Users.ToList();
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Label - Axtarış edin
            Label lblSearch = new Label();
            lblSearch.Text = "____________";
            lblSearch.Location = new Point(10, 10);
            scaleInfoContent.Controls.Add(lblSearch);

            // Axtar Button
            Button btnSearch = new Button();
            //btnSearch.Image()
            btnSearch.Size = new Size(70, 25);
            btnSearch.Location = new Point(115, 7);
            scaleInfoContent.Controls.Add(btnSearch);

            // İstifadəçini sil Button (sağ yuxarı)
            Button btnDelete = new Button();
            btnDelete.Text = "İstifadəçini sil";
            btnDelete.Size = new Size(120, 25);
            btnDelete.Location = new Point(scaleInfoContent.Width - 250, 7);
            scaleInfoContent.Controls.Add(btnDelete);

            // Yeni istifadəçi Button (sağ yuxarıda, delete-in yanında)
            Button btnNew = new Button();
            btnNew.Text = "Yeni istifadəçi";

            btnNew.Size = new Size(120, 25);
            btnNew.BackColor = Color.Orange;
            btnNew.Location = new Point(scaleInfoContent.Width - 125, 7);
            scaleInfoContent.Controls.Add(btnNew);

            // ListView (orta hissədə)
            ListView listView = new ListView();
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Size = new Size(1150, 730);
            listView.Location = new Point(10, 40);

            // Sütunlar
            listView.Columns.Add("İstifadəçi adı", 570);
            listView.Columns.Add("Yaradılma tarixi", 570);


            // Məsələn test üçün sətir əlavə edək
            ListViewItem item1 = new ListViewItem("Akram");
            item1.SubItems.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            listView.Items.Add(item1);

            scaleInfoContent.Controls.Add(listView);


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
                Text = "RFID status:   ● Oxuyucuya bağlı",
                ForeColor = Color.Green,
                Font = labelFont,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            bottomPanel.Controls.Add(lblRfid);

            // Düymələr
            Button btnBagla = new Button { Text = "Bağla", ForeColor = Color.Red, FlatStyle = FlatStyle.Flat, Location = new Point(300, 5), Width = 80 };
            Button btnAc = new Button { Text = "Aç", ForeColor = Color.Green, FlatStyle = FlatStyle.Flat, Location = new Point(390, 5), Width = 80 };
            Button btnTara = new Button { Text = "Tara", FlatStyle = FlatStyle.Flat, Location = new Point(480, 5), Width = 80 };
            Button btnTesdiqla = new Button { Text = "Təsdiqlə", BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(570, 5), Width = 100 };
            Button btnOxucu = new Button { Text = "Oxucuya bağlan", BackColor = Color.Goldenrod, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(680, 5), Width = 140 };

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
            listView.Columns.Add("Giriş çəkisi", 100);
            listView.Columns.Add("Çıxış çəkisi", 100);
            listView.Columns.Add("Ümumi çəki", 100);
            listView.Columns.Add("Giriş tarixi", 150);
            listView.Columns.Add("Çıxış tarixi", 150);
            listView.Columns.Add("Kart", 100);
            listView.Columns.Add("Grade", 150);
            listView.Columns.Add("Post", 150);
            listView.Columns.Add("Maşın nömrəsi", 120);

            // Demo datalar (test üçün)
            string[,] data =
            {
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","High quality gold","Azermining Group3","77JB456"},
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","Low quality gold","Azermining Group3","77JB456"},
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","Medium","Azermining Group3","77JB456"},
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","Waste","Azermining Group3","77JB456"}
    };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                ListViewItem item = new ListViewItem(data[i, 0]); // Giriş çəkisi
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    item.SubItems.Add(data[i, j]);
                }
                listView.Items.Add(item);
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
            listView.Columns.Add("Giriş çəkisi", 100);
            listView.Columns.Add("Çıxış çəkisi", 100);
            listView.Columns.Add("Ümumi çəki", 100);
            listView.Columns.Add("Giriş tarixi", 150);
            listView.Columns.Add("Çıxış tarixi", 150);
            listView.Columns.Add("Kart", 100);
            listView.Columns.Add("Grade", 150);
            listView.Columns.Add("Post", 150);
            listView.Columns.Add("Maşın nömrəsi", 120);

            // Demo datalar (test üçün)
            string[,] data =
            {
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","High quality gold","Azermining Group3","77JB456"},
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","Low quality gold","Azermining Group3","77JB456"},
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","Medium","Azermining Group3","77JB456"},
        {"564","564","1096","18.01.2025 06:59","18.01.2025 06:59","4564564","Waste","Azermining Group3","77JB456"}
    };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                ListViewItem item = new ListViewItem(data[i, 0]); // Giriş çəkisi
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    item.SubItems.Add(data[i, j]);
                }
                listView.Items.Add(item);
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
            listView.Columns.Add("Kart", 100);
            listView.Columns.Add("Avto markası", 100);
            listView.Columns.Add("Avto nişanı", 100);
            listView.Columns.Add("Yüklü çəki", 100);
            listView.Columns.Add("Yüklü tarix", 150);
            listView.Columns.Add("Boş çəki", 100);
            listView.Columns.Add("Boş tarix", 150);
            listView.Columns.Add("Xammal çəki", 100);
            listView.Columns.Add("Xammal növü", 150);
            listView.Columns.Add("Tərəzi", 120);

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
    }
}
