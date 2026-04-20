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

        private void btnContinuar_Click(object sender, EventArgs e)
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
    }
    
}
