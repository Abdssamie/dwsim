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
using DWSIM.Interfaces;

namespace DWSIM.AI.ConvergenceAssistant.Editors
{
    public static class ManagerForm
    {

        public static void DisplayConfigForm(IFlowsheet Flowsheet)
        {

            var su = Flowsheet.FlowsheetOptions.SelectedUnitSystem;
            var nf = Flowsheet.FlowsheetOptions.NumberFormat;

            var c1 = ext.GetDefaultContainer();
            c1.Padding = new Padding(20);
            c1.Tag = "Settings";

            c1.CreateAndAddLabelRow("AI-Assisted Convergence Options");

            var options = new string[] { "Disabled", "Provide Initial Estimates", "Provide Initial Estimates (2-pass)", "Provide Estimates and Solutions", "Provide Solutions" };

            c1.CreateAndAddDropDownRow("AI-Assisted Convergence Level", options.ToList(), GlobalSettings.Settings.AIAssistedConvergenceLevel,
                (dd, e) =>
                {
                    GlobalSettings.Settings.AIAssistedConvergenceLevel = dd.SelectedIndex;
                    if (!Manager.Initialized) Manager.Initialize();
                }, 200);

            c1.CreateAndAddDescriptionRow("Provide Initial Estimates: initial estimates for converging the algorithms to the solution will be provided.", true);

            c1.CreateAndAddDescriptionRow("Provide Initial Estimates (2-pass): initial estimates for converging the algorithms to the solution will be provided for a second try only\nif the default behavior results in convergence errors.", true);

            c1.CreateAndAddDescriptionRow("Provide Estimates and Solutions: initial estimates for converging the algorithms to the solution will be provided and also solutions will\nbe given for non-converging cases.", true);

            c1.CreateAndAddDescriptionRow("Provide Solutions: Solutions will be given for non-converging cases.", true);

            var c2 = ext.GetDefaultContainer();
            c2.Padding = new Padding(20);
            c2.Tag = "Model Creation and Training";

            var sf = GlobalSettings.Settings.DpiScale;

            c2.CreateAndAddLabelRow("Training Data Generation");

            var fsname = Flowsheet.FlowsheetOptions.SimulationName;
            if (fsname == "")
            {
                if (System.IO.File.Exists(Flowsheet.FlowsheetOptions.FilePath))
                    fsname = System.IO.Path.GetFileNameWithoutExtension(Flowsheet.FlowsheetOptions.FilePath);
            }

            c2.CreateAndAddDescriptionRow("Current Flowsheet: " + fsname, true);

            var mslist = Flowsheet.GraphicObjects.Values.Where((x) => x.ObjectType == Interfaces.Enums.GraphicObjects.ObjectType.MaterialStream).Select((m) => m.Tag).ToList();
            mslist.Insert(0, "");

            var selectedStreamTag = "";

            TextBox tt1 = null, tt2 = null, tp1 = null, tp2 = null, tm1 = null, tm2 = null;

            var dd1 = c2.CreateAndAddDropDownRow("Select Feed Material Stream", mslist, 0,
                (dd, e) =>
                {
                    selectedStreamTag = dd.SelectedValue.ToString();

                    var ms = Flowsheet.GetFlowsheetSimulationObject(dd.SelectedValue.ToString());
                    if (ms != null)
                    {
                        var T = ((IMaterialStream)ms).GetTemperature();
                        var P = ((IMaterialStream)ms).GetPressure();
                        var W = ((IMaterialStream)ms).GetMassFlow();
                        tt1.Text = (T - 20).ConvertFromSI(su.temperature).ToString(nf);
                        tt2.Text = (T + 20).ConvertFromSI(su.temperature).ToString(nf);
                        tp1.Text = (P*0.9).ConvertFromSI(su.pressure).ToString(nf);
                        tp2.Text = (P*1.1).ConvertFromSI(su.pressure).ToString(nf);
                        tm1.Text = (W*0.9).ConvertFromSI(su.massflow).ToString(nf);
                        tm2.Text = (T*1.1).ConvertFromSI(su.massflow).ToString(nf);
                    }
                }, 200);

            var tbs1 = c2.CreateAndAddDoubleTextBoxRow2(nf, string.Format("Temperature Range ({0})", su.temperature),
                300.0.ConvertFromSI(su.temperature), 400.0.ConvertFromSI(su.temperature),
                (btn1, e) =>
                {
                },
                (btn2, e) =>
                {
                });

            tt1 = tbs1[0];
            tt2 = tbs1[1];

            var tbs2 = c2.CreateAndAddDoubleTextBoxRow2(nf, string.Format("Pressure Range ({0})", su.pressure),
                101325.0.ConvertFromSI(su.pressure), 1013250.0.ConvertFromSI(su.pressure),
                (btn1, e) =>
                {
                },
                (btn2, e) =>
                {
                });

            tp1 = tbs2[0];
            tp2 = tbs2[1];

            var tbs3 = c2.CreateAndAddDoubleTextBoxRow2(nf, string.Format("Mass Flow Range ({0})", su.massflow),
                1.0.ConvertFromSI(su.massflow), 10.0.ConvertFromSI(su.massflow),
                (btn1, e) =>
                {
                },
                (btn2, e) =>
                {
                });

            tm1 = tbs3[0];
            tm2 = tbs3[1];

            var intervals = 27;

            c2.CreateAndAddNumericEditorRow("Number of solver runs", intervals, 8, 270, 0,
                (nup, e) =>
                {
                    intervals = (int)nup.Value;
                });

            c2.CreateAndAddButtonRow("Generate Data for Training", null, (btn, e) =>
            {
                Manager.AutoUpdateEnabled = false;
                var currstate = Flowsheet.GetSnapshot(Interfaces.Enums.SnapshotType.ObjectData);
                var delta = Math.Round(Math.Pow(intervals, 0.3333));
                var T1 = tt1.Text.ToDoubleFromCurrent().ConvertToSI(su.temperature);
                var T2 = tt2.Text.ToDoubleFromCurrent().ConvertToSI(su.temperature);
                var P1 = tp1.Text.ToDoubleFromCurrent().ConvertToSI(su.pressure);
                var P2 = tp2.Text.ToDoubleFromCurrent().ConvertToSI(su.pressure);
                var W1 = tm1.Text.ToDoubleFromCurrent().ConvertToSI(su.massflow);
                var W2 = tm2.Text.ToDoubleFromCurrent().ConvertToSI(su.massflow);
                var DT = (T2-T1)/delta;
                var DP = (P2-P1)/delta;
                var DW = (W2-W1)/delta;
                var ms = (IMaterialStream)Flowsheet.GetFlowsheetSimulationObject(selectedStreamTag);
                if (ms != null)
                {
                    Task.Run(() =>
                    {
                        for (int i = 0; i < delta; i++)
                        {
                            for (int j = 0; j < delta; j++)
                            {
                                for (int k = 0; k < delta; k++)
                                {
                                    var T = T1 + i * DT;
                                    var P = P1 + j * DP;
                                    var W = W1 + k * DW;
                                    try
                                    {
                                        ms.SetTemperature(T);
                                        ms.SetPressure(P);
                                        ms.SetMassFlow(W);
                                        Flowsheet.RequestCalculationAndWait();
                                    }
                                    catch (Exception) { Flowsheet.RestoreSnapshot(currstate, Interfaces.Enums.SnapshotType.ObjectData); }
                                }
                            }
                        }
                        Flowsheet.RestoreSnapshot(currstate, Interfaces.Enums.SnapshotType.ObjectData);
                        Manager.AutoUpdateEnabled = true;
                    });
                }
            });

            c2.CreateAndAddLabelRow("Training and Validation Details");

            c2.CreateAndAddDescriptionRow("Current Model: ", true);

            TextArea tb = new TextArea
            {
                Width = (int)(350 * sf),
                Font = new Font(FontFamilies.Monospace, 8.0f),
                ReadOnly = true
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
            plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Blue, "Validation");
            plot.Model.Title = "Model Training Results";

            var tl = new TableLayout(new TableRow(tb, plot)) { Spacing = new Size(10, 10), Height = (int)(220 * sf) };

            c2.CreateAndAddButtonRow("Train", null, (btn, e) =>
            {
                plot.Model.Series.Clear();
                plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Red, "Training");
                plot.Model.AddLineSeries(new double[] { }, new double[] { }, OxyColors.Blue, "Validation");
                Task.Run(() => Manager.UpdateModels(tb, plot));
            });

            c2.CreateAndAddControlRow(tl);

            var c3 = ext.GetDefaultContainer();
            c3.Padding = new Padding(20);
            c3.Tag = "Model Explorer";

            var p1 = ext.GetDefaultContainer();
            var list = new ListBox { Height = (int)(440 * sf) };
            p1.CreateAndAddLabelRow("Model List");
            p1.Add(list);
            p1.CreateAndAddButtonRow("Import Model", null, null);
            p1.CreateAndAddButtonRow("Delete Selected Model", null, null);

            var p2 = ext.GetDefaultContainer();
            p2.CreateAndAddLabelRow("Selected Model Details");
            var ta = new TextArea { ReadOnly = true };
            p2.Add(ta);

            var split = new Splitter
            {
                Panel1 = p1,
                Panel2 = p2,
                Panel1MinimumSize = (int)(250 * sf),
                Position =  (int)(250 * sf),
                Height = (int)(540 * sf)
            };

            c3.Add(split);

            var form = Extensions2.GetTabbedForm("AI-Assisted Convergence Manager", 800, 600, new DynamicLayout[] { c1, c2, c3 });
            form.SetFontAndPadding();
            form.Show();
            form.Center();

        }

    }
}
