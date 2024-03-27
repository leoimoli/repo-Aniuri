namespace Añuri
{
    partial class ModContable_CargaMasivaProveedoresWF
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
            this.components = new System.ComponentModel.Container();
            this.btnCargaMasiva = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImagenPagina = new System.Windows.Forms.PictureBox();
            this.lblPantalla = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblContador = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NroFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NroDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoCambio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpNetoGravado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpNetoNoGravado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpOpExentas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercepIngBrutos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagenPagina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCargaMasiva
            // 
            this.btnCargaMasiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargaMasiva.Image = global::Añuri.Properties.Resources.importar__1_;
            this.btnCargaMasiva.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCargaMasiva.Location = new System.Drawing.Point(334, 389);
            this.btnCargaMasiva.Name = "btnCargaMasiva";
            this.btnCargaMasiva.Size = new System.Drawing.Size(80, 51);
            this.btnCargaMasiva.TabIndex = 82;
            this.btnCargaMasiva.Text = "Importar";
            this.btnCargaMasiva.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCargaMasiva.UseVisualStyleBackColor = true;
            this.btnCargaMasiva.Click += new System.EventHandler(this.btnCargaMasiva_Click);
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.Image = global::Añuri.Properties.Resources.buscar__1_;
            this.btnCargarArchivo.Location = new System.Drawing.Point(458, 14);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(46, 35);
            this.btnCargarArchivo.TabIndex = 43;
            this.toolTip1.SetToolTip(this.btnCargarArchivo, "Buscar Archivos");
            this.btnCargarArchivo.UseVisualStyleBackColor = true;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Image = global::Añuri.Properties.Resources.volver_flecha_izquierda;
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVolver.Location = new System.Drawing.Point(193, 389);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(80, 51);
            this.btnVolver.TabIndex = 81;
            this.btnVolver.Text = "Volver";
            this.btnVolver.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.panel1.Controls.Add(this.ImagenPagina);
            this.panel1.Controls.Add(this.lblPantalla);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 35);
            this.panel1.TabIndex = 79;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuCabecera_MouseDown);
            // 
            // ImagenPagina
            // 
            this.ImagenPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ImagenPagina.Image = global::Añuri.Properties.Resources.importar;
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
            this.lblPantalla.Size = new System.Drawing.Size(137, 19);
            this.lblPantalla.TabIndex = 4;
            this.lblPantalla.Text = "Importar padrón";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = global::Añuri.Properties.Resources.cancelar2;
            this.btnCerrar.Location = new System.Drawing.Point(603, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCargaMasiva);
            this.groupBox2.Controls.Add(this.lblContador);
            this.groupBox2.Controls.Add(this.btnVolver);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtRuta);
            this.groupBox2.Controls.Add(this.btnCargarArchivo);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(616, 446);
            this.groupBox2.TabIndex = 80;
            this.groupBox2.TabStop = false;
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblContador.Location = new System.Drawing.Point(515, 339);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(18, 19);
            this.lblContador.TabIndex = 99;
            this.lblContador.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.label1.Location = new System.Drawing.Point(336, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 19);
            this.label1.TabIndex = 98;
            this.label1.Text = "Total de Proveedores:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(106, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 17);
            this.label6.TabIndex = 97;
            this.label6.Text = "Buscar Archivo:";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(219, 22);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(217, 20);
            this.txtRuta.TabIndex = 44;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(143, 220);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(330, 23);
            this.progressBar1.TabIndex = 42;
            this.progressBar1.Value = 50;
            this.progressBar1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.Tipo,
            this.NroFactura,
            this.TipoDoc,
            this.NroDoc,
            this.Proveedor,
            this.TipoCambio,
            this.Moneda,
            this.ImpNetoGravado,
            this.ImpNetoNoGravado,
            this.ImpOpExentas,
            this.PercIva,
            this.PercepIngBrutos});
            this.dataGridView1.Location = new System.Drawing.Point(6, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(601, 266);
            this.dataGridView1.TabIndex = 42;
            this.dataGridView1.Visible = false;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Razon Social";
            this.Fecha.Name = "Fecha";
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Pais";
            this.Tipo.Name = "Tipo";
            // 
            // NroFactura
            // 
            this.NroFactura.HeaderText = "Provincia";
            this.NroFactura.Name = "NroFactura";
            // 
            // TipoDoc
            // 
            this.TipoDoc.HeaderText = "Localidad";
            this.TipoDoc.Name = "TipoDoc";
            // 
            // NroDoc
            // 
            this.NroDoc.HeaderText = "Domicilio";
            this.NroDoc.Name = "NroDoc";
            // 
            // Proveedor
            // 
            this.Proveedor.HeaderText = "Altura";
            this.Proveedor.Name = "Proveedor";
            // 
            // TipoCambio
            // 
            this.TipoCambio.HeaderText = "Piso";
            this.TipoCambio.Name = "TipoCambio";
            // 
            // Moneda
            // 
            this.Moneda.HeaderText = "Departamento";
            this.Moneda.Name = "Moneda";
            // 
            // ImpNetoGravado
            // 
            this.ImpNetoGravado.HeaderText = "Codigo Postal";
            this.ImpNetoGravado.Name = "ImpNetoGravado";
            // 
            // ImpNetoNoGravado
            // 
            this.ImpNetoNoGravado.HeaderText = "Sit.Iva";
            this.ImpNetoNoGravado.Name = "ImpNetoNoGravado";
            // 
            // ImpOpExentas
            // 
            this.ImpOpExentas.HeaderText = "NroDocumento";
            this.ImpOpExentas.Name = "ImpOpExentas";
            // 
            // PercIva
            // 
            this.PercIva.HeaderText = "Ingresos Brutos";
            this.PercIva.Name = "PercIva";
            // 
            // PercepIngBrutos
            // 
            this.PercepIngBrutos.HeaderText = "Numero IB";
            this.PercepIngBrutos.Name = "PercepIngBrutos";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ModContable_CargaMasivaProveedoresWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 514);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModContable_CargaMasivaProveedoresWF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModContable_CargaMasivaProveedoresWF";
            this.Load += new System.EventHandler(this.ModContable_CargaMasivaProveedoresWF_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagenPagina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCargaMasiva;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ImagenPagina;
        private System.Windows.Forms.Label lblPantalla;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NroFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn NroDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoCambio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Moneda;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpNetoGravado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpNetoNoGravado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpOpExentas;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercIva;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercepIngBrutos;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblContador;
    }
}