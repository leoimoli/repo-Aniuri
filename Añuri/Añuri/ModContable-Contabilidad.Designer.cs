namespace Añuri
{
    partial class ModContable_Contabilidad
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.grbRetencion = new System.Windows.Forms.GroupBox();
            this.txtComprobantePropio = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvComprobantes = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNroInscripcion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIngresosBrutos = new System.Windows.Forms.TextBox();
            this.dtFechaRetencion = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAlicuota = new System.Windows.Forms.ComboBox();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.txtCuit = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TipoComprobante = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Letra = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NroFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaComprobanta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotalFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoSujetoRetencion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Retenido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalMontoRetencion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cmbOrganismo = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.retencionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaRetenciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarRetencionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarPadrónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.grbRetencion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprobantes)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retencionesToolStripMenuItem,
            this.configuracionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1099, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // grbRetencion
            // 
            this.grbRetencion.Controls.Add(this.cmbOrganismo);
            this.grbRetencion.Controls.Add(this.label13);
            this.grbRetencion.Controls.Add(this.btnGuardar);
            this.grbRetencion.Controls.Add(this.btnCancelar);
            this.grbRetencion.Controls.Add(this.txtComprobantePropio);
            this.grbRetencion.Controls.Add(this.label12);
            this.grbRetencion.Controls.Add(this.dgvComprobantes);
            this.grbRetencion.Controls.Add(this.label11);
            this.grbRetencion.Controls.Add(this.label5);
            this.grbRetencion.Controls.Add(this.txtNroInscripcion);
            this.grbRetencion.Controls.Add(this.label4);
            this.grbRetencion.Controls.Add(this.label2);
            this.grbRetencion.Controls.Add(this.label3);
            this.grbRetencion.Controls.Add(this.txtIngresosBrutos);
            this.grbRetencion.Controls.Add(this.dtFechaRetencion);
            this.grbRetencion.Controls.Add(this.label1);
            this.grbRetencion.Controls.Add(this.cmbAlicuota);
            this.grbRetencion.Controls.Add(this.txtIva);
            this.grbRetencion.Controls.Add(this.label8);
            this.grbRetencion.Controls.Add(this.txtOperacion);
            this.grbRetencion.Controls.Add(this.txtCuit);
            this.grbRetencion.Controls.Add(this.label6);
            this.grbRetencion.Controls.Add(this.label10);
            this.grbRetencion.Controls.Add(this.txtRazonSocial);
            this.grbRetencion.Controls.Add(this.label9);
            this.grbRetencion.Controls.Add(this.label7);
            this.grbRetencion.Location = new System.Drawing.Point(12, 35);
            this.grbRetencion.Name = "grbRetencion";
            this.grbRetencion.Size = new System.Drawing.Size(1044, 453);
            this.grbRetencion.TabIndex = 1;
            this.grbRetencion.TabStop = false;
            this.grbRetencion.Text = "Carga Retención";
            // 
            // txtComprobantePropio
            // 
            this.txtComprobantePropio.Enabled = false;
            this.txtComprobantePropio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtComprobantePropio.Location = new System.Drawing.Point(6, 202);
            this.txtComprobantePropio.Name = "txtComprobantePropio";
            this.txtComprobantePropio.Size = new System.Drawing.Size(234, 20);
            this.txtComprobantePropio.TabIndex = 185;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(6, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(159, 15);
            this.label12.TabIndex = 184;
            this.label12.Text = "Nro.Comprobante propio(*):";
            // 
            // dgvComprobantes
            // 
            this.dgvComprobantes.BackgroundColor = System.Drawing.Color.White;
            this.dgvComprobantes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvComprobantes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComprobantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvComprobantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComprobantes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TipoComprobante,
            this.Letra,
            this.NroFactura,
            this.FechaComprobanta,
            this.MontoTotalFactura,
            this.MontoSujetoRetencion,
            this.Retenido,
            this.TotalMontoRetencion});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComprobantes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvComprobantes.EnableHeadersVisualStyles = false;
            this.dgvComprobantes.Location = new System.Drawing.Point(6, 230);
            this.dgvComprobantes.Name = "dgvComprobantes";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComprobantes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvComprobantes.RowHeadersVisible = false;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvComprobantes.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvComprobantes.Size = new System.Drawing.Size(1032, 170);
            this.dgvComprobantes.TabIndex = 183;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(12, 168);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 13);
            this.label11.TabIndex = 51;
            this.label11.Text = "3- Datos del Comprobante";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "1- Datos del Retenido";
            // 
            // txtNroInscripcion
            // 
            this.txtNroInscripcion.Enabled = false;
            this.txtNroInscripcion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtNroInscripcion.Location = new System.Drawing.Point(851, 52);
            this.txtNroInscripcion.Name = "txtNroInscripcion";
            this.txtNroInscripcion.Size = new System.Drawing.Size(169, 20);
            this.txtNroInscripcion.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(11, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "2- Datos de la Retención";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(848, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 38;
            this.label2.Text = "Nro.Inscrip.IB:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(779, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 42;
            this.label3.Text = "Fecha Retención:";
            // 
            // txtIngresosBrutos
            // 
            this.txtIngresosBrutos.Enabled = false;
            this.txtIngresosBrutos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtIngresosBrutos.Location = new System.Drawing.Point(664, 52);
            this.txtIngresosBrutos.Name = "txtIngresosBrutos";
            this.txtIngresosBrutos.Size = new System.Drawing.Size(169, 20);
            this.txtIngresosBrutos.TabIndex = 37;
            // 
            // dtFechaRetencion
            // 
            this.dtFechaRetencion.Enabled = false;
            this.dtFechaRetencion.Location = new System.Drawing.Point(782, 127);
            this.dtFechaRetencion.Name = "dtFechaRetencion";
            this.dtFechaRetencion.Size = new System.Drawing.Size(241, 20);
            this.dtFechaRetencion.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(661, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Ing. Brutos:";
            // 
            // cmbAlicuota
            // 
            this.cmbAlicuota.Enabled = false;
            this.cmbAlicuota.FormattingEnabled = true;
            this.cmbAlicuota.Location = new System.Drawing.Point(526, 127);
            this.cmbAlicuota.Name = "cmbAlicuota";
            this.cmbAlicuota.Size = new System.Drawing.Size(226, 21);
            this.cmbAlicuota.TabIndex = 40;
            // 
            // txtIva
            // 
            this.txtIva.Enabled = false;
            this.txtIva.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtIva.Location = new System.Drawing.Point(469, 52);
            this.txtIva.Name = "txtIva";
            this.txtIva.Size = new System.Drawing.Size(169, 20);
            this.txtIva.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(466, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 15);
            this.label8.TabIndex = 34;
            this.label8.Text = "IVA:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.Enabled = false;
            this.txtOperacion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtOperacion.Location = new System.Drawing.Point(8, 127);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.Size = new System.Drawing.Size(234, 20);
            this.txtOperacion.TabIndex = 35;
            this.txtOperacion.Text = "Retención";
            // 
            // txtCuit
            // 
            this.txtCuit.Location = new System.Drawing.Point(6, 52);
            this.txtCuit.Mask = "00-00000000-0";
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(159, 20);
            this.txtCuit.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "Cuit(*):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(523, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 15);
            this.label10.TabIndex = 32;
            this.label10.Text = "Alicuota(*):";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Enabled = false;
            this.txtRazonSocial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtRazonSocial.Location = new System.Drawing.Point(188, 52);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(259, 20);
            this.txtRazonSocial.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(8, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "Operación(*):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(185, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 15);
            this.label7.TabIndex = 32;
            this.label7.Text = "Razon Social:";
            // 
            // TipoComprobante
            // 
            this.TipoComprobante.HeaderText = "TipoComprobante";
            this.TipoComprobante.Name = "TipoComprobante";
            this.TipoComprobante.Width = 200;
            // 
            // Letra
            // 
            this.Letra.HeaderText = "Letra";
            this.Letra.Name = "Letra";
            this.Letra.Width = 80;
            // 
            // NroFactura
            // 
            this.NroFactura.HeaderText = "Nro.Factura";
            this.NroFactura.Name = "NroFactura";
            this.NroFactura.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NroFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NroFactura.Width = 160;
            // 
            // FechaComprobanta
            // 
            this.FechaComprobanta.HeaderText = "Fecha Comprobanta";
            this.FechaComprobanta.Name = "FechaComprobanta";
            this.FechaComprobanta.Width = 120;
            // 
            // MontoTotalFactura
            // 
            this.MontoTotalFactura.HeaderText = "Total de la Factura";
            this.MontoTotalFactura.Name = "MontoTotalFactura";
            this.MontoTotalFactura.Width = 120;
            // 
            // MontoSujetoRetencion
            // 
            this.MontoSujetoRetencion.HeaderText = "Monto sujeto a retención";
            this.MontoSujetoRetencion.Name = "MontoSujetoRetencion";
            this.MontoSujetoRetencion.Width = 120;
            // 
            // Retenido
            // 
            this.Retenido.HeaderText = "Retenido";
            this.Retenido.Name = "Retenido";
            // 
            // TotalMontoRetencion
            // 
            this.TotalMontoRetencion.HeaderText = "Total Monto Retención";
            this.TotalMontoRetencion.Name = "TotalMontoRetencion";
            this.TotalMontoRetencion.Width = 120;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardar.Location = new System.Drawing.Point(542, 405);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(84, 40);
            this.btnGuardar.TabIndex = 187;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(423, 405);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(84, 40);
            this.btnCancelar.TabIndex = 186;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // cmbOrganismo
            // 
            this.cmbOrganismo.Enabled = false;
            this.cmbOrganismo.FormattingEnabled = true;
            this.cmbOrganismo.Location = new System.Drawing.Point(267, 127);
            this.cmbOrganismo.Name = "cmbOrganismo";
            this.cmbOrganismo.Size = new System.Drawing.Size(226, 21);
            this.cmbOrganismo.TabIndex = 189;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label13.Location = new System.Drawing.Point(264, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 15);
            this.label13.TabIndex = 188;
            this.label13.Text = "Organismo(*):";
            // 
            // retencionesToolStripMenuItem
            // 
            this.retencionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaRetenciónToolStripMenuItem,
            this.consultarRetencionesToolStripMenuItem});
            this.retencionesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.retencionesToolStripMenuItem.Image = global::Añuri.Properties.Resources.saco_de_dinero;
            this.retencionesToolStripMenuItem.Name = "retencionesToolStripMenuItem";
            this.retencionesToolStripMenuItem.Size = new System.Drawing.Size(110, 23);
            this.retencionesToolStripMenuItem.Text = "Retenciones";
            // 
            // nuevaRetenciónToolStripMenuItem
            // 
            this.nuevaRetenciónToolStripMenuItem.Image = global::Añuri.Properties.Resources.nuevo;
            this.nuevaRetenciónToolStripMenuItem.Name = "nuevaRetenciónToolStripMenuItem";
            this.nuevaRetenciónToolStripMenuItem.Size = new System.Drawing.Size(214, 24);
            this.nuevaRetenciónToolStripMenuItem.Text = "Nueva Retención";
            // 
            // consultarRetencionesToolStripMenuItem
            // 
            this.consultarRetencionesToolStripMenuItem.Image = global::Añuri.Properties.Resources.consulta;
            this.consultarRetencionesToolStripMenuItem.Name = "consultarRetencionesToolStripMenuItem";
            this.consultarRetencionesToolStripMenuItem.Size = new System.Drawing.Size(214, 24);
            this.consultarRetencionesToolStripMenuItem.Text = "Consultar Retenciones";
            // 
            // configuracionesToolStripMenuItem
            // 
            this.configuracionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importarPadrónToolStripMenuItem});
            this.configuracionesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.configuracionesToolStripMenuItem.Image = global::Añuri.Properties.Resources.engranaje;
            this.configuracionesToolStripMenuItem.Name = "configuracionesToolStripMenuItem";
            this.configuracionesToolStripMenuItem.Size = new System.Drawing.Size(135, 23);
            this.configuracionesToolStripMenuItem.Text = "Configuraciones";
            // 
            // importarPadrónToolStripMenuItem
            // 
            this.importarPadrónToolStripMenuItem.Image = global::Añuri.Properties.Resources.almacenamiento_en_la_nube;
            this.importarPadrónToolStripMenuItem.Name = "importarPadrónToolStripMenuItem";
            this.importarPadrónToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.importarPadrónToolStripMenuItem.Text = "Importar Padrón";
            this.importarPadrónToolStripMenuItem.Click += new System.EventHandler(this.importarPadrónToolStripMenuItem_Click);
            // 
            // ModContable_Contabilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 564);
            this.Controls.Add(this.grbRetencion);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModContable_Contabilidad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModContable_Contabilidad";
            this.Load += new System.EventHandler(this.ModContable_Contabilidad_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grbRetencion.ResumeLayout(false);
            this.grbRetencion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprobantes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem retencionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaRetenciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultarRetencionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarPadrónToolStripMenuItem;
        private System.Windows.Forms.GroupBox grbRetencion;
        public System.Windows.Forms.TextBox txtNroInscripcion;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtIngresosBrutos;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.MaskedTextBox txtCuit;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFechaRetencion;
        private System.Windows.Forms.ComboBox cmbAlicuota;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvComprobantes;
        public System.Windows.Forms.TextBox txtComprobantePropio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewComboBoxColumn TipoComprobante;
        private System.Windows.Forms.DataGridViewComboBoxColumn Letra;
        private System.Windows.Forms.DataGridViewTextBoxColumn NroFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaComprobanta;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotalFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoSujetoRetencion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Retenido;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalMontoRetencion;
        private System.Windows.Forms.ComboBox cmbOrganismo;
        private System.Windows.Forms.Label label13;
    }
}