using Assets.Classes;

public class RequiresResourcesButtonController : ResourceChecker
{
    void Update()
    {
        SetRequiredResources(new TeamResource(10, 0, 0, 0, 0));
    }
}
