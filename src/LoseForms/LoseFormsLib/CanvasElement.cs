using LoseFormsLib.Brushes;

namespace LoseFormsLib;

public interface ICanvasElement
{
    /// <summary>
    /// Is true if mouse is over the element
    /// </summary>
    public bool Selected { get; set; }
    /// <summary>
    /// If the element is functional or not
    /// </summary>
    public bool Enabled { get; set; }
    /// <summary>
    /// A brush to display selected and non selected
    /// </summary>
    public BrushType Brush { get; set; }
    /// <summary>
    /// Custom behavoir for click
    /// </summary>
    // TODO: Prolly change to event? Also add data about click ( position etc )
    public Action OnClick { get; set; }
    /// <summary>
    /// Dynamically updates location
    /// Evaluated every render cycle
    /// </summary>
    public Func<PointF> Location { get; set; }
    /// <summary>
    /// Max Size of element
    /// </summary>
    public Func<Size> Size { get; set; }
    /// <summary>
    /// First Render Cycle. Used for based elements such as Boxes.
    /// </summary>
    /// <param name="g">Graphics object of <c>PictureBox</c></param>
    public void PreRender(Graphics g);
    /// <summary>
    /// Second Render Cycle. Used for secondary elements like text.
    /// </summary>
    /// <param name="g">Graphics object of <c>PictureBox</c></param>
    public void Render(Graphics g);
    /// <summary>
    /// Last Render Cycle. Used for post processing.
    /// </summary>
    /// <param name="g">Graphics object of <c>PictureBox</c></param>
    public void PostRender(Graphics g);
}
