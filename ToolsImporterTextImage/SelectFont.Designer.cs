namespace CNCImporterGkode
{
    partial class SelectFont
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
            this.buttonSetFontFile = new System.Windows.Forms.Button();
            this.radioButtonFontBitmap = new System.Windows.Forms.RadioButton();
            this._UseFontVector = new System.Windows.Forms.RadioButton();
            this.textString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textSize = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._UseSystemFont = new System.Windows.Forms.RadioButton();
            this.radioButtonFromFile = new System.Windows.Forms.RadioButton();
            this.nameFontFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.textSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSetFontFile
            // 
            this.buttonSetFontFile.Location = new System.Drawing.Point(311, 41);
            this.buttonSetFontFile.Name = "buttonSetFontFile";
            this.buttonSetFontFile.Size = new System.Drawing.Size(29, 22);
            this.buttonSetFontFile.TabIndex = 70;
            this.buttonSetFontFile.Text = "...";
            this.buttonSetFontFile.UseVisualStyleBackColor = true;
            this.buttonSetFontFile.Visible = false;
            this.buttonSetFontFile.Click += new System.EventHandler(this.buttonSetFontFile_Click);
            // 
            // radioButtonFontBitmap
            // 
            this.radioButtonFontBitmap.AutoSize = true;
            this.radioButtonFontBitmap.Location = new System.Drawing.Point(6, 40);
            this.radioButtonFontBitmap.Name = "radioButtonFontBitmap";
            this.radioButtonFontBitmap.Size = new System.Drawing.Size(103, 17);
            this.radioButtonFontBitmap.TabIndex = 69;
            this.radioButtonFontBitmap.Text = "В виде рисунка";
            this.radioButtonFontBitmap.UseVisualStyleBackColor = true;
            this.radioButtonFontBitmap.CheckedChanged += new System.EventHandler(this.radioButtonFontBitmap_CheckedChanged);
            // 
            // _UseFontVector
            // 
            this._UseFontVector.AutoSize = true;
            this._UseFontVector.Checked = true;
            this._UseFontVector.Location = new System.Drawing.Point(6, 18);
            this._UseFontVector.Name = "_UseFontVector";
            this._UseFontVector.Size = new System.Drawing.Size(207, 17);
            this._UseFontVector.TabIndex = 68;
            this._UseFontVector.TabStop = true;
            this._UseFontVector.Text = "В виде отрезков (шаг 2 недоступен)";
            this._UseFontVector.UseVisualStyleBackColor = true;
            this._UseFontVector.CheckedChanged += new System.EventHandler(this.radioButtonFontVector_CheckedChanged);
            // 
            // textString
            // 
            this.textString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textString.Location = new System.Drawing.Point(6, 138);
            this.textString.Multiline = true;
            this.textString.Name = "textString";
            this.textString.Size = new System.Drawing.Size(591, 99);
            this.textString.TabIndex = 61;
            this.textString.Text = "Sample text!";
            this.textString.TextChanged += new System.EventHandler(this.textString_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Размер:";
            // 
            // textSize
            // 
            this.textSize.Location = new System.Drawing.Point(414, 43);
            this.textSize.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.textSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textSize.Name = "textSize";
            this.textSize.Size = new System.Drawing.Size(69, 20);
            this.textSize.TabIndex = 62;
            this.textSize.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.textSize.ValueChanged += new System.EventHandler(this.textSize_ValueChanged);
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(52, 42);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(253, 21);
            this.comboBoxFont.TabIndex = 65;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxFont_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Шрифт:";
            // 
            // _UseSystemFont
            // 
            this._UseSystemFont.AutoSize = true;
            this._UseSystemFont.Checked = true;
            this._UseSystemFont.Location = new System.Drawing.Point(13, 10);
            this._UseSystemFont.Name = "_UseSystemFont";
            this._UseSystemFont.Size = new System.Drawing.Size(184, 17);
            this._UseSystemFont.TabIndex = 72;
            this._UseSystemFont.TabStop = true;
            this._UseSystemFont.Text = "Используем системный шрифт";
            this._UseSystemFont.UseVisualStyleBackColor = true;
            this._UseSystemFont.CheckedChanged += new System.EventHandler(this.radioButtonFromSystem_CheckedChanged);
            // 
            // radioButtonFromFile
            // 
            this.radioButtonFromFile.AutoSize = true;
            this.radioButtonFromFile.Location = new System.Drawing.Point(219, 10);
            this.radioButtonFromFile.Name = "radioButtonFromFile";
            this.radioButtonFromFile.Size = new System.Drawing.Size(174, 17);
            this.radioButtonFromFile.TabIndex = 73;
            this.radioButtonFromFile.TabStop = true;
            this.radioButtonFromFile.Text = "Используем шрифт из файла";
            this.radioButtonFromFile.UseVisualStyleBackColor = true;
            this.radioButtonFromFile.CheckedChanged += new System.EventHandler(this.radioButtonFromFile_CheckedChanged);
            // 
            // nameFontFile
            // 
            this.nameFontFile.Location = new System.Drawing.Point(53, 42);
            this.nameFontFile.Name = "nameFontFile";
            this.nameFontFile.Size = new System.Drawing.Size(252, 20);
            this.nameFontFile.TabIndex = 74;
            this.nameFontFile.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._UseFontVector);
            this.groupBox1.Controls.Add(this.radioButtonFontBitmap);
            this.groupBox1.Location = new System.Drawing.Point(13, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 61);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вариант извлечения текста из файла шрифта";
            // 
            // SelectFont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nameFontFile);
            this.Controls.Add(this.radioButtonFromFile);
            this.Controls.Add(this._UseSystemFont);
            this.Controls.Add(this.buttonSetFontFile);
            this.Controls.Add(this.textString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textSize);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.label4);
            this.Name = "SelectFont";
            this.Size = new System.Drawing.Size(600, 240);
            this.Load += new System.EventHandler(this.SelectFont_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSetFontFile;
        private System.Windows.Forms.RadioButton radioButtonFontBitmap;
        private System.Windows.Forms.RadioButton _UseFontVector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton _UseSystemFont;
        private System.Windows.Forms.RadioButton radioButtonFromFile;
        public System.Windows.Forms.TextBox nameFontFile;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox textString;
        public System.Windows.Forms.NumericUpDown textSize;
        public System.Windows.Forms.ComboBox comboBoxFont;
    }
}
