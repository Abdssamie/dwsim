using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cairo;
using DWSIM.Drawing.SkiaSharp;
using DWSIM.UI.Controls;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace DWSIM.UI.Desktop.GTK3
{
    public class FlowsheetSurfaceControlHandler : Eto.GtkSharp.Forms.GtkControl<Gtk.EventBox, FlowsheetSurfaceControl, FlowsheetSurfaceControl.ICallback>, FlowsheetSurfaceControl.IFlowsheetSurface
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
            //nativecontrol.DragDataGet += (sender, e2) => {
            //    Console.WriteLine(e2.SelectionData.Type.Name);
            //};
            //nativecontrol.DragEnd += (sender, e2) => {
            //    foreach (var item in e2.Args)
            //    {
            //        Console.WriteLine(item.ToString());
            //    }
            //};
            //nativecontrol.DragFailed += (sender, e2) => {
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

    public class FlowsheetSurface_GTK : Gtk.EventBox
    {

        public GraphicsSurface fsurface;
        public DWSIM.UI.Desktop.Shared.Flowsheet fbase;

        private float _lastTouchX;
        private float _lastTouchY;

        public FlowsheetSurface_GTK()
        {
            this.AddEvents((int)Gdk.EventMask.PointerMotionMask);
            this.ButtonPressEvent += FlowsheetSurface_GTK_ButtonPressEvent;
            this.ButtonReleaseEvent += FlowsheetSurface_GTK_ButtonReleaseEvent;
            this.MotionNotifyEvent += FlowsheetSurface_GTK_MotionNotifyEvent;
            this.ScrollEvent += FlowsheetSurface_GTK_ScrollEvent;

            var targets = new List<Gtk.TargetEntry>();
            targets.Add(new Gtk.TargetEntry("ObjectName", 0, 1));
            Gtk.Drag.DestSet(this, Gtk.DestDefaults.All, targets.ToArray(), Gdk.DragAction.Copy | Gdk.DragAction.Link | Gdk.DragAction.Move);

        }

        void FlowsheetSurface_GTK_ScrollEvent(object o, Gtk.ScrollEventArgs args)
        {
            fbase?.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectLayout);

            var oldzoom = fsurface.Zoom;

            if (args.Event.Direction == Gdk.ScrollDirection.Down)
            {
                fsurface.Zoom += -5 / 100f;
            }
            else
            {
                fsurface.Zoom += 5 / 100f;
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
            fsurface.InputMove((int)_lastTouchX, (int)_lastTouchY);
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
                //if (args.Event.State == Gdk.ModifierType.ShiftMask)
                //{
                //    fsurface.Zoom = 1.0f;
                //}
                //else {
                //    fsurface.ZoomAll((int)this.Allocation.Width, (int)this.Allocation.Height);
                //}
            }
            else
            {
                _lastTouchX = (int)args.Event.X;
                _lastTouchY = (int)args.Event.Y;
                fsurface.InputPress((int)_lastTouchX, (int)_lastTouchY);
            }
            this.QueueDraw();

        }

        private ImageSurface pix;
        private SKSurface surface;

        protected override bool OnDrawn(Context cr)
        {
            // get the pixbuf
            var imgInfo = CreateDrawingObjects();

            if (imgInfo.Width == 0 || imgInfo.Height == 0)
                return true;

            // start drawing
            using (new SKAutoCanvasRestore(surface.Canvas, true))
            {
                OnPaintSurface(new SKPaintSurfaceEventArgs(surface, imgInfo));
            }

            if (fsurface != null) fsurface.UpdateSurface(surface);
            surface.Canvas.Flush();

            pix.MarkDirty();

            // swap R and B
            if (imgInfo.ColorType == SKColorType.Rgba8888)
            {
                using (var pixmap = surface.PeekPixels())
                {
                    SKSwizzle.SwapRedBlue(pixmap.GetPixels(), imgInfo.Width * imgInfo.Height);
                }
            }

            // write the pixbuf to the graphics
            cr.SetSourceSurface(pix, 0, 0);
            cr.Paint();

            return true;
        }

        private SKImageInfo CreateDrawingObjects()
        {
            var alloc = Allocation;
            var w = alloc.Width;
            var h = alloc.Height;
            var imgInfo = new SKImageInfo(w, h, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

            if (pix == null || pix.Width != imgInfo.Width || pix.Height != imgInfo.Height)
            {
                FreeDrawingObjects();

                if (imgInfo.Width != 0 && imgInfo.Height != 0)
                {
                    pix = new ImageSurface(Format.Argb32, imgInfo.Width, imgInfo.Height);

                    // (re)create the SkiaSharp drawing objects
                    surface = SKSurface.Create(imgInfo, pix.DataPtr, imgInfo.RowBytes);
                }
            }

            return imgInfo;
        }

        private void FreeDrawingObjects()
        {
            pix?.Dispose();
            pix = null;

            // SkiaSharp objects should only exist if the Pixbuf is set as well
            surface?.Dispose();
            surface = null;
        }
        
        [Category("Appearance")]
        public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface;

        protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            // invoke the event
            PaintSurface?.Invoke(this, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FreeDrawingObjects();
            }
        }

    }

}
