using Foster.Framework;
using System.Numerics;

namespace GAMF;

public class Paddle : Actor
{
    public const int Width = 100;
    public const int Height = 20;
    public float Speed = 200;
    public Vector2 Position = new((1280 + Width) / 2, 650);

    public override void Init()
    {

    }

    public override void Update()
    {
        if (Game.Instance.Input.Keyboard.Down(Keys.Left))
            Position.X -= Speed * Game.Instance.Time.Delta;
        if (Game.Instance.Input.Keyboard.Down(Keys.Right))
            Position.X += Speed * Game.Instance.Time.Delta;

        // hits left
        if (Position.X <= 0)
            Position.X = 0;
        // hits right
        else if (Position.X >= 1280 - Width)
            Position.X = 1280 - Width;
    }

    public override void Render(Batcher batcher)
    {
        batcher.RectRounded(new Rect(in Position, Width, Height), 5f, Color.White);
    }

    public override void Delete()
    {

    }
}
