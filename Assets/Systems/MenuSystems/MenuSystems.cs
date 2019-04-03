public class MenuSystems : Feature
{
    public MenuSystems(Contexts contexts) : base("Menu Systems")
    {
        Add(new MenuNavigationInitializerSystem(contexts));

        Add(new MenuInputSystem(contexts));
    }
}
