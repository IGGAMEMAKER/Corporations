using Entitas;
using UnityEngine;

public class UpgradeProductSystem : IExecuteSystem
{
    readonly GameContext _context;

    public UpgradeProductSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameEntity[] entities = _context.GetEntities(GameMatcher.AnyOf(GameMatcher.Product));

            foreach(var e in entities) {
                var p = e.product;

                Debug.Log($"update {e.product.Name}");
                e.ReplaceProduct(p.Id, p.Name, p.Niche, p.ProductLevel + 1, p.ExplorationLevel, p.Resources);
            }
        }
    }
}
