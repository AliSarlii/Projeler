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
    partial class Form2 : Form
    {
        bool Logined;
        Form3 Form3;
        public Form2()
        {
            InitializeComponent();
        }
        public IFirebaseConfig fc = new FirebaseConfig()
        {
            BasePath = "https://kitap-7398e-default-rtdb.europe-west1.firebasedatabase.app/",
            AuthSecret = "0QRLCeIfmjAMt3legsTRmI1fyyBxMESMyMLTw7eN"
        };
        public IFirebaseClient client;
        private void Form2_Load(object sender, EventArgs e)
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

            FirebaseResponse recive = client.Get(@"Account");
            Dictionary<string, Account> veri = JsonConvert.DeserializeObject<Dictionary<string, Account>>(recive.Body.ToString());
            foreach (var a in veri)
            {
                if (a.Value.KullaniciAdi == textBox1.Text && a.Value.Sifre == textBox2.Text)
                {

                    this.Close();
                    Form3 form3 = new Form3();
                    form3.yonetici = a.Value.Yönetici;
                    form3.Show();
                    Logined = true;
                    break;
                }
            }
            if (Logined == false)
            {
                MessageBox.Show("Böyle bir hesap bulunmamakta.");
            }
        }
    }
}
