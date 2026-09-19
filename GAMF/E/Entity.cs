using Foster.Framework;
using GAMF.S;

namespace GAMF.E;

public abstract class Entity
{
    internal Scene Owner { get; set; } = null!;

    public Rect Hitbox; // it's silly but everything is a rectangle now.

    public abstract void Init();
    public abstract void Update(in Time time);
    public abstract void Render(Batcher batcher);
    public abstract void Dispose();
}
