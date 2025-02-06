using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Eto.Drawing;
using DWSIM.ExtensionMethods;
using DWSIM.ExtensionMethods.Eto;
using ext = DWSIM.UI.Shared.Common;
using DWSIM.UI.Shared;
using OxyPlot;

namespace DWSIM.AI.ConvergenceAssistant.Editors
{
    public static class ManagerForm
    {

        public static void DisplayConfigForm()
        {

            var c1 = ext.GetDefaultContainer();
            c1.Padding = new Padding(20);
            c1.Tag = "Settings";

            var options = new string[] {"Disabled", "Provide Initial Estimates", "Provide Initial Estimates (2-pass)", "Provide Estimates and Solutions", "Provide Solutions" };

            c1.CreateAndAddDropDownRow("AI-Assisted Convergence Level", options.ToList(), GlobalSettings.Settings.AIAssistedConvergenceLevel,
                (dd, e) => { 
                    GlobalSettings.Settings.AIAssistedConvergenceLevel = dd.SelectedIndex;
                    if (!Manager.Initialized) Manager.Initialize();
                });

            var c2 = ext.GetDefaultContainer();
            c2.Padding = new Padding(20);
            c2.Tag = "Models";

            var sf = GlobalSettings.Settings.DpiScale;

            TextArea tb = new TextArea
            {
                Width = (int)(350 * sf),
                Font = new Font(FontFamilies.Monospace, 10.0f)
            };

            var plot = new Eto.OxyPlot.Plot();

            plot.Model = new PlotModel();
            plot.Model.Background = OxyPlot.OxyColors.White;
            plot.Model.TitleFontSize = 12;
            plot.Model.SubtitleFontSize = 10;
            plot.Model.Axes.Add(new OxyPlot.Axes.LinearAxis()
            {
                MajorGridlineStyle = OxyPlot.LineStyle.Dash,
                MinorGridlineStyle = OxyPlot.LineStyle.Dot,
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                FontSize = 10,
                Title = "Epoch",
                Key = "x",
            });
            plot.Model.Axes.Add(new OxyPlot.Axes.LogarithmicAxis()
            {
                MajorGridlineStyle = OxyPlot.LineStyle.Dash,
                MinorGridlineStyle = OxyPlot.LineStyle.Dot,
                Position = OxyPlot.Axes.AxisPosition.Left,
                FontSize = 10,
                Title = "MSE"
            });
            plot.Model.LegendFontSize = 11;
            plot.Model.LegendPlacement = OxyPlot.LegendPlacement.Outside;
            plot.Model.LegendOrientation = OxyPlot.LegendOrientation.Horizontal;
            plot.Model.LegendPosition = OxyPlot.LegendPosition.BottomCenter;
            plot.Model.TitleHorizontalAlignment = OxyPlot.TitleHorizontalAlignment.CenteredWithinView;
            plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Red, "Training");
            plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Blue, "Testing");
            plot.Model.Title = "Model Training Results";

            var tl = new TableLayout(new TableRow(tb, plot)) { Spacing = new Size(10, 10), Height = (int)(500 * sf) };

            c2.CreateAndAddButtonRow("Train", null, (btn, e) => {
                plot.Model.Series.Clear();
                plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Red, "Training");
                plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Blue, "Testing");
                Task.Run(() => Manager.UpdateModels(tb, plot)); 
            });

            c2.CreateAndAddControlRow(tl);

            var c3 = ext.GetDefaultContainer();
            c3.Padding = new Padding(20);
            c3.Tag = "Data";

            var form = Extensions2.GetTabbedForm("AI-Assisted Convergence Manager", 800, 600, new DynamicLayout[] { c1, c2, c3 });
            form.SetFontAndPadding();
            form.Show();
            form.Center();

        }

    }
}
