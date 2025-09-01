using ScaleManagment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TereziEla
{
    public partial class AddCard : Form
    {
        public string KartNomresi { get; private set; }
        public string SurucuAdi { get; private set; }
        public string SurucuSoyadi { get; private set; }
        public string AvtomobilNomresi { get; private set; }
        public string AvtomobilMarkasi { get; private set; }
        public string AvtomobilMansubiyyati { get; private set; }
        public string Status { get; private set; }
        public string Grade { get; private set; }




        public AddCard()
        {
            InitializeComponent();
          
        }




        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KartNomresi = textBox1.Text;
            SurucuAdi = textBox2.Text;
            SurucuSoyadi = textBox3.Text;
            AvtomobilNomresi = textBox4.Text;
            AvtomobilMarkasi = textBox5.Text;
            AvtomobilMansubiyyati = textBox6.Text;
            Status = textBox7.Text;
            Grade = textBox8.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();

            // Bu formu bağla
        }
    }
}



             // Məlumatları oxuyuruq
     

        // Form1-ə göndəririk

