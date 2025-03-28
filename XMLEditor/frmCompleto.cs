using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XMLEditor
{
    public partial class frmCompleto : Form
    {
        public frmCompleto()
        {
            InitializeComponent();
        }
        string titulo;
        XElement[] personass;
        Thread h1,hpers ;
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    titulo = openFileDialog1.FileName;
                    string texto = File.ReadAllText(titulo);

                    string[] reg = texto.Split('\n');
                    string[] col = reg[0].Split(',');
                    dgvMain.ColumnCount = col.Length;
                    dgvMain.RowCount = reg.Length;

                    for (int i = 0; i < reg.Length -1; i++)
                    {
                        string[] textoR = reg[i].Split(',');

                        for (int j = 0; j < col.Length; j++)
                        {
                            dgvMain.Rows[i].Cells[j].Value = textoR[j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al abrir el archivo: " + ex);
            }
        }

        private void hiloMostrarDatos()
        {
            
        }

        private void frmCompleto_FormClosed(object sender, FormClosedEventArgs e)
        {
            h1.Abort();
            hpers.Abort();
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                titulo = saveFileDialog1.FileName;
            }
            hpers = new Thread(new ThreadStart(personas));
            hpers.Start();
            hpers.Join();
            h1 = new Thread(new ThreadStart(hilo1));
            h1.Start();
        }
        private void hilo1()
        {
            XDocument xmlDoc = new XDocument(new XElement("Personas"));
            xmlDoc.Root.Add(personass);
          

            xmlDoc.Save(titulo);
            MessageBox.Show("¡Guardado con exito!");

        }

       private  void personas()
        {
             personass = new XElement[dgvMain.RowCount];
            for (int j =1; j<dgvMain.RowCount-1;j++) {
                personass[j] = new XElement("Persona",
                               new XAttribute("ID",j+1),atributos(j));
            }

        }

        private XElement[] atributos(int indice ) 
        {
            int i = 0;
            XElement[] atributos = new XElement[dgvMain.ColumnCount];
            while (i < dgvMain.ColumnCount) {
                string nombrec = dgvMain[i, 0].Value.ToString().Contains("\r")? dgvMain[i, 0].Value.ToString().Replace("\r",""): dgvMain[i, 0].Value.ToString();
                atributos[i] = new XElement(nombrec, dgvMain[i, indice].Value.ToString());
                i++;
            }
            return atributos;
            
        }
    }
}
