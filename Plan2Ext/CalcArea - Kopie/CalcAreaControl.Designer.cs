namespace Plan2Ext.CalcArea
{
    partial class CalcAreaControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCalcArea = new System.Windows.Forms.Button();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.lblAttribute = new System.Windows.Forms.Label();
            this.btnAG = new System.Windows.Forms.Button();
            this.txtAG = new System.Windows.Forms.TextBox();
            this.lblAG = new System.Windows.Forms.Label();
            this.btnFG = new System.Windows.Forms.Button();
            this.txtFG = new System.Windows.Forms.TextBox();
            this.lblFG = new System.Windows.Forms.Label();
            this.btnSelectBlock = new System.Windows.Forms.Button();
            this.txtBlockname = new System.Windows.Forms.TextBox();
            this.lblBlock = new System.Windows.Forms.Label();
            this.typeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeightAtt = new System.Windows.Forms.TextBox();
            this.btnSelVolAttribs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVolAtt = new System.Windows.Forms.TextBox();
            this.btnCalcVol = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCalcArea
            // 
            this.btnCalcArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalcArea.Location = new System.Drawing.Point(6, 105);
            this.btnCalcArea.Name = "btnCalcArea";
            this.btnCalcArea.Size = new System.Drawing.Size(344, 23);
            this.btnCalcArea.TabIndex = 38;
            this.btnCalcArea.Text = "Fläche rechnen";
            this.btnCalcArea.UseVisualStyleBackColor = true;
            this.btnCalcArea.Click += new System.EventHandler(this.btnCalcArea_Click);
            // 
            // txtAttribute
            // 
            this.txtAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttribute.Enabled = false;
            this.txtAttribute.Location = new System.Drawing.Point(91, 27);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(231, 20);
            this.txtAttribute.TabIndex = 37;
            this.txtAttribute.TextChanged += new System.EventHandler(this.txtAttribute_TextChanged);
            // 
            // lblAttribute
            // 
            this.lblAttribute.AutoSize = true;
            this.lblAttribute.Location = new System.Drawing.Point(3, 30);
            this.lblAttribute.Name = "lblAttribute";
            this.lblAttribute.Size = new System.Drawing.Size(66, 13);
            this.lblAttribute.TabIndex = 36;
            this.lblAttribute.Text = "Attributname";
            // 
            // btnAG
            // 
            this.btnAG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAG.Location = new System.Drawing.Point(328, 78);
            this.btnAG.Name = "btnAG";
            this.btnAG.Size = new System.Drawing.Size(24, 22);
            this.btnAG.TabIndex = 35;
            this.btnAG.Text = "...";
            this.btnAG.UseVisualStyleBackColor = true;
            this.btnAG.Click += new System.EventHandler(this.btnAG_Click);
            // 
            // txtAG
            // 
            this.txtAG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAG.Enabled = false;
            this.txtAG.Location = new System.Drawing.Point(91, 80);
            this.txtAG.Name = "txtAG";
            this.txtAG.Size = new System.Drawing.Size(231, 20);
            this.txtAG.TabIndex = 34;
            this.txtAG.TextChanged += new System.EventHandler(this.txtAG_TextChanged);
            // 
            // lblAG
            // 
            this.lblAG.AutoSize = true;
            this.lblAG.Location = new System.Drawing.Point(3, 83);
            this.lblAG.Name = "lblAG";
            this.lblAG.Size = new System.Drawing.Size(71, 13);
            this.lblAG.TabIndex = 33;
            this.lblAG.Text = "Abzugsfläche";
            // 
            // btnFG
            // 
            this.btnFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFG.Location = new System.Drawing.Point(328, 52);
            this.btnFG.Name = "btnFG";
            this.btnFG.Size = new System.Drawing.Size(24, 22);
            this.btnFG.TabIndex = 32;
            this.btnFG.Text = "...";
            this.btnFG.UseVisualStyleBackColor = true;
            this.btnFG.Click += new System.EventHandler(this.btnFG_Click);
            // 
            // txtFG
            // 
            this.txtFG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFG.Enabled = false;
            this.txtFG.Location = new System.Drawing.Point(91, 54);
            this.txtFG.Name = "txtFG";
            this.txtFG.Size = new System.Drawing.Size(231, 20);
            this.txtFG.TabIndex = 31;
            this.txtFG.TextChanged += new System.EventHandler(this.txtFG_TextChanged);
            // 
            // lblFG
            // 
            this.lblFG.AutoSize = true;
            this.lblFG.Location = new System.Drawing.Point(3, 57);
            this.lblFG.Name = "lblFG";
            this.lblFG.Size = new System.Drawing.Size(77, 13);
            this.lblFG.TabIndex = 30;
            this.lblFG.Text = "Flächengrenze";
            // 
            // btnSelectBlock
            // 
            this.btnSelectBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectBlock.Location = new System.Drawing.Point(328, 2);
            this.btnSelectBlock.Name = "btnSelectBlock";
            this.btnSelectBlock.Size = new System.Drawing.Size(24, 22);
            this.btnSelectBlock.TabIndex = 29;
            this.btnSelectBlock.Text = "...";
            this.btnSelectBlock.UseVisualStyleBackColor = true;
            this.btnSelectBlock.Click += new System.EventHandler(this.btnSelectBlock_Click);
            // 
            // txtBlockname
            // 
            this.txtBlockname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBlockname.Enabled = false;
            this.txtBlockname.Location = new System.Drawing.Point(91, 4);
            this.txtBlockname.Name = "txtBlockname";
            this.txtBlockname.Size = new System.Drawing.Size(231, 20);
            this.txtBlockname.TabIndex = 28;
            this.txtBlockname.TextChanged += new System.EventHandler(this.txtBlockname_TextChanged);
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.Location = new System.Drawing.Point(3, 7);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(60, 13);
            this.lblBlock.TabIndex = 27;
            this.lblBlock.Text = "Blockname";
            // 
            // typeTextBox
            // 
            this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeTextBox.Location = new System.Drawing.Point(7, 228);
            this.typeTextBox.Multiline = true;
            this.typeTextBox.Name = "typeTextBox";
            this.typeTextBox.ReadOnly = true;
            this.typeTextBox.Size = new System.Drawing.Size(344, 137);
            this.typeTextBox.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Höhenattribut";
            // 
            // txtHeightAtt
            // 
            this.txtHeightAtt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeightAtt.Enabled = false;
            this.txtHeightAtt.Location = new System.Drawing.Point(91, 134);
            this.txtHeightAtt.Name = "txtHeightAtt";
            this.txtHeightAtt.Size = new System.Drawing.Size(231, 20);
            this.txtHeightAtt.TabIndex = 42;
            // 
            // btnSelVolAttribs
            // 
            this.btnSelVolAttribs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelVolAttribs.Location = new System.Drawing.Point(327, 132);
            this.btnSelVolAttribs.Name = "btnSelVolAttribs";
            this.btnSelVolAttribs.Size = new System.Drawing.Size(24, 22);
            this.btnSelVolAttribs.TabIndex = 43;
            this.btnSelVolAttribs.Text = "...";
            this.btnSelVolAttribs.UseVisualStyleBackColor = true;
            this.btnSelVolAttribs.Click += new System.EventHandler(this.btnSelVolAttribs_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Volumsattribut";
            // 
            // txtVolAtt
            // 
            this.txtVolAtt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVolAtt.Enabled = false;
            this.txtVolAtt.Location = new System.Drawing.Point(91, 160);
            this.txtVolAtt.Name = "txtVolAtt";
            this.txtVolAtt.Size = new System.Drawing.Size(231, 20);
            this.txtVolAtt.TabIndex = 45;
            // 
            // btnCalcVol
            // 
            this.btnCalcVol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalcVol.Location = new System.Drawing.Point(6, 186);
            this.btnCalcVol.Name = "btnCalcVol";
            this.btnCalcVol.Size = new System.Drawing.Size(344, 23);
            this.btnCalcVol.TabIndex = 46;
            this.btnCalcVol.Text = "Volumen berechnen";
            this.btnCalcVol.UseVisualStyleBackColor = true;
            this.btnCalcVol.Click += new System.EventHandler(this.btnCalcVol_Click);
            // 
            // CalcAreaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCalcVol);
            this.Controls.Add(this.txtVolAtt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelVolAttribs);
            this.Controls.Add(this.txtHeightAtt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeTextBox);
            this.Controls.Add(this.btnCalcArea);
            this.Controls.Add(this.txtAttribute);
            this.Controls.Add(this.lblAttribute);
            this.Controls.Add(this.btnAG);
            this.Controls.Add(this.txtAG);
            this.Controls.Add(this.lblAG);
            this.Controls.Add(this.btnFG);
            this.Controls.Add(this.txtFG);
            this.Controls.Add(this.lblFG);
            this.Controls.Add(this.btnSelectBlock);
            this.Controls.Add(this.txtBlockname);
            this.Controls.Add(this.lblBlock);
            this.Name = "CalcAreaControl";
            this.Size = new System.Drawing.Size(354, 368);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalcArea;
        public System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.Label lblAttribute;
        private System.Windows.Forms.Button btnAG;
        public System.Windows.Forms.TextBox txtAG;
        private System.Windows.Forms.Label lblAG;
        private System.Windows.Forms.Button btnFG;
        public System.Windows.Forms.TextBox txtFG;
        private System.Windows.Forms.Label lblFG;
        private System.Windows.Forms.Button btnSelectBlock;
        public System.Windows.Forms.TextBox txtBlockname;
        private System.Windows.Forms.Label lblBlock;
        public System.Windows.Forms.TextBox typeTextBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtHeightAtt;
        private System.Windows.Forms.Button btnSelVolAttribs;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtVolAtt;
        private System.Windows.Forms.Button btnCalcVol;
    }
}
