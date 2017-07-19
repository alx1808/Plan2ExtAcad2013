namespace Plan2Ext.Fenster
{
    partial class FensterOptionsControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.btnSelWidth = new System.Windows.Forms.Button();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.butSelHeight = new System.Windows.Forms.Button();
            this.lblParapet = new System.Windows.Forms.Label();
            this.txtParapet = new System.Windows.Forms.TextBox();
            this.btnSelParapet = new System.Windows.Forms.Button();
            this.lblOberlichte = new System.Windows.Forms.Label();
            this.txtOlAb = new System.Windows.Forms.TextBox();
            this.lblStaerke = new System.Windows.Forms.Label();
            this.txtStaerke = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(3, 6);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(34, 13);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "Breite";
            // 
            // txtWidth
            // 
            this.txtWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWidth.Location = new System.Drawing.Point(68, 3);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(42, 20);
            this.txtWidth.TabIndex = 1;
            this.txtWidth.Validating += new System.ComponentModel.CancelEventHandler(this.txtWidth_Validating);
            // 
            // btnSelWidth
            // 
            this.btnSelWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelWidth.Location = new System.Drawing.Point(116, 1);
            this.btnSelWidth.Name = "btnSelWidth";
            this.btnSelWidth.Size = new System.Drawing.Size(24, 22);
            this.btnSelWidth.TabIndex = 17;
            this.btnSelWidth.Text = "...";
            this.btnSelWidth.UseVisualStyleBackColor = true;
            this.btnSelWidth.Click += new System.EventHandler(this.btnSelWidth_Click);
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(3, 32);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(33, 13);
            this.lblHeight.TabIndex = 18;
            this.lblHeight.Text = "Höhe";
            // 
            // txtHeight
            // 
            this.txtHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeight.Location = new System.Drawing.Point(68, 29);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(42, 20);
            this.txtHeight.TabIndex = 19;
            this.txtHeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtHeight_Validating);
            // 
            // butSelHeight
            // 
            this.butSelHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSelHeight.Location = new System.Drawing.Point(116, 27);
            this.butSelHeight.Name = "butSelHeight";
            this.butSelHeight.Size = new System.Drawing.Size(24, 22);
            this.butSelHeight.TabIndex = 20;
            this.butSelHeight.Text = "...";
            this.butSelHeight.UseVisualStyleBackColor = true;
            this.butSelHeight.Click += new System.EventHandler(this.butSelHeight_Click);
            // 
            // lblParapet
            // 
            this.lblParapet.AutoSize = true;
            this.lblParapet.Location = new System.Drawing.Point(3, 58);
            this.lblParapet.Name = "lblParapet";
            this.lblParapet.Size = new System.Drawing.Size(44, 13);
            this.lblParapet.TabIndex = 21;
            this.lblParapet.Text = "Parapet";
            // 
            // txtParapet
            // 
            this.txtParapet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParapet.Location = new System.Drawing.Point(68, 55);
            this.txtParapet.Name = "txtParapet";
            this.txtParapet.Size = new System.Drawing.Size(42, 20);
            this.txtParapet.TabIndex = 22;
            this.txtParapet.Validating += new System.ComponentModel.CancelEventHandler(this.txtParapet_Validating);
            // 
            // btnSelParapet
            // 
            this.btnSelParapet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelParapet.Location = new System.Drawing.Point(116, 53);
            this.btnSelParapet.Name = "btnSelParapet";
            this.btnSelParapet.Size = new System.Drawing.Size(24, 22);
            this.btnSelParapet.TabIndex = 23;
            this.btnSelParapet.Text = "...";
            this.btnSelParapet.UseVisualStyleBackColor = true;
            this.btnSelParapet.Click += new System.EventHandler(this.btnSelParapet_Click);
            // 
            // lblOberlichte
            // 
            this.lblOberlichte.AutoSize = true;
            this.lblOberlichte.Location = new System.Drawing.Point(3, 84);
            this.lblOberlichte.Name = "lblOberlichte";
            this.lblOberlichte.Size = new System.Drawing.Size(36, 13);
            this.lblOberlichte.TabIndex = 24;
            this.lblOberlichte.Text = "OL ab";
            // 
            // txtOlAb
            // 
            this.txtOlAb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOlAb.Location = new System.Drawing.Point(68, 81);
            this.txtOlAb.Name = "txtOlAb";
            this.txtOlAb.Size = new System.Drawing.Size(42, 20);
            this.txtOlAb.TabIndex = 25;
            this.txtOlAb.Validating += new System.ComponentModel.CancelEventHandler(this.txtOlAb_Validating);
            // 
            // lblStaerke
            // 
            this.lblStaerke.AutoSize = true;
            this.lblStaerke.Location = new System.Drawing.Point(3, 110);
            this.lblStaerke.Name = "lblStaerke";
            this.lblStaerke.Size = new System.Drawing.Size(38, 13);
            this.lblStaerke.TabIndex = 26;
            this.lblStaerke.Text = "Stärke";
            // 
            // txtStaerke
            // 
            this.txtStaerke.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStaerke.Location = new System.Drawing.Point(68, 107);
            this.txtStaerke.Name = "txtStaerke";
            this.txtStaerke.Size = new System.Drawing.Size(42, 20);
            this.txtStaerke.TabIndex = 27;
            this.txtStaerke.Validating += new System.ComponentModel.CancelEventHandler(this.txtStaerke_Validating);
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(3, 136);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(35, 13);
            this.lblStock.TabIndex = 28;
            this.lblStock.Text = "Stock";
            // 
            // txtStock
            // 
            this.txtStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStock.Location = new System.Drawing.Point(68, 133);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(42, 20);
            this.txtStock.TabIndex = 29;
            this.txtStock.Validating += new System.ComponentModel.CancelEventHandler(this.txtStock_Validating);
            // 
            // FensterOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtStaerke);
            this.Controls.Add(this.lblStaerke);
            this.Controls.Add(this.txtOlAb);
            this.Controls.Add(this.lblOberlichte);
            this.Controls.Add(this.btnSelParapet);
            this.Controls.Add(this.txtParapet);
            this.Controls.Add(this.lblParapet);
            this.Controls.Add(this.butSelHeight);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.btnSelWidth);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.lblWidth);
            this.MinimumSize = new System.Drawing.Size(143, 164);
            this.Name = "FensterOptionsControl";
            this.Size = new System.Drawing.Size(143, 164);
            this.Load += new System.EventHandler(this.FensterOptionsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Button btnSelWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Button butSelHeight;
        private System.Windows.Forms.Label lblParapet;
        private System.Windows.Forms.Button btnSelParapet;
        private System.Windows.Forms.Label lblOberlichte;
        private System.Windows.Forms.Label lblStaerke;
        private System.Windows.Forms.Label lblStock;
        internal System.Windows.Forms.TextBox txtWidth;
        internal System.Windows.Forms.TextBox txtHeight;
        internal System.Windows.Forms.TextBox txtParapet;
        internal System.Windows.Forms.TextBox txtOlAb;
        internal System.Windows.Forms.TextBox txtStaerke;
        internal System.Windows.Forms.TextBox txtStock;
    }
}
