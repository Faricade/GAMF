using Foster.Framework;

namespace GAMF;

public abstract class Actor
{
    public abstract void Init();
    public abstract void Update();
    public abstract void Render(Batcher batcher);
    public abstract void Delete();
}
