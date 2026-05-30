using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Tarjetas_de_Credito
{
    public partial class frmBuscar : Form
    {
        // Propiedades públicas para almacenar temporalmente los datos seleccionados del DataGridView
        // Esto permite que el formulario de Referencias pueda leerlos.
        public string CurpSeleccionado { get; private set; }
        public string NombreSeleccionado { get; private set; }
        public string DomicilioSeleccionado { get; private set; }

        public frmBuscar()
        {
            InitializeComponent();
        }

        private void frmBuscar_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'tarjetaDeCreditoDataSet.Clientes' Puede moverla o quitarla según sea necesario.
            this.clientesTableAdapter.Fill(this.tarjetaDeCreditoDataSet.Clientes);

            // Ocultamos el campo de contraseña ya que ahora es automático
            txtContraseña.Visible = false;

            // Descifrar toda la información para presentarla automáticamente
            DescifrarDatos();
        }

        private void DescifrarDatos()
        {
            string clave = "12345678";
            byte[] rc2Key = System.Text.Encoding.UTF8.GetBytes(clave.PadRight(8, '0').Substring(0, 8)); 
            byte[] rc2Iv = System.Text.Encoding.UTF8.GetBytes(clave.PadLeft(8, '0').Substring(0, 8));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["nombreCompletoDataGridViewTextBoxColumn"].Value != null)
                {
                    try
                    {
                        string encryptedNombre = row.Cells["nombreCompletoDataGridViewTextBoxColumn"].Value.ToString();
                        byte[] cipherBytes = Convert.FromBase64String(encryptedNombre);
                        row.Cells["nombreCompletoDataGridViewTextBoxColumn"].Value = C_RC2.Desencriptar(cipherBytes, rc2Key, rc2Iv);
                    }
                    catch { }
                }

                if (row.Cells["curpDataGridViewTextBoxColumn"].Value != null)
                {
                    try
                    {
                        string encryptedCurp = row.Cells["curpDataGridViewTextBoxColumn"].Value.ToString();
                        byte[] cipherBytes = Convert.FromBase64String(encryptedCurp);
                        row.Cells["curpDataGridViewTextBoxColumn"].Value = C_RC2.Desencriptar(cipherBytes, rc2Key, rc2Iv);
                    }
                    catch { }
                }

                if (row.Cells["domicilioDataGridViewTextBoxColumn"].Value != null)
                {
                    try
                    {
                        string encryptedDom = row.Cells["domicilioDataGridViewTextBoxColumn"].Value.ToString();
                        byte[] cipherBytes = Convert.FromBase64String(encryptedDom);
                        row.Cells["domicilioDataGridViewTextBoxColumn"].Value = C_RC2.Desencriptar(cipherBytes, rc2Key, rc2Iv);
                    }
                    catch { }
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Asegurarnos de que el usuario haya seleccionado una fila en el DataGridView
            // Asumiendo que tu tabla se llama dataGridView1. Cámbiale el nombre si es distinto.
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow fila = dataGridView1.SelectedRows[0];

                // Obtener los datos. OJO: Los nombres adentro del string ["Curp"], ["Nombre"], ["Domicilio"] 
                // DEBEN SER los nombres de las columnas que le pusiste a tu base de datos / datagridview
                this.CurpSeleccionado = fila.Cells["curpDataGridViewTextBoxColumn"].Value.ToString();
                this.NombreSeleccionado = fila.Cells["nombreCompletoDataGridViewTextBoxColumn"].Value.ToString();
                this.DomicilioSeleccionado = fila.Cells["domicilioDataGridViewTextBoxColumn"].Value.ToString();

                // Le dice al programa que devolvemos un 'resultado OK'
                this.DialogResult = DialogResult.OK; 

                // Cerramos el frmBuscar automáticamente después de agregar
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un cliente de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            // Ignorado intencionalmente (la desencriptación ahora es automática al cargar).
        }

    }
}
