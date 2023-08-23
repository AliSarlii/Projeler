using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form3 : Form
    {
        public bool yonetici;
        public IFirebaseConfig fc = new FirebaseConfig()
        {
            BasePath = "https://kitap-7398e-default-rtdb.europe-west1.firebasedatabase.app/",
            AuthSecret = "0QRLCeIfmjAMt3legsTRmI1fyyBxMESMyMLTw7eN"
        };
        public IFirebaseClient client;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FirebaseClient(fc);
            }
            catch (Exception)
            {
                MessageBox.Show("Bağlantı Kurulamadı.");
            }
            button1.Visible = yonetici;
            Reflesh();
        }


        public void Reflesh()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Kitap Adı", "Kitap Adı");
            dataGridView1.Columns.Add("Yazar", "Yazar");
            dataGridView1.Columns.Add("Konu", "Konu");

            FirebaseResponse recive = client.Get(@"Book");
            Dictionary<string, Book> veri = JsonConvert.DeserializeObject<Dictionary<string, Book>>(recive.Body.ToString());
            if (comboBox1.Text == "Hepsi")
            {
                if (veri != null)
                {
                    if (veri.Count > 0)
                    {
                        foreach (var a in veri)
                        {
                            dataGridView1.Rows.Add(a.Value.Kitapİsim, a.Value.Yazar, a.Value.Konu);
                        }
                    }
                }
            }
            else
            {
                if (veri != null)
                {
                    if (veri.Count > 0)
                    {
                        foreach (var a in veri)
                        {
                            if (a.Value.Konu == comboBox1.Text)
                            {
                                dataGridView1.Rows.Add(a.Value.Kitapİsim, a.Value.Yazar, a.Value.Konu);
                            }
                        }
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reflesh();
        }
    }
}
