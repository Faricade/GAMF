using Foster.Framework;
using System.Numerics;

namespace GAMF;

internal sealed class Game : App
{
    public static readonly Game Instance = new();

    private readonly Batcher batcher;
    private readonly List<Entity> _entites = [];
    private readonly List<(bool add, Entity entity)> Pending = [];
    public IReadOnlyList<Entity> Entites => _entites;
    public GameState State { get; set; } = GameState.Start;
    public SpriteFont font = null!;

    public Game() : base(new()
    {
        ApplicationName = "GAMF",
        WindowTitle = "GAMF",
        Width = 1280,
        Height = 720,
    })
    {
        batcher = new Batcher(GraphicsDevice);
    }

    protected override void Startup()
    {
        font = new SpriteFont(GraphicsDevice, Path.Join("Assets", "monogram.ttf"), 32);

        _entites.Add(new Paddle());
        _entites.Add(new Ball());
        _entites.Add(new Wall() // up
        {
            Hitbox = new(0, -10, 1280, 10),
        });
        _entites.Add(new Wall() // left
        {
            Hitbox = new(-10, 0, 10, 720),
        });
        _entites.Add(new Wall() // right
        {
            Hitbox = new(1290, 0, 10, 720),
        });

        int width = 50, height = 30;
        for(int x=10; x<1280-width; x += width)
        {
            for (int y = 15; y < 350 - height; y += height)
            {
                _entites.Add(new Brick()
                {
                    Hitbox = new(x, y, width, height),
                });
                y += 2;
            }
            x += 2;
        }

        foreach (var entity in Entites)
            entity.Init();
    }

    protected override void Update()
    {
        foreach (var entity in Pending.Where(x => x.add))
            _entites.Add(entity.entity);

        foreach (var entity in Entites)
        {
            if(State is GameState.Start or GameState.Progress)
                entity.Update();
        }

        foreach (var entity in Pending.Where(x => !x.add))
            _entites.Remove(entity.entity);

        Pending.Clear();

        if (!_entites.Any(x => x is Ball))
            State = GameState.Lose;
        if (!_entites.Any(x => x is Brick))
            State = GameState.Win;
    }

    protected override void Render()
    {
        Window.Clear(Color.Black);

        foreach (var entity in Entites) 
            entity.Render(batcher);

        if (State is GameState.Lose)
            batcher.Text(font, "L", new Vector2(600, 200), 256, Color.BlueViolet);
        else if (State is GameState.Win)
            batcher.Text(font, "W", new Vector2(600, 200), 256, Color.OrangeRed);

        batcher.Render(Window);
        batcher.Clear();
    }

    protected override void Shutdown()
    {
        foreach (var actor in Entites)
            actor.Delete();
    }

    public void AddEntity(Entity entity)
    {
        Pending.Add((true, entity));
    }

    public void RemoveEntity(Entity entity)
    {
        Pending.Add((false, entity));
    }

    public enum GameState
    {
        Start,
        Progress,
        Win,
        Lose
    }
}
