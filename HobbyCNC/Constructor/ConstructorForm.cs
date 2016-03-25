using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing;

namespace CNC_Assist.Constructor
{
    public partial class ConstructorForm : Form
    {
        /// <summary>
        /// Для хранения ссылок на объекты данных конструктора
        /// </summary>
        public static ConstructionObject items;

        /// <summary>
        /// Данные по элементам "Группа"
        /// </summary>
        public static List<PrimitivCatalog> itemsCatalog;

        /// <summary>
        /// Данные по элементам "Точка"
        /// </summary>
        public static List<PrimitivPoint> itemsPoint;

        /// <summary>
        /// Данные по элементам "Цикл"
        /// </summary>
        public static List<PrimitivLoop> itemsLoop;

        /// <summary>
        /// Данные о вращении
        /// </summary>
        public static List<PrimitivArc> itemsArc; 

        /// <summary>
        /// Сумарная информация о всех данных
        /// </summary>
        public static List<PrimitivGkode> ItemsGkode; 

        // Для упращения ввода новых данных, запоминается последняя введенная координата,
        // и при добавлении нового примитива эти координаты устанавливаются
        private double _lastX;       // координата в мм
        private double _lastY;       // координата в мм
        private double _lastZ;       // координата в мм

        public ConstructorForm()
        {
            InitializeComponent();
        }

        private void ConstructorForm_Load(object sender, EventArgs e)
        {
            itemsPoint = new List<PrimitivPoint>();  
          
            itemsCatalog = new List<PrimitivCatalog>();

            itemsLoop = new List<PrimitivLoop>();

            ItemsGkode = new List<PrimitivGkode>();

            itemsArc = new List<PrimitivArc>();

            // Добавим в набор данных "группы" первый элемент
            string tmpGUID = System.Guid.NewGuid().ToString();
            itemsCatalog.Add(new PrimitivCatalog(0, 0, 0, "Начальная группа", tmpGUID));

            // и этот элемент укажем в общем списке
            items = new ConstructionObject(PrimitivType.Catalog, tmpGUID, new List<ConstructionObject>());

            RefreshTree();
        }



        #region Поиск по ГУИДу

        /// <summary>
        /// Поиск в коллекции объектов, объект с заданным идентификатором
        /// </summary>
        /// <param name="_guid"></param>
        /// <param name="_items"></param>
        /// <returns></returns>
        private ConstructionObject GetObjectFromGUID(string _guid, ConstructionObject _items = null)
        {
            // для хранения текущего значения
            ConstructionObject findValue = null;

            if (_items == null)
            {
                // Если сюда попали, значит функция вызвана не рекурсивно
                //и в качестве текущего значения выберем первый элемент
                findValue = items;
            }
            else
            {
                findValue = _items;
            }

            // это этот элемент?
            if (_guid == findValue.GUID) return findValue;

            //пройдемся по коллекции данного элемента

            foreach (ConstructionObject VARIABLE in findValue.items)
            {
                ConstructionObject res = GetObjectFromGUID(_guid, VARIABLE);

                if (res != null) return res;
            }

            return null;
        }

        private PrimitivPoint GetPointFromGUID(string _guid)
        {
            return itemsPoint.Find(x => x.GUID == _guid);
        }

        private PrimitivCatalog GetCatalogFromGUID(string _guid)
        {
            return itemsCatalog.Find(x => x.GUID == _guid);
        }

        private PrimitivLoop GetLoopFromGUID(string _guid)
        {
            return itemsLoop.Find(x => x.GUID == _guid);
        }

        private PrimitivArc GetArcFromGUID(string _guid)
        {
            return itemsArc.Find(x => x.GUID == _guid);
        }

        private PrimitivGkode GetGkodeFromGUID(string _guid)
        {
            return ItemsGkode.Find(x => x.GUID == _guid);
        }


        #endregion

        private void DrawPrimitivInTree(ConstructionObject _object, TreeNode _rootNode = null)
        {
            var trNode = _rootNode == null ? treeDataConstructor.Nodes.Add(_object.GUID) : _rootNode.Nodes.Add(_object.GUID);


            if (_object.type == PrimitivType.Catalog)
            {
                PrimitivCatalog tmpCatalog = GetCatalogFromGUID(_object.GUID);
                trNode.ImageIndex = 1;
                trNode.Name = _object.GUID;
                if (tmpCatalog != null) trNode.Text = tmpCatalog.Name;
            }

            if (_object.type == PrimitivType.Gkode)
            {
                PrimitivGkode tmpg = GetGkodeFromGUID(_object.GUID);
                trNode.ImageIndex = 7;
                trNode.Name = _object.GUID;
                if (tmpg != null) trNode.Text = tmpg.Name;
            }

            if (_object.type == PrimitivType.Point)
            {
                PrimitivPoint tmpPoint = GetPointFromGUID(_object.GUID);
                trNode.ImageIndex = 2;
                trNode.Name = _object.GUID;
                if (tmpPoint != null) trNode.Text = tmpPoint.Name;
            }

            if (_object.type == PrimitivType.Loop)
            {
                PrimitivLoop tmpLoop = GetLoopFromGUID(_object.GUID);
                trNode.ImageIndex = 4;
                trNode.Name = _object.GUID;
                if (tmpLoop != null) trNode.Text = tmpLoop.Name;
                //TODO: перепестить в класс
                trNode.Text = tmpLoop.Name + @"(с: " + tmpLoop.CStart + @" по: " + tmpLoop.CStop + @" шаг:" + +tmpLoop.CStep + @" для:" + (tmpLoop.AllowDeltaX ? " X" : " ") + (tmpLoop.AllowDeltaY ? " Y" : " ") + (tmpLoop.AllowDeltaZ ? " Z" : " ") + @")";
            }

            if (_object.type == PrimitivType.Arc)
            {
                PrimitivArc tmpRotate = GetArcFromGUID(_object.GUID);
                trNode.ImageIndex = 6;
                trNode.Name = _object.GUID;
                if (tmpRotate != null) trNode.Text = tmpRotate.Name;
            }

            if (_object.items.Count == 0) return;

            foreach (ConstructionObject variable in _object.items)
            {
                DrawPrimitivInTree(variable, trNode); 
            }
        }

