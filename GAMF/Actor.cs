using Foster.Framework;

namespace GAMF;

public abstract class Actor
{
    public Mask Mask = Mask.None;

    public abstract void Init();
    public abstract void Update();
    public abstract void Render(Batcher batcher);
    public abstract void Delete();
}

public enum Mask
{
    None,
    Ball,
    Paddle,
    Brick,
    Wall,
    Floor,
}