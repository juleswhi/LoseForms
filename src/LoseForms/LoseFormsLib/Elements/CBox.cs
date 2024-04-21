using LoseFormsLib.Brushes;

namespace LoseFormsLib.Elements;

public class CBox : ICanvasElement
{
    public bool Selected { get; set; }
    public bool Enabled { get; set; }
    public BrushType Brush { get; set; }
    public virtual Action OnClick { get; set; }
    public Func<PointF> Location { get; set; }
    public Func<Size> Size { get; set; }
    public CBox(Func<Size> size, Func<PointF> loc)
    {
        Brush = Helper.GetButtonColours();
        Size = size;
        Location = loc.JustifyCenter(Size());
        Enabled = true;
        OnClick += () => { if (!Selected) return; };
    }
    public virtual void PreRender(Graphics g)
    {
        g.FillPath(
            Selected ? Brush.Selected : Brush.Default,
            Helper.GetRoundedCorners(Location(), Size()));
    }

    public virtual void Render(Graphics g)
    {}

    public virtual void PostRender(Graphics g)
    {}
}
