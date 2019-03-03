using Entitas;

public class MenuSystems : Feature
{
    public MenuSystems(Contexts contexts) : base("Menu Systems")
    {
        Add(new MenuInputSystem(contexts));
    }
}

