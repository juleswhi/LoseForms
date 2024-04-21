using LoseFormsLib.Brushes;

namespace LoseFormsLib.Elements;

public class CText : ICanvasElement, ICanvasText
{
    public bool Selected { get; set; }
    public bool Enabled { get; set; }
    public BrushType Brush { get; set; }
    public Action OnClick { get; set; }
    public Func<PointF> Location { get; set; }
    public Func<Size> Size { get; set; }
    public Font Font { get; set; }
    public string Str { get; set; }
    public CText(string str, Font font, Func<PointF> loc)
    {
        Font = font;
        Brush = new BrushType(new SolidBrush(Color.Green), new SolidBrush(Color.Blue));
        Str = str;
        Location = loc;
        Size = () => Font.GetTextSize(Str);
        Enabled = true;
        OnClick += () => { if (!Selected) return; };
    }

    public void PostRender(Graphics g)
    {
    }

    public void PreRender(Graphics g)
    {
    }

    public void Render(Graphics g)
    {
        g.DrawString(Str, Font, Brush.Selected, this.AccountForSize());
    }
}
