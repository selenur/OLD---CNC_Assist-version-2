namespace CNC_Assist.Constructor
{
    partial class ConstructorForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConstructorForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportToGKodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddGKodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddRotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddCircleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeDataConstructor = new System.Windows.Forms.TreeView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuCopyDATA = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delPrimitivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.moveupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movedownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.arcToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileToolStripMenuItem,
            this.menuAddObjectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(621, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFileToolStripMenuItem
            // 
            this.menuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoadFromFileToolStripMenuItem,
            this.menuSaveToFileToolStripMenuItem,
            this.menuExportToGKodeToolStripMenuItem,
            this.menuClearToolStripMenuItem});
            this.menuFileToolStripMenuItem.Name = "menuFileToolStripMenuItem";
            this.menuFileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.menuFileToolStripMenuItem.Text = "Файл";
            // 
            // menuLoadFromFileToolStripMenuItem
            // 
            this.menuLoadFromFileToolStripMenuItem.Name = "menuLoadFromFileToolStripMenuItem";
            this.menuLoadFromFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.menuLoadFromFileToolStripMenuItem.Text = "Загрузить данные из файла";
            this.menuLoadFromFileToolStripMenuItem.Click += new System.EventHandler(this.menuLoadFromFileToolStripMenuItem_Click);
            // 
            // menuSaveToFileToolStripMenuItem
            // 
            this.menuSaveToFileToolStripMenuItem.Name = "menuSaveToFileToolStripMenuItem";
            this.menuSaveToFileToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.menuSaveToFileToolStripMenuItem.Text = "Сохранить данные в файл";
            this.menuSaveToFileToolStripMenuItem.Click += new System.EventHandler(this.menuSaveToFileToolStripMenuItem_Click);
            // 
            // menuExportToGKodeToolStripMenuItem
            // 
            this.menuExportToGKodeToolStripMenuItem.Name = "menuExportToGKodeToolStripMenuItem";
            this.menuExportToGKodeToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.menuExportToGKodeToolStripMenuItem.Text = "Экспорт данных в G-код";
            this.menuExportToGKodeToolStripMenuItem.Click += new System.EventHandler(this.menuExportToGKodeToolStripMenuItem_Click);
            // 
            // menuClearToolStripMenuItem
            // 
            this.menuClearToolStripMenuItem.Name = "menuClearToolStripMenuItem";
            this.menuClearToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.menuClearToolStripMenuItem.Text = "Очистить от данных";
            this.menuClearToolStripMenuItem.Click += new System.EventHandler(this.menuClearToolStripMenuItem_Click);
            // 
            // menuAddObjectToolStripMenuItem
            // 
            this.menuAddObjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddGroupToolStripMenuItem,
            this.menuAddPointToolStripMenuItem,
            this.menuLoopToolStripMenuItem,
            this.menuAddGKodeToolStripMenuItem,
            this.arcToolStripMenuItem,
            this.menuAddRotateToolStripMenuItem,
            this.menuAddPropertyToolStripMenuItem,
            this.menuAddLineToolStripMenuItem,
            this.menuAddCurveToolStripMenuItem,
            this.menuAddCircleToolStripMenuItem});
            this.menuAddObjectToolStripMenuItem.Name = "menuAddObjectToolStripMenuItem";
            this.menuAddObjectToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.menuAddObjectToolStripMenuItem.Text = "Добавить ОБЪЕКТ";
            // 
            // menuAddGroupToolStripMenuItem
            // 
            this.menuAddGroupToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.folder;
            this.menuAddGroupToolStripMenuItem.Name = "menuAddGroupToolStripMenuItem";
            this.menuAddGroupToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddGroupToolStripMenuItem.Text = "Группа объектов";
            this.menuAddGroupToolStripMenuItem.Click += new System.EventHandler(this.menuAddGroupToolStripMenuItem_Click);
            // 
            // menuAddPointToolStripMenuItem
            // 
            this.menuAddPointToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.bullet_blue;
            this.menuAddPointToolStripMenuItem.Name = "menuAddPointToolStripMenuItem";
            this.menuAddPointToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddPointToolStripMenuItem.Text = "Точку";
            this.menuAddPointToolStripMenuItem.Click += new System.EventHandler(this.menuAddPointToolStripMenuItem_Click);
            // 
            // menuLoopToolStripMenuItem
            // 
            this.menuLoopToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.arrow_refresh;
            this.menuLoopToolStripMenuItem.Name = "menuLoopToolStripMenuItem";
            this.menuLoopToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuLoopToolStripMenuItem.Text = "Цикл";
            this.menuLoopToolStripMenuItem.Click += new System.EventHandler(this.menuLoopToolStripMenuItem_Click);
            // 
            // menuAddGKodeToolStripMenuItem
            // 
            this.menuAddGKodeToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.file_extension_sea;
            this.menuAddGKodeToolStripMenuItem.Name = "menuAddGKodeToolStripMenuItem";
            this.menuAddGKodeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddGKodeToolStripMenuItem.Text = "Текст G-кода";
            this.menuAddGKodeToolStripMenuItem.Click += new System.EventHandler(this.menuAddGKodeToolStripMenuItem_Click);
            // 
            // arcToolStripMenuItem
            // 
            this.arcToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.draw_spiral;
            this.arcToolStripMenuItem.Name = "arcToolStripMenuItem";
            this.arcToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.arcToolStripMenuItem.Text = "Дуга";
            this.arcToolStripMenuItem.Click += new System.EventHandler(this.arcToolStripMenuItem_Click);
            // 
            // menuAddRotateToolStripMenuItem
            // 
            this.menuAddRotateToolStripMenuItem.Enabled = false;
            this.menuAddRotateToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.draw_spiral;
            this.menuAddRotateToolStripMenuItem.Name = "menuAddRotateToolStripMenuItem";
            this.menuAddRotateToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddRotateToolStripMenuItem.Text = "Вращение данных";
            this.menuAddRotateToolStripMenuItem.Visible = false;
            this.menuAddRotateToolStripMenuItem.Click += new System.EventHandler(this.menuAddRotateToolStripMenuItem_Click);
            // 
            // menuAddPropertyToolStripMenuItem
            // 
            this.menuAddPropertyToolStripMenuItem.Enabled = false;
            this.menuAddPropertyToolStripMenuItem.Name = "menuAddPropertyToolStripMenuItem";
            this.menuAddPropertyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddPropertyToolStripMenuItem.Text = "Параметр";
            this.menuAddPropertyToolStripMenuItem.Visible = false;
            // 
            // menuAddLineToolStripMenuItem
            // 
            this.menuAddLineToolStripMenuItem.Enabled = false;
            this.menuAddLineToolStripMenuItem.Name = "menuAddLineToolStripMenuItem";
            this.menuAddLineToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddLineToolStripMenuItem.Text = "Линию";
            this.menuAddLineToolStripMenuItem.Visible = false;
            this.menuAddLineToolStripMenuItem.Click += new System.EventHandler(this.menuAddLineToolStripMenuItem_Click);
            // 
            // menuAddCurveToolStripMenuItem
            // 
            this.menuAddCurveToolStripMenuItem.Enabled = false;
            this.menuAddCurveToolStripMenuItem.Name = "menuAddCurveToolStripMenuItem";
            this.menuAddCurveToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddCurveToolStripMenuItem.Text = "Кривую";
            this.menuAddCurveToolStripMenuItem.Visible = false;
            // 
            // menuAddCircleToolStripMenuItem
            // 
            this.menuAddCircleToolStripMenuItem.Enabled = false;
            this.menuAddCircleToolStripMenuItem.Name = "menuAddCircleToolStripMenuItem";
            this.menuAddCircleToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.menuAddCircleToolStripMenuItem.Text = "Окружность";
            this.menuAddCircleToolStripMenuItem.Visible = false;
            this.menuAddCircleToolStripMenuItem.Click += new System.EventHandler(this.menuAddCircleToolStripMenuItem_Click);
            // 
            // treeDataConstructor
            // 
            this.treeDataConstructor.AllowDrop = true;
            this.treeDataConstructor.ContextMenuStrip = this.contextMenu;
            this.treeDataConstructor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDataConstructor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeDataConstructor.FullRowSelect = true;
            this.treeDataConstructor.HideSelection = false;
            this.treeDataConstructor.ImageIndex = 0;
            this.treeDataConstructor.ImageList = this.imageList;
            this.treeDataConstructor.Location = new System.Drawing.Point(0, 24);
            this.treeDataConstructor.Name = "treeDataConstructor";
            this.treeDataConstructor.SelectedImageIndex = 0;
            this.treeDataConstructor.Size = new System.Drawing.Size(621, 344);
            this.treeDataConstructor.TabIndex = 0;
            this.treeDataConstructor.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeDataConstructor_BeforeCollapse);
            this.treeDataConstructor.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeDataConstructor_BeforeExpand);
            this.treeDataConstructor.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeDataConstructor_ItemDrag);
            this.treeDataConstructor.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDataConstructor_AfterSelect);
            this.treeDataConstructor.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeDataConstructor_DragDrop);
            this.treeDataConstructor.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeDataConstructor_DragEnter);
            this.treeDataConstructor.DragOver += new System.Windows.Forms.DragEventHandler(this.treeDataConstructor_DragOver);
            this.treeDataConstructor.DoubleClick += new System.EventHandler(this.treeDataConstructor_DoubleClick);
            this.treeDataConstructor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeDataConstructor_MouseDown);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDialogToolStripMenuItem,
            this.toolStripSeparator3,
            this.ToolStripMenuCopyDATA,
            this.pasteToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.delPrimitivToolStripMenuItem,
            this.toolStripSeparator5,
            this.moveupToolStripMenuItem,
            this.movedownToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.arcToolStripMenuItem1,
            this.toolStripMenuItem3});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(190, 330);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // openDialogToolStripMenuItem
            // 
            this.openDialogToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.application;
            this.openDialogToolStripMenuItem.Name = "openDialogToolStripMenuItem";
            this.openDialogToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openDialogToolStripMenuItem.Text = "Свойства примитива";
            this.openDialogToolStripMenuItem.Click += new System.EventHandler(this.openDialogToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
            // 
            // ToolStripMenuCopyDATA
            // 
            this.ToolStripMenuCopyDATA.Enabled = false;
            this.ToolStripMenuCopyDATA.Image = global::CNC_Assist.Properties.Resources.page_copy;
            this.ToolStripMenuCopyDATA.Name = "ToolStripMenuCopyDATA";
            this.ToolStripMenuCopyDATA.Size = new System.Drawing.Size(189, 22);
            this.ToolStripMenuCopyDATA.Text = "Копировать";
            this.ToolStripMenuCopyDATA.Click += new System.EventHandler(this.ToolStripMenuCopyDATA_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.page_paste;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.pasteToolStripMenuItem.Text = "Вставить";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.cut_red;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.cutToolStripMenuItem.Text = "Вырезать";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // delPrimitivToolStripMenuItem
            // 
            this.delPrimitivToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.cross;
            this.delPrimitivToolStripMenuItem.Name = "delPrimitivToolStripMenuItem";
            this.delPrimitivToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.delPrimitivToolStripMenuItem.Text = "Удалить";
            this.delPrimitivToolStripMenuItem.Click += new System.EventHandler(this.delPrimitivToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(186, 6);
            // 
            // moveupToolStripMenuItem
            // 
            this.moveupToolStripMenuItem.Enabled = false;
            this.moveupToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.arrow_up;
            this.moveupToolStripMenuItem.Name = "moveupToolStripMenuItem";
            this.moveupToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.moveupToolStripMenuItem.Text = "Сдвинуть вверх";
            // 
            // movedownToolStripMenuItem
            // 
            this.movedownToolStripMenuItem.Enabled = false;
            this.movedownToolStripMenuItem.Image = global::CNC_Assist.Properties.Resources.arrow_down;
            this.movedownToolStripMenuItem.Name = "movedownToolStripMenuItem";
            this.movedownToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.movedownToolStripMenuItem.Text = "Сдвинуть вниз";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::CNC_Assist.Properties.Resources.folder;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem1.Text = "Группа объектов";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::CNC_Assist.Properties.Resources.bullet_blue;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem2.Text = "Точку";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = global::CNC_Assist.Properties.Resources.arrow_refresh;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem4.Text = "Цикл";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = global::CNC_Assist.Properties.Resources.file_extension_sea;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem5.Text = "Текст G-кода";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // arcToolStripMenuItem1
            // 
            this.arcToolStripMenuItem1.Image = global::CNC_Assist.Properties.Resources.draw_spiral;
            this.arcToolStripMenuItem1.Name = "arcToolStripMenuItem1";
            this.arcToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.arcToolStripMenuItem1.Text = "Дуга";
            this.arcToolStripMenuItem1.Click += new System.EventHandler(this.arcToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Image = global::CNC_Assist.Properties.Resources.draw_spiral;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem3.Text = "Вращение данных";
            this.toolStripMenuItem3.Visible = false;
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "asterisk_orange.png");
            this.imageList.Images.SetKeyName(1, "folder.png");
            this.imageList.Images.SetKeyName(2, "bullet_blue.png");
            this.imageList.Images.SetKeyName(3, "draw_line.png");
            this.imageList.Images.SetKeyName(4, "arrow_repeat.png");
            this.imageList.Images.SetKeyName(5, "arrow_out.png");
            this.imageList.Images.SetKeyName(6, "arrow_rotate_clockwise.png");
            this.imageList.Images.SetKeyName(7, "file_extension_sea.png");
            // 
            // ConstructorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 368);
            this.Controls.Add(this.treeDataConstructor);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConstructorForm";
            this.Text = "Конструктор G-кода";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConstructorForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConstructorForm_FormClosed);
            this.Load += new System.EventHandler(this.ConstructorForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuLoadFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSaveToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuExportToGKodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddCurveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddCircleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddRotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddGKodeToolStripMenuItem;
        private System.Windows.Forms.TreeView treeDataConstructor;
        private System.Windows.Forms.ToolStripMenuItem menuClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddGroupToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem menuLoopToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem openDialogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuCopyDATA;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delPrimitivToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem moveupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movedownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem arcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcToolStripMenuItem1;
    }
}