using System.Numerics;
using Foster.Framework;
using GAMF.Entities;
using GAMF.Scenes;

namespace GAMF;

// b a l l s
public class Ball : Entity
{
    public float Direction = Calc.Up;
    public float Speed = 300;
    public Circle Circle;

    public override void Init()
    {
        Circle = new(new(1280 / 2, 630), 10f);
    }

    public override void Update(in Time time)
    {
        GameScene scene = (GameScene)Owner;
        if (scene.State == GameScene.GameState.Start)
        {
            Entity pad = Owner.EntityManager.Entities.First(x => x is Paddle);
            Circle.Position = new(pad.Hitbox.X + pad.Hitbox.Width / 2, pad.Hitbox.Y - 20);
            if (Owner.Input.Keyboard.Down(Keys.Up))
                scene.State = GameScene.GameState.Progress;
        }
        else if (scene.State == GameScene.GameState.Progress)
        {
            Circle.Position += Calc.AngleToVector(Direction) * Speed * time.Delta;
            foreach (Entity solidWall in Owner.EntityManager.Entities)
            {
                if (solidWall is Wall wall)
                {
                    if (Circle.Overlaps(wall.Hitbox, out Vector2 pushout))
                    {
                        Vector2 normal = pushout.Normalized();
                        Vector2 incoming = Calc.AngleToVector(Direction);
                        Vector2 reflected = incoming - (2 * Vector2.Dot(incoming, normal) * normal);

                        Direction = Calc.Angle(reflected);
                        Circle.Position += pushout;
                    }
                }
                if (solidWall is Brick brick)
                {
                    if (Circle.Overlaps(brick.Hitbox, out Vector2 pushout))
                    {
                        Vector2 normal = pushout.Normalized();
                        Vector2 incoming = Calc.AngleToVector(Direction);
                        Vector2 reflected = incoming - (2 * Vector2.Dot(incoming, normal) * normal);

                        Direction = Calc.Angle(reflected);
                        Circle.Position += pushout;

                        Owner.EntityManager.RemoveEntity(solidWall);
                    }
                }
                if (solidWall is Paddle paddle)
                {
                    if (Circle.Overlaps(paddle.Hitbox, out Vector2 pushout))
                    {
                        Circle.Position += pushout;

                        float hitPosition = (Circle.Position.X - paddle.Hitbox.Left) / paddle.Hitbox.Width;
                        Direction = Calc.ClampedLerp(Calc.DegToRad * -160f, Calc.DegToRad * -20f, hitPosition);
                    }
                }
            }
            if (Circle.Position.Y > 720)
                Owner.EntityManager.RemoveEntity(this);
        }
    }

    public override void Render(Batcher batcher)
    {
        batcher.Circle(Circle, 8, Color.IndianRed);
    }

    public override void Dispose()
    {

    }
}
