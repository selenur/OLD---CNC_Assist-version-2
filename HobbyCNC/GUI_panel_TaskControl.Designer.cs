namespace CNC_Assist
{
    partial class GuiPanelTaskControl
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxManualSpeedGkode = new System.Windows.Forms.GroupBox();
            this.checkBoxManualSpeed = new System.Windows.Forms.CheckBox();
            this.numericUpDownChangeSpeed = new System.Windows.Forms.NumericUpDown();
            this.groupBoxWorking = new System.Windows.Forms.GroupBox();
            this.labelStatusTask = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxNumberLine = new System.Windows.Forms.TextBox();
            this.buttonPauseTask = new System.Windows.Forms.Button();
            this.btStopTask = new System.Windows.Forms.Button();
            this.buttonStartTask = new System.Windows.Forms.Button();
            this.TimerRefresh = new System.Windows.Forms.Timer(this.components);
            this.listGkodeCommand = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxManualSpeedGkode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChangeSpeed)).BeginInit();
            this.groupBoxWorking.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxManualSpeedGkode
            // 
            this.groupBoxManualSpeedGkode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxManualSpeedGkode.Controls.Add(this.label1);
            this.groupBoxManualSpeedGkode.Controls.Add(this.checkBoxManualSpeed);
            this.groupBoxManualSpeedGkode.Controls.Add(this.numericUpDownChangeSpeed);
            this.groupBoxManualSpeedGkode.Location = new System.Drawing.Point(6, 245);
            this.groupBoxManualSpeedGkode.Name = "groupBoxManualSpeedGkode";
            this.groupBoxManualSpeedGkode.Size = new System.Drawing.Size(382, 74);
            this.groupBoxManualSpeedGkode.TabIndex = 13;
            this.groupBoxManualSpeedGkode.TabStop = false;
            // 
            // checkBoxManualSpeed
            // 
            this.checkBoxManualSpeed.AutoSize = true;
            this.checkBoxManualSpeed.Location = new System.Drawing.Point(7, 6);
            this.checkBoxManualSpeed.Name = "checkBoxManualSpeed";
            this.checkBoxManualSpeed.Size = new System.Drawing.Size(124, 17);
            this.checkBoxManualSpeed.TabIndex = 9;
            this.checkBoxManualSpeed.Text = "Изменить скорсть:";
            this.checkBoxManualSpeed.UseVisualStyleBackColor = true;
            this.checkBoxManualSpeed.CheckedChanged += new System.EventHandler(this.checkBoxManualSpeed_CheckedChanged);
            // 
            // numericUpDownChangeSpeed
            // 
            this.numericUpDownChangeSpeed.Enabled = false;
            this.numericUpDownChangeSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownChangeSpeed.Location = new System.Drawing.Point(22, 29);
            this.numericUpDownChangeSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownChangeSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownChangeSpeed.Name = "numericUpDownChangeSpeed";
            this.numericUpDownChangeSpeed.Size = new System.Drawing.Size(65, 29);
            this.numericUpDownChangeSpeed.TabIndex = 5;
            this.numericUpDownChangeSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownChangeSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBoxWorking
            // 
            this.groupBoxWorking.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxWorking.Controls.Add(this.labelStatusTask);
            this.groupBoxWorking.Controls.Add(this.button2);
            this.groupBoxWorking.Controls.Add(this.button1);
            this.groupBoxWorking.Controls.Add(this.textBoxNumberLine);
            this.groupBoxWorking.Controls.Add(this.buttonPauseTask);
            this.groupBoxWorking.Controls.Add(this.btStopTask);
            this.groupBoxWorking.Controls.Add(this.buttonStartTask);
            this.groupBoxWorking.Enabled = false;
            this.groupBoxWorking.Location = new System.Drawing.Point(6, 325);
            this.groupBoxWorking.Name = "groupBoxWorking";
            this.groupBoxWorking.Size = new System.Drawing.Size(382, 111);
            this.groupBoxWorking.TabIndex = 12;
            this.groupBoxWorking.TabStop = false;
            this.groupBoxWorking.Text = "Выполнение программы";
            // 
            // labelStatusTask
            // 
            this.labelStatusTask.Image = global::CNC_Assist.Properties.Resources.ball_red;
            this.labelStatusTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelStatusTask.Location = new System.Drawing.Point(217, 20);
            this.labelStatusTask.Name = "labelStatusTask";
            this.labelStatusTask.Size = new System.Drawing.Size(159, 25);
            this.labelStatusTask.TabIndex = 16;
            this.labelStatusTask.Text = "Нет задания";
            this.labelStatusTask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(63, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 46);
            this.button2.TabIndex = 15;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(7, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 46);
            this.button1.TabIndex = 14;
            this.button1.Text = "<";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxNumberLine
            // 
            this.textBoxNumberLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNumberLine.Location = new System.Drawing.Point(7, 17);
            this.textBoxNumberLine.Name = "textBoxNumberLine";
            this.textBoxNumberLine.ReadOnly = true;
            this.textBoxNumberLine.Size = new System.Drawing.Size(184, 31);
            this.textBoxNumberLine.TabIndex = 11;
            this.textBoxNumberLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonPauseTask
            // 
            this.buttonPauseTask.Enabled = false;
            this.buttonPauseTask.Image = global::CNC_Assist.Properties.Resources.control_pause_blue;
            this.buttonPauseTask.Location = new System.Drawing.Point(270, 58);
            this.buttonPauseTask.Name = "buttonPauseTask";
            this.buttonPauseTask.Size = new System.Drawing.Size(50, 46);
            this.buttonPauseTask.TabIndex = 10;
            this.buttonPauseTask.UseVisualStyleBackColor = true;
            this.buttonPauseTask.Click += new System.EventHandler(this.buttonPauseTask_Click);
            // 
            // btStopTask
            // 
            this.btStopTask.Enabled = false;
            this.btStopTask.Image = global::CNC_Assist.Properties.Resources.control_stop_blue;
            this.btStopTask.Location = new System.Drawing.Point(326, 58);
            this.btStopTask.Name = "btStopTask";
            this.btStopTask.Size = new System.Drawing.Size(50, 46);
            this.btStopTask.TabIndex = 1;
            this.btStopTask.UseVisualStyleBackColor = true;
            this.btStopTask.Click += new System.EventHandler(this.btStopTask_Click);
            // 
            // buttonStartTask
            // 
            this.buttonStartTask.Image = global::CNC_Assist.Properties.Resources.control_play_blue;
            this.buttonStartTask.Location = new System.Drawing.Point(214, 58);
            this.buttonStartTask.Name = "buttonStartTask";
            this.buttonStartTask.Size = new System.Drawing.Size(50, 46);
            this.buttonStartTask.TabIndex = 0;
            this.buttonStartTask.UseVisualStyleBackColor = true;
            this.buttonStartTask.Click += new System.EventHandler(this.buttonStartTask_Click);
            // 
            // TimerRefresh
            // 
            this.TimerRefresh.Enabled = true;
            this.TimerRefresh.Interval = 40;
            this.TimerRefresh.Tick += new System.EventHandler(this.TaskTimer_Tick);
            // 
            // listGkodeCommand
            // 
            this.listGkodeCommand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listGkodeCommand.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listGkodeCommand.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listGkodeCommand.ForeColor = System.Drawing.Color.MidnightBlue;
            this.listGkodeCommand.FormattingEnabled = true;
            this.listGkodeCommand.ItemHeight = 19;
            this.listGkodeCommand.Location = new System.Drawing.Point(6, 18);
            this.listGkodeCommand.Name = "listGkodeCommand";
            this.listGkodeCommand.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listGkodeCommand.Size = new System.Drawing.Size(382, 194);
            this.listGkodeCommand.TabIndex = 10;
            this.listGkodeCommand.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listGkodeCommand_DrawItem);
            this.listGkodeCommand.SelectedIndexChanged += new System.EventHandler(this.listGkodeCommand_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.listGkodeCommand);
            this.groupBox1.Controls.Add(this.groupBoxWorking);
            this.groupBox1.Controls.Add(this.groupBoxManualSpeedGkode);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 442);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные для выполнения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "%";
            // 
            // GuiPanelTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "GuiPanelTaskControl";
            this.Size = new System.Drawing.Size(400, 448);
            this.groupBoxManualSpeedGkode.ResumeLayout(false);
            this.groupBoxManualSpeedGkode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChangeSpeed)).EndInit();
            this.groupBoxWorking.ResumeLayout(false);
            this.groupBoxWorking.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxManualSpeedGkode;
        private System.Windows.Forms.CheckBox checkBoxManualSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownChangeSpeed;
        private System.Windows.Forms.GroupBox groupBoxWorking;
        private System.Windows.Forms.TextBox textBoxNumberLine;
        private System.Windows.Forms.Button buttonPauseTask;
        private System.Windows.Forms.Button btStopTask;
        private System.Windows.Forms.Button buttonStartTask;
        private System.Windows.Forms.Timer TimerRefresh;
        public System.Windows.Forms.ListBox listGkodeCommand;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelStatusTask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}
