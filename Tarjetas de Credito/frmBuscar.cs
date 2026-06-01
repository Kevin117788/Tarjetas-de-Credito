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
            // Ya que el dataset aparentemente se eliminó o cambió de nombre en el diseñador temporalmente,
            // llenamos el DataGridView de manera manual utilizando la nueva clase de Conexion.
            try
            {
                string connectionString = Conexion.ObtenerCadena();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT IdClientes, Nombre_Completo, Curp, Domicilio FROM Clientes";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Si el dataGridView1 no tiene columnas creadas, autogenerarlas.
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Ocultamos el campo de contraseña
            txtContraseña.Visible = false;

            // Desciframos la información encriptada contenida en el DataGridView
            DescifrarDatos();
        }

        private void DescifrarDatos()
        {
            string clave = "12345678";
            byte[] rc2Key = System.Text.Encoding.UTF8.GetBytes(clave.PadRight(8, '0').Substring(0, 8)); 
            byte[] rc2Iv = System.Text.Encoding.UTF8.GetBytes(clave.PadLeft(8, '0').Substring(0, 8));

            // Si las columnas fueron autogeneradas desde DataTable, sus nombres coincidirán con los de la tabla SQL.
            string colNombre = "Nombre_Completo";
            string colCurp = "Curp";
            string colDomicillo = "Domicilio";

            // Si estamos usando las columnas predefinidas en el diseñador (las que empiezan con minusculas), cambiaríamos los nombres, pero esto cubre el caso manual.
            if (dataGridView1.Columns.Contains("nombreCompletoDataGridViewTextBoxColumn")) colNombre = "nombreCompletoDataGridViewTextBoxColumn";
            if (dataGridView1.Columns.Contains("curpDataGridViewTextBoxColumn")) colCurp = "curpDataGridViewTextBoxColumn";
            if (dataGridView1.Columns.Contains("domicilioDataGridViewTextBoxColumn")) colDomicillo = "domicilioDataGridViewTextBoxColumn";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                if (dataGridView1.Columns.Contains(colNombre) && row.Cells[colNombre].Value != null)
                {
                    try
                    {
                        string encryptedNombre = row.Cells[colNombre].Value.ToString();
                        byte[] cipherBytes = Convert.FromBase64String(encryptedNombre);
                        row.Cells[colNombre].Value = C_RC2.Desencriptar(cipherBytes, rc2Key, rc2Iv);
                    }
                    catch { }
                }

                if (dataGridView1.Columns.Contains(colCurp) && row.Cells[colCurp].Value != null)
                {
                    try
                    {
                        string encryptedCurp = row.Cells[colCurp].Value.ToString();
                        byte[] cipherBytes = Convert.FromBase64String(encryptedCurp);
                        row.Cells[colCurp].Value = C_RC2.Desencriptar(cipherBytes, rc2Key, rc2Iv);
                    }
                    catch { }
                }

                if (dataGridView1.Columns.Contains(colDomicillo) && row.Cells[colDomicillo].Value != null)
                {
                    try
                    {
                        string encryptedDom = row.Cells[colDomicillo].Value.ToString();
                        byte[] cipherBytes = Convert.FromBase64String(encryptedDom);
                        row.Cells[colDomicillo].Value = C_RC2.Desencriptar(cipherBytes, rc2Key, rc2Iv);
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

                string colNombre = "Nombre_Completo";
                string colCurp = "Curp";
                string colDomicillo = "Domicilio";

                if (dataGridView1.Columns.Contains("nombreCompletoDataGridViewTextBoxColumn")) colNombre = "nombreCompletoDataGridViewTextBoxColumn";
                if (dataGridView1.Columns.Contains("curpDataGridViewTextBoxColumn")) colCurp = "curpDataGridViewTextBoxColumn";
                if (dataGridView1.Columns.Contains("domicilioDataGridViewTextBoxColumn")) colDomicillo = "domicilioDataGridViewTextBoxColumn";

                // Obtener los datos. OJO: Los nombres adentro del string ["Curp"], ["Nombre"], ["Domicilio"] 
                // DEBEN SER los nombres de las columnas que le pusiste a tu base de datos / datagridview
                this.CurpSeleccionado = fila.Cells[colCurp].Value?.ToString();
                this.NombreSeleccionado = fila.Cells[colNombre].Value?.ToString();
                this.DomicilioSeleccionado = fila.Cells[colDomicillo].Value?.ToString();

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
