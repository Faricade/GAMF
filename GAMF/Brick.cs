using Foster.Framework;
using GAMF.E;

namespace GAMF;

internal class Brick : Actor
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
