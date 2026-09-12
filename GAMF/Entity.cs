using Foster.Framework;

namespace GAMF;

public abstract class Entity
{
    public Rect Hitbox; // it's silly but everything is a rectangle now.

    public abstract void Init();
    public abstract void Update();
    public abstract void Render(Batcher batcher);
    public abstract void Delete();
}
