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
    public partial class frmAdquisicion : Form
    {
        public frmAdquisicion(string planSugerido)
        {
            InitializeComponent();

            this.Load += FrmAdquisicion_Load;

            // Asignamos el evento click al boton calcular
            btnCalcular.Click += BtnCalcular_Click;

            // Asignar el evento click al boton de Pantalla Principal
            btnPantallaPrincipal.Click += BtnPantallaPrincipal_Click;

            // Asignar los valores recibidos
            txtPlanSugerido.Text = planSugerido;
            txtPlanSugerido.ReadOnly = true;
        }

        private void BtnPantallaPrincipal_Click(object sender, EventArgs e)
        {
            // Creamos una instancia del Form1 (la pantalla principal)
            frmInicio principal = new frmInicio();

            // Lo posicionamos para que aparezca fluidamente donde está el formulario actual
            principal.StartPosition = FormStartPosition.Manual;
            principal.Location = this.Location;

            // Mostramos el Form1
            principal.Show();

            // Cerramos o escondemos la ventana actual
            this.Close();
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            // Validar que el saldo deudor no esté vacío
            if (string.IsNullOrWhiteSpace(txtSaldoDeudor.Text))
            {
                MessageBox.Show("Por favor captura el saldo deudor antes de calcular.");
                return;
            }

            try
            {
                // Limpiar el texto: quitar signo $, quitar las comas y los espacios vacíos
                string saldoRaw = txtSaldoDeudor.Text.Replace("$", "").Replace(",", "").Trim();

                // Convertirlo a un número
                double saldoDeudor = double.Parse(saldoRaw);

                // Hacer cada uno de los cálculos (intereses basados en la captura de pantalla)
                // y dividir por sus respectivos meses

                // A 12 Meses (55% de interés, dividido en 12)
                double interes12 = saldoDeudor * 0.55; 
                double total12 = saldoDeudor + interes12;
                double mensualidad12 = total12 / 12;

                txt12Meses.Text = mensualidad12.ToString("C2"); // "C2" da formato de moneda con 2 decimales

                // A 6 Meses (27.5% de interés, dividido en 6)
                double interes6 = saldoDeudor * 0.275;
                double total6 = saldoDeudor + interes6;
                double mensualidad6 = total6 / 6;

                txt6Meses.Text = mensualidad6.ToString("C2");

                // A 3 Meses (13.75% de interés, dividido en 3)
                double interes3 = saldoDeudor * 0.1375;
                double total3 = saldoDeudor + interes3;
                double mensualidad3 = total3 / 3;

                txt3Meses.Text = mensualidad3.ToString("C2");

                // A 1 Mes (4.58% de interés)
                double interes1 = saldoDeudor * 0.0458;
                double total1 = saldoDeudor + interes1;
                // Dividido en 1, así que es el total
                double mensualidad1 = total1; 

                txt1Menes.Text = mensualidad1.ToString("C2"); // Tu caja de texto parece llamarse txt1Menes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Asegúrate de haber ingresado un número válido para el Saldo Deudor. Detalle: " + ex.Message);
            }
        }

        private void FrmAdquisicion_Load(object sender, EventArgs e)
        {
            // Determinar el límite de crédito según el plan sugerido
            string plan = txtPlanSugerido.Text.ToUpper();

            if (plan == "BÁSICO" || plan == "BASICO")
            {
                txtLimiteDeCredito.Text = "$20,000.00";
            }
            else if (plan == "ORO")
            {
                txtLimiteDeCredito.Text = "$50,000.00";
            }
            else if (plan == "PLATINUM")
            {
                txtLimiteDeCredito.Text = "$200,000.00";
            }
            else
            {
                txtLimiteDeCredito.Text = "$0.00";
            }

            txtLimiteDeCredito.ReadOnly = true;
        }

        private void btnPantallaPrincipal_Click(object sender, EventArgs e)
        {

        }
    }
}
