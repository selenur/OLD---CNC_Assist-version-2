namespace ToolsImporterVectors
{
    partial class DRLcontrol
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
            this.TreeDrill = new System.Windows.Forms.TreeView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNumTools = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNumDrills = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownZdrill = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownZmove = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.readFromFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZdrill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZmove)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TreeDrill);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.readFromFile);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 415);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Импорт данных из DRL (EXCELLON) файла";
            // 
            // TreeDrill
            // 
            this.TreeDrill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeDrill.Location = new System.Drawing.Point(139, 34);
            this.TreeDrill.Name = "TreeDrill";
            this.TreeDrill.Size = new System.Drawing.Size(324, 375);
            this.TreeDrill.TabIndex = 33;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.numericUpDown3);
            this.groupBox4.Location = new System.Drawing.Point(12, 148);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(121, 80);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Формат чисел";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 32);
            this.label15.TabIndex = 32;
            this.label15.Text = "Кол. cимв. дробной части";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(9, 51);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown3.TabIndex = 31;
            this.numericUpDown3.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.labelNumTools);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.labelNumDrills);
            this.groupBox3.Location = new System.Drawing.Point(12, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(121, 86);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Статистика";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Всего инструментов:";
            // 
            // labelNumTools
            // 
            this.labelNumTools.AutoSize = true;
            this.labelNumTools.Location = new System.Drawing.Point(51, 31);
            this.labelNumTools.Name = "labelNumTools";
            this.labelNumTools.Size = new System.Drawing.Size(13, 13);
            this.labelNumTools.TabIndex = 5;
            this.labelNumTools.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Всего отверстий:";
            // 
            // labelNumDrills
            // 
            this.labelNumDrills.AutoSize = true;
            this.labelNumDrills.Location = new System.Drawing.Point(51, 69);
            this.labelNumDrills.Name = "labelNumDrills";
            this.labelNumDrills.Size = new System.Drawing.Size(13, 13);
            this.labelNumDrills.TabIndex = 6;
            this.labelNumDrills.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownZdrill);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericUpDownZmove);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(121, 107);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Высота Z";
            // 
            // numericUpDownZdrill
            // 
            this.numericUpDownZdrill.DecimalPlaces = 3;
            this.numericUpDownZdrill.Location = new System.Drawing.Point(6, 78);
            this.numericUpDownZdrill.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownZdrill.Name = "numericUpDownZdrill";
            this.numericUpDownZdrill.Size = new System.Drawing.Size(99, 20);
            this.numericUpDownZdrill.TabIndex = 3;
            this.numericUpDownZdrill.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Сверления:";
            // 
            // numericUpDownZmove
            // 
            this.numericUpDownZmove.DecimalPlaces = 3;
            this.numericUpDownZmove.Location = new System.Drawing.Point(6, 36);
            this.numericUpDownZmove.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownZmove.Name = "numericUpDownZmove";
            this.numericUpDownZmove.Size = new System.Drawing.Size(99, 20);
            this.numericUpDownZmove.TabIndex = 1;
            this.numericUpDownZmove.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Подхода:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Инструменты, и координаты сверловок:";
            // 
            // readFromFile
            // 
            this.readFromFile.Location = new System.Drawing.Point(12, 20);
            this.readFromFile.Name = "readFromFile";
            this.readFromFile.Size = new System.Drawing.Size(121, 33);
            this.readFromFile.TabIndex = 1;
            this.readFromFile.Text = "Получить из файла";
            this.readFromFile.UseVisualStyleBackColor = true;
            this.readFromFile.Click += new System.EventHandler(this.readFromFile_Click);
            // 
            // DRLcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DRLcontrol";
            this.Size = new System.Drawing.Size(469, 415);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZdrill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZmove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelNumDrills;
        private System.Windows.Forms.Label labelNumTools;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button readFromFile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownZdrill;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownZmove;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.TreeView TreeDrill;
    }
}
