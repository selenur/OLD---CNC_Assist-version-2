namespace ToolsImporterVectors
{
    partial class PLTcontrol
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
            this.checkBoxIsCorel = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNumTools = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNumDrills = new System.Windows.Forms.Label();
            this.treeViewPLT = new System.Windows.Forms.TreeView();
            this.buttonGetFromFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxIsCorel);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.treeViewPLT);
            this.groupBox1.Controls.Add(this.buttonGetFromFile);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 338);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Импорт данных из PLT файла";
            // 
            // checkBoxIsCorel
            // 
            this.checkBoxIsCorel.Location = new System.Drawing.Point(10, 156);
            this.checkBoxIsCorel.Name = "checkBoxIsCorel";
            this.checkBoxIsCorel.Size = new System.Drawing.Size(106, 33);
            this.checkBoxIsCorel.TabIndex = 11;
            this.checkBoxIsCorel.Text = "файл из COREL draw";
            this.checkBoxIsCorel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.labelNumTools);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.labelNumDrills);
            this.groupBox3.Location = new System.Drawing.Point(6, 61);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(121, 86);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Статистика";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Всего отрезков:";
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
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Всего точек:";
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
            // treeViewPLT
            // 
            this.treeViewPLT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewPLT.Location = new System.Drawing.Point(133, 22);
            this.treeViewPLT.Name = "treeViewPLT";
            this.treeViewPLT.Size = new System.Drawing.Size(198, 303);
            this.treeViewPLT.TabIndex = 1;
            // 
            // buttonGetFromFile
            // 
            this.buttonGetFromFile.Location = new System.Drawing.Point(6, 19);
            this.buttonGetFromFile.Name = "buttonGetFromFile";
            this.buttonGetFromFile.Size = new System.Drawing.Size(121, 36);
            this.buttonGetFromFile.TabIndex = 0;
            this.buttonGetFromFile.Text = "Получить из файла";
            this.buttonGetFromFile.UseVisualStyleBackColor = true;
            this.buttonGetFromFile.Click += new System.EventHandler(this.buttonGetFromFile_Click);
            // 
            // PLTcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PLTcontrol";
            this.Size = new System.Drawing.Size(343, 338);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGetFromFile;
        private System.Windows.Forms.TreeView treeViewPLT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelNumTools;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelNumDrills;
        private System.Windows.Forms.CheckBox checkBoxIsCorel;
    }
}
