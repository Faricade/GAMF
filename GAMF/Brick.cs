using Foster.Framework;
using GAMF.Entities;

namespace GAMF;

internal class Brick : Entity
{
    public Color Color { get; init; } = Color.White;

    public override void Init() { }
    public override void Update(in Time time) { }

    public override void Render(Batcher batcher)
    {
        batcher.RectRounded(Hitbox, 5f, Color);
    }

    public override void Dispose() { }
}
