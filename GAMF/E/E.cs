using GAMF.S;

namespace GAMF.E;

/// <summary>
/// AKA EntityManager
/// </summary>
internal class E(Scene ownerScene)
{
    private readonly List<Entity> _entites = [];
    private readonly HashSet<Entity> ToAdd = [];
    private readonly HashSet<Entity> ToRemove = [];

    public IReadOnlyList<Entity> Entities => _entites;

    public void AddEntity(Entity entity)
    {
        ToAdd.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        ToRemove.Add(entity);
    }

    public void ApplyAdditions()
    {
        foreach (Entity i in ToAdd)
        {
            i.Owner = ownerScene;
            _entites.Add(i);
            i.Init();
        }
        ToAdd.Clear();
    }

    public void ApplyRemovals()
    {
        foreach (Entity i in ToRemove)
        {
            i.Dispose();
            i.Owner = null!;
            _entites.Remove(i);
        }
        ToRemove.Clear();
    }
}
