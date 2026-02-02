namespace Ejercicio4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.btnPosicion = new System.Windows.Forms.Button();
            this.btnHttp = new System.Windows.Forms.Button();
            this.txtComun = new System.Windows.Forms.TextBox();
            this.listaBusqueda = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(37, 28);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(453, 20);
            this.txtUrl.TabIndex = 0;
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Location = new System.Drawing.Point(37, 125);
            this.btnBusqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(80, 31);
            this.btnBusqueda.TabIndex = 1;
            this.btnBusqueda.Text = "Búsqueda";
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_click);
            // 
            // btnPosicion
            // 
            this.btnPosicion.Location = new System.Drawing.Point(213, 125);
            this.btnPosicion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPosicion.Name = "btnPosicion";
            this.btnPosicion.Size = new System.Drawing.Size(80, 31);
            this.btnPosicion.TabIndex = 2;
            this.btnPosicion.Text = "Posición";
            this.btnPosicion.UseVisualStyleBackColor = true;
            this.btnPosicion.Click += new System.EventHandler(this.btnPosicion_Click);
            // 
            // btnHttp
            // 
            this.btnHttp.Location = new System.Drawing.Point(409, 125);
            this.btnHttp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHttp.Name = "btnHttp";
            this.btnHttp.Size = new System.Drawing.Size(80, 31);
            this.btnHttp.TabIndex = 3;
            this.btnHttp.Text = "Http";
            this.btnHttp.UseVisualStyleBackColor = true;
            this.btnHttp.Click += new System.EventHandler(this.btnHttp_Click);
            // 
            // txtComun
            // 
            this.txtComun.Location = new System.Drawing.Point(37, 105);
            this.txtComun.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtComun.Name = "txtComun";
            this.txtComun.Size = new System.Drawing.Size(453, 20);
            this.txtComun.TabIndex = 4;
            // 
            // listaBusqueda
            // 
            this.listaBusqueda.FormattingEnabled = true;
            this.listaBusqueda.Location = new System.Drawing.Point(37, 167);
            this.listaBusqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listaBusqueda.Name = "listaBusqueda";
            this.listaBusqueda.Size = new System.Drawing.Size(147, 160);
            this.listaBusqueda.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 333);
            this.Controls.Add(this.listaBusqueda);
            this.Controls.Add(this.txtComun);
            this.Controls.Add(this.btnHttp);
            this.Controls.Add(this.btnPosicion);
            this.Controls.Add(this.btnBusqueda);
            this.Controls.Add(this.txtUrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Ejercicio4 Bol Hilos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnBusqueda;
        private System.Windows.Forms.Button btnPosicion;
        private System.Windows.Forms.Button btnHttp;
        private System.Windows.Forms.TextBox txtComun;
        private System.Windows.Forms.ListBox listaBusqueda;
    }
}

