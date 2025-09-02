using System;
using System.Drawing;
using System.Windows.Forms;
using TereziEla;

namespace ScaleManagment.Components
{
    public class CardManager
    {
        

        public static void Init(Panel scaleInfoContent)
        {

            // Panel təmizlənir
           

            //
            // ===== Yuxarı panel (Search + Buttons) =====
            //
            Panel topPanel = new Panel();
            InitTopPanel(topPanel);

            //
            // ===== Orta hissə (ListView) =====
            //
            ListView listView = new ListView();
            InitBottomData(listView);

            //
            // ===== Alt panel (Pagination + Info) =====
            //
            Panel bottomPanel = new Panel();

            InitBottomnPanel(bottomPanel, listView);


            //
            // ===== Panel2-yə yerləşdir =====
            //
            scaleInfoContent.Controls.Add(listView);
            scaleInfoContent.Controls.Add(topPanel);
            scaleInfoContent.Controls.Add(bottomPanel);
        }

        #region private methods
        private static  void InitTopPanel(Panel topPanel)
        {

            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 50;

            TextBox txtSearch = new TextBox();
            txtSearch.Width = 200;
            txtSearch.Location = new Point(10, 12);
            //txtSearch.PlaceholderText = "Axtarış edin"; // .NET 6+ üçün işləyir

            Button btnDelete = new Button();
            btnDelete.Text = "Kart sil";
            btnDelete.Location = new Point(700, 10);
            btnDelete.BackColor = Color.LightGray;

            Button btnNewww = new Button();
            btnNewww.Text = "Yeni kart";
            btnNewww.Location = new Point(780, 10);
            btnNewww.BackColor = Color.Gold;

            //btnNewww.Click += new EventHandler(btnNewww_Click);



            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(btnNewww);

        }

       

        //private void btnNeww_Click(object sender, EventArgs e)
        //{
        //    
        //}



        private static  void InitBottomnPanel(Panel bottomPanel, ListView listView)
        {
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 50;

            Label lblStatus = new Label();
            lblStatus.Text = "Sütun sayı: " + listView.Items.Count.ToString();
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(10, 15);

            ComboBox cmbPageSize = new ComboBox();
            cmbPageSize.Items.AddRange(new object[] { "10 / səhifə", "20 / səhifə", "50 / səhifə" });
            cmbPageSize.SelectedIndex = 0;
            cmbPageSize.Location = new Point(700, 12);

            Label lblGoTo = new Label();
            lblGoTo.Text = "Səhifəyə keç:";
            lblGoTo.Location = new Point(820, 15);
            lblGoTo.AutoSize = true;

            TextBox txtPage = new TextBox();
            txtPage.Width = 40;
            txtPage.Location = new Point(900, 12);

            // Pagination düymələri
            Button btnFirst = new Button() { Text = "<<", Location = new Point(250, 12), Width = 40 };
            Button btnPrev = new Button() { Text = "<", Location = new Point(295, 12), Width = 40 };
            Button btn1 = new Button() { Text = "1", Location = new Point(340, 12), Width = 40 };
            Button btn2 = new Button() { Text = "2", Location = new Point(385, 12), Width = 40 };
            Button btn3 = new Button() { Text = "3", Location = new Point(430, 12), Width = 40 };
            Button btnNext = new Button() { Text = ">", Location = new Point(475, 12), Width = 40 };
            Button btnLast = new Button() { Text = ">>", Location = new Point(520, 12), Width = 40 };

            bottomPanel.Controls.Add(lblStatus);
            bottomPanel.Controls.Add(cmbPageSize);
            bottomPanel.Controls.Add(lblGoTo);
            bottomPanel.Controls.Add(txtPage);
            bottomPanel.Controls.Add(btnFirst);
            bottomPanel.Controls.Add(btnPrev);
            bottomPanel.Controls.Add(btn1);
            bottomPanel.Controls.Add(btn2);
            bottomPanel.Controls.Add(btn3);
            bottomPanel.Controls.Add(btnNext);
            bottomPanel.Controls.Add(btnLast);
        }

        private static  void InitBottomData(ListView listView)
        {
           

        }
        #endregion
    }
}
