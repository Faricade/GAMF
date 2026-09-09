using Foster.Framework;

namespace GAMF;

internal sealed class Game : App
{
    public static readonly Game Instance = new();
    public static readonly ContentStorage storage = FileSystem.OpenTitleStorage();

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
        // Load content, initialize game state, etc.
    }

    protected override void Update()
    {
        // Game logic / input handling goes here. Access input via app.Input,
        // e.g. app.Input.Keyboard.Down(Keys.Left).
    }

    protected override void Render()
    {
        Window.Clear(Color.Black);

        // Draw calls go here, e.g. batch.Rect(...), batch.Image(...).

        batch.Render(Window);
        batch.Clear();
    }

    protected override void Shutdown()
    {
        // Cleanup, if needed.
    }
}
