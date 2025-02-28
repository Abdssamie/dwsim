using Cairo;
using Gtk;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.UI.Desktop.GTK3.SkiaSharp
{
    public class SKDrawingArea : DrawingArea
    {
        private ImageSurface pix;

        private SKSurface surface;

        //
        // Summary:
        //     Gets the current canvas size.
        //
        // Remarks:
        //     The canvas size may be different to the view size as a result of the current
        //     device's pixel density.
        public SKSize CanvasSize
        {
            get
            {
                if (pix != null)
                {
                    return new SKSize(pix.Width, pix.Height);
                }

                return SKSize.Empty;
            }
        }

        //
        // Summary:
        //     Occurs when the the canvas needs to be redrawn.
        //
        // Remarks:
        //     ## Remarks There are two ways to draw on this surface: by overriding the <xref:SkiaSharp.Views.Gtk.SKDrawingArea.OnPaintSurface(SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs)>
        //     method, or by attaching a handler to the <xref:SkiaSharp.Views.Gtk.SKDrawingArea.PaintSurface>
        //     event. ## Examples ```csharp myView.PaintSurface += (sender, e) => { var surface
        //     = e.Surface; var surfaceWidth = e.Info.Width; var surfaceHeight = e.Info.Height;
        //     var canvas = surface.Canvas; // draw on the canvas canvas.Flush (); }; ```
        [Category("Appearance")]
        public event EventHandler<SKPaintSurfaceEventArgs> PaintSurface;

        //
        // Summary:
        //     Default handler for the Gtk.Widget.Drawn event.
        //
        // Parameters:
        //   cr:
        //     The Cairo.Context to be used to paint the widget.
        //
        // Returns:
        //     Return true to stop other handlers from being invoked for the event, or false
        //     to continue the event propagation.
        //
        // Remarks:
        //     Override this method in a subclass to provide a default handler for the Gtk.Widget.Drawn
        //     event. The Cairo.Context will be disposed after this method returns, so you should
        //     not keep a reference to it outside of the scope of this method.
        protected override bool OnDrawn(Context cr)
        {
            SKImageInfo info = CreateDrawingObjects();
            if (info.Width == 0 || info.Height == 0)
            {
                return true;
            }

            using (new SKAutoCanvasRestore(surface.Canvas, doSave: true))
            {
                OnPaintSurface(new SKPaintSurfaceEventArgs(surface, info));
            }

            surface.Canvas.Flush();
            pix.MarkDirty();
            if (info.ColorType == SKColorType.Rgba8888)
            {
                using (SKPixmap sKPixmap = surface.PeekPixels())
                {
                    SKSwizzle.SwapRedBlue(sKPixmap.GetPixels(), info.Width * info.Height);
                }
            }

            float dpi;
            if (GlobalSettings.Settings.RunningPlatform() == GlobalSettings.Settings.Platform.Windows)
                dpi = 1.0f/Display.PrimaryMonitor.ScaleFactor;
            else
                dpi = 1.0f/((float)Gdk.Screen.Default.Resolution / 96f);
            cr.Scale(dpi, dpi);
            cr.SetSourceSurface(pix, 0, 0);
            cr.Paint();
            return true;
        }

        //
        // Summary:
        //     Implement this to draw on the canvas.
        //
        // Parameters:
        //   e:
        //     The event arguments that contain the drawing surface and information.
        //
        // Remarks:
        //     ## Remarks There are two ways to draw on this surface: by overriding the <xref:SkiaSharp.Views.Gtk.SKDrawingArea.OnPaintSurface(SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs)>
        //     method, or by attaching a handler to the <xref:SkiaSharp.Views.Gtk.SKDrawingArea.PaintSurface>
        //     event. > [!IMPORTANT] > If this method is overridden, then the base must be called,
        //     otherwise the > event will not be fired. ## Examples ```csharp protected override
        //     void OnPaintSurface (SKPaintSurfaceEventArgs e) { // call the base method base.OnPaintSurface
        //     (e); var surface = e.Surface; var surfaceWidth = e.Info.Width; var surfaceHeight
        //     = e.Info.Height; var canvas = surface.Canvas; // draw on the canvas canvas.Flush
        //     (); } ```
        protected virtual void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            this.PaintSurface?.Invoke(this, e);
        }

        //
        // Summary:
        //     Releases the unmanaged resources used by the SkiaSharp.Views.Gtk.SKDrawingArea
        //     and optionally releases the managed resources.
        //
        // Parameters:
        //   disposing:
        //     true to release both managed and unmanaged resources; false to release only unmanaged
        //     resources.
        //
        // Remarks:
        //     Always dispose the object before you release your last reference to the SkiaSharp.Views.Gtk.SKDrawingArea.
        //     Otherwise, the resources it is using will not be freed until the garbage collector
        //     calls the finalizer.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FreeDrawingObjects();
            }
        }

        private SKImageInfo CreateDrawingObjects()
        {
            float dpi;
            if (GlobalSettings.Settings.RunningPlatform() == GlobalSettings.Settings.Platform.Windows)
                dpi = Display.PrimaryMonitor.ScaleFactor;
            else
                dpi = (float)Gdk.Screen.Default.Resolution / 96f;
            Gdk.Rectangle allocation = base.Allocation;
            int width = (int)(allocation.Width * dpi);
            int height = (int)(allocation.Height * dpi);
            SKImageInfo sKImageInfo = new SKImageInfo(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            if (pix == null || pix.Width != sKImageInfo.Width || pix.Height != sKImageInfo.Height)
            {
                FreeDrawingObjects();
                if (sKImageInfo.Width != 0 && sKImageInfo.Height != 0)
                {
                    pix = new ImageSurface(Format.Argb32, sKImageInfo.Width, sKImageInfo.Height);
                    surface = SKSurface.Create(sKImageInfo, pix.DataPtr, sKImageInfo.RowBytes);
                }
            }

            return sKImageInfo;
        }

        private void FreeDrawingObjects()
        {
            pix?.Dispose();
            pix = null;
            surface?.Dispose();
            surface = null;
        }
    }
}
