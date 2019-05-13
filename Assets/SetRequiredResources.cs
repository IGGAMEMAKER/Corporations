using Assets.Classes;
using UnityEngine;

[RequireComponent(typeof(RequiresResourcesButtonController))]
public class SetRequiredResources : MonoBehaviour
{
    public int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints;
    public long Money;

    RequiresResourcesButtonController RequiresResourcesButtonController;

    void Start()
    {
        RequiresResourcesButtonController = GetComponent<RequiresResourcesButtonController>();
    }

    void Update()
    {
        RequiresResourcesButtonController.RequiredResources = new TeamResource(ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money);
    }
}
