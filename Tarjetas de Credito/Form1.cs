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

            // 1. Crear una nueva instancia de ToolTip
            ToolTip toolTipTarjetas = new ToolTip();

            // 2. Opcional: Configurar los tiempos (en milisegundos)
            toolTipTarjetas.AutoPopDelay = 8000;  // Tiempo que el mensaje permanece visible (8 segundos)
            toolTipTarjetas.InitialDelay = 300;   // Tiempo que tarda en aparecer el mensaje (0.3 segundos)
            toolTipTarjetas.ReshowDelay = 200;    // Tiempo que tarda en aparecer entre controles
            toolTipTarjetas.ShowAlways = true;    // Forzar a que se muestre incluso si la ventana no está activa

            // Habilitar dibujo personalizado (para cambiar tamaño y fuente)
            toolTipTarjetas.OwnerDraw = true;
            toolTipTarjetas.Draw += ToolTipTarjetas_Draw;
            toolTipTarjetas.Popup += ToolTipTarjetas_Popup;

            // 3. Asignar el texto a cada una de tus tarjetas (PictureBox)
            toolTipTarjetas.SetToolTip(this.picBasica, "BASICO. Límite de crédito $20,000.00 con tasa de interés del 65% anual.");
            toolTipTarjetas.SetToolTip(this.pictureBoxOro, "ORO. Límite de crédito $50,000.00 con tasa de interés del 55% anual.");
            toolTipTarjetas.SetToolTip(this.pictureBoxPlatinum, "PLATINUM. Límite de crédito $200,000.00 con tasa de interés del 45% anual.");
        }

        // Definimos la fuente (tamaño 12, negrita)
        private Font fontToolTip = new Font("Arial", 11f, FontStyle.Bold);

        private void ToolTipTarjetas_Popup(object sender, PopupEventArgs e)
        {
            // Calcula el tamaño necesario para el texto con la fuente más grande
            Size textSize = TextRenderer.MeasureText(
                ((ToolTip)sender).GetToolTip(e.AssociatedControl),
                fontToolTip
            );

            // Añade un poco de relleno adicional (padding)
            e.ToolTipSize = new Size(textSize.Width + 10, textSize.Height + 10);
        }

        private void ToolTipTarjetas_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Dibujar fondo (puedes cambiar colores aquí)
            e.Graphics.FillRectangle(SystemBrushes.Info, e.Bounds);
            // Dibujar borde
            e.DrawBorder();

            // Dibujar el texto grande
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(e.ToolTipText, fontToolTip, SystemBrushes.InfoText, e.Bounds, sf);
            }
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

        private void picBasica_Click_1(object sender, EventArgs e)
        {
            frmReferencias segundaPagina = new frmReferencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            segundaPagina.Show();
            this.Hide(); // Oculta la anterior
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Aseguramos que el formulario pueda detectar pulsaciones de teclas sin importar
            // qué control tenga el foco
            this.KeyPreview = true;
        }

        // Agrega este método para detectar la pulsación de la tecla
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            frmReferencias segundaPagina = new frmReferencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            // Mostrar el nuevo formulario y ocultar el actual
            segundaPagina.Show();
            this.Hide(); 
        }

        private void pictureBoxOro_Click_1(object sender, EventArgs e)
        {
            frmReferencias segundaPagina = new frmReferencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            segundaPagina.Show();
            this.Hide(); // Oculta la anterior
        }

        private void pictureBoxPlatinum_Click_1(object sender, EventArgs e)
        {
            frmReferencias segundaPagina = new frmReferencias();

            // Le decimos que use la misma ubicación (Location) que la ventana actual
            segundaPagina.StartPosition = FormStartPosition.Manual;
            segundaPagina.Location = this.Location;

            segundaPagina.Show();
            this.Hide(); // Oculta la anterior
        }
    }
}
