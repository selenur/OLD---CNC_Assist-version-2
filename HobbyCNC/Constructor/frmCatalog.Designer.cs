namespace CNC_Assist.Constructor
{
    partial class frmCatalog
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelposX = new System.Windows.Forms.Label();
            this.deltaZ = new System.Windows.Forms.NumericUpDown();
            this.labelposY = new System.Windows.Forms.Label();
            this.deltaY = new System.Windows.Forms.NumericUpDown();
            this.labelposZ = new System.Windows.Forms.Label();
            this.deltaX = new System.Windows.Forms.NumericUpDown();
            this.btCancel = new System.Windows.Forms.Button();
            this.btAppy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.deltaZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(97, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(164, 22);
            this.textBoxName.TabIndex = 37;
            this.textBoxName.Text = "Группа";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Имя группы:";
            // 
            // labelposX
            // 
            this.labelposX.AutoSize = true;
            this.labelposX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelposX.Location = new System.Drawing.Point(6, 21);
            this.labelposX.Name = "labelposX";
            this.labelposX.Size = new System.Drawing.Size(39, 20);
            this.labelposX.TabIndex = 28;
            this.labelposX.Text = "∆ X:";
            // 
            // deltaZ
            // 
            this.deltaZ.DecimalPlaces = 3;
            this.deltaZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deltaZ.Location = new System.Drawing.Point(101, 54);
            this.deltaZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.deltaZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.deltaZ.Name = "deltaZ";
            this.deltaZ.Size = new System.Drawing.Size(73, 26);
            this.deltaZ.TabIndex = 33;
            this.deltaZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelposY
            // 
            this.labelposY.AutoSize = true;
            this.labelposY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelposY.Location = new System.Drawing.Point(140, 21);
            this.labelposY.Name = "labelposY";
            this.labelposY.Size = new System.Drawing.Size(39, 20);
            this.labelposY.TabIndex = 29;
            this.labelposY.Text = "∆ Y:";
            // 
            // deltaY
            // 
            this.deltaY.DecimalPlaces = 3;
            this.deltaY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deltaY.Location = new System.Drawing.Point(183, 18);
            this.deltaY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.deltaY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.deltaY.Name = "deltaY";
            this.deltaY.Size = new System.Drawing.Size(73, 26);
            this.deltaY.TabIndex = 32;
            this.deltaY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelposZ
            // 
            this.labelposZ.AutoSize = true;
            this.labelposZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelposZ.Location = new System.Drawing.Point(59, 57);
            this.labelposZ.Name = "labelposZ";
            this.labelposZ.Size = new System.Drawing.Size(38, 20);
            this.labelposZ.TabIndex = 30;
            this.labelposZ.Text = "∆ Z:";
            // 
            // deltaX
            // 
            this.deltaX.DecimalPlaces = 3;
            this.deltaX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deltaX.Location = new System.Drawing.Point(49, 18);
            this.deltaX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.deltaX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.deltaX.Name = "deltaX";
            this.deltaX.Size = new System.Drawing.Size(73, 26);
            this.deltaX.TabIndex = 31;
            this.deltaX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Image = global::CNC_Assist.Properties.Resources.cancel2;
            this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancel.Location = new System.Drawing.Point(148, 125);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(118, 57);
            this.btCancel.TabIndex = 39;
            this.btCancel.Text = "Отмена";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btAppy
            // 
            this.btAppy.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAppy.Image = global::CNC_Assist.Properties.Resources.accept_button2;
            this.btAppy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAppy.Location = new System.Drawing.Point(12, 125);
            this.btAppy.Name = "btAppy";
            this.btAppy.Size = new System.Drawing.Size(114, 57);
            this.btAppy.TabIndex = 38;
            this.btAppy.Text = "Применить";
            this.btAppy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btAppy.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelposX);
            this.groupBox1.Controls.Add(this.deltaX);
            this.groupBox1.Controls.Add(this.labelposY);
            this.groupBox1.Controls.Add(this.deltaY);
            this.groupBox1.Controls.Add(this.labelposZ);
            this.groupBox1.Controls.Add(this.deltaZ);
            this.groupBox1.Location = new System.Drawing.Point(4, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 85);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Смещение данных в группе:";
            // 
            // frmCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 189);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAppy);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCatalog";
            this.Text = "Группа элементов";
            this.Load += new System.EventHandler(this.frmCatalog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deltaZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btAppy;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelposX;
        public System.Windows.Forms.NumericUpDown deltaZ;
        private System.Windows.Forms.Label labelposY;
        public System.Windows.Forms.NumericUpDown deltaY;
        private System.Windows.Forms.Label labelposZ;
        public System.Windows.Forms.NumericUpDown deltaX;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}