using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public IFirebaseConfig fc = new FirebaseConfig()
        {
            BasePath = "https://kitap-7398e-default-rtdb.europe-west1.firebasedatabase.app/",
            AuthSecret = "0QRLCeIfmjAMt3legsTRmI1fyyBxMESMyMLTw7eN"
        };
        public IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FirebaseClient(fc);
            }
            catch (Exception)
            {
                MessageBox.Show("Baðlantý Kurulamadý.");
            }

            Reflesh();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Hiçbir bölme boþ býrakýlamaz!");
            }
            else
            {
                FirebaseResponse recive = client.Get(@"Account");
                Dictionary<string, Account> veri = JsonConvert.DeserializeObject<Dictionary<string, Account>>(recive.Body.ToString());
                if (veri != null)
                {
                    foreach (var a in veri)
                    {
                        if (a.Value.KullaniciAdi == textBox1.Text)
                        {
                            MessageBox.Show("Kullanýcý adý zaten kullanýlmakta.");
                            break;
                        }
                        else
                        {
                            var b = client.Set("Account/" + textBox1.Text, new Account() { isim = textBox3.Text, soy_isim = textBox4.Text, KullaniciAdi = textBox1.Text, Sifre = textBox2.Text });
                            MessageBox.Show("Hesabýnýz oluþturuldu.");
                            break;
                        }

                    }
                }
                else
                {
                    var b = client.Set("Account/" + textBox1.Text, new Account() { isim = textBox3.Text, soy_isim = textBox4.Text, KullaniciAdi = textBox1.Text, Sifre = textBox2.Text });
                    MessageBox.Show("Hesabýnýz oluþturuldu.");
                }

                Reflesh();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reflesh();
        }


        public void Reflesh()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("nick", "Kullanýcý Adý");
            dataGridView1.Columns.Add("name", "Ýsim " + "\n" + "Soy Ýsim");

            FirebaseResponse recive = client.Get(@"Account");
            Dictionary<string, Account> veri = JsonConvert.DeserializeObject<Dictionary<string, Account>>(recive.Body.ToString());
            if (veri != null)
            {
                if (veri.Count > 0)
                {
                    foreach (var a in veri)
                    {
                        dataGridView1.Rows.Add(a.Value.KullaniciAdi, a.Value.isim + " " + a.Value.soy_isim);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}