using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarjetas_de_Credito
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool guardadoOro = false;
        private bool guardadoPlat = false;
        private bool guardadoBasica = false;

        // Tus otras variables se quedan igual
        private Size sizeOro; private Point posOro;
        private Size sizePlat; private Point posPlat;
        private Size sizeBasica; private Point posBasica;
        private bool dimensionesGuardadas = false; // Esto nos dirá si ya guardamos los datos
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        // --- TARJETA ORO ---
        private void pictureBoxOro_MouseEnter(object sender, EventArgs e)
        {
            if (!guardadoOro) // Usamos su propia llave
            {
                sizeOro = pictureBoxOro.Size;
                posOro = pictureBoxOro.Location;
                guardadoOro = true;
            }

            int expansion = 20;
            pictureBoxOro.Size = new Size(sizeOro.Width + expansion, sizeOro.Height + expansion);
            pictureBoxOro.Location = new Point(posOro.X - (expansion / 2), posOro.Y - (expansion / 2));
            pictureBoxOro.BringToFront();
        }

        private void pictureBoxOro_MouseLeave(object sender, EventArgs e)
        {
            if (guardadoOro)
            {
                pictureBoxOro.Size = sizeOro;
                pictureBoxOro.Location = posOro;
            }
        }

        // --- TARJETA PLATINUM ---
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            if (!guardadoPlat) // Usamos su propia llave
            {
                sizePlat = pictureBoxPlatinum.Size;
                posPlat = pictureBoxPlatinum.Location;
                guardadoPlat = true;
            }

            int expansion = 20;
            pictureBoxPlatinum.Size = new Size(sizePlat.Width + expansion, sizePlat.Height + expansion);
            pictureBoxPlatinum.Location = new Point(posPlat.X - (expansion / 2), posPlat.Y - (expansion / 2));
            pictureBoxPlatinum.BringToFront();
        }

        private void pictureBoxPlatinum_MouseLeave(object sender, EventArgs e)
        {
            if (guardadoPlat)
            {
                pictureBoxPlatinum.Size = sizePlat;
                pictureBoxPlatinum.Location = posPlat;
            }
        }

        // --- TARJETA BÁSICA ---
        private void picBasica_MouseEnter(object sender, EventArgs e)
        {
            if (!guardadoBasica) // Usamos su propia llave
            {
                sizeBasica = picBasica.Size;
                posBasica = picBasica.Location;
                guardadoBasica = true;
            }

            int expansion = 20;
            picBasica.Size = new Size(sizeBasica.Width + expansion, sizeBasica.Height + expansion);
            picBasica.Location = new Point(posBasica.X - (expansion / 2), posBasica.Y - (expansion / 2));
            picBasica.BringToFront();
        }

        private void picBasica_MouseLeave(object sender, EventArgs e)
        {
            if (guardadoBasica)
            {
                picBasica.Size = sizeBasica;
                picBasica.Location = posBasica;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Cambia la visibilidad del label cada vez que el timer "suena"
            labelContinuar.Visible = !labelContinuar.Visible;
        }

        private void picBasica_Click(object sender, EventArgs e)
        {

            Referencias segundaPagina = new Referencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            segundaPagina.Show();
            this.Hide(); // Oculta la anterior

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBoxOro_Click(object sender, EventArgs e)
        {
            Referencias segundaPagina = new Referencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            segundaPagina.Show();
            this.Hide(); // Oculta la anterior

        }

        private void pictureBoxPlatinum_Click(object sender, EventArgs e)
        {
            Referencias segundaPagina = new Referencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            segundaPagina.Show();
            this.Hide(); // Oculta la anterior

        }
    }
}
