using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
