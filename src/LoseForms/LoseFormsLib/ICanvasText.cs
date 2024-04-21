namespace LoseFormsLib;

public interface ICanvasText
{
    public Font Font { get; set; }
    public string Str { get; set; }
    public Func<PointF> Location { get; set; }
}
