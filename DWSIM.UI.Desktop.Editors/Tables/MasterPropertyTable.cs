using System;
using System.Collections.Generic;
using System.Linq;
using DWSIM.Interfaces.Enums.GraphicObjects;
using Eto.Forms;
using Eto.Drawing;
using DWSIM.Drawing.SkiaSharp.GraphicObjects.Tables;
using DWSIM.Interfaces.Enums;
using DWSIM.UI.Shared;

namespace DWSIM.UI.Desktop.Editors.Tables
{
    public class MasterPropertyTableEditor : Form
    {

        public MasterTableGraphic Table;

        public Button btnOK, btnOrderDown, btnOrderUp;
        public TreeGridView lvObjects, lvProps;
        public DropDown cbObjectType, cbOrderBy;
        public NumericStepper nsNumberOfLines;

        TreeGridItemCollection tgc, tgc2;

        public MasterPropertyTableEditor()
        {
            Init();
        }

        void Init()
        {

            string imgprefix = "DWSIM.UI.Desktop.Editors.Resources.Icons.";

            Icon = Eto.Drawing.Icon.FromResource(imgprefix + "DWSIM_ico.ico");

            Maximizable = false;
            Minimizable = false;
            WindowStyle = Eto.Forms.WindowStyle.Default;

            Title = "Configure Master Property Table";

            var container = new TableLayout();

            var topcontainer = new TableLayout();
            var topcontainer2 = new TableLayout();
            var centercontainer = new TableLayout();
            var bottomcontainer = new TableLayout();

            var tableleft = new TableLayout { Width = 200 };

            btnOrderUp = new Button { Text = "˄", Width = 25 };
            btnOrderDown = new Button { Text = "˅", Width = 25 };

            tableleft.Rows.Add(new TableRow(new Label { Text = "Show Objects/Properties:", VerticalAlignment = VerticalAlignment.Center }));
            tableleft.Rows.Add(null);
            tableleft.Rows.Add(new TableRow(new Label { Text = "Order objects", VerticalAlignment = VerticalAlignment.Center }, null, btnOrderUp, btnOrderDown));

            lvObjects = new TreeGridView { AllowMultipleSelection = false, Height = 300, Width = 200 };

            lvObjects.ShowHeader = false;
            lvObjects.Columns.Clear();
            lvObjects.Columns.Add(new GridColumn { DataCell = new TextBoxCell(0), Visible = false });
            lvObjects.Columns.Add(new GridColumn { DataCell = new CheckBoxCell(1), Sortable = true, Editable = true });
            lvObjects.Columns.Add(new GridColumn { DataCell = new TextBoxCell(2), Sortable = true, Editable = false });

            lvProps = new TreeGridView { AllowMultipleSelection = false, Height = 300, Width = 300 };

            lvProps.ShowHeader = false;
            lvProps.Columns.Clear();
            lvProps.Columns.Add(new GridColumn { DataCell = new TextBoxCell(0), Visible = false });
            lvProps.Columns.Add(new GridColumn { DataCell = new CheckBoxCell(1), Sortable = true, Editable = true });
            lvProps.Columns.Add(new GridColumn { DataCell = new TextBoxCell(2), Sortable = true, Editable = false });

            btnOK = new Button { Text = "Close", Enabled = true };

            btnOK.Click += (sender, e) => Close();

            var header = new TextBox();
            header.TextChanged += (sender, e) => Table.HeaderText = header.Text;

            cbObjectType = new DropDown { Width = 300 };
            cbOrderBy = new DropDown { Width = 200 };

            topcontainer2.Rows.Add(new TableRow(new Label { Text = "Show Objects of Type", VerticalAlignment = VerticalAlignment.Center }, cbObjectType, null, new Label { Text = "Order Objects By", VerticalAlignment = VerticalAlignment.Center }, cbOrderBy));
            topcontainer2.Padding = new Padding(5, 5, 5, 5);
            topcontainer2.Spacing = new Size(10, 10);

            nsNumberOfLines = new NumericStepper { MinValue = 1, MaxValue = 10, Value = 1, DecimalPlaces = 0, Increment = 1.0 };

            nsNumberOfLines.ValueChanged += (s, e) =>
            {
                if (Table != null) Table.NumberOfLines = (int)nsNumberOfLines.Value;
            };

            topcontainer.Rows.Add(new TableRow(new Label { Text = "Table Header", VerticalAlignment = VerticalAlignment.Center }, header, new Label { Text = "Number of Grouping Rows", VerticalAlignment = VerticalAlignment.Center }, nsNumberOfLines));
            topcontainer.Rows[0].Cells[1].ScaleWidth = true;
            topcontainer.Padding = new Padding(5, 5, 5, 5);
            topcontainer.Spacing = new Size(10, 10);

            centercontainer.Rows.Add(new TableRow(tableleft, lvObjects, lvProps));
            centercontainer.Padding = new Padding(5, 5, 5, 5);
            centercontainer.Spacing = new Size(10, 10);

            bottomcontainer.Rows.Add(new TableRow(null, btnOK));
            bottomcontainer.Padding = new Padding(5, 5, 5, 5);
            bottomcontainer.Spacing = new Size(10, 10);

            container.Rows.Add(new TableRow(topcontainer));
            container.Rows.Add(new TableRow(topcontainer2));
            container.Rows.Add(new TableRow(centercontainer));
            container.Rows.Add(new TableRow(bottomcontainer));
            container.Rows.Add(null);

            container.Padding = new Padding(5, 5, 5, 5);

            Content = container;

            cbObjectType.SelectedIndexChanged += (sender, e) =>
            {
                if (Loaded)
                {
                    Table.ObjectFamily = (ObjectType)Enum.Parse(Table.ObjectType.GetType(), cbObjectType.SelectedValue.ToString());
                    Table.ObjectList.Clear();
                    Table.SortedList.Clear();
                    Table.PropertyList.Clear();
                    Populate();
                }
            };

            cbOrderBy.SelectedIndexChanged += (sender, e) =>
            {
                if (cbOrderBy.SelectedIndex < 0) return;
                Table.SortBy = cbOrderBy.SelectedValue.ToString();
                if (Table.SortBy == "Custom")
                {
                    btnOrderDown.Enabled = true;
                    btnOrderUp.Enabled = true;
                }
                else
                {
                    btnOrderDown.Enabled = false;
                    btnOrderUp.Enabled = false;
                }
                Populate();
            };

            lvObjects.CellEdited += (sender, e) =>
            {
                if (e.Item != null)
                {
                    var tgitem = (TreeGridItem)e.Item;
                    var key = tgitem.Values[2].ToString();
                    if (!Table.ObjectList.ContainsKey(key))
                    {
                        Table.ObjectList.Add(key, (bool)tgitem.Values[1]);
                    }
                    else
                    {
                                        
                    }
                    {
                        Table.ObjectList[key] = (bool)tgitem.Values[1];
                    }
                    PopulateProps();
                }
            };

            lvProps.CellEdited += (sender, e) =>
            {
                if (e.Item != null)
                {
                    var tgitem = (TreeGridItem)e.Item;
                    var key = tgitem.Values[0].ToString();
                    if (!Table.PropertyList.ContainsKey(key))
                    {
                        Table.PropertyList.Add(key, (bool)tgitem.Values[1]);
                    }
                    else
                    {
                        Table.PropertyList[key] = (bool)tgitem.Values[1];
                    }
                }
            };

            btnOrderUp.Click += (sender, e) =>
            {
                int index = 0;
                if (lvObjects.SelectedItem != null)
                {
                    index = lvObjects.SelectedRow;
                    if (index != 0)
                    {
                        var item = tgc[index];
                        tgc.RemoveAt(index);
                        tgc.Insert(index - 1, item);
                        lvObjects.ReloadData();
                        lvObjects.SelectedRow = index - 1;
                    }
                }
            };

            btnOrderDown.Click += (sender, e) =>
            {
                int index = 0;
                if (lvObjects.SelectedItem != null)
                {
                    index = this.lvObjects.SelectedRow;
                    if (index != tgc.Count - 1)
                    {
                        var item = tgc[index];
                        tgc.RemoveAt(index);
                        tgc.Insert(index + 1, item);
                        lvObjects.ReloadData();
                        lvObjects.SelectedRow = index + 1;
                    }
                }
            };

            Load += (sender, e) =>
            {
                header.Text = Table.HeaderText;

                var names = Enum.GetNames(Table.ObjectType.GetType());
                foreach (var name in names)
                {
                    cbObjectType.Items.Add(name);
                }

                var sitems = Table.SortableItems;
                foreach (var item in sitems)
                {
                    cbOrderBy.Items.Add(item);
                }

                cbObjectType.SelectedIndex = names.ToList().IndexOf(Table.ObjectFamily.ToString());
                cbOrderBy.SelectedIndex = sitems.ToList().IndexOf(Table.SortBy);

                nsNumberOfLines.Value = Table.NumberOfLines;

                Populate();

                this.Center();
            };

            Closed += (sender, e) =>
            {
                Table.SortedList.Clear();
                List<string> list = new List<string>();
                foreach (TreeGridItem lvi in tgc)
                {
                    list.Add(lvi.Values[2].ToString());
                }
                Table.SortedList = list;
            };

        }

