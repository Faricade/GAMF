using Foster.Framework;
using System.Numerics;

namespace GAMF;

public class Border : Actor
{
    public Rect Rectangle;
    public Vector2 Position;

    public override void Init()
    {
        Mask = Mask.Wall;
    }

    public override void Update()
    {

    }

    public override void Render(Batcher batcher)
    {

    }

    public override void Delete()
    {

    }
}
