using UnityEngine;
using System.Collections;

public static class DisplayMetricsUtil
{
    public enum ResolutionType
    {
        ldpi,
        mdpi,
        hdpi,
        xhdpi
    }

    private const float DEFAULT_DPI = 160.0f;

    private static bool isScreenSizeInitialized = false;

    private static Rect ScreenSize;

    public static Vector2 DpToPixel(this Vector2 vector)
    {
        return new Vector2(vector.x.DpToPixel(), vector.y.DpToPixel());
    }

    public static Vector3 DpToPixel(this Vector3 vector)
    {
        return new Vector3(vector.x.DpToPixel(), vector.y.DpToPixel(), vector.z.DpToPixel());
    }

    public static Rect DpToPixel(this Rect rect)
    {
        return new Rect(rect.x.DpToPixel(), rect.y.DpToPixel(), rect.width.DpToPixel(), rect.height.DpToPixel());
    }

    public static int DpToPixel(this int dp)
    {
        // Convert the dps to pixels
        return (int)(dp * GetScale() + 0.5f);
    }

    public static int DpToPixel(this float dp)
    {
        // Convert the dps to pixels
        return (int)(dp * GetScale() + 0.5f);
    }

    public static int PixelToDp(this int px)
    {
        // Convert the pxs to dps
        return (int)(px / GetScale() - 0.5f);
    }

    public static int PixelToDp(this float px)
    {
        // Convert the pxs to dps
        return (int)(px / GetScale() - 0.5f);
    }


    public static GUIStyle DpToPixel(this GUIStyle style)
    {
        GUIStyle stylePx = new GUIStyle(style);
        stylePx.border = stylePx.border.DpToPixel();
        stylePx.padding = stylePx.padding.DpToPixel();
        stylePx.margin = stylePx.margin.DpToPixel();
        stylePx.overflow = stylePx.overflow.DpToPixel();
        stylePx.contentOffset = stylePx.contentOffset.DpToPixel();
        stylePx.fixedWidth = stylePx.fixedWidth.DpToPixel();
        stylePx.fixedHeight = stylePx.fixedHeight.DpToPixel();
        stylePx.fontSize = stylePx.fontSize.DpToPixel();

        return stylePx;
    }


    public static RectOffset DpToPixel(this RectOffset rectOffset)
    {
        return new RectOffset(
            rectOffset.left.DpToPixel(),
            rectOffset.right.DpToPixel(),
            rectOffset.top.DpToPixel(),
            rectOffset.bottom.DpToPixel());
    }


    public static Rect ScreenSizeDpUnit()
    {
        if (!isScreenSizeInitialized)
        {
            ScreenSize = new Rect(0, 0, Screen.width.PixelToDp(), Screen.height.PixelToDp());

            isScreenSizeInitialized = true;
        }

        return ScreenSize;
    }

    //e.g. switch fonts to have the correct fontsize. 
    public static ResolutionType GetResolutionType()
    {
        float scale = GetDPI() / DEFAULT_DPI;

        ResolutionType res;

        //http://developer.android.com/guide/practices/screens_support.html
        if (scale > 1.5f)
        {
            res = DisplayMetricsUtil.ResolutionType.xhdpi;
        }
        else if (scale > 1f)
        {
            res = DisplayMetricsUtil.ResolutionType.hdpi;
        }
        else if (scale > 0.75f)
        {
            res = DisplayMetricsUtil.ResolutionType.mdpi;
        }
        else
        {
            res = DisplayMetricsUtil.ResolutionType.ldpi;
        }

        return res;
    }


    private static float GetDPI()
    {
        return Screen.dpi == 0 ? DEFAULT_DPI : Screen.dpi;
    }

    public static float GetScale()
    {
        return GetDPI() / DEFAULT_DPI;
    }
}