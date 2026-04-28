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

            // Asignar los valores recibidos
            txtPlanSugerido.Text = planSugerido;
            txtPlanSugerido.ReadOnly = true;
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
    }
}
