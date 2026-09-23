using Foster.Framework;
using GAMF.Entities;

namespace GAMF.Scenes;

internal abstract class Scene
{
    public readonly EntityManager EntityManager;
    public Input Input = null!;

    protected Scene()
    {
        EntityManager = new(this);
    }

    public abstract void Init();
    public abstract void Update(in Time time);
    public abstract void Render(Batcher batcher);
    public abstract void Dispose();
}
