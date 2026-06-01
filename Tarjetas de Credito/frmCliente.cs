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
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();

            // Limitar los caracteres de acuerdo con límites para la base de datos
            // asumiendo un tamaño de base de datos suficiente antes de encriptar
            txtNombre2.MaxLength = 50; 
            txtCurp2.MaxLength = 18;  
            txtDomicilio2.MaxLength = 100;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurp2.Text) || string.IsNullOrWhiteSpace(txtNombre2.Text) || string.IsNullOrWhiteSpace(txtDomicilio2.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Usamos una contraseña universal interna para que encripte transparente
            try
            {
                // Configurar clave para encriptar
                string clave = "12345678";
                byte[] rc2Key = Encoding.UTF8.GetBytes(clave.PadRight(8, '0').Substring(0, 8)); 
                byte[] rc2Iv = Encoding.UTF8.GetBytes(clave.PadLeft(8, '0').Substring(0, 8));

                // Encriptar los datos
                string nombreCifrado = Convert.ToBase64String(C_RC2.Encriptar(txtNombre2.Text, rc2Key, rc2Iv));
                string curpCifrado = Convert.ToBase64String(C_RC2.Encriptar(txtCurp2.Text, rc2Key, rc2Iv));
                string domicilioCifrado = Convert.ToBase64String(C_RC2.Encriptar(txtDomicilio2.Text, rc2Key, rc2Iv));

                string connectionString = Conexion.ObtenerCadena();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Guardamos cliente encriptado
                    string queryCliente = "INSERT INTO Clientes (Nombre_Completo, Curp, Domicilio) VALUES (@Nombre, @Curp, @Domicilio)";
                    using (SqlCommand cmdCliente = new SqlCommand(queryCliente, con))
                    {
                        cmdCliente.Parameters.AddWithValue("@Nombre", nombreCifrado);
                        cmdCliente.Parameters.AddWithValue("@Curp", curpCifrado);
                        cmdCliente.Parameters.AddWithValue("@Domicilio", domicilioCifrado);
                        cmdCliente.ExecuteNonQuery();
                    }

                    MessageBox.Show("Cliente guardado y cifrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiamos los campos
                    txtCurp2.Clear();
                    txtNombre2.Clear();
                    txtDomicilio2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
