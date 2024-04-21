using LoseFormsLib.Elements;

namespace LoseFormsLib;

public class Form : System.Windows.Forms.Form
{
    private PictureBox current = new();
    public void Init()
    {
        Helper.Form = this;
        Size = new(Size.Width, Size.Height + 175);
        CenterToScreen();
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);

        Resize += (s, e) =>
        {
            Invalidate();
            Refresh();
        };
        Move += (s, e) =>
        {
            Invalidate();
            Refresh();
        };

        KeyDown += (s, e) =>
        {
            // Helper.CurrentTextBox.
        };

        Helper.TimeSinceLastClick = new();
        Helper.TimeSinceLastClick.Start();

        Controls.Add(current);

        var initial = new View(
                [new CText("Hello, World!", new Font(FontFamily.GenericMonospace, 10), () => new PointF(20, 20))],
                current
            );

        initial.Run();
        current = initial.Canvas;

        Controls.Add(current);

        System.Timers.Timer timer = new(10);
        timer.Elapsed += (s, e) => Helper.Form.Invoke(() => Helper.MouseLocation = PointToClient(MousePosition));

        timer.Start();

    }
}
