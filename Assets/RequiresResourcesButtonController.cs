using Assets.Classes;

public class RequiresResourcesButtonController : ResourceChecker
{
    new void Start()
    {
        base.Start();

        SetRequiredResources(new TeamResource(10, 0, 0, 0, 0));
    }
}
