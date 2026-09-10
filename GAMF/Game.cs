using Foster.Framework;
using System.Numerics;

namespace GAMF;

internal sealed class Game : App
{
    public static readonly Game Instance = new();
    public static Texture PlayerTexture { get; private set; } = null!;

    private readonly Batcher batch;

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
        FileSystem.OpenTitleStorage((cs) =>
        {
            byte[] pngBytes = cs.ReadAllBytes("Assets/player.png");

            PlayerTexture = new Texture(GraphicsDevice, new(pngBytes));
        });
    }

    protected override void Update()
    {
        // Game logic / input handling goes here. Access input via app.Input,
        // e.g. app.Input.Keyboard.Down(Keys.Left).
    }

    protected override void Render()
    {
        Window.Clear(Color.Black);

        batch.Image(PlayerTexture, new Vector2(200, 50), Color.White);

        batch.Render(Window);
        batch.Clear();
    }

    protected override void Shutdown()
    {
        // Cleanup, if needed.
    }
}
