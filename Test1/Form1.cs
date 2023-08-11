using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;       
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test1
{
    public partial class Form1 : Form
    {
        public static IMongoClient client = new MongoClient("mongodb://localhost:27017");

        public static IMongoDatabase db = client.GetDatabase("mydatabase");

        public static IMongoCollection<Resident> collection = db.GetCollection<Resident>("mycol");

        public Form1()
        {
            InitializeComponent();
        }

        public class Resident
        {
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonElement("Name")]
            public string Name { get; set; }
            [BsonElement("Age")]
            public string Age { get; set; }
            [BsonElement("Gender")]
            public string Gender { get; set; }
            [BsonElement("Address")]
            public string Address { get; set; }

            public Resident(string name, string age, string gender, string address)
            {
                Name = name;
                Age = age;
                Gender = gender;
                Address = address;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Resident resident = new Resident(txtName.Text,txtAge.Text,txtGender.Text,txtAddress.Text);
            collection.InsertOne(resident);
            readData();
        }

        public void readData()
        {
            List<Resident> list = collection.AsQueryable().ToList();
            dataGridView1.DataSource = list;
            txtName.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            txtAge.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            txtGender.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
        }
    }
}
