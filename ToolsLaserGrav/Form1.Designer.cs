namespace ToolsLaserGrav
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuOpenImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTextSize = new System.Windows.Forms.Label();
            this.radioButton_FullSize = new System.Windows.Forms.RadioButton();
            this.radioButton_Zoom = new System.Windows.Forms.RadioButton();
            this.checkBoxStep1Refresh = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarVar1Koeff = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.numericUpDownVar1speed = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownVar1SizeLaserLine = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCalcXY = new System.Windows.Forms.CheckBox();
            this.checkBoxUsePoint = new System.Windows.Forms.CheckBox();
            this.numericUpDownCalcY = new System.Windows.Forms.NumericUpDown();
            this.checkBoxMirrorY = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.numericUpDownCalcX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVar1Koeff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVar1speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVar1SizeLaserLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCalcY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCalcX)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenImageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1006, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuOpenImageToolStripMenuItem
            // 
            this.menuOpenImageToolStripMenuItem.Name = "menuOpenImageToolStripMenuItem";
            this.menuOpenImageToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.menuOpenImageToolStripMenuItem.Text = "Выбрать рисунок";
            this.menuOpenImageToolStripMenuItem.Click += new System.EventHandler(this.menuOpenImageToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTextSize);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 42);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные о рисунке";
            // 
            // labelTextSize
            // 
            this.labelTextSize.Location = new System.Drawing.Point(7, 16);
            this.labelTextSize.Name = "labelTextSize";
            this.labelTextSize.Size = new System.Drawing.Size(225, 21);
            this.labelTextSize.TabIndex = 0;
            this.labelTextSize.Text = "- - - - - - - - -";
            // 
            // radioButton_FullSize
            // 
            this.radioButton_FullSize.AutoSize = true;
            this.radioButton_FullSize.Checked = true;
            this.radioButton_FullSize.Location = new System.Drawing.Point(315, 45);
            this.radioButton_FullSize.Name = "radioButton_FullSize";
            this.radioButton_FullSize.Size = new System.Drawing.Size(117, 17);
            this.radioButton_FullSize.TabIndex = 19;
            this.radioButton_FullSize.TabStop = true;
            this.radioButton_FullSize.Text = "Реальный размер";
            this.radioButton_FullSize.UseVisualStyleBackColor = true;
            this.radioButton_FullSize.CheckedChanged += new System.EventHandler(this.radioButton_FullSize_CheckedChanged);
            // 
            // radioButton_Zoom
            // 
            this.radioButton_Zoom.AutoSize = true;
            this.radioButton_Zoom.Location = new System.Drawing.Point(455, 45);
            this.radioButton_Zoom.Name = "radioButton_Zoom";
            this.radioButton_Zoom.Size = new System.Drawing.Size(140, 17);
            this.radioButton_Zoom.TabIndex = 18;
            this.radioButton_Zoom.Text = "Растянуть на всё окно";
            this.radioButton_Zoom.UseVisualStyleBackColor = true;
            this.radioButton_Zoom.CheckedChanged += new System.EventHandler(this.radioButton_Zoom_CheckedChanged);
            // 
            // checkBoxStep1Refresh
            // 
            this.checkBoxStep1Refresh.Checked = true;
            this.checkBoxStep1Refresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStep1Refresh.ForeColor = System.Drawing.Color.MidnightBlue;
            this.checkBoxStep1Refresh.Location = new System.Drawing.Point(631, 45);
            this.checkBoxStep1Refresh.Name = "checkBoxStep1Refresh";
            this.checkBoxStep1Refresh.Size = new System.Drawing.Size(305, 20);
            this.checkBoxStep1Refresh.TabIndex = 21;
            this.checkBoxStep1Refresh.Text = "При любых изменениях обновить в предпросмотре";
            this.checkBoxStep1Refresh.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.pictureBoxPreview);
            this.panel1.Location = new System.Drawing.Point(264, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 542);
            this.panel1.TabIndex = 22;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPreview.BackColor = System.Drawing.Color.MediumPurple;
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(724, 536);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 15;
            this.pictureBoxPreview.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.trackBarVar1Koeff);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.numericUpDownVar1speed);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.numericUpDownVar1SizeLaserLine);
            this.groupBox3.Controls.Add(this.checkBoxCalcXY);
            this.groupBox3.Controls.Add(this.checkBoxUsePoint);
            this.groupBox3.Controls.Add(this.numericUpDownCalcY);
            this.groupBox3.Controls.Add(this.checkBoxMirrorY);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.numericUpDownCalcX);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(12, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(246, 532);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Желаемый размер в мм";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 43);
            this.button2.TabIndex = 25;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(19, 27);
            this.button1.TabIndex = 24;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Коэффициент преобразования в ЧБ:";
            // 
            // trackBarVar1Koeff
            // 
            this.trackBarVar1Koeff.Location = new System.Drawing.Point(6, 227);
            this.trackBarVar1Koeff.Maximum = 255;
            this.trackBarVar1Koeff.Name = "trackBarVar1Koeff";
            this.trackBarVar1Koeff.Size = new System.Drawing.Size(234, 45);
            this.trackBarVar1Koeff.SmallChange = 10;
            this.trackBarVar1Koeff.TabIndex = 22;
            this.trackBarVar1Koeff.TickFrequency = 10;
            this.trackBarVar1Koeff.Scroll += new System.EventHandler(this.trackBarVar1Koeff_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "скорость:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 114);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(207, 17);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ЧБ прожиг на постоянной скорости";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownVar1speed
            // 
            this.numericUpDownVar1speed.Location = new System.Drawing.Point(136, 137);
            this.numericUpDownVar1speed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownVar1speed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownVar1speed.Name = "numericUpDownVar1speed";
            this.numericUpDownVar1speed.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownVar1speed.TabIndex = 19;
            this.numericUpDownVar1speed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 337);
            this.textBox1.MaxLength = 999999999;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(233, 189);
            this.textBox1.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(11, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 45);
            this.label13.TabIndex = 17;
            this.label13.Text = "ширина прожигаемой линии на данной скорсти мм:";
            // 
            // numericUpDownVar1SizeLaserLine
            // 
            this.numericUpDownVar1SizeLaserLine.DecimalPlaces = 5;
            this.numericUpDownVar1SizeLaserLine.Location = new System.Drawing.Point(136, 163);
            this.numericUpDownVar1SizeLaserLine.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownVar1SizeLaserLine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownVar1SizeLaserLine.Name = "numericUpDownVar1SizeLaserLine";
            this.numericUpDownVar1SizeLaserLine.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownVar1SizeLaserLine.TabIndex = 16;
            this.numericUpDownVar1SizeLaserLine.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // checkBoxCalcXY
            // 
            this.checkBoxCalcXY.AutoSize = true;
            this.checkBoxCalcXY.Checked = true;
            this.checkBoxCalcXY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCalcXY.Location = new System.Drawing.Point(6, 45);
            this.checkBoxCalcXY.Name = "checkBoxCalcXY";
            this.checkBoxCalcXY.Size = new System.Drawing.Size(158, 17);
            this.checkBoxCalcXY.TabIndex = 4;
            this.checkBoxCalcXY.Text = "пересчет пропорции X к Y";
            this.checkBoxCalcXY.UseVisualStyleBackColor = true;
            // 
            // checkBoxUsePoint
            // 
            this.checkBoxUsePoint.AutoSize = true;
            this.checkBoxUsePoint.Checked = true;
            this.checkBoxUsePoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUsePoint.Location = new System.Drawing.Point(6, 91);
            this.checkBoxUsePoint.Name = "checkBoxUsePoint";
            this.checkBoxUsePoint.Size = new System.Drawing.Size(164, 17);
            this.checkBoxUsePoint.TabIndex = 14;
            this.checkBoxUsePoint.Text = "разделитель дроб. \"Точка\"";
            this.checkBoxUsePoint.UseVisualStyleBackColor = true;
            // 
            // numericUpDownCalcY
            // 
            this.numericUpDownCalcY.Location = new System.Drawing.Point(124, 19);
            this.numericUpDownCalcY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCalcY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCalcY.Name = "numericUpDownCalcY";
            this.numericUpDownCalcY.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownCalcY.TabIndex = 3;
            this.numericUpDownCalcY.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxMirrorY
            // 
            this.checkBoxMirrorY.AutoSize = true;
            this.checkBoxMirrorY.Checked = true;
            this.checkBoxMirrorY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMirrorY.Location = new System.Drawing.Point(6, 68);
            this.checkBoxMirrorY.Name = "checkBoxMirrorY";
            this.checkBoxMirrorY.Size = new System.Drawing.Size(156, 17);
            this.checkBoxMirrorY.TabIndex = 13;
            this.checkBoxMirrorY.Text = "Отразить зеркально по Y";
            this.checkBoxMirrorY.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(101, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Y:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 278);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 53);
            this.button4.TabIndex = 12;
            this.button4.Text = "Сформировать код фрезеровки";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // numericUpDownCalcX
            // 
            this.numericUpDownCalcX.Location = new System.Drawing.Point(30, 19);
            this.numericUpDownCalcX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCalcX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCalcX.Name = "numericUpDownCalcX";
            this.numericUpDownCalcX.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownCalcX.TabIndex = 1;
            this.numericUpDownCalcX.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "X:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 625);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxStep1Refresh);
            this.Controls.Add(this.radioButton_FullSize);
            this.Controls.Add(this.radioButton_Zoom);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Генератор G-кода для лазерной гравировки";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVar1Koeff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVar1speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVar1SizeLaserLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCalcY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCalcX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuOpenImageToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_FullSize;
        private System.Windows.Forms.RadioButton radioButton_Zoom;
        private System.Windows.Forms.CheckBox checkBoxStep1Refresh;
        private System.Windows.Forms.Label labelTextSize;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDownVar1SizeLaserLine;
        private System.Windows.Forms.CheckBox checkBoxCalcXY;
        private System.Windows.Forms.CheckBox checkBoxUsePoint;
        private System.Windows.Forms.NumericUpDown numericUpDownCalcY;
        private System.Windows.Forms.CheckBox checkBoxMirrorY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown numericUpDownCalcX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.NumericUpDown numericUpDownVar1speed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarVar1Koeff;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

