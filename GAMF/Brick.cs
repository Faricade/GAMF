using Foster.Framework;

namespace GAMF;

internal class Brick : Actor
{
    public override void Init() { }
    public override void Update() { }

    public override void Render(Batcher batcher) 
    {
        batcher.RectRounded(Hitbox, 5f, Color.White);
    }

    public override void Delete() { }
}
