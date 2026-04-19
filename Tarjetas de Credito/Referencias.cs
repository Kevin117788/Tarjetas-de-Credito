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
    public partial class Referencias : Form
    {
        public Referencias()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Limpiar el texto para que sea un número puro
                string limpio = txtIngresosMensuales.Text.Replace("$", "").Replace(",", "").Trim();
                double mensual = double.Parse(limpio);

                // 2. Calcular porcentaje según la tabla
                double porcentaje = 0;
                int hijos = (int)numericHijos.Value;

                if (rbSoltero.Checked)
                {
                    porcentaje = 0.80; // Soltero 80%
                }
                else if (rbCasado.Checked)
                {
                    if (hijos == 0) porcentaje = 0.70;
                    else if (hijos == 1) porcentaje = 0.60;
                    else if (hijos == 2) porcentaje = 0.55;
                    else porcentaje = 0.50; // 3 o más
                }

                // 3. Resultado de Ingreso Acumulable
                double acumulable = mensual * porcentaje;
                lblIngresoAcumulable.Text = acumulable.ToString("C2");

                // 4. Decidir la Tarjeta según el Acumulable
                if (acumulable <= 10000)
                {
                    txtPlanSugerido.Text = "BÁSICO";
                    txtPlanSugerido.BackColor = Color.LightBlue;
                }
                else if (acumulable > 10000 && acumulable <= 30000)
                {
                    txtPlanSugerido.Text = "ORO";
                    txtPlanSugerido.BackColor = Color.Gold;
                }
                else
                {
                    txtPlanSugerido.Text = "PLATINUM";
                    txtPlanSugerido.BackColor = Color.Silver;
                }
            }
            catch
            {
                MessageBox.Show("Por favor, ingresa una cantidad válida en ingresos.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlanSugerido.Text))
            {
                MessageBox.Show("Primero debes calcular tu plan sugerido.");
            }
            else
            {
                MessageBox.Show("Solicitud procesada con éxito. ¡Bienvenido a LinceCard!");
                // Aquí podrías cerrar esta ventana y volver a la principal
                this.Close();
            }
        }

        private void txtIngresosMensuales_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtIngresosMensuales.Text, out double monto))
            {
                txtIngresosMensuales.Text = monto.ToString("C2");
            }
        }
    }
    
}
