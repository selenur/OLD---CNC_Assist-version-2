namespace CNC_Assist.Constructor.rotate
{
    partial class variant1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelposX = new System.Windows.Forms.Label();
            this.num_centerX = new System.Windows.Forms.NumericUpDown();
            this.labelposZ = new System.Windows.Forms.Label();
            this.num_centerY = new System.Windows.Forms.NumericUpDown();
            this.labelposY = new System.Windows.Forms.Label();
            this.num_centerZ = new System.Windows.Forms.NumericUpDown();
            this.btGetPosition = new System.Windows.Forms.Button();
            this.num_EasyRotate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_centerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_centerY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_centerZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_EasyRotate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.num_EasyRotate);
            this.groupBox1.Controls.Add(this.labelposX);
            this.groupBox1.Controls.Add(this.num_centerX);
            this.groupBox1.Controls.Add(this.labelposZ);
            this.groupBox1.Controls.Add(this.num_centerY);
            this.groupBox1.Controls.Add(this.labelposY);
            this.groupBox1.Controls.Add(this.num_centerZ);
            this.groupBox1.Controls.Add(this.btGetPosition);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 300);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вращение объекта относительно точки";
            // 
            // labelposX
            // 
            this.labelposX.AutoSize = true;
            this.labelposX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelposX.Location = new System.Drawing.Point(6, 42);
            this.labelposX.Name = "labelposX";
            this.labelposX.Size = new System.Drawing.Size(16, 16);
            this.labelposX.TabIndex = 68;
            this.labelposX.Text = "X";
            // 
            // num_centerX
            // 
            this.num_centerX.DecimalPlaces = 3;
            this.num_centerX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.num_centerX.Location = new System.Drawing.Point(33, 38);
            this.num_centerX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_centerX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.num_centerX.Name = "num_centerX";
            this.num_centerX.Size = new System.Drawing.Size(73, 22);
            this.num_centerX.TabIndex = 71;
            this.num_centerX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelposZ
            // 
            this.labelposZ.AutoSize = true;
            this.labelposZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelposZ.Location = new System.Drawing.Point(7, 87);
            this.labelposZ.Name = "labelposZ";
            this.labelposZ.Size = new System.Drawing.Size(16, 16);
            this.labelposZ.TabIndex = 70;
            this.labelposZ.Text = "Z";
            // 
            // num_centerY
            // 
            this.num_centerY.DecimalPlaces = 3;
            this.num_centerY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.num_centerY.Location = new System.Drawing.Point(33, 61);
            this.num_centerY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_centerY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.num_centerY.Name = "num_centerY";
            this.num_centerY.Size = new System.Drawing.Size(73, 22);
            this.num_centerY.TabIndex = 72;
            this.num_centerY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelposY
            // 
            this.labelposY.AutoSize = true;
            this.labelposY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelposY.Location = new System.Drawing.Point(6, 65);
            this.labelposY.Name = "labelposY";
            this.labelposY.Size = new System.Drawing.Size(17, 16);
            this.labelposY.TabIndex = 69;
            this.labelposY.Text = "Y";
            // 
            // num_centerZ
            // 
            this.num_centerZ.DecimalPlaces = 3;
            this.num_centerZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.num_centerZ.Location = new System.Drawing.Point(33, 84);
            this.num_centerZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_centerZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.num_centerZ.Name = "num_centerZ";
            this.num_centerZ.Size = new System.Drawing.Size(73, 22);
            this.num_centerZ.TabIndex = 73;
            this.num_centerZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btGetPosition
            // 
            this.btGetPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btGetPosition.Image = global::CNC_Assist.Properties.Resources.geolocation_sight;
            this.btGetPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btGetPosition.Location = new System.Drawing.Point(128, 38);
            this.btGetPosition.Name = "btGetPosition";
            this.btGetPosition.Size = new System.Drawing.Size(141, 68);
            this.btGetPosition.TabIndex = 74;
            this.btGetPosition.Text = "Использовать текущее положение";
            this.btGetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btGetPosition.UseVisualStyleBackColor = true;
            // 
            // num_EasyRotate
            // 
            this.num_EasyRotate.DecimalPlaces = 3;
            this.num_EasyRotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.num_EasyRotate.Location = new System.Drawing.Point(153, 141);
            this.num_EasyRotate.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.num_EasyRotate.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            -2147483648});
            this.num_EasyRotate.Name = "num_EasyRotate";
            this.num_EasyRotate.Size = new System.Drawing.Size(57, 22);
            this.num_EasyRotate.TabIndex = 87;
            this.num_EasyRotate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Поворот на угол:";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(86, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 46);
            this.label2.TabIndex = 89;
            this.label2.Text = "Вращение по часовой стрелке\r\n";
            // 
            // variant1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "variant1";
            this.Size = new System.Drawing.Size(300, 300);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_centerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_centerY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_centerZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_EasyRotate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelposX;
        public System.Windows.Forms.NumericUpDown num_centerX;
        private System.Windows.Forms.Label labelposZ;
        public System.Windows.Forms.NumericUpDown num_centerY;
        private System.Windows.Forms.Label labelposY;
        public System.Windows.Forms.NumericUpDown num_centerZ;
        private System.Windows.Forms.Button btGetPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown num_EasyRotate;
    }
}
