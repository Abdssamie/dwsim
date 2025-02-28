using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cairo;
using DWSIM.Drawing.SkiaSharp;
using DWSIM.UI.Controls;
using Eto.Drawing;
using Eto.Forms;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace DWSIM.UI.Desktop.GTK3
{
    public class FlowsheetSurfaceControlHandler : Eto.GtkSharp.Forms.GtkControl<Gtk.Widget, FlowsheetSurfaceControl, FlowsheetSurfaceControl.ICallback>, FlowsheetSurfaceControl.IFlowsheetSurface
    {
        private FlowsheetSurface_GTK nativecontrol;

        public FlowsheetSurfaceControlHandler()
        {
            nativecontrol = new FlowsheetSurface_GTK();
            this.Control = nativecontrol;

        }

        public override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            nativecontrol.fbase = this.Widget.FlowsheetObject;
            nativecontrol.fsurface = this.Widget.FlowsheetSurface;
            //nativecontrol.DragDataGet += (sender, e2) =>
            //{
            //    Console.WriteLine(e2.SelectionData.Target.Name);
            //};
            //nativecontrol.DragEnd += (sender, e2) =>
            //{
            //    foreach (var item in e2.Args)
            //    {
            //        Console.WriteLine(item.ToString());
            //    }
            //};
            //nativecontrol.DragFailed += (sender, e2) =>
            //{
            //    foreach (var item in e2.Args)
            //    {
            //        Console.WriteLine(item.ToString());
            //    }
            //};
        }

        public override Eto.Drawing.Color BackgroundColor
        {
            get
            {
                return Eto.Drawing.Colors.White;
            }
            set
            {
                return;
            }
        }

        public GraphicsSurface FlowsheetSurface
        {
            get
            {
                return ((FlowsheetSurface_GTK)this.Control).fsurface;
            }
            set
            {
                ((FlowsheetSurface_GTK)this.Control).fsurface = value;
            }
        }

        public DWSIM.UI.Desktop.Shared.Flowsheet FlowsheetObject
        {
            get
            {
                return ((FlowsheetSurface_GTK)this.Control).fbase;
            }
            set
            {
                ((FlowsheetSurface_GTK)this.Control).fbase = value;
            }
        }

    }

    public class FlowsheetSurface_GTK : SkiaSharp.SKDrawingArea
    {

        public GraphicsSurface fsurface;
        public DWSIM.UI.Desktop.Shared.Flowsheet fbase;

        private float _lastTouchX;
        private float _lastTouchY;

        private float dpi = 1.0f;

        public FlowsheetSurface_GTK()
        {

            if (GlobalSettings.Settings.RunningPlatform() == GlobalSettings.Settings.Platform.Windows)
            {
                dpi = Screen.Display.PrimaryMonitor.ScaleFactor;
                GlobalSettings.Settings.DpiScale = dpi;
            }

            this.AddEvents((int)Gdk.EventMask.PointerMotionMask);
            this.AddEvents((int)Gdk.EventMask.ScrollMask);
            this.ButtonPressEvent += FlowsheetSurface_GTK_ButtonPressEvent;
            this.ButtonReleaseEvent += FlowsheetSurface_GTK_ButtonReleaseEvent;
            this.MotionNotifyEvent += FlowsheetSurface_GTK_MotionNotifyEvent;
            this.ScrollEvent += FlowsheetSurface_GTK_ScrollEvent;

            if (GlobalSettings.Settings.RunningPlatform() == GlobalSettings.Settings.Platform.Linux)
            {
                var targets = new List<Gtk.TargetEntry>();
                targets.Add(new Gtk.TargetEntry("ObjectName", 0, 1));
                Gtk.Drag.DestSet(this, Gtk.DestDefaults.Highlight | Gtk.DestDefaults.Motion, targets.ToArray(), Gdk.DragAction.Copy | Gdk.DragAction.Link | Gdk.DragAction.Move);
            }

        }


        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            fsurface?.UpdateCanvas(e.Surface.Canvas);
            if (fbase != null && fbase.SetGTKDragDest == null && GlobalSettings.Settings.RunningPlatform() != GlobalSettings.Settings.Platform.Linux)
            {
                fbase.SetGTKDragDest = () =>
                {
                    var targets = new List<Gtk.TargetEntry>();
                    targets.Add(new Gtk.TargetEntry("ObjectName", 0, 1));
                    Gtk.Drag.DestSet(this, Gtk.DestDefaults.Highlight | Gtk.DestDefaults.Motion, targets.ToArray(), Gdk.DragAction.Copy | Gdk.DragAction.Link | Gdk.DragAction.Move);
                };
            }
        }

        void FlowsheetSurface_GTK_ScrollEvent(object o, Gtk.ScrollEventArgs args)
        {
            fbase?.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectLayout);

            var oldzoom = fsurface.Zoom;

            if (args.Event.Direction == Gdk.ScrollDirection.Down)
            {
                fsurface.Zoom += -5 * dpi / 100f;
            }
            else
            {
                fsurface.Zoom += 5 * dpi / 100f;
            }
            if (fsurface.Zoom < 0.05) fsurface.Zoom = 0.05f;

            int x = (int)args.Event.X;
            int y = (int)args.Event.Y;

            fbase?.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectLayout);

            fsurface.CenterTo(oldzoom, x, y, this.WidthRequest, this.HeightRequest);

            this.QueueDraw();
        }

        void FlowsheetSurface_GTK_MotionNotifyEvent(object o, Gtk.MotionNotifyEventArgs args)
        {
            float x = (int)args.Event.X;
            float y = (int)args.Event.Y;
            _lastTouchX = x;
            _lastTouchY = y;
            fsurface.InputMove((int)((int)_lastTouchX * dpi), (int)((int)_lastTouchY * dpi));
            this.QueueDraw();
        }

        void FlowsheetSurface_GTK_ButtonReleaseEvent(object o, Gtk.ButtonReleaseEventArgs args)
        {
            fsurface.InputRelease();
            this.QueueDraw();
        }

        void FlowsheetSurface_GTK_ButtonPressEvent(object o, Gtk.ButtonPressEventArgs args)
        {
            fbase?.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectLayout);
            if (args.Event.Type == Gdk.EventType.TwoButtonPress)
            {
                if (args.Event.State == Gdk.ModifierType.ShiftMask)
                {
                    fsurface.Zoom = 1.0f;
                }
                else
                {
                    fsurface.ZoomAll((int)(Allocation.Width * dpi), (int)(Allocation.Height * dpi));
                }
            }
            else
            {
                _lastTouchX = (int)args.Event.X;
                _lastTouchY = (int)args.Event.Y;
                fsurface.InputPress((int)((int)_lastTouchX * dpi), (int)((int)_lastTouchY * dpi));
            }
            this.QueueDraw();

        }

    }

}
