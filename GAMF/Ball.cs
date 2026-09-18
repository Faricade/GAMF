using Foster.Framework;
using System.Numerics;

namespace GAMF;

// b a l l s
public class Ball : Actor
{
    public float Direction = Calc.Up;
    public float Speed = 300;
    public Circle Circle;

    public override void Init()
    {
        Circle = new(new(1280 / 2, 630), 10f);
    }

    public override void Update()
    {
        Circle.Position += Calc.AngleToVector(Direction) * Speed * Game.Instance.Time.Delta;
        foreach (Entity solidWall in Game.Instance.Entites)
        {
            if (solidWall is Wall wall)
            {
                if (Circle.Overlaps(wall.Hitbox, out Vector2 pushout))
                {
                    Vector2 normal = pushout.Normalized();
                    Vector2 incoming = Calc.AngleToVector(Direction);
                    Vector2 reflected = incoming - 2 * Vector2.Dot(incoming, normal) * normal;
                    
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
                    Vector2 reflected = incoming - 2 * Vector2.Dot(incoming, normal) * normal;

                    Direction = Calc.Angle(reflected);
                    Circle.Position += pushout;

                    Game.Instance.RemoveEntity(solidWall);
                }
            }
            if (solidWall is Paddle paddle)
            {
                if (Circle.Overlaps(paddle.Rectangle, out Vector2 pushout))
                {
                    Circle.Position += pushout;

                    float hitPosition = (Circle.Position.X - paddle.Rectangle.Left) / paddle.Rectangle.Width;
                    Direction = Calc.ClampedLerp(Calc.DegToRad * -160f, Calc.DegToRad * -20f, hitPosition);
                }
            }
        }
        if (Circle.Position.Y > 720)
            Delete();
    }

    public override void Render(Batcher batcher)
    {
        batcher.Circle(new Circle(Circle.Position, Circle.Radius), 8, Color.White);
    }

    public override void Delete()
    {
        Game.Instance.RemoveEntity(this);
    }
}
