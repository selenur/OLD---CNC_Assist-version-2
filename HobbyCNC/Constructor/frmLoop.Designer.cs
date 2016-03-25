namespace CNC_Assist.Constructor
{
    partial class frmLoop
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbZ = new System.Windows.Forms.CheckBox();
            this.cbY = new System.Windows.Forms.CheckBox();
            this.cbX = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.numStop = new System.Windows.Forms.NumericUpDown();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbZ);
            this.groupBox1.Controls.Add(this.cbY);
            this.groupBox1.Controls.Add(this.cbX);
            this.groupBox1.Location = new System.Drawing.Point(14, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 50);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Применять для координат:";
            // 
            // cbZ
            // 
            this.cbZ.AutoSize = true;
            this.cbZ.Location = new System.Drawing.Point(197, 23);
            this.cbZ.Name = "cbZ";
            this.cbZ.Size = new System.Drawing.Size(33, 17);
            this.cbZ.TabIndex = 2;
            this.cbZ.Text = "Z";
            this.cbZ.UseVisualStyleBackColor = true;
            // 
            // cbY
            // 
            this.cbY.AutoSize = true;
            this.cbY.Location = new System.Drawing.Point(109, 23);
            this.cbY.Name = "cbY";
            this.cbY.Size = new System.Drawing.Size(33, 17);
            this.cbY.TabIndex = 1;
            this.cbY.Text = "Y";
            this.cbY.UseVisualStyleBackColor = true;
            // 
            // cbX
            // 
            this.cbX.AutoSize = true;
            this.cbX.Location = new System.Drawing.Point(19, 23);
            this.cbX.Name = "cbX";
            this.cbX.Size = new System.Drawing.Size(33, 17);
            this.cbX.TabIndex = 0;
            this.cbX.Text = "X";
            this.cbX.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Шаг:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Конечное значение:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Начальное значение:";
            // 
            // numStep
            // 
            this.numStep.DecimalPlaces = 3;
            this.numStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numStep.Location = new System.Drawing.Point(188, 81);
            this.numStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numStep.Name = "numStep";
            this.numStep.Size = new System.Drawing.Size(73, 22);
            this.numStep.TabIndex = 45;
            this.numStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numStop
            // 
            this.numStop.DecimalPlaces = 3;
            this.numStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numStop.Location = new System.Drawing.Point(188, 57);
            this.numStop.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStop.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numStop.Name = "numStop";
            this.numStop.Size = new System.Drawing.Size(73, 22);
            this.numStop.TabIndex = 44;
            this.numStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numStop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numStart
            // 
            this.numStart.DecimalPlaces = 3;
            this.numStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numStart.Location = new System.Drawing.Point(188, 33);
            this.numStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStart.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(73, 22);
            this.numStart.TabIndex = 43;
            this.numStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(55, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(206, 22);
            this.textBoxName.TabIndex = 40;
            this.textBoxName.Text = "Цикл";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "Имя:";
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Image = global::CNC_Assist.Properties.Resources.cancel2;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(155, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 57);
            this.button3.TabIndex = 42;
            this.button3.Text = "Отмена";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = global::CNC_Assist.Properties.Resources.accept_button2;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(12, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 57);
            this.button1.TabIndex = 41;
            this.button1.Text = "Применить";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmLoop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 226);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numStep);
            this.Controls.Add(this.numStop);
            this.Controls.Add(this.numStart);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLoop";
            this.Text = "Циклическая операция";
            this.Load += new System.EventHandler(this.frmLoop_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox cbZ;
        public System.Windows.Forms.CheckBox cbY;
        public System.Windows.Forms.CheckBox cbX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numStep;
        public System.Windows.Forms.NumericUpDown numStop;
        public System.Windows.Forms.NumericUpDown numStart;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}