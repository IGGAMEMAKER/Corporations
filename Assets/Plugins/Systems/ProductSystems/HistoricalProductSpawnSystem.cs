using System;
using System.Collections.Generic;

public partial class HistoricalProductSpawnSystem : OnMonthChange
{
    readonly GameContext GameContext;

    public List<GameEntity> Products;

    public HistoricalProductSpawnSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        
    }

    void Initialize()
    {

    }
}
