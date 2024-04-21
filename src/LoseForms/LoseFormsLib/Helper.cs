using System.Diagnostics;
using System.Drawing.Drawing2D;
using LoseFormsLib.Brushes;
using LoseFormsLib.Elements;
using LoseFormsLib.Fonts;
using static LoseFormsLib.Colourscheme.Colours;
using static LoseFormsLib.Fonts.FontSize;

namespace LoseFormsLib;

public static class Helper
{
    public static CTextBox? CurrentTextBox;
    public static LoseFormsLib.Form Form = new();

    public static Stopwatch TimeSinceLastClick = new();


    public static (BrushType, BrushType?) DefaultBrush<T>(this T element) where T : ICanvasElement
    {
        if (element is CButton)
        {
            return (
                new BrushType(Colourscheme.BrushDictionary[PALE_DOGWOOD], Colourscheme.BrushDictionary[KHAKI]),
                new BrushType(Colourscheme.BrushDictionary[RESEDA_GREEN], Colourscheme.BrushDictionary[WENGE])
                );
        }
        else if (element is CText)
        {
            return (
                new BrushType(Colourscheme.BrushDictionary[WENGE], Colourscheme.BrushDictionary[VIOLET]),
                null
            );
        }

        return (
            new BrushType(Colourscheme.BrushDictionary[KHAKI], Colourscheme.BrushDictionary[RESEDA_GREEN]),
            null
        );
    }

    public static BrushType GetPopupColours()
    {
        return new(Colourscheme.BrushDictionary[KHAKI], new SolidBrush(Color.White));
    }
    public static BrushType GetButtonColours()
    {
        return new(Colourscheme.BrushDictionary[PALE_DOGWOOD], Colourscheme.BrushDictionary[KHAKI]);
    }
    public static BrushType GetTextColours()
    {
        return new(Colourscheme.BrushDictionary[RESEDA_GREEN], Colourscheme.BrushDictionary[RESEDA_GREEN]);
    }

    public static Dictionary<FontSize, Font> Fonts = new()
    {
        { HeadingOne, new Font(FontFamily.GenericMonospace, 10) },
        { HeadingTwo, new Font(FontFamily.GenericMonospace, 20) },
        { HeadingThree, new Font(FontFamily.GenericMonospace, 30) },
        { HeadingFour, new Font(FontFamily.GenericMonospace, 40) },
        { HeadingFive, new Font(FontFamily.GenericMonospace, 50) },
    };

    public static PointF MouseLocation { get; set; }

