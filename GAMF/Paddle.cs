using Foster.Framework;

namespace GAMF;

public class Paddle : Actor
{
    public float Speed = 280;
    public Rect Rectangle = new(100, 20);
    
    public override void Init()
    {
        Rectangle.Position = new((1280 - Rectangle.Width) / 2, 650);
    }

    public override void Update()
    {
        if (Game.Instance.Input.Keyboard.Down(Keys.Left))
            Rectangle.X -= Speed * Game.Instance.Time.Delta;
        if (Game.Instance.Input.Keyboard.Down(Keys.Right))
            Rectangle.X += Speed * Game.Instance.Time.Delta;

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

    public override void Delete()
    {

    }
}
