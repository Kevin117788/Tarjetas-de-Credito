using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarjetas_de_Credito
{
    public partial class frmReferencias : Form
    {
        // Agregamos propiedades públicas para poder asignarles valores desde otro formulario
        public string CurpCliente
        {
            get { return txtCurp.Text; }
            set { txtCurp.Text = value; }
        }

        public string NombreCliente
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string DomicilioCliente
        {
            get { return txtDomicilio.Text; }
            set { txtDomicilio.Text = value; }
        }

        public frmReferencias()
        {
            InitializeComponent();

            // Suscribir el evento Load del formulario
            this.Load += FrmReferencias_Load;

            // Formatear al salir de la caja de texto
            txtIngresosMensuales.Leave += TxtIngresosMensuales_Leave;

            // Quitar el formato al entrar (hacer clic) en la caja de texto
            txtIngresosMensuales.Enter += TxtIngresosMensuales_Enter;

            // Suscribir el evento KeyDown para calcular automáticamente al dar Enter
            txtIngresosMensuales.KeyDown += TxtIngresosMensuales_KeyDown;
        }

        private void TxtIngresosMensuales_Enter(object sender, EventArgs e)
        {
            // Remover el símbolo de peso y las comas para facilitar la edición
            txtIngresosMensuales.Text = txtIngresosMensuales.Text.Replace("$", "").Replace(",", "").Trim();
        }

        private void TxtIngresosMensuales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Previene que se haga el sonido 'ding' del sistema
                e.SuppressKeyPress = true; 

                // Forzamos el mismo proceso de formatear que se hace al salir de la caja
                TxtIngresosMensuales_Leave(this, EventArgs.Empty);

                // Mandamos a llamar la función que calcula como si le dieran al botón
                btnCalcular_Click(this, EventArgs.Empty);
            }
        }

        private void FrmReferencias_Load(object sender, EventArgs e)
        {
            // Asignar la fecha actual al campo txtFecha de forma automática
            if (txtFecha != null)
            {
                txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                // Evitar que el usuario modifique la fecha
                txtFecha.ReadOnly = true;
            }

            // Limitar la cantidad de caracteres que el usuario puede escribir en los TextBox 
            // de acuerdo con la estructura de la base de datos:

            // CURP (CHAR(18))
            if (txtCurp != null) txtCurp.MaxLength = 18;

            // Nombre Completo (VARCHAR(100))
            if (txtNombre != null) txtNombre.MaxLength = 100;

            // Domicilio (VARCHAR(200))
            if (txtDomicilio != null) txtDomicilio.MaxLength = 200;
        }

        private void TxtIngresosMensuales_Leave(object sender, EventArgs e)
        {
            // Formatear la caja de texto con formato numérico de dos decimales (sin símbolo de peso) cuando se le quita el foco
            if (double.TryParse(txtIngresosMensuales.Text.Replace("$", "").Replace(",", "").Trim(), out double cantidad))
            {
                txtIngresosMensuales.Text = cantidad.ToString("N2");
            }
        }


        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlanSugerido.Text))
            {
                MessageBox.Show("Primero debes calcular tu plan sugerido.");
            }
            else
            {
                GuardarClienteSiNoExiste();

                MessageBox.Show("Solicitud procesada con éxito. ¡Bienvenido a LinceCard!");

                // 1. Instanciar (crear) el formulario Adquisicion pasándole el plan sugerido
                frmAdquisicion ventanaAdquisicion = new frmAdquisicion(txtPlanSugerido.Text);

                // 2. Hacer que aparezca en la misma posición de la pantalla actual 
                ventanaAdquisicion.StartPosition = FormStartPosition.Manual;
                ventanaAdquisicion.Location = this.Location;

                // 3. Mostrar el formulario
                ventanaAdquisicion.Show();

                // 4. Ocultar el actual (Referencias)
                this.Hide();
            }
        }

        private void GuardarClienteSiNoExiste()
        {
            // Usamos la misma cadena de conexión que tienes configurada en el otro formulario
            string connectionString = "Data Source=Kevin;Initial Catalog=TarjetaDeCredito;User ID=sa;Password=1234567Abc;TrustServerCertificate=True";

            string curpLimpio = txtCurp.Text.Trim();

            // Validar que los campos no estén vacíos antes de intentar guardar
            if (string.IsNullOrWhiteSpace(curpLimpio) || 
                string.IsNullOrWhiteSpace(txtNombre.Text) || 
                string.IsNullOrWhiteSpace(txtDomicilio.Text))
            {
                // Si están vacíos, simplemente regresamos sin hacer nada a la BD
                return;
            }

            // Validar la base de datos (CURP es CHAR(18) o VARCHAR(18))
            if (curpLimpio.Length > 18)
            {
                MessageBox.Show("El CURP no puede tener más de 18 caracteres. Por favor verifique e intente de nuevo.");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // 1. Verificar si el cliente ya existe mediante su CURP
                    string queryVerificar = "SELECT COUNT(*) FROM Clientes WHERE Curp = @Curp";
                    using (SqlCommand comVerificar = new SqlCommand(queryVerificar, conexion))
                    {
                        comVerificar.Parameters.AddWithValue("@Curp", curpLimpio);
                        int existe = (int)comVerificar.ExecuteScalar();

                        if (existe == 0) // Si no existe, lo insertamos
                        {
                            string queryInsertar = "INSERT INTO Clientes (Nombre_Completo, Curp, Domicilio) VALUES (@Nombre, @Curp, @Domicilio)";
                            using (SqlCommand comInsertar = new SqlCommand(queryInsertar, conexion))
                            {
                                comInsertar.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                                comInsertar.Parameters.AddWithValue("@Curp", curpLimpio);
                                comInsertar.Parameters.AddWithValue("@Domicilio", txtDomicilio.Text.Trim());

                                comInsertar.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al verificar o guardar el cliente: " + ex.Message);
                }
            }
        }


        private void numericHijos_ValueChanged(object sender, EventArgs e)
        {

        }

        // Evento para abrir el formulario de búsqueda de clientes
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar ventanaBuscar = new frmBuscar();

            // Mostrar el formulario usando ShowDialog
            // Al hacer esto, si en frmBuscar establecemos DialogResult = OK en btnAgregar, 
            // este if será verdadero al cerrarse la ventana.
            if (ventanaBuscar.ShowDialog() == DialogResult.OK)
            {
                // Como frmBuscar se cerró correctamente (se le dio al boton agregar)
                // copiamos los valores de frmBuscar hacia las propiedades públicas que creamos
                this.CurpCliente = ventanaBuscar.CurpSeleccionado;
                this.NombreCliente = ventanaBuscar.NombreSeleccionado;
                this.DomicilioCliente = ventanaBuscar.DomicilioSeleccionado;
            }
        }

        // Asegúrate de enlazar este evento al "CheckedChanged" de rbSoltero en el diseñador
        private void rbSoltero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSoltero.Checked)
            {
                // Desactiva el control de hijos y reinicia el valor a 0
                numericHijos.Enabled = false;
                numericHijos.Value = 0;
            }
        }

        // Asegúrate de enlazar este evento al "CheckedChanged" de rbCasado en el diseñador

        private void rbCasado_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbCasado.Checked)
            {
                // Reactiva el control de hijos si la persona está casada
                numericHijos.Enabled = true;
            }
        }

        private void txtIngresosMensuales_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPantallaPrincipal_Click(object sender, EventArgs e)
        {
            frmInicio principal = new frmInicio();

            // Lo posicionamos para que aparezca fluidamente donde está el formulario actual
            principal.StartPosition = FormStartPosition.Manual;
            principal.Location = this.Location;

            // Mostramos el Form1
            principal.Show();

            // Cerramos o escondemos la ventana actual
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmCliente ventanaCliente = new frmCliente();
            ventanaCliente.StartPosition = FormStartPosition.CenterParent; // O Manual si quieres que comparta Location
            ventanaCliente.ShowDialog();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // Validar que los campos de CURP, Nombre y Domicilio no estén vacíos
            if (string.IsNullOrWhiteSpace(txtCurp.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDomicilio.Text))
            {
                MessageBox.Show("Los campos de CURP, Nombre y Domicilio son obligatorios. Por favor, llénelos antes de continuar.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que se haya seleccionado el estado civil
            if (!rbSoltero.Checked && !rbCasado.Checked)
            {
                MessageBox.Show("Por favor, selecciona tu estado civil antes de calcular.", "Estado Civil Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Limpiamos y obtenemos el ingreso mensual
                string textoLimpio = txtIngresosMensuales.Text.Replace("$", "").Replace(",", "").Trim();
                double mensual = double.Parse(textoLimpio);

                // 2. Calculamos el porcentaje según tu tabla de reglas
                double porcentaje = 0;
                int hijos = (int)numericHijos.Value;

                if (rbSoltero.Checked) porcentaje = 0.80;
                else if (rbCasado.Checked)
                {
                    if (hijos == 0) porcentaje = 0.70;
                    else if (hijos == 1) porcentaje = 0.60;
                    else if (hijos == 2) porcentaje = 0.55;
                    else porcentaje = 0.50;
                }

                // 3. Calculamos el ACUMULABLE (Este es el que manda)
                double acumulable = mensual * porcentaje;
                lblIngresoAcumulable.Text = acumulable.ToString("C2");

                // 4. AQUÍ ESTABA EL ERROR: Evaluamos el 'acumulable', no el 'mensual'
                // Si 40,000 * 0.50 (casado con 3 hijos) = 20,000 -> Debe ser ORO
                if (acumulable <= 5000)
                {
                    txtPlanSugerido.Text = "BÁSICO";
                    txtPlanSugerido.BackColor = Color.LightBlue;
                }
                else if (acumulable >= 5001 && acumulable <= 18000)
                {
                    txtPlanSugerido.Text = "ORO";
                    txtPlanSugerido.BackColor = Color.Gold;
                }
                else // Si es más de 30,000
                {
                    txtPlanSugerido.Text = "PLATINUM";
                    txtPlanSugerido.BackColor = Color.Silver;
                }
            }
            catch
            {
                MessageBox.Show("Escribe una cantidad válida (solo números) en el ingreso mensual.");
            }
        }
    }

}
