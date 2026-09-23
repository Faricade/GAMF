using Foster.Framework;

namespace GAMF.S;

internal abstract class Scene
{
    public readonly E.E EntityManager;
    public required Input Input;

    protected Scene()
    {
        EntityManager = new(this);
    }

    public abstract void Init();
    public abstract void Update(in Time time);
    public abstract void Render(Batcher batcher);
    public abstract void Dispose();
}
