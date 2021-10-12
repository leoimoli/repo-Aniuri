namespace Añuri
{
    partial class InventarioMaterialesPesosWF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.idObra = new System.Windows.Forms.Label();
            this.ImagenPagina = new System.Windows.Forms.PictureBox();
            this.lblPantalla = new System.Windows.Forms.Label();
            this.btnRestaurar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.FiltroInventario = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.dtFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bntInventario = new System.Windows.Forms.Button();
            this.btnMaterialesEnPesos = new System.Windows.Forms.Button();
            this.btnMaterialesKilos = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagenPagina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.FiltroInventario.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.panel1.Controls.Add(this.idObra);
            this.panel1.Controls.Add(this.ImagenPagina);
            this.panel1.Controls.Add(this.lblPantalla);
            this.panel1.Controls.Add(this.btnRestaurar);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Controls.Add(this.btnMaximizar);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 35);
            this.panel1.TabIndex = 5;
            // 
            // idObra
            // 
            this.idObra.AutoSize = true;
            this.idObra.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idObra.ForeColor = System.Drawing.Color.White;
            this.idObra.Location = new System.Drawing.Point(190, 7);
            this.idObra.Name = "idObra";
            this.idObra.Size = new System.Drawing.Size(64, 19);
            this.idObra.TabIndex = 7;
            this.idObra.Text = "idObra";
            this.idObra.Visible = false;
            // 
            // ImagenPagina
            // 
            this.ImagenPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ImagenPagina.Image = global::Añuri.Properties.Resources.informe_empresarial_con_crecimiento__1_;
            this.ImagenPagina.Location = new System.Drawing.Point(10, 1);
            this.ImagenPagina.Name = "ImagenPagina";
            this.ImagenPagina.Size = new System.Drawing.Size(32, 32);
            this.ImagenPagina.TabIndex = 3;
            this.ImagenPagina.TabStop = false;
            // 
            // lblPantalla
            // 
            this.lblPantalla.AutoSize = true;
            this.lblPantalla.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPantalla.ForeColor = System.Drawing.Color.White;
            this.lblPantalla.Location = new System.Drawing.Point(47, 7);
            this.lblPantalla.Name = "lblPantalla";
            this.lblPantalla.Size = new System.Drawing.Size(113, 19);
            this.lblPantalla.TabIndex = 4;
            this.lblPantalla.Text = "Reporte Stock";
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurar.Image = global::Añuri.Properties.Resources.restaurar;
            this.btnRestaurar.Location = new System.Drawing.Point(1170, 3);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(25, 25);
            this.btnRestaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRestaurar.TabIndex = 6;
            this.btnRestaurar.TabStop = false;
            this.btnRestaurar.Visible = false;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = global::Añuri.Properties.Resources.menos__2_1;
            this.btnMinimizar.Location = new System.Drawing.Point(1201, 3);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 25);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 4;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Visible = false;
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.Image = global::Añuri.Properties.Resources.maximizar1;
            this.btnMaximizar.Location = new System.Drawing.Point(1232, 3);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(25, 25);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizar.TabIndex = 5;
            this.btnMaximizar.TabStop = false;
            this.btnMaximizar.Visible = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = global::Añuri.Properties.Resources.cancelar2;
            this.btnCerrar.Location = new System.Drawing.Point(1263, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.TabStop = false;
            // 
            // FiltroInventario
            // 
            this.FiltroInventario.BackColor = System.Drawing.Color.Gainsboro;
            this.FiltroInventario.Controls.Add(this.btnBuscar);
            this.FiltroInventario.Controls.Add(this.label10);
            this.FiltroInventario.Controls.Add(this.dtFechaHasta);
            this.FiltroInventario.Location = new System.Drawing.Point(332, 83);
            this.FiltroInventario.Name = "FiltroInventario";
            this.FiltroInventario.Size = new System.Drawing.Size(626, 74);
            this.FiltroInventario.TabIndex = 177;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Image = global::Añuri.Properties.Resources.buscar__2_;
            this.btnBuscar.Location = new System.Drawing.Point(574, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(37, 34);
            this.btnBuscar.TabIndex = 171;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(118, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 17);
            this.label10.TabIndex = 169;
            this.label10.Text = "Informe Stock al:";
            // 
            // dtFechaHasta
            // 
            this.dtFechaHasta.Location = new System.Drawing.Point(249, 20);
            this.dtFechaHasta.Name = "dtFechaHasta";
            this.dtFechaHasta.Size = new System.Drawing.Size(200, 20);
            this.dtFechaHasta.TabIndex = 167;
            this.dtFechaHasta.Value = new System.DateTime(2021, 10, 12, 12, 40, 44, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label2.Location = new System.Drawing.Point(334, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 176;
            this.label2.Text = "Filtros de Busqueda";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.bntInventario);
            this.panel3.Controls.Add(this.btnMaterialesEnPesos);
            this.panel3.Controls.Add(this.btnMaterialesKilos);
            this.panel3.Location = new System.Drawing.Point(10, 202);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(202, 224);
            this.panel3.TabIndex = 181;
            // 
            // bntInventario
            // 
            this.bntInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.bntInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntInventario.FlatAppearance.BorderSize = 0;
            this.bntInventario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.bntInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntInventario.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntInventario.ForeColor = System.Drawing.Color.White;
            this.bntInventario.Image = global::Añuri.Properties.Resources.inventario;
            this.bntInventario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntInventario.Location = new System.Drawing.Point(3, 15);
            this.bntInventario.Name = "bntInventario";
            this.bntInventario.Size = new System.Drawing.Size(186, 49);
            this.bntInventario.TabIndex = 27;
            this.bntInventario.Text = "Inventario";
            this.bntInventario.UseVisualStyleBackColor = false;
            this.bntInventario.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnMaterialesEnPesos
            // 
            this.btnMaterialesEnPesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnMaterialesEnPesos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaterialesEnPesos.FlatAppearance.BorderSize = 0;
            this.btnMaterialesEnPesos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btnMaterialesEnPesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialesEnPesos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialesEnPesos.ForeColor = System.Drawing.Color.White;
            this.btnMaterialesEnPesos.Image = global::Añuri.Properties.Resources.dar_dinero;
            this.btnMaterialesEnPesos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaterialesEnPesos.Location = new System.Drawing.Point(2, 147);
            this.btnMaterialesEnPesos.Name = "btnMaterialesEnPesos";
            this.btnMaterialesEnPesos.Size = new System.Drawing.Size(187, 49);
            this.btnMaterialesEnPesos.TabIndex = 25;
            this.btnMaterialesEnPesos.Text = "Materiales en Pesos";
            this.btnMaterialesEnPesos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMaterialesEnPesos.UseVisualStyleBackColor = false;
            this.btnMaterialesEnPesos.Click += new System.EventHandler(this.btnMaterialesEnPesos_Click);
            // 
            // btnMaterialesKilos
            // 
            this.btnMaterialesKilos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnMaterialesKilos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaterialesKilos.FlatAppearance.BorderSize = 0;
            this.btnMaterialesKilos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btnMaterialesKilos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialesKilos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialesKilos.ForeColor = System.Drawing.Color.White;
            this.btnMaterialesKilos.Image = global::Añuri.Properties.Resources.peso;
            this.btnMaterialesKilos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaterialesKilos.Location = new System.Drawing.Point(2, 83);
            this.btnMaterialesKilos.Name = "btnMaterialesKilos";
            this.btnMaterialesKilos.Size = new System.Drawing.Size(187, 49);
            this.btnMaterialesKilos.TabIndex = 23;
            this.btnMaterialesKilos.Text = "Materiales en Kilos";
            this.btnMaterialesKilos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMaterialesKilos.UseVisualStyleBackColor = false;
            this.btnMaterialesKilos.Click += new System.EventHandler(this.btnMaterialesKilos_Click);
            // 
            // InventarioMaterialesPesosWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 650);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.FiltroInventario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InventarioMaterialesPesosWF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventarioMaterialesPesosWF";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagenPagina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.FiltroInventario.ResumeLayout(false);
            this.FiltroInventario.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label idObra;
        private System.Windows.Forms.PictureBox ImagenPagina;
        private System.Windows.Forms.Label lblPantalla;
        private System.Windows.Forms.PictureBox btnRestaurar;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Panel FiltroInventario;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtFechaHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bntInventario;
        private System.Windows.Forms.Button btnMaterialesEnPesos;
        private System.Windows.Forms.Button btnMaterialesKilos;
    }
}