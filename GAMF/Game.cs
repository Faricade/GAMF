using Foster.Framework;
using GAMF.S;

namespace GAMF;

internal sealed class Game : App
{
    private Scene currentScene = null!;
    private Scene? pendingNewScene = null;

    private readonly Batcher batcher;

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
        ChangeScene(new GameScene(new SpriteFont(GraphicsDevice, Path.Join("Assets", "monogram.ttf"), 32)) { Input = Input });
    }

    protected override void Update()
    {
        if (pendingNewScene is not null)
        {
            currentScene?.Dispose();
            currentScene = pendingNewScene;
            currentScene.Input = Input;
            currentScene.Init();
            pendingNewScene = null;
        }
        currentScene.Update(Time);
    }

    protected override void Render()
    {
        Window.Clear(Color.Black);
        currentScene.Render(batcher);
        batcher.Render(Window);
        batcher.Clear();
    }

    protected override void Shutdown()
    {
        currentScene.Dispose();
        pendingNewScene?.Dispose();
    }

    public void ChangeScene(Scene newScene)
    {
        pendingNewScene = newScene;
    }
}