        public void Populate()
        {

            tgc = new TreeGridItemCollection();

            foreach (var item in Table.SortedList)
            {
                var obj = Table.Flowsheet.GetFlowsheetSimulationObject(item);
                if (obj != null)
                {
                    var li = new TreeGridItem();
                    li.Values = new object[] { obj.Name, Table.ObjectList.ContainsKey(obj.GraphicObject.Tag), obj.GraphicObject.Tag };
                    tgc.Add(li);
                }
            }
            foreach (var obj in Table.Flowsheet.SimulationObjects.Values)
            {
                if (obj.GraphicObject.ObjectType == Table.ObjectFamily & !Table.SortedList.Contains(obj.GraphicObject.Tag))
                {
                    var li = new TreeGridItem();
                    li.Values = new object[] { obj.Name, Table.ObjectList.ContainsKey(obj.GraphicObject.Tag), obj.GraphicObject.Tag };
                    tgc.Add(li);
                }
            }
            lvObjects.DataStore = tgc;

            PopulateProps();
        }

        private void PopulateProps()
        {

            string[] props = null;

            tgc2 = new TreeGridItemCollection();

            if (Table.ObjectList.Count > 0)
            {
                foreach (string s in Table.ObjectList.Keys)
                {
                    props = Table.Flowsheet.GetFlowsheetSimulationObject(s).GetProperties(PropertyType.ALL);
                    break;
                }
                foreach (string p in props)
                {
                    if (!Table.PropertyList.ContainsKey(p))
                        Table.PropertyList.Add(p, false);
                    var li = new TreeGridItem();
                    li.Values = new object[] { p, Table.PropertyList[p], Table.Flowsheet.GetTranslatedString(p) };
                    tgc2.Add(li);
                }
                lvProps.DataStore = tgc2;
            }

        }

    }
}