    /// <summary>
    /// Checks if Mouse is hovering over rectangle
    /// </summary>
    /// <param name="rectangle">Rectangle to check mouse over</param>
    /// <returns>True if mouse intersects with rectangle</returns>
    public static bool GetMouseOver(this Rectangle rectangle)
    {
        if (new Rectangle(MouseLocation.Round(), new Size(5, 5)).IntersectsWith(rectangle))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Takes a ICanvasElement an returns a rectangle 
    /// </summary>
    /// <typeparam name="T">Generic Type constrained by ICanvasElement</typeparam>
    /// <param name="control">The control in question</param>
    /// <returns>A rectangle representing the element</returns>
    public static Rectangle GetRectangle<T>(this T control) where T : ICanvasElement
    {
        return new Rectangle(control.Location().Round(), control.Size());
    }
    /// <summary>
    /// Transforms a Point to be justified to the actual center of the specified size, rather than the TL
    /// </summary>
    /// <param name="func">The original function that represents the point to be transformed</param>
    /// <param name="size">The size to be transformed by</param>
    /// <returns>An updated function pointer</returns>
    public static Func<PointF> JustifyCenter(this Func<PointF> func, Size size)
    {
        return () => func().AccountForSize(size);
    }
    public static Func<PointF> JustifyCenter(this CButton control)
    {
        return control.Location.JustifyCenter(control.Str.GetSize(control.Font));
    }
    /// <summary>
    /// Takes a string and returns the screen size
    /// </summary>
    /// <param name="str">The string to be measured</param>
    /// <param name="font">The font type</param>
    /// <returns>A size object that represents the screen size of the string</returns>
    public static Size GetSize(this string str, Font font)
    {
        return TextRenderer.MeasureText(str, font);
    }
    /// <summary>
    /// Rounds a PointF to a Point 
    /// </summary>
    /// <param name="pointf">The point to round</param>
    /// <returns>A Rounded point</returns>
    public static Point Round(this PointF pointf)
    {
        return Point.Round(pointf);
    }
    /// <summary>
    /// Gets screen size of string
    /// </summary>
    /// <param name="font">Font to use on string</param>
    /// <param name="str">String to measure</param>
    /// <returns>Size object</returns>
    public static Size GetTextSize(this Font font, string str)
    {
        return TextRenderer.MeasureText(str, font);
    }
    /// <summary>
    /// Returns the default size that a box should be
    /// </summary>
    /// <typeparam name="T">Generic constrained by a PictureBox</typeparam>
    /// <param name="control">Control to be extended onto</param>
    /// <returns>Size object</returns>
    public static Size GetDefaultBoxSize<T>(this T control) where T : PictureBox
    {
        return new Size(200, 100);
    }
    /// <summary>
    /// Takes PointF and a Size and justifies it to center of size rather than TL
    /// </summary>
    /// <param name="point">The position of the size</param>
    /// <param name="size">The size to be accounted for</param>
    /// <returns>A PointF representing the center of the size</returns>
    public static PointF AccountForSize(this PointF point, Size size)
    {
        return new PointF(point.X - (int)(size.Width * 0.5), point.Y - (int)(size.Height * 0.5));
    }
    /// <summary>
    /// Takes a rectangle and justifies it to the center, rather than TL
    /// </summary>
    /// <param name="rect">The rectangle to be justified</param>
    /// <returns>Justified PointF</returns>
    public static PointF AccountForSize(this Rectangle rect)
    {
        return new PointF(rect.X - (int)(0.5 * rect.Size.Width), rect.Y - (int)(0.5 * rect.Size.Height));
    }
    /// <summary>
    /// Generic version of AccountForSize()
    /// </summary>
    /// <typeparam name="T">Generic constraint of ICanvasElement</typeparam>
    /// <param name="control">Control to account for size of</param>
    /// <returns></returns>
    public static PointF AccountForSize<T>(this T control) where T : ICanvasElement
    {
        var loc = control.Location();
        return new PointF(loc.X - (int)(0.5 * control.Size().Width), loc.Y - (int)(0.5 * control.Size().Height));
    }

    public static PointF GetCenterElement<T>(this T control) where T : ICanvasElement
    {
        var loc = control.Location();
        return new PointF(loc.X / 2, loc.Y / 2);
    }
    public static PointF GetCenterControl<T>(this T control) where T : Control
    {
        return new PointF(control.Width / 2, control.Height / 2);
    }

    public static PointF GetCenterRectangle(this Rectangle control)
    {
        return new PointF(control.Width / 2, control.Height / 2);
    }


    public static PointF GetCenter<T>(this T control, float dx, float dy) where T : Control, ICanvasElement
    {
        if (typeof(T) == typeof(ICanvasElement))
        {
            var size = control.Size();
            return new PointF((size.Width / 2) + dx, (size.Height / 2) + dy);
        }
        else return new PointF((control.Width / 2) + dx, (control.Height / 2) + dy);
    }

    public static PointF GetCenter(float dx, float dy)
    {
        return new PointF((View.Current.Canvas.Width / 2) + dx, (View.Current.Canvas.Height / 2) + dy);
    }

    public static GraphicsPath GetRoundedCorners(PointF point, Size size)
    {
        int radius = 20;
        var path = new GraphicsPath();
        path.AddArc(point.X, point.Y, radius, radius, 180, 90);
        path.AddArc(point.X + size.Width - radius, point.Y, radius, radius, 270, 90);
        path.AddArc(point.X + size.Width - radius, point.Y + size.Height - radius, radius, radius, 0, 90);
        path.AddArc(point.X, point.Y + size.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();
        return path;
    }




}
