using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO.Ports;
using System.Windows.Forms;
using RealRfid.DAO;
using RealRfid.GUID;

namespace RealRfid{
    public partial class Form1 : Form{
        string tarj = "";
        Tablaalumnos obj = new Tablaalumnos();
        private SerialPort serialPort1 = new SerialPort();
        public Form1(){
            InitializeComponent();
            serialPort1 = new SerialPort();
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 9600;
            serialPort1.DtrEnable = true;
            serialPort1.Open();
            serialPort1.DataReceived += serialPort1_DataReceived;
        }
        private void Form1_Load(object sender, EventArgs e){
            dataGridView1.DataSource = obj.tabla();
            timer1.Start();
        }
        private delegate void LineReceivedEvent(string tarj);
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e){
            tarj = serialPort1.ReadLine();
            tarj = tarj.Substring(0, tarj.Length - 1);
            this.BeginInvoke(new LineReceivedEvent(LineReceived), tarj);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e){
            if (serialPort1.IsOpen){
                serialPort1.Close();
                MessageBox.Show("Conexión cerrada");
            }
        }
        private void LineReceived(string tarj){
            dataGridView1.DataSource = obj.tabla();
            string a = "";
            foreach (DataGridViewRow row in dataGridView1.Rows){
                a = row.Cells[4].Value.ToString();
                if (tarj == a){
                    try{
                        MySqlCommand cmd;
                        MySqlConnection Con = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=alumnos");
                        string consul = "UPDATE alumnos SET asis = 'SI' WHERE tarjeta ='" + a + "'";
                        cmd = new MySqlCommand(consul, Con);
                        Con.Open();
                        cmd.ExecuteNonQuery();
                        Con.Close();
                    }catch (Exception ex){
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    dataGridView1.DataSource = obj.tabla();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e) {
            string[] materias = { "Lenguajes y autómatas 2", "Computación en la nube", "Programación", "Desarrollo de aplicaciones móviles en la nube" };
            Random rnd = new Random();
            int al = rnd.Next(0,3);
            MessageBox.Show("Comienza el pase de lista para la materia: " + materias[al]);
            try{
                MySqlCommand cmd;
                MySqlConnection Con = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=alumnos");
                string consul = "UPDATE alumnos SET asis = 'NO' WHERE asis ='SI'";
                cmd = new MySqlCommand(consul, Con);
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();
            }catch (Exception ex){
                MessageBox.Show("Error: " + ex.Message);
            }
            dataGridView1.DataSource = obj.tabla();
        }
        private void pictureBox4_Click(object sender, EventArgs e){
            Acerca Ac = new Acerca();
            this.Hide();
            Ac.ShowDialog();
            this.Show();
        }
    }
}