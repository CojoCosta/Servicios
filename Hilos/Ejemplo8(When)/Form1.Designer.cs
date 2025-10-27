namespace Ejemplo8_When_
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
            this.btncolor = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnAny = new System.Windows.Forms.Button();
            this.resultado = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btncolor
            // 
            this.btncolor.Location = new System.Drawing.Point(40, 34);
            this.btncolor.Name = "btncolor";
            this.btncolor.Size = new System.Drawing.Size(75, 23);
            this.btncolor.TabIndex = 0;
            this.btncolor.Text = "Color";
            this.btncolor.UseVisualStyleBackColor = true;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(40, 94);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "WhenAll";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // btnAny
            // 
            this.btnAny.Location = new System.Drawing.Point(40, 150);
            this.btnAny.Name = "btnAny";
            this.btnAny.Size = new System.Drawing.Size(75, 23);
            this.btnAny.TabIndex = 2;
            this.btnAny.Text = "WhenAny";
            this.btnAny.UseVisualStyleBackColor = true;
            // 
            // resultado
            // 
            this.resultado.Location = new System.Drawing.Point(190, 34);
            this.resultado.Multiline = true;
            this.resultado.Name = "resultado";
            this.resultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultado.Size = new System.Drawing.Size(568, 139);
            this.resultado.TabIndex = 3;
            this.resultado.Text = "Resultado";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.resultado);
            this.Controls.Add(this.btnAny);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btncolor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btncolor;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnAny;
        private System.Windows.Forms.TextBox resultado;
    }
}

