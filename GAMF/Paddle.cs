using Foster.Framework;
using GAMF.Entities;

namespace GAMF;

public class Paddle : Entity
{
    public float Speed = 280;

    public override void Init()
    {
        Hitbox = new((1280 - Hitbox.Width) / 2, 650, 100, 20);
    }

    public override void Update(in Time time)
    {
        if (Owner.Input.Keyboard.Down(Keys.Left))
            Hitbox.X -= Speed * time.Delta;
        if (Owner.Input.Keyboard.Down(Keys.Right))
            Hitbox.X += Speed * time.Delta;

        // hits left
        if (Hitbox.X <= 0)
            Hitbox.X = 0;
        // hits right
        else if (Hitbox.Position.X >= 1280 - Hitbox.Width)
            Hitbox.X = 1280 - Hitbox.Width;
    }

    public override void Render(Batcher batcher)
    {
        batcher.RectRounded(Hitbox, 5f, Color.White);
    }

    public override void Dispose()
    {

    }
}
