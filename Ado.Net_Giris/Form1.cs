using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ado.Net_Giris
{
    using System.Data.SqlClient;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Projeden SQL sunucuya kanal açıyoruz(ConnectingString).
            // Hangi server'a bağlanılacak, hangi veritabanını kullanacak, nasıl bağlanılacak olayları yazılır.
            SqlConnection cnn = new SqlConnection("Server=.; Database=NORTHWND;uid=sa;pwd=123");
            // Windows Authentication ile bağlanıyorsak:
            // SqlConnection baglanti = new SqlConnection("Server=.; Database=NORTHWND;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("select * from Categories", cnn);
            cnn.Open();
            // rdr.Read(); Sadece bir satırı okur. Reader içerisindeki her bir satır okunmalıdır.
            // ADO.NET için okuma işlemini ExecuteReader yapar.
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Kategori> kategori_listesi = new List<Kategori>();
            while (rdr.Read()) // Koşul true döndüğü sürece ve rdr içerisindeki son satıra kadar okuma işlemi yapar.
            {
                Kategori kategori = new Kategori();
                kategori.CategoryID = (int)rdr["CategoryID"];
                kategori.CategoryName = (string)rdr["CategoryName"];
                if (rdr["Description"] != DBNull.Value) // Eğer okunan kısmın değeri null değilse.
                    kategori.Description = (string)rdr["Description"];
                kategori_listesi.Add(kategori);
                // MessageBox.Show(rdr["CategoryName"].ToString());
            }
            cnn.Close();
            dataGridView1.DataSource = kategori_listesi;
        }
    }
}
// ADO.NET, SQL'deki klasik sorguların C#'ta yazıldığı ve sunucuya gönderilip cevap alınan bir     framework'tur.
// ADO.NET için Form'a System.Data.SqlClient kütüphanesi eklenmelidir.