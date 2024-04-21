using LoseFormsLib.Brushes;

namespace LoseFormsLib.Elements;

public class CButton : CBox, ICanvasText
{
    public Font Font { get; set; }
    public string Str { get; set; }
    public Func<PointF> StrLocation { get; set; }
    public override Action OnClick { get; set; }
    public BrushType TextBrush { get; set; }
  
    public CButton(string str, Font font, Func<Size> size, Func<PointF> loc, Action onClick) : base(size, loc)
    {
        Str = str;
        Font = font;
        TextBrush = Helper.GetTextColours();
        Size textSize = Str.GetSize(Font);
        StrLocation = loc.JustifyCenter(Size());
        OnClick += () => { if (!Selected) return; };
        OnClick += () => { if (!Enabled) return; };
        OnClick += onClick;
    }


    public override void Render(Graphics g)
    {
        g.DrawString(Str, Font, Selected ? TextBrush.Selected : TextBrush.Default, new PointF(StrLocation().X + (int)(0.5 * Size().Width) - (int)(0.5 * Str.GetSize(Font).Width), StrLocation().Y + (int)(0.5 * Size().Height) - (int)(0.5 * Str.GetSize(Font).Height)));
    }
}
