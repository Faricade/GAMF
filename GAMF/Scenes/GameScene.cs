using System.Numerics;
using Foster.Framework;
using GAMF.Entities;

namespace GAMF.Scenes;

internal class GameScene(SpriteFont font) : Scene()
{
    public int Lives = 3;
    public GameState State { get; set; } = GameState.Start;
    public required Game Game;

    public override void Init()
    {
        EntityManager.AddEntity(new Paddle());
        EntityManager.AddEntity(new Ball());
        EntityManager.AddEntity(new Wall() // up
        {
            Hitbox = new(0, -10, 1280, 10),
        });
        EntityManager.AddEntity(new Wall() // left
        {
            Hitbox = new(-10, 0, 10, 720),
        });
        EntityManager.AddEntity(new Wall() // right
        {
            Hitbox = new(1290, 0, 10, 720),
        });

        int width = 50, height = 30;
        for (int x = 10; x < 1280 - width; x += width)
        {
            for (int y = 15; y < 350 - height; y += height)
            {
                EntityManager.AddEntity(new Brick()
                {
                    Hitbox = new(x, y, width, height),
                    Color = new(1, 0.5f, Calc.Map(y, 15, 350 - height, 0, 1), 255),
                });
                y += 2;
            }
            x += 2;
        }

        EntityManager.ApplyAdditions();
    }

    public override void Update(in Time time)
    {
        EntityManager.ApplyAdditions();

        foreach (Entity entity in EntityManager.Entities)
        {
             entity.Update(time);
        }

        EntityManager.ApplyRemovals();

        if (!EntityManager.Entities.Any(x => x is Ball))
        {
            if(--Lives <= 0)
                State = GameState.Lose;
            else
            {
                EntityManager.AddEntity(new Ball());
                State = GameState.Start;
            }
        }
        if (!EntityManager.Entities.Any(x => x is Brick))
            State = GameState.Win;

        if (State == GameState.Lose && Input.Keyboard.Down(Keys.Up))
            Game.ChangeScene(new GameScene(font) { Game = Game });
    }

    public override void Render(Batcher batcher)
    {
        foreach (Entity entity in EntityManager.Entities)
            entity.Render(batcher);

        if (State is GameState.Lose)
            batcher.Text(font, "L", new Vector2(600, 200), 256, Color.BlueViolet);
        else if (State is GameState.Win)
            batcher.Text(font, "W", new Vector2(600, 200), 256, Color.OrangeRed);

        for(int i=1; i<=Lives; i++)
        {
            batcher.Circle(new Circle(25*i, 20, 8f), 8, Color.White);
        }
    }

    public override void Dispose()
    {
        foreach (Entity Entity in EntityManager.Entities)
            Entity.Dispose();
    }

    public enum GameState
    {
        Start,
        Progress,
        Win,
        Lose
    }
}
