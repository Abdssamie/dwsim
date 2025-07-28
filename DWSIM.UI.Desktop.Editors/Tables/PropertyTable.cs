using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using Eto.Drawing;
using DWSIM.UI.Shared;
using DWSIM.Drawing.SkiaSharp.GraphicObjects.Tables;
using DWSIM.Interfaces.Enums;

namespace DWSIM.UI.Desktop.Editors.Tables
{
    public class PropertyTableEditor : Form
    {

        public TableGraphic Table;


        public Button btnOK;
        public ListBox lvObjects;
        public TreeGridView lvProperties;

        public PropertyTableEditor()
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

            Title = "Configure Property Table";

            var container = new TableLayout();

            var topcontainer = new TableLayout();
            var centercontainer = new TableLayout();
            var bottomcontainer = new TableLayout();

            lvObjects = new ListBox { Height = 300, Width = 200 };

            lvProperties = new TreeGridView { AllowMultipleSelection = false, Height = 300, Width = 350 };

            lvProperties.AllowMultipleSelection = false;
            lvProperties.ShowHeader = false;
            lvProperties.Columns.Clear();
            lvProperties.Columns.Add(new GridColumn { DataCell = new TextBoxCell(0), Visible = false});
            lvProperties.Columns.Add(new GridColumn { DataCell = new CheckBoxCell(1), Sortable = true, Editable = true });
            lvProperties.Columns.Add(new GridColumn { DataCell = new TextBoxCell(2), Sortable = true, Editable = false });

            btnOK = new Button { Text = "Close", Enabled = true };

            btnOK.Click += (sender, e) =>
            {
                Close();
            };

            var header = new TextBox();
            header.TextChanged += (sender, e) =>
            {
                Table.HeaderText = header.Text;
            };

            topcontainer.Rows.Add(new TableRow(new Label { Text = "Table Header", VerticalAlignment = VerticalAlignment.Center }, header));
            topcontainer.Padding = new Padding(5, 5, 5, 5);
            topcontainer.Spacing = new Size(10, 10);

            centercontainer.Rows.Add(new TableRow(new Label { Text = "Object / Property", VerticalAlignment = VerticalAlignment.Center }));
            centercontainer.Rows.Add(new TableRow(lvObjects, lvProperties));
            centercontainer.Padding = new Padding(5, 5, 5, 5);
            centercontainer.Spacing = new Size(10, 10);

            bottomcontainer.Rows.Add(new TableRow(null, btnOK));
            bottomcontainer.Padding = new Padding(5, 5, 5, 5);
            bottomcontainer.Spacing = new Size(10, 10);

            container.Rows.Add(new TableRow(topcontainer));
            container.Rows.Add(new TableRow(centercontainer));
            container.Rows.Add(new TableRow(bottomcontainer));
            container.Rows.Add(null);

            container.Padding = new Padding(5, 5, 5, 5);

            Content = container;

            var tgc = new TreeGridItemCollection();

            lvObjects.SelectedIndexChanged += (sender, e) =>
            {
                if (lvObjects.SelectedIndex < 0) return;
                if (lvObjects.SelectedValue != null)
                {
                    tgc = new TreeGridItemCollection();
                    foreach (var item in Table.Flowsheet.SimulationObjects[lvObjects.SelectedKey].GetProperties(PropertyType.ALL))
                    {
                        var li = new TreeGridItem();
                        li.Values = new object[] { item, Table.VisibleProperties.ContainsKey(lvObjects.SelectedKey) ?
                            Table.VisibleProperties[lvObjects.SelectedKey].Contains(item) : 
                            false, Table.Flowsheet.GetTranslatedString(item) };
                        tgc.Add(li);
                    }
                    lvProperties.DataStore = tgc;
                }
            };

            lvProperties.CellEdited += (sender, e) =>
            {
                if (e.Item != null)
                {
                    var tgitem = (TreeGridItem)e.Item;
                    if (!Table.VisibleProperties.ContainsKey(lvObjects.SelectedKey))
                    {
                        Table.VisibleProperties.Add(lvObjects.SelectedKey, new List<string>());
                    }
                    if ((bool)tgitem.Values[1] == true)
                    {
                        if (!Table.VisibleProperties[lvObjects.SelectedKey].Contains(tgitem.Values[0].ToString()))
                        {
                            Table.VisibleProperties[lvObjects.SelectedKey].Add(tgitem.Values[0].ToString());
                        }
                    }
                    else
                    {
                        if (Table.VisibleProperties[lvObjects.SelectedKey].Contains(tgitem.Values[0].ToString()))
                        {
                            Table.VisibleProperties[lvObjects.SelectedKey].Remove(tgitem.Values[0].ToString());
                        }
                    }

                }
            };

            Load += (sender, e) =>
            {
                header.Text = Table.HeaderText;
                lvObjects.Items.Clear();
                foreach (var obj in Table.Flowsheet.SimulationObjects.Values.OrderBy(o => o.GraphicObject.Tag))
                {
                    lvObjects.Items.Add(obj.GraphicObject.Tag, obj.Name);
                }

            };

            this.Center();
            
        }

    }
}