        // перерисовка данных
        private void RefreshTree()
        {
            //элемент на котом нужно будет оставить курсор/выделение
            string guiDselectedNode = "";

            if (treeDataConstructor.SelectedNode != null) guiDselectedNode = treeDataConstructor.SelectedNode.Name;

            treeDataConstructor.BeginUpdate();
            treeDataConstructor.Nodes.Clear();

                DrawPrimitivInTree(items);

            treeDataConstructor.EndUpdate();
            treeDataConstructor.ExpandAll();

            // и G-код сразу сгенерируем
            CREATE_GKOD();

            // установим активность на узле с GUIDselectedNode
            TreeNode[] trArray = treeDataConstructor.Nodes.Find(guiDselectedNode, true);
            if (trArray.Length != 0) treeDataConstructor.SelectedNode = trArray[0];
        }

        private void ConstructorForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ConstructorForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        #region меню Файл

        private void menuClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            items.items.Clear();
            itemsCatalog.Clear();
            itemsLoop.Clear();
            itemsPoint.Clear();
            ItemsGkode.Clear();
            RefreshTree();

        }


        private void menuLoadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    Filter = @"Данные конструктора (*.dat)|*.dat|Все файлы (*.*)|*.*"
                };

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    BinaryFormatter binFormat = new BinaryFormatter();


                    using (Stream fStream = File.OpenRead(openFileDialog1.FileName))
                    {
                        DataSaveLoad tmp = (DataSaveLoad)binFormat.Deserialize(fStream);
                        items = tmp.items;
                        ItemsGkode = tmp.ItemsGkode;
                        itemsCatalog = tmp.itemsCatalog;
                        itemsLoop = tmp.itemsLoop;
                        itemsPoint = tmp.itemsPoint;
                        itemsArc = tmp.itemsArc;
                    }
                    RefreshTree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка парсинга файла с данными! " + ex.Message);

            }
        }

        private void menuSaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = @"Данные конструктора (*.dat)|*.dat|Все файлы (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataSaveLoad tmp = new DataSaveLoad();
                tmp.items = items;
                tmp.ItemsGkode = ItemsGkode;
                tmp.itemsCatalog = itemsCatalog;
                tmp.itemsLoop = itemsLoop;
                tmp.itemsPoint = itemsPoint;
                tmp.itemsArc = itemsArc;

                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, tmp);
                }
            }
        }

        private void menuExportToGKodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (items.items.Count == 0) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = @"G-код (*.txt)|*.txt|Все файлы (*.*)|*.*" };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string code = ParsePrimitivesToGkode(items);

                StreamWriter sw = new StreamWriter(new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write));
                sw.Write(code);
                sw.Close();
            }
        }



        #endregion

        #region Меню добавление примитивов

        private void menuAddPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPoint();
        }

        private void menuAddLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sGkode += "g1 x1 y1 z1" + Environment.NewLine;
            //RefreshData();
        }

        private void menuAddGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewGroup();
        }

        private void menuAddCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        // Добавление новой группы в "дерево данных"
        private void AddNewGroup()
        {
            // попытаемся получить выделенный узел в дереве
            TreeNode pNode = treeDataConstructor.SelectedNode ?? treeDataConstructor.Nodes[0];


            if (pNode == null) // всетаки неудалось.......
            {
                MessageBox.Show(@"Не выбран элемент в дереве объектов, для которого будет добавлена подчиненная группа");
                return;
            }

            ConstructionObject obj = GetObjectFromGUID(pNode.Name);

            if (obj == null)
            {
                MessageBox.Show(@"Не найден элемент группы в массиве");
                return;
            }

            // Сразу проверим узел выше, т.к. каталог можно создавать в корне дерева, или внутри другого каталога, а создание каталога внутри других элементов нелогично
            if (!(obj.type == PrimitivType.Catalog || obj.type == PrimitivType.Loop))
            {
                MessageBox.Show(@"Создание каталога в нутри данного примитива невозможно!");
                return;
            }

            // вызовем диалог добавления группы
            frmCatalog fCatalog = new frmCatalog()
            {
                deltaX = { Value = (decimal)_lastX },
                deltaY = { Value = (decimal)_lastY },
                deltaZ = { Value = (decimal)_lastZ }
            };

            DialogResult dlResult = fCatalog.ShowDialog();

            if (dlResult != DialogResult.OK) return;
            
            // запомним временные данные
            _lastX = (double)fCatalog.deltaX.Value;
            _lastY = (double)fCatalog.deltaY.Value;
            _lastZ = (double)fCatalog.deltaZ.Value;

            // создадим новую группу, в массиве групп, и добавим её в список элементов дерева
            PrimitivCatalog tmpCat = new PrimitivCatalog((double)fCatalog.deltaX.Value, (double)fCatalog.deltaY.Value, (double)fCatalog.deltaZ.Value, fCatalog.textBoxName.Text);
            itemsCatalog.Add(tmpCat);
            obj.items.Add(new ConstructionObject(PrimitivType.Catalog, tmpCat.GUID,new List<ConstructionObject>()));

            //обновим отображение
            RefreshTree();
            
        }

        // Добавление новой точки в дерево
        private void AddNewPoint()
        {
            // попытаемся получить выделенный узел в дереве
            TreeNode pNode = treeDataConstructor.SelectedNode ?? treeDataConstructor.Nodes[0];

            if (pNode == null) // всетаки неудалось.......
            {
                MessageBox.Show(@"Не выбран элемент в дереве объектов, для которого будет добавлена подчиненная группа");
                return;
            }

            ConstructionObject obj = GetObjectFromGUID(pNode.Name);

            if (obj == null)
            {
                MessageBox.Show(@"Не найден элемент группы в массиве");
                return;
            }

            // Сразу проверим узел выше, что-бы случайно точку несоздали в подчинении другой точки
            if (!(obj.type == PrimitivType.Catalog || obj.type == PrimitivType.Loop))
            {
                MessageBox.Show(@"Создание точки в нутри данного примитива невозможно!");
                return;
            }

            // вызовем диалог добавления точки

            frmPoint fPoint = new frmPoint()
            {
                numPosX = { Value = (decimal)_lastX },
                numPosY = { Value = (decimal)_lastY },
                numPosZ = { Value = (decimal)_lastZ }
            };

            DialogResult dlResult = fPoint.ShowDialog();

            if (dlResult != DialogResult.OK) return;

            // запомним временные данные
            _lastX = (double)fPoint.numPosX.Value;
            _lastY = (double)fPoint.numPosY.Value;
            _lastZ = (double)fPoint.numPosZ.Value;

            

            // создадим новую точку, в массиве точек, и добавим её в список элементов дерева
            PrimitivPoint tmppnt = new PrimitivPoint((double)fPoint.numPosX.Value, (double)fPoint.numPosY.Value, (double)fPoint.numPosZ.Value);
            itemsPoint.Add(tmppnt);
            obj.items.Add(new ConstructionObject(PrimitivType.Point, tmppnt.GUID, new List<ConstructionObject>()));

            RefreshTree();
        }

        private void AddNewLoop()
        {
            TreeNode pNode = treeDataConstructor.SelectedNode ?? treeDataConstructor.Nodes[0];

            if (pNode == null) // всетаки неудалось.......
            {
                MessageBox.Show(@"Не выбран элемент в дереве объектов, для которого будет добавлена циклическая операция");
                return;
            }

            ConstructionObject obj = GetObjectFromGUID(pNode.Name);

            if (obj == null)
            {
                MessageBox.Show(@"Не найден элемент группы в массиве");
                return;
            }

            // Сразу проверим узел выше, для возможности добавления циклической операции
            if (!(obj.type == PrimitivType.Catalog || obj.type == PrimitivType.Loop))
            {
                MessageBox.Show(@"Циклическую операцию нельзя добавить у данного примитива!");
                return;
            }

            // вызовем диалог добавления циклёра
            frmLoop fCycler = new frmLoop();

            DialogResult dlResult = fCycler.ShowDialog();

            if (dlResult != DialogResult.OK) return;

            PrimitivLoop tmpLoop = new PrimitivLoop((double)fCycler.numStart.Value, (double)fCycler.numStop.Value, (double)fCycler.numStep.Value, fCycler.cbX.Checked, fCycler.cbY.Checked, fCycler.cbZ.Checked, fCycler.textBoxName.Text);
            itemsLoop.Add(tmpLoop);
            obj.items.Add(new ConstructionObject(PrimitivType.Loop, tmpLoop.GUID, new List<ConstructionObject>()));
            RefreshTree();
            

        }

        private void AddNewArc()
        {
            TreeNode pNode = treeDataConstructor.SelectedNode ?? treeDataConstructor.Nodes[0];

            // попытаемся получить вышестоящий узел в дереве

            if (pNode == null) // всетаки неудалось.......
            {
                MessageBox.Show(@"DANGER!!! Ошибка указания родителя?!?!");
                return;
            }

            // найдем вышестоящий узел в ListPrimitives
            ConstructionObject obj = GetObjectFromGUID(pNode.Name);

            if (obj == null)
            {
                MessageBox.Show(@"Не найден элемент группы в массиве");
                return;
            }

            // Сразу проверим узел выше, для возможности добавления циклической операции
            if (!(obj.type == PrimitivType.Catalog || obj.type == PrimitivType.Loop))
            {
                MessageBox.Show(@"Операцию вращения нельзя добавить у данного примитива!");
                return;
            }

            // вызовем диалог добавления вращения
            frmArc frotate = new frmArc();

            DialogResult dlResult = frotate.ShowDialog();

            if (dlResult == DialogResult.OK)
            {
                //TODO: доделать

                PrimitivArc tmpRotate = new PrimitivArc((double) frotate.num_centerX.Value,
                    (double) frotate.num_centerY.Value, (double) frotate.num_centerZ.Value,
                    (double) frotate.num_rotateStartAngle.Value, (double) frotate.num_rotateStopAngle.Value,
                    (double) frotate.num_rotateStepAngle.Value, (double) frotate.num_rotateRadius.Value,
                    (double) frotate.num_deltaStepRadius.Value, frotate.textBoxName.Text);

                itemsArc.Add(tmpRotate);
                obj.items.Add(new ConstructionObject(PrimitivType.Arc, tmpRotate.GUID, new List<ConstructionObject>()));
                
                RefreshTree();
            }
        }


        private void AddNewGkodeData()
        {
            // попытаемся получить выделенный узел в дереве
            TreeNode pNode = treeDataConstructor.SelectedNode ?? treeDataConstructor.Nodes[0];

            if (pNode == null) // всетаки неудалось.......
            {
                MessageBox.Show(@"Не выбран элемент в дереве объектов, для которого будет добавлен примитив");
                return;
            }

            ConstructionObject obj = GetObjectFromGUID(pNode.Name);

            if (obj == null)
            {
                MessageBox.Show(@"Не найден элемент группы в массиве");
                return;
            }


            // Сразу проверим узел выше, что-бы случайно точку несоздали в подчинении другой точки
            if (obj.type == PrimitivType.Point)
            {
                MessageBox.Show(@"Создание примитива 'G-код' в нутри данного примитива невозможно!");
                return;
            }

            // вызовем диалог добавления точки

            frmGkode fcode = new frmGkode()
            {
                textBoxGkode = {Text = ""}
            };

            DialogResult dlResult = fcode.ShowDialog();

            if (dlResult != DialogResult.OK) return;

            PrimitivGkode tmpg = new PrimitivGkode(fcode.textBoxGkode.Text,fcode.textBoxName.Text);
            ItemsGkode.Add(tmpg);
            obj.items.Add(new ConstructionObject(PrimitivType.Gkode, tmpg.GUID, new List<ConstructionObject>()));

            RefreshTree();
        }

        private void menuAddRotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddNewRotate();
        }

        private void menuLoopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewLoop();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddNewGroup();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddNewPoint();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //AddNewRotate();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AddNewLoop();
        }

        private void menuAddGKodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewGkodeData();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AddNewGkodeData();
        }

        #endregion

        #region Открытие настроек существующего примитива

        private void treeDataConstructor_DoubleClick(object sender, EventArgs e)
        {
            OpenFormDialog();
        }

        private void openDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFormDialog();
        }

        private void OpenFormDialog()
        {
            if (treeDataConstructor.SelectedNode == null) return;

            // Необходимость перезаполнения дерева обновленными данными
            bool needRefreshTree = false;

            //получим примитив по гуиду
            ConstructionObject obj = GetObjectFromGUID(treeDataConstructor.SelectedNode.Name);

            if (obj == null) return;

            //определим его тип, и откроем необходимый диалог
            if (obj.type == PrimitivType.Catalog)
            {
                PrimitivCatalog tmpCatalog = GetCatalogFromGUID(obj.GUID);

                if (tmpCatalog == null) return;

                // вызовем диалог добавления группы
                frmCatalog fCatalog = new frmCatalog()
                {
                    deltaX = { Value = (decimal)tmpCatalog.DeltaX },
                    deltaY = { Value = (decimal)tmpCatalog.DeltaY },
                    deltaZ = { Value = (decimal)tmpCatalog.DeltaZ },
                    textBoxName = { Text = tmpCatalog.Name },
                };

                DialogResult dlResult = fCatalog.ShowDialog();

                if (dlResult == DialogResult.OK)
                {
                    tmpCatalog.DeltaX = (double)fCatalog.deltaX.Value;
                    tmpCatalog.DeltaY = (double)fCatalog.deltaY.Value;
                    tmpCatalog.DeltaZ = (double)fCatalog.deltaZ.Value;
                    tmpCatalog.Name = fCatalog.textBoxName.Text;

                    needRefreshTree = true;
                }
            }


            if (obj.type == PrimitivType.Gkode)
            {
                PrimitivGkode tmpg = GetGkodeFromGUID(obj.GUID);

                if (tmpg == null) return;

                // вызовем диалог добавления группы
                frmGkode fg = new frmGkode()
                {
                    textBoxGkode = { Text = tmpg.Gkode },
                    textBoxName = { Text = tmpg.Name }
                };

                DialogResult dlResult = fg.ShowDialog();

                if (dlResult == DialogResult.OK)
                {
                    tmpg.Gkode = fg.textBoxGkode.Text;
                    tmpg.Name = fg.textBoxName.Text;

                    needRefreshTree = true;
                }
            }





            if (obj.type == PrimitivType.Point)
            {
                PrimitivPoint tmpPoint = GetPointFromGUID(obj.GUID);

                // вызовем диалог добавления группы
                frmPoint fPoint = new frmPoint
                {
                    numPosX = { Value = (decimal)tmpPoint.X },
                    numPosY = { Value = (decimal)tmpPoint.Y },
                    numPosZ = { Value = (decimal)tmpPoint.Z }
                };

                DialogResult dlResult = fPoint.ShowDialog();

                if (dlResult == DialogResult.OK)
                {
                    tmpPoint.X = (double)fPoint.numPosX.Value;
                    tmpPoint.Y = (double)fPoint.numPosY.Value;
                    tmpPoint.Z = (double)fPoint.numPosZ.Value;
                    //TODO: добавить автообновление наименования
                    tmpPoint.Name = @"Точка: (" + tmpPoint.X + @", " + tmpPoint.Y + @", " + tmpPoint.Z + @")";

                    needRefreshTree = true;
                }
            }



            if (obj.type == PrimitivType.Loop)
            {
                PrimitivLoop tmpLoop = GetLoopFromGUID(obj.GUID);
                // вызовем диалог добавления группы

                frmLoop fCycler = new frmLoop()
                {
                    numStart = { Value = (decimal)tmpLoop.CStart },
                    numStop = { Value = (decimal)tmpLoop.CStop },
                    numStep = { Value = (decimal)tmpLoop.CStep },
                    textBoxName = { Text = tmpLoop.Name },
                    cbX = { Checked = tmpLoop.AllowDeltaX },
                    cbY = { Checked = tmpLoop.AllowDeltaY },
                    cbZ = { Checked = tmpLoop.AllowDeltaZ }
                };


                // ReSharper disable once SuggestVarOrType_SimpleTypes
                DialogResult dlResult = fCycler.ShowDialog();

                if (dlResult != DialogResult.OK) return;


                tmpLoop.CStart = (double)fCycler.numStart.Value;
                tmpLoop.CStop = (double)fCycler.numStop.Value;
                tmpLoop.CStep = (double)fCycler.numStep.Value;

                tmpLoop.Name = fCycler.textBoxName.Text;
                tmpLoop.AllowDeltaX = fCycler.cbX.Checked;
                tmpLoop.AllowDeltaY = fCycler.cbY.Checked;
                tmpLoop.AllowDeltaZ = fCycler.cbZ.Checked;

                needRefreshTree = true;
            }

            if (obj.type == PrimitivType.Arc)
            {
                PrimitivArc tmparc = GetArcFromGUID(obj.GUID);

                frmArc fArc = new frmArc()
                {
                    num_centerX = { Value = (decimal)tmparc.X },
                    num_centerY = { Value = (decimal)tmparc.Y },
                    num_centerZ = { Value = (decimal)tmparc.Z },
                    num_rotateRadius = { Value = (decimal)tmparc.Radius },
                    num_rotateStartAngle = { Value = (decimal)tmparc.AngleStart },
                    num_rotateStopAngle = { Value = (decimal)tmparc.AngleStop },
                    num_rotateStepAngle = { Value = (decimal)tmparc.AngleStep },
                    num_deltaStepRadius = { Value = (decimal)tmparc.DeltaStepRadius },
                    textBoxName = { Text = tmparc.Name }
                };


                DialogResult dlResult = fArc.ShowDialog();

                if (dlResult != DialogResult.OK) return;

                tmparc.Name = fArc.textBoxName.Text;

                tmparc.X = (double)fArc.num_centerX.Value;
                tmparc.Y = (double)fArc.num_centerY.Value;
                tmparc.Z = (double)fArc.num_centerZ.Value;

                tmparc.Radius = (double)fArc.num_rotateRadius.Value;
                tmparc.AngleStart = (double)fArc.num_rotateStartAngle.Value;
                tmparc.AngleStop = (double)fArc.num_rotateStopAngle.Value;
                tmparc.AngleStep = (double)fArc.num_rotateStepAngle.Value;

                tmparc.DeltaStepRadius = (double)fArc.num_deltaStepRadius.Value;

                needRefreshTree = true;
            }

            if (needRefreshTree) RefreshTree();
        }
        #endregion

        /// <summary>
        /// Псевдо проверка на содержимое передаваемых символов, что они могут являться частью числа
        /// </summary>
        /// <param name="chd"></param>
        /// <returns></returns>
        private bool isDigits(string chd)
        {
            if (chd == "0" || chd == "1" || chd == "2" || chd == "3" || chd == "4" || chd == "5" ||
                chd == "6" || chd == "7" || chd == "8" || chd == "9" ||
                chd == "." || chd == "," || chd == "-" || chd == "+" || chd == " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Конвертация строки в число
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private decimal GetDecValue(string _str)
        {
            //возможные значения для парсинга
            // 0
            // -10 
            // - 60
            // 0.01 
            // ,01
            // +0       
            
            decimal dValue = 0;

            string tmpStr = _str.Trim();
            //удаляем лишние символы, которые ни как не скажутся на значении
            tmpStr = tmpStr.Replace(" ", "");
            tmpStr = tmpStr.Replace("+", "");

            //получим символ разделения дробной и целой части.
            string symbSeparatorDec = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            char csourse = '.';
            char cdestination = ',';

            if (symbSeparatorDec == ".")
            {
                csourse = ',';
                cdestination = '.';
            }

            tmpStr = tmpStr.Replace(csourse, cdestination); //приведем к национальному формату

            if (!decimal.TryParse(tmpStr, out dValue)) return 0;

            return dValue;
        }


        
        private string AddToGkodeOffsetAxes(string _code, decimal _X, decimal _Y, decimal _Z)
        {
            if (_code.Length == 0) return "";

            string newString = "";

            int sLenght = _code.Length;
            int posNow = 0;
            decimal dvalue = 0;
            bool needAppendValue = false;

            while (posNow < sLenght)
            {
                // в цикле ищем символ X,Y,Z
                string ch = _code[posNow].ToString().ToUpper();

                if (ch == "X" || ch == "Y" || ch == "Z")
                {
                    string tmpValue = "";
                    bool needFindDigits = true;
                    bool digitsFinded = false;


                    while (needFindDigits)
                    {
                        posNow++;

                        if (posNow >= sLenght)
                        {
                            //остановка нужна, достигли конца
                            needFindDigits = false;
                            dvalue = GetDecValue(tmpValue);
                            needAppendValue = true;
                            posNow--;
                            continue;
                        }

                        string chd = _code[posNow].ToString();

                        if (isDigits(chd))
                        {
                            tmpValue += chd;
                        }
                        else
                        {
                            //попался уже сторонний символ, так что нужно прекращать собирать числове значение
                            needFindDigits = false;

                            dvalue = GetDecValue(tmpValue);
                            needAppendValue = true;
                            posNow--;
                        }
                    }
                }
                newString += ch;
                // TODO: если стоит флаг дополнения то сюда добавим число zzz
                if (needAppendValue)
                {
                    if (ch == "X") dvalue += _X;

                    if (ch == "Y") dvalue += _Y;

                    if (ch == "Z") dvalue += _Z;

                    newString += dvalue.ToString("#0.###") + " ";
                    needAppendValue = false;
                }

                posNow++;
            }

            return newString;
        }



        private string RotateGkode(string _code, decimal _angle, PointF _mainPoint)
        {
            // нужно распарсить текст


            return "";

        }

        //PointF pointA = new PointF(0,0);
        //PointF pointB = new PointF(10,0);
        //PointF newPoint = rotatePoint(pointA, pointB, 0);
        //newPoint = rotatePoint(pointA, pointB, 45);
        //newPoint = rotatePoint(pointA, pointB, 90);
        //newPoint = rotatePoint(pointA, pointB, 135);
        //newPoint = rotatePoint(pointA, pointB, 180);
        public static PointF rotatePoint(PointF basePoint, PointF sourcePoint, double rotationAngle)
        {
            double r;
            double theta;
            double offsetX;
            double offsetY;
            double offsetTheta;
            double rotateX;
            double rotateY;
            double rotationRadians;
            PointF retPoint;
            try
            {
                //shift x and y relative to 0,0 origin
                offsetX = (sourcePoint.X + (basePoint.X * -1));
                offsetY = (sourcePoint.Y + (basePoint.Y * -1));
                //convert to radians. take absolute value (necessary for x coord only).
                offsetX = Math.Abs(offsetX * (Math.PI / 180));
                offsetY = offsetY * (Math.PI / 180);
                rotationRadians = rotationAngle * (Math.PI / 180);
                //get distance from origin to source point
                r = Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2));
                //get current angle of orientation
                theta = Math.Atan(offsetY / offsetX);
                // add rotation value to theta to get new angle of orientation
                offsetTheta = theta + rotationRadians;
                //calculate new x coord
                rotateX = r * Math.Cos(offsetTheta);
                //calculate new y coord
                rotateY = r * Math.Sin(offsetTheta);
                //convert new x and y back to decimal degrees
                rotateX = rotateX * (180 / Math.PI);
                rotateY = rotateY * (180 / Math.PI);
                //shift new x and y relative to base point
                rotateX = (rotateX + basePoint.X);
                rotateY = (rotateY + basePoint.Y);
                //return new point
                retPoint = new PointF();
                retPoint.X = (float)rotateX;
                retPoint.Y = (float)rotateY;
                return retPoint;
            }
            catch
            {
                return sourcePoint;
            }
        }
    
    






        
        private void CREATE_GKOD()
        {
            string code = "%" + Environment.NewLine;

            code+= ParsePrimitivesToGkode(items);

            Clipboard.SetText(code);
            DataLoader.ReadFromBuffer(false);
            code = "";
        }

        // Получение G-кода из данных конструктора
        private string ParsePrimitivesToGkode(ConstructionObject _item)
        {
            string returnValue = "";

            // вначале пройдемся по подчиненным элементам, переданного элемента

            foreach (var VARIABLE in _item.items)
            {
                returnValue += ParsePrimitivesToGkode(VARIABLE);
            }

            // Если данный примитив точка, то просто вернем строку с координатами
            if (_item.type == PrimitivType.Point)
            {
                PrimitivPoint pp = GetPointFromGUID(_item.GUID);

                if (pp == null) return "";

                return "X" + pp.X.ToString("#0.###") + " Y" + pp.Y.ToString("#0.###") + " Z" + pp.Z.ToString("#0.###") + Environment.NewLine;
            }

            if (_item.type == PrimitivType.Gkode)
            {
                PrimitivGkode pg = GetGkodeFromGUID(_item.GUID);

                if (pg == null) return "";

                return pg.Gkode;
            }

            if (_item.type == PrimitivType.Catalog)
            {
                // выполним если необходимо смещение данных в данной группе
                PrimitivCatalog pc = GetCatalogFromGUID(_item.GUID);

                if (pc == null) return returnValue; //В случае ошибки просто вернем текст с кодом без дальнейших модификаций

                if (pc.DeltaX == 0 && pc.DeltaY == 0 && pc.DeltaZ == 0) return returnValue; //смещений нет, поэтому вернем как есть

                //а вот тут уже нужно пропарсить код и добавить смещение в код.
                return AddToGkodeOffsetAxes(returnValue, (decimal)pc.DeltaX, (decimal)pc.DeltaY, (decimal)pc.DeltaZ);
            }

            if (_item.type == PrimitivType.Loop)
            {
                PrimitivLoop pl = GetLoopFromGUID(_item.GUID);

                if (pl == null) return returnValue;

                bool increment = true; //направление счетчика на увеличение или уменьшение

                if (pl.CStart > pl.CStop) increment = false;

                bool needLoops = true;

                string tmpS = "";

                decimal dValueNow = (decimal) pl.CStart;
                decimal dValueStop = (decimal)pl.CStop;
                decimal dValueStep = (decimal)pl.CStep;
    
                decimal deltaX = 0;
                decimal deltaY = 0;
                decimal deltaZ = 0;

                while (needLoops)
                {
                    if (increment && dValueNow >= dValueStop) needLoops = false;

                    if (!increment && dValueNow <= dValueStop) needLoops = false;

                    if (pl.AllowDeltaX) deltaX = dValueNow;

                    if (pl.AllowDeltaY) deltaY = dValueNow;

                    if (pl.AllowDeltaZ) deltaZ = dValueNow;

                    tmpS += AddToGkodeOffsetAxes(returnValue, deltaX, deltaY, deltaZ) + Environment.NewLine;

                    if (increment)
                    {
                        dValueNow += dValueStep;
                    }
                    else
                    {
                        dValueNow -= dValueStep;
                    }
                }

                return tmpS;
            }


            if (_item.type == PrimitivType.Arc)
            {

                PrimitivArc pr = GetArcFromGUID(_item.GUID);

                if (pr == null) return returnValue;

                bool increment = true; //направление счетчика на увеличение или уменьшение

                if (pr.AngleStart > pr.AngleStop) increment = false;

                bool needLoops = true;

                string tmpS = "";

                double dValueNow = (double)pr.AngleStart;
                double dValueStop = (double)pr.AngleStop;
                double dValueStep = (double)pr.AngleStep;

                double cX = (double)pr.X;
                double cY = (double)pr.Y;

                double dRadius = (double) pr.Radius;

                double deltRadius = (double) pr.DeltaStepRadius;

                while (needLoops)
                {
                    if (increment && dValueNow >= dValueStop) needLoops = false;

                    if (!increment && dValueNow <= dValueStop) needLoops = false;

                    double x1 = cX + dRadius * Math.Cos(dValueNow * (Math.PI / 180));
                    double y1 = cY + dRadius * Math.Sin(dValueNow * (Math.PI / 180));

                    tmpS += "X" + x1.ToString("#0.###") + " Y" + y1.ToString("#0.###") + " Z" + pr.Z.ToString("#0.###") + Environment.NewLine;

                    dRadius += deltRadius;

                    if (increment)
                    {
                        dValueNow += dValueStep;
                    }
                    else
                    {
                        dValueNow -= dValueStep;
                    }
                }

                return tmpS;

                ////PrimitivPoint pp = GetPointFromGUID(_item.GUID);
                ////if (pp == null) return "";
                ////return "X" + pp.X.ToString("#0.###") + " Y" + pp.Y.ToString("#0.###") + " Z" + pp.Z.ToString("#0.###") + Environment.NewLine;




                //    double dZ = deltaZ;  // координата в мм

                //    double dSr = node.Rotate.DeltaStepRadius; // дельта изменения радиуса с каждым шагом
                //    double dRo = node.Rotate.RotateRotates;   // угол дополнительного вращения объекта
                //    double nowdRo = 0;

                //    double dRadius = node.Rotate.Radius;

                //    for (double angle = node.Rotate.AngleStart; angle < node.Rotate.AngleStop; angle += node.Rotate.AngleStep)
                //    {



                //        var dX = x1 + deltaX;       // координата в мм
                //        var dY = y1 + deltaY;       // координата в мм
                //        //dZ += _node.catalog.Z;

                //        foreach (PrimitivNode variable in node.Nodes)
                //        {
                //            ParsePrimitivesToGkode(ref strCode, variable, dX, dY, dZ, nowdRo);
                //            nowdRo += dRo;

                //        }

                //        dRadius += dSr;

                //    }





            }





            //, double deltaX = 0, double deltaY = 0, double deltaZ = 0, double deltaRotate = 0

            //if (node.TypeNode == PrimitivType.Point)
            //{
            //    double rotatedX = node.Point.X * Math.Cos(deltaRotate * (Math.PI / 180)) - node.Point.Y * Math.Sin(deltaRotate * (Math.PI / 180));
            //    double rotatedY = node.Point.X * Math.Sin(deltaRotate * (Math.PI / 180)) + node.Point.Y * Math.Cos(deltaRotate * (Math.PI / 180));

            //    double xpp = rotatedX + deltaX;
            //    double ypp = rotatedY + deltaY;
            //    double zpp = node.Point.Z + deltaZ;

            //    strCode += "G1 X" + (Math.Round(xpp, 3)) + " Y" + (Math.Round(ypp, 3)) + " Z" + (zpp) + "\n";
            //    return;
            //}


            //if (node.TypeNode == PrimitivType.Catalog)
            //{
            //    double dX = deltaX;       // координата в мм
            //    double dY = deltaY;       // координата в мм
            //    double dZ = deltaZ;       // координата в мм
            //    double dR = deltaRotate;  // значение в градусах

            //    dX += node.Catalog.DeltaX;
            //    dY += node.Catalog.DeltaY;
            //    dZ += node.Catalog.DeltaZ;
            //    //dR += node.Catalog.DeltaRotate;

            //    foreach (PrimitivNode variable in node.Nodes)
            //    {
            //        ParsePrimitivesToGkode(ref strCode, variable, dX, dY, dZ, 0);
            //    }
            //}






            return returnValue;
        }

        private void treeDataConstructor_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // что-бы не менялся значек
            if (treeDataConstructor.SelectedNode != null)
            {
                treeDataConstructor.SelectedImageIndex = treeDataConstructor.SelectedNode.ImageIndex;
            }
        }

        private void treeDataConstructor_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            treeDataConstructor.ExpandAll();
            e.Cancel = true;
        }

        /// <summary>
        /// Буфер для хранения промежуточных данных
        /// </summary>
        private ConstructionObject _clipboardPrimitivNode;

        private void ToolStripMenuCopyDATA_Click(object sender, EventArgs e)
        {
            if (treeDataConstructor.SelectedNode == null) return;

            // а вот тут сложнее нужно сменить все гуиды, и во всех коллекциях добавить новые элементы


          //  _clipboardPrimitivNode = FindNodeWithGuid(treeDataConstructor.SelectedNode.Name);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeDataConstructor.SelectedNode == null) return;

            if (_clipboardPrimitivNode == null) return;

            ConstructionObject tmp = GetObjectFromGUID(treeDataConstructor.SelectedNode.Name);

            tmp.items.Add(_clipboardPrimitivNode);

            //PrimitivNode tmp = FindNodeWithGuid(treeDataConstructor.SelectedNode.Name);

            //// надо перед вставкой все гуиды поменять на новые
            //_clipboardPrimitivNode.SetNewGUID();

            //// а теперь вставляем
            //tmp.Nodes.Add(_clipboardPrimitivNode);

            _clipboardPrimitivNode = null;

            RefreshTree();
        }





        private void DeleteNode(string _guid,ConstructionObject _tmp = null)
        {
            //проверим что это не самый первый элемент списка
            if (items.GUID == _guid) return; // ну не можем мы его удалить....

            ConstructionObject tmp = items;

            if (_tmp != null) tmp = _tmp;

            foreach (ConstructionObject VARIABLE in tmp.items)
            {
                if (VARIABLE.GUID == _guid)
                {
                    tmp.items.Remove(VARIABLE); 
                    break;
                }

                DeleteNode(_guid, VARIABLE);
            }

            RefreshTree();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeDataConstructor.SelectedNode == null) return;

            _clipboardPrimitivNode = GetObjectFromGUID(treeDataConstructor.SelectedNode.Name);

            DeleteNode(treeDataConstructor.SelectedNode.Name);

            RefreshTree();
        }

        private void delPrimitivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeDataConstructor.SelectedNode == null) return;

            if (MessageBox.Show("Удалить выделенный примитив?", "Удаление", MessageBoxButtons.OKCancel) != DialogResult.OK) return;

            DeleteNode(treeDataConstructor.SelectedNode.Name);

            RefreshTree();
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            pasteToolStripMenuItem.Enabled = (_clipboardPrimitivNode != null);
        }

        private void treeDataConstructor_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeDataConstructor.ExpandAll();
            //e.Cancel = true;
        }

        private void treeDataConstructor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }

        }







        #region Процедуры перемещения элементов по дереву

       private void treeDataConstructor_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // С нажатой левой кнопкой мыши мы перемещаем элемент
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // с нажатой правой кнопкой мы дублируем элемент
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void treeDataConstructor_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void treeDataConstructor_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = treeDataConstructor.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            treeDataConstructor.SelectedNode = treeDataConstructor.GetNodeAt(targetPoint);
        }

        private void treeDataConstructor_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeDataConstructor.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeDataConstructor.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current 
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    // тут сделаем перемещение
                    _clipboardPrimitivNode = GetObjectFromGUID(draggedNode.Name);
                    DeleteNode(draggedNode.Name);
                    ConstructionObject tmp = GetObjectFromGUID(targetNode.Name);
                    tmp.items.Add(_clipboardPrimitivNode);
                    _clipboardPrimitivNode = null;

                    RefreshTree();

                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                }

                // If it is a copy operation, clone the dragged node 
                // and add it to the node at the drop location.
                else if (e.Effect == DragDropEffects.Copy)
                {
                  
                    //TODO: копирование не реализовано
                    
                    
                    
                    //targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                }

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();
            }
        }

        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }

        #endregion

        private void arcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewArc();
        }

        private void arcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddNewArc();
        }

 



    }

    [Serializable]
    public class DataSaveLoad
    {
        public ConstructionObject items;
        public List<PrimitivCatalog> itemsCatalog;
        public List<PrimitivPoint> itemsPoint;
        public List<PrimitivLoop> itemsLoop;
        public List<PrimitivArc> itemsArc;
        public List<PrimitivGkode> ItemsGkode;

        public DataSaveLoad()
        {
            items = new ConstructionObject();
            itemsCatalog = new List<PrimitivCatalog>();
            itemsPoint = new List<PrimitivPoint>();
            itemsLoop = new List<PrimitivLoop>();
            itemsArc = new List<PrimitivArc>();
            ItemsGkode = new List<PrimitivGkode>();

        }
    }



    /// <summary>
    /// Описание одиночного объекта конструктора
    /// </summary>
    [Serializable]
    public class ConstructionObject
    {
        public PrimitivType type;
        public string GUID;
        public List<ConstructionObject> items;

        public ConstructionObject(PrimitivType _type, string _guid, List<ConstructionObject> _items )
        {
            type = _type;
            GUID = _guid;
            items = _items;
        }

        public ConstructionObject()
        {
            type = PrimitivType.Catalog;
            GUID = System.Guid.NewGuid().ToString();
            items = new List<ConstructionObject>();
        }
    }

    /// <summary>
    /// Типы примитивов
    /// </summary>
    [Serializable]
    public enum PrimitivType
    {
        Catalog,
        Point,
        Loop,
        Arc,
        Gkode
    }

    /// <summary>
    /// Класс описания группы элементов
    /// </summary>
    [Serializable]
    public class PrimitivCatalog
    {
        //Смещения элементов в нутри данной группы
        public double DeltaX;       // координата в мм
        public double DeltaY;       // координата в мм
        public double DeltaZ;       // координата в мм

        // для представления
        public string Name;
        // для идентификации
        public string GUID;

        public PrimitivCatalog(double deltaX, double deltaY, double deltaZ, string name = "Группа", string _guid = "")
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
            DeltaZ = deltaZ;
            Name = name;

            if (_guid == "") GUID = System.Guid.NewGuid().ToString();
            else GUID = _guid;
        }
    }

    /// <summary>
    /// Класс описания дуги (пока на плоскости xy)
    /// </summary>
    [Serializable]
    public class PrimitivArc
    {
        //центр вращения
        public double X;  // координата в мм
        public double Y;  // координата в мм
        public double Z;  // координата в мм
        // шаг вращения
        public double AngleStart;  // градус начала
        public double AngleStop;   // градус окончания
        public double AngleStep;   // градус шага
        public double Radius;      // радиус окружности

        public double DeltaStepRadius; // изменение радиуса с каждым шагом

        // для представления
        public string Name;
        // для идентификации
        public string GUID;

        public PrimitivArc(double x, double y, double z, double angleStart, double angleStop, double angleStep, double radius, double deltaStepRadius, string name = "Вращение", string _guid = "")
        {
            X = x;
            Y = y;
            Z = z;

            AngleStart = angleStart;
            AngleStop = angleStop;
            AngleStep = angleStep;
            Radius = radius;
            DeltaStepRadius = deltaStepRadius;

            Name = name;

            if (_guid == "") GUID = System.Guid.NewGuid().ToString();
            else GUID = _guid;
        }
    }

    /// <summary>
    /// Класс описания цикла
    /// </summary>
    [Serializable]
    public class PrimitivLoop
    {
        public double CStart;       // начальное значение цикла
        public double CStop;       // конечное значение цикла
        public double CStep;       // шаг
        public bool AllowDeltaX; // необходимость применять цикл к оси X
        public bool AllowDeltaY; // необходимость применять цикл к оси Y
        public bool AllowDeltaZ; // необходимость применять цикл к оси Z

        // для представления
        public string Name;
        // для идентификации
        public string GUID;

        public PrimitivLoop(double cStart, double cStop, double cStep, bool allowDeltaX, bool allowDeltaY, bool allowDeltaZ, string name = "Цикл", string _guid = "")
        {
            CStart = cStart;
            CStop = cStop;
            CStep = cStep;
            Name = name;
            AllowDeltaX = allowDeltaX;
            AllowDeltaY = allowDeltaY;
            AllowDeltaZ = allowDeltaZ;

            if (_guid == "") GUID = System.Guid.NewGuid().ToString();
            else GUID = _guid;
        }
    }

    /// <summary>
    /// Примитив точка
    /// </summary>
    [Serializable]
    public class PrimitivPoint
    {
        public double X;       // координата в мм
        public double Y;       // координата в мм
        public double Z;       // координата в мм

        // для представления
        public string Name;
        // для идентификации
        public string GUID;

        public PrimitivPoint(double x, double y, double z, string _guid = "")
        {
            X = x;
            Y = y;
            Z = z;

            Name = @"Точка: (" + X + @", " + Y + @", " + Z + @")";
            
            if (_guid == "") GUID = System.Guid.NewGuid().ToString();
            else GUID = _guid;
        }
    }

    [Serializable]
    public class PrimitivGkode
    {
        public string Gkode;
        // для представления
        public string Name;
        // для идентификации
        public string GUID;

        public PrimitivGkode(string _gkode, string name = "G-код", string _guid = "")
        {
            Gkode = _gkode;
            Name = name;

            if (_guid == "") GUID = System.Guid.NewGuid().ToString();
            else GUID = _guid;           

        }

    }

    /// <summary>
    /// Класс для хранения данных, конструктора
    /// </summary>
    [Serializable]
    public class PrimitivNode
    {
        public string Guid;
        public PrimitivType TypeNode;

        public PrimitivCatalog Catalog;
        public PrimitivPoint Point;
        public PrimitivLoop Cycler;
        public PrimitivArc Rotate;

        public List<PrimitivNode> Nodes;

        private PrimitivNode()
        {
            Guid = "";
            TypeNode = PrimitivType.Catalog;

            Catalog = null;
            Point = null;
            Cycler = null;
            Rotate = null;

            Nodes = new List<PrimitivNode>();
        }

        // ReSharper disable once InconsistentNaming
        public PrimitivNode(PrimitivCatalog _catalog)
        {
            Guid = System.Guid.NewGuid().ToString();
            TypeNode = PrimitivType.Catalog;

            Catalog = _catalog;
            Point = null;
            Cycler = null;
            Rotate = null;

            Nodes = new List<PrimitivNode>();
        }

        public PrimitivNode(PrimitivLoop cycle)
        {
            Guid = System.Guid.NewGuid().ToString();
            TypeNode = PrimitivType.Loop;

            Catalog = null;
            Point = null;
            Cycler = cycle;
            Rotate = null;

            Nodes = new List<PrimitivNode>();
        }

        // ReSharper disable once InconsistentNaming
        public PrimitivNode(PrimitivPoint _point)
        {
            Guid = System.Guid.NewGuid().ToString();
            TypeNode = PrimitivType.Point;

            Catalog = null;
            Point = _point;
            Cycler = null;
            Rotate = null;

            Nodes = new List<PrimitivNode>();
        }

        // ReSharper disable once InconsistentNaming
        public PrimitivNode(PrimitivArc _rotate)
        {
            Guid = System.Guid.NewGuid().ToString();
            TypeNode = PrimitivType.Arc;

            Catalog = null;
            Point = null;
            Cycler = null;
            Rotate = _rotate;

            Nodes = new List<PrimitivNode>();
        }


        public void SetNewGUID()
        {
            Guid = System.Guid.NewGuid().ToString();
            foreach (PrimitivNode _nodes in Nodes)
            {
                _nodes.SetNewGUID();
            }
        }

    }
}
