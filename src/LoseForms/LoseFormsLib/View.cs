using static LoseFormsLib.Helper;

namespace LoseFormsLib;

public class View
{
    public static View Current { get; set; } = new(new(), new());
    public PictureBox Canvas { get; set; }
    public List<ICanvasElement> Elements { get; set; }

    System.Timers.Timer timer = new(1);
    public bool Stopped { get; set; } = false;

    public View(List<ICanvasElement> elements, PictureBox canvas)
    {
        Canvas = canvas;
        Elements = elements;
    }

    public void Stop()
    {
        Canvas.Paint -= Paint!;
        timer.Stop();
        Stopped = true;
    }

    public void Run()
    {
        Current.Stop();
        System.Timers.Timer timer = new(16);

        timer.Elapsed += (s, e) =>
        {
            if (Stopped)
            {
                timer.Stop();
                return;
            }
            if (Helper.TimeSinceLastClick.ElapsedMilliseconds <= 200) return;
            if (Control.MouseButtons != MouseButtons.Left) return;

            Helper.TimeSinceLastClick.Restart();

            foreach (var element in Elements.Where(x => x.Selected))
            {
                element.OnClick();
            }
        };

        timer.Start();
        Canvas.Show();
        Current = this;
        Canvas.BorderStyle = BorderStyle.None;
        Canvas.BackColor = Color.White;
        Canvas.Anchor = AnchorStyles.None;
        Canvas.Dock = DockStyle.Fill;
        Canvas.BringToFront();

        timer.Elapsed += (s, e) =>
        {
            if (Stopped)
            {
                timer.Stop();
                return;
            }
            Helper.Form.Invoke(Canvas.Refresh);
        };
        timer.Start();
    }

    private void Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

        foreach (var element in Elements)
        {
            if (element.GetRectangle().GetMouseOver())
            {
                element.Selected = true;
            }
            else
            {
                element.Selected = false;
            }

            element.PreRender(e.Graphics);
        }

        foreach (var element in Elements)
        {
            element.Render(e.Graphics);
        }

        foreach (var element in Elements)
        {
            element.PostRender(e.Graphics);
        }
    }
}
