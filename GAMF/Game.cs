using Foster.Framework;
using System.Numerics;

namespace GAMF;

internal sealed class Game : App
{
    public static readonly Game Instance = new();

    private readonly Batcher batch;
    private readonly List<Actor> _actors = [];
    public IReadOnlyList<Actor> Actors => _actors;

    public Game() : base(new()
    {
        ApplicationName = "GAMF",
        WindowTitle = "GAMF",
        Width = 1280,
        Height = 720,
    })
    {
        batch = new Batcher(GraphicsDevice);
    }

    protected override void Startup()
    {
        _actors.Add(new Paddle());
        _actors.Add(new Ball());
        _actors.Add(new Border() // up
        {
            Rectangle = new(0, -10, 1280, 10),
            Mask = Mask.Wall,
        });
        _actors.Add(new Border() // left
        {
            Rectangle = new(-10, 0, 10, 720),
            Mask = Mask.Wall,
        });
        _actors.Add(new Border() // right
        {
            Rectangle = new(1290, 0, 10, 720),
            Mask = Mask.Wall,
        });
        foreach (var actor in Actors)
            actor.Init();
    }

    protected override void Update()
    {
        foreach (var actor in Actors)
            actor.Update();
    }

    protected override void Render()
    {
        Window.Clear(Color.Black);

        foreach (var actor in Actors)
            actor.Render(batch);

        batch.Render(Window);
        batch.Clear();
    }

    protected override void Shutdown()
    {
        foreach (var actor in Actors)
            actor.Delete();
    }
}
