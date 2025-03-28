using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLEditor
{
    public partial class frmLectura : Form
    {
        public frmLectura()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogXML.ShowDialog() == DialogResult.OK)
            {
                try {
                    DataSet ds = new DataSet();
                    ds.ReadXml(openFileDialogXML.FileName);
                    if (ds != null) { 
                        dgvXML.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("No se pudo hacer la lecutra del archivo");
                    }

                } catch (Exception ex) {
                    MessageBox.Show("No se pudo leer correctamente "+ex);
                }
           
            }
        }
    }
}
