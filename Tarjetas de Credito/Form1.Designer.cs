namespace Tarjetas_de_Credito
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.labelContinuar = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlatinum = new System.Windows.Forms.PictureBox();
            this.pictureBoxOro = new System.Windows.Forms.PictureBox();
            this.picBasica = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timerParpadeo = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlatinum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBasica)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(215, 315);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(791, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "PLANES DE TARJETAS DE CREDITO QUE OFRECEMOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelContinuar
            // 
            this.labelContinuar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelContinuar.AutoSize = true;
            this.labelContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContinuar.Location = new System.Drawing.Point(345, 831);
            this.labelContinuar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelContinuar.Name = "labelContinuar";
            this.labelContinuar.Size = new System.Drawing.Size(526, 29);
            this.labelContinuar.TabIndex = 7;
            this.labelContinuar.Text = "PRESIONA UNA TECLA PARA CONTINUAR";
            this.labelContinuar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labelContinuar.Click += new System.EventHandler(this.label3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = global::Tarjetas_de_Credito.Properties.Resources.logocard;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(453, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(367, 221);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxPlatinum
            // 
            this.pictureBoxPlatinum.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPlatinum.Image = global::Tarjetas_de_Credito.Properties.Resources.TARJETA_PLATINUM;
            this.pictureBoxPlatinum.Location = new System.Drawing.Point(875, 406);
            this.pictureBoxPlatinum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxPlatinum.Name = "pictureBoxPlatinum";
            this.pictureBoxPlatinum.Size = new System.Drawing.Size(337, 340);
            this.pictureBoxPlatinum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPlatinum.TabIndex = 5;
            this.pictureBoxPlatinum.TabStop = false;
            this.pictureBoxPlatinum.Click += new System.EventHandler(this.pictureBoxPlatinum_Click);
            this.pictureBoxPlatinum.MouseEnter += new System.EventHandler(this.pictureBox3_MouseEnter);
            this.pictureBoxPlatinum.MouseLeave += new System.EventHandler(this.pictureBoxPlatinum_MouseLeave);
            // 
            // pictureBoxOro
            // 
            this.pictureBoxOro.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxOro.Image = global::Tarjetas_de_Credito.Properties.Resources.TARJETA_ORO;
            this.pictureBoxOro.Location = new System.Drawing.Point(480, 406);
            this.pictureBoxOro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxOro.Name = "pictureBoxOro";
            this.pictureBoxOro.Size = new System.Drawing.Size(319, 340);
            this.pictureBoxOro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOro.TabIndex = 4;
            this.pictureBoxOro.TabStop = false;
            this.pictureBoxOro.Click += new System.EventHandler(this.pictureBoxOro_Click);
            this.pictureBoxOro.MouseEnter += new System.EventHandler(this.pictureBoxOro_MouseEnter);
            this.pictureBoxOro.MouseLeave += new System.EventHandler(this.pictureBoxOro_MouseLeave);
            // 
            // picBasica
            // 
            this.picBasica.BackColor = System.Drawing.Color.Transparent;
            this.picBasica.Image = global::Tarjetas_de_Credito.Properties.Resources.TARJETA_BASICA;
            this.picBasica.Location = new System.Drawing.Point(61, 406);
            this.picBasica.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picBasica.Name = "picBasica";
            this.picBasica.Size = new System.Drawing.Size(343, 340);
            this.picBasica.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBasica.TabIndex = 3;
            this.picBasica.TabStop = false;
            this.picBasica.Click += new System.EventHandler(this.picBasica_Click);
            this.picBasica.MouseEnter += new System.EventHandler(this.picBasica_MouseEnter);
            this.picBasica.MouseLeave += new System.EventHandler(this.picBasica_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calisto MT", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(445, 227);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(388, 28);
            this.label4.TabIndex = 9;
            this.label4.Text = "Inteligencia En Cada Movimiento.";
            // 
            // timerParpadeo
            // 
            this.timerParpadeo.Enabled = true;
            this.timerParpadeo.Interval = 600;
            this.timerParpadeo.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1273, 894);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelContinuar);
            this.Controls.Add(this.pictureBoxPlatinum);
            this.Controls.Add(this.pictureBoxOro);
            this.Controls.Add(this.picBasica);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlatinum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBasica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBasica;
        private System.Windows.Forms.PictureBox pictureBoxOro;
        private System.Windows.Forms.PictureBox pictureBoxPlatinum;
        private System.Windows.Forms.Label labelContinuar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timerParpadeo;
    }
}

