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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnAgregar;
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseña2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña2.Text))
            {
                MessageBox.Show("Por favor, termine de llenar los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = Conexion.ObtenerCadena();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Rol FROM Empleados WHERE Nombre = @Nombre AND Contraseña = @Contraseña";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@Contraseña", txtContraseña2.Text);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string rol = result.ToString();
                            
                            // Guardamos la sesión
                            SesionGlobal.UsuarioActual = txtUsuario.Text;
                            SesionGlobal.ContrasenaActual = txtContraseña2.Text;
                            SesionGlobal.RolActual = rol;
                            
                            MessageBox.Show($"Bienvenido {txtUsuario.Text}. Rol: {rol}", "Ingreso exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            frmInicio inicio = new frmInicio();
                            inicio.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
