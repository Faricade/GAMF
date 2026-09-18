using Foster.Framework;

namespace GAMF;

internal class Brick : Actor
{
    public Color Color { get; init; } = Color.White;

    public override void Init() { }
    public override void Update() { }

    public override void Render(Batcher batcher)
    {
        batcher.RectRounded(Hitbox, 5f, Color);
    }

    public override void Delete() { }
}
