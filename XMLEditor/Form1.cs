using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XMLEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread p1;
        string titulo;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK) { 
                titulo = saveFileDialog1.FileName;
            }
            p1 = new Thread(new ThreadStart(hilo1));
            p1.Start();
        }

        private void hilo1()
        {
            XDocument xmlDoc = new XDocument(new XElement("Personas"));

            int i = 0;
            while (dgvXML[0, i].Value != null && dgvXML[0, i].Value.ToString() != "")
            {
                xmlDoc.Root.Add(
                new XElement("Persona",
                    new XAttribute("ID", i + 1),
                    new XElement("Nombre", dgvXML[0, i].Value.ToString()),
                    new XElement("Paterno", dgvXML[1, i].Value.ToString()),
                    new XElement("Materno", dgvXML[2, i].Value.ToString()),
                    new XElement("Edad", dgvXML[3, i].Value.ToString())
                   ));
                i++;
            }
     
            xmlDoc.Save(titulo);
            MessageBox.Show("¡Guardado con exito!");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            p1.Abort();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }



    
}
