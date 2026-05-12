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
            btnCalcular.Click += btnCalcular_Click_1;

            // Asignar el evento click al boton de Pantalla Principal
            btnPantallaPrincipal.Click += btnPantallaPrincipal_Click_1;

            // Formatear al salir de la caja de texto
            txtSaldoDeudor.Leave += TxtSaldoDeudor_Leave;

            // Quitar el formato al entrar en la caja de texto
            txtSaldoDeudor.Enter += TxtSaldoDeudor_Enter;

            // Calcular automáticamente cuando presionen 'Enter' en el saldo deudor
            txtSaldoDeudor.KeyDown += TxtSaldoDeudor_KeyDown;

            // Asignar los valores recibidos
            txtPlanSugerido.Text = planSugerido;
            txtPlanSugerido.ReadOnly = true;

            // Hacer de solo lectura los campos donde se mostrarán los cálculos
            txt12Meses.ReadOnly = true;
            txt6Meses.ReadOnly = true;
            txt3Meses.ReadOnly = true;
            txt1Menes.ReadOnly = true;
        }

        private void TxtSaldoDeudor_Enter(object sender, EventArgs e)
        {
            // Remover símbolos de dinero para editar el número más fácilmente
            txtSaldoDeudor.Text = txtSaldoDeudor.Text.Replace("$", "").Replace(",", "").Trim();
        }

        private void TxtSaldoDeudor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Prevenir el sonido 'ding'
                e.SuppressKeyPress = true;

                // Formatea automáticamente a dinero
                TxtSaldoDeudor_Leave(this, EventArgs.Empty);

                // Llama al clic de calcular simulando que el usuario lo presionó
                btnCalcular_Click_1(this, EventArgs.Empty);
            }
        }

        private void TxtSaldoDeudor_Leave(object sender, EventArgs e)
        {
            // Formatear la caja de texto de Saldo Deudor con formato numérico de 2 decimales (sin símbolo de peso) cuando se le quita el foco
            if (double.TryParse(txtSaldoDeudor.Text.Replace("$", "").Replace(",", "").Trim(), out double cantidad))
            {
                txtSaldoDeudor.Text = cantidad.ToString("N2");
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


        private void btnCalcular_Click_1(object sender, EventArgs e)
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

                // Determinar la tasa de interés anual según el plan sugerido
                string plan = txtPlanSugerido.Text.ToUpper();
                double interesAnual = 0; // Porcentaje anual

                if (plan == "BÁSICO" || plan == "BASICO")
                {
                    interesAnual = 0.65; // 65% anual
                }
                else if (plan == "ORO")
                {
                    interesAnual = 0.55; // 55% anual
                }
                else if (plan == "PLATINUM")
                {
                    interesAnual = 0.45; // 45% anual
                }

                // Hacer cada uno de los cálculos según el interés detectado

                // A 12 Meses (100% de la tasa anual)
                double interes12 = saldoDeudor * interesAnual; 
                double total12 = saldoDeudor + interes12;
                double mensualidad12 = total12 / 12;

                txt12Meses.Text = mensualidad12.ToString("N2"); // "N2" da formato con 2 decimales sin símbolo de peso

                // A 6 Meses (Mitad del interés anual)
                double interes6 = saldoDeudor * (interesAnual / 2);
                double total6 = saldoDeudor + interes6;
                double mensualidad6 = total6 / 6;

                txt6Meses.Text = mensualidad6.ToString("N2");

                // A 3 Meses (Un cuarto del interés anual)
                double interes3 = saldoDeudor * (interesAnual / 4);
                double total3 = saldoDeudor + interes3;
                double mensualidad3 = total3 / 3;

                txt3Meses.Text = mensualidad3.ToString("N2");

                // A 1 Mes (Un doceavo del interés anual)
                double interes1 = saldoDeudor * (interesAnual / 12);
                double total1 = saldoDeudor + interes1;
                double mensualidad1 = total1; // Dividido en 1 mes

                txt1Menes.Text = mensualidad1.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Asegúrate de haber ingresado un número válido para el Saldo Deudor. Detalle: " + ex.Message);
            }
        }

        private void btnPantallaPrincipal_Click_1(object sender, EventArgs e)
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
    }
}
