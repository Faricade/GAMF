using Foster.Framework;
using System.Numerics;

namespace GAMF;

internal sealed class Game : App
{
    public static readonly Game Instance = new();

    private readonly Batcher batch;
    private readonly List<Actor> Actors = [];

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
        Actors.Add(new Paddle());
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
