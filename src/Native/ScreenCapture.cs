using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace WinLens.Native;

/// <summary>
/// Captures the entire virtual screen (all monitors) as a single Bitmap.
/// Pixel-accurate regardless of DPI when the process is per-monitor v2 aware.
/// </summary>
[SupportedOSPlatform("windows")]
public static class ScreenCapture
{
    public readonly record struct CaptureResult(Bitmap Bitmap, Rectangle Bounds);

    private const int SM_XVIRTUALSCREEN  = 76;
    private const int SM_YVIRTUALSCREEN  = 77;
    private const int SM_CXVIRTUALSCREEN = 78;
    private const int SM_CYVIRTUALSCREEN = 79;

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    public static CaptureResult CaptureVirtualScreen()
    {
        int x = GetSystemMetrics(SM_XVIRTUALSCREEN);
        int y = GetSystemMetrics(SM_YVIRTUALSCREEN);
        int w = GetSystemMetrics(SM_CXVIRTUALSCREEN);
        int h = GetSystemMetrics(SM_CYVIRTUALSCREEN);
        var bounds = new Rectangle(x, y, w, h);

        var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(x, y, 0, 0, new Size(w, h), CopyPixelOperation.SourceCopy);
        }
        return new CaptureResult(bmp, bounds);
    }
}
