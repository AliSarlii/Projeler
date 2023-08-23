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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp2
{
    public partial class Form4 : Form
    {
        public bool yonetici;
        public IFirebaseConfig fc = new FirebaseConfig()
        {
            BasePath = "https://kitap-7398e-default-rtdb.europe-west1.firebasedatabase.app/",
            AuthSecret = "0QRLCeIfmjAMt3legsTRmI1fyyBxMESMyMLTw7eN"
        };
        public IFirebaseClient client;
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FirebaseClient(fc);
            }
            catch (Exception)
            {
                MessageBox.Show("Bağlantı Kurulamadı.");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Hiçbir bölme boş bırakılamaz!");
            }
            else
            {
                FirebaseResponse recive = client.Get(@"Book");
                Dictionary<string, Book> veri = JsonConvert.DeserializeObject<Dictionary<string, Book>>(recive.Body.ToString());
                if (veri != null)
                {
                    foreach (var a in veri)
                    {
                        if (a.Value.Kitapİsim == textBox1.Text)
                        {
                            MessageBox.Show("Bu kitap zaten bulunmakta.");
                            break;
                        }
                        else
                        {
                            var b = client.Set("Book/" + textBox1.Text, new Book() { Kitapİsim = textBox1.Text, Yazar = textBox2.Text, Konu = comboBox1.Text });
                            MessageBox.Show("Kitap oluşturuldu.");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            break;
                        }

                    }
                }
                else
                {
                    var b = client.Set("Book/" + textBox1.Text, new Book() { Kitapİsim = textBox1.Text, Yazar = textBox2.Text, Konu = comboBox1.Text });
                    MessageBox.Show("Kitap oluşturuldu.");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }


        }
    }
}
