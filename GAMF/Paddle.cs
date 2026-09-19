using Foster.Framework;
using GAMF.E;

namespace GAMF;

public class Paddle : Actor
{
    public float Speed = 280;
    public Rect Rectangle = new(100, 20);

    public override void Init()
    {
        Rectangle.Position = new((1280 - Rectangle.Width) / 2, 650);
    }

    public override void Update(in Time time)
    {
        if (Owner.Input.Keyboard.Down(Keys.Left))
            Rectangle.X -= Speed * time.Delta;
        if (Owner.Input.Keyboard.Down(Keys.Right))
            Rectangle.X += Speed * time.Delta;

        // hits left
        if (Rectangle.X <= 0)
            Rectangle.X = 0;
        // hits right
        else if (Rectangle.Position.X >= 1280 - Rectangle.Width)
            Rectangle.X = 1280 - Rectangle.Width;
    }

    public override void Render(Batcher batcher)
    {
        batcher.RectRounded(Rectangle, 5f, Color.White);
    }

    public override void Dispose()
    {

    }
}
