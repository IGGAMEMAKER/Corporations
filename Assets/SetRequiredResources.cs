using Assets.Classes;
using UnityEngine;

[RequireComponent(typeof(RequiresResourcesButtonController))]
public class SetRequiredResources : MonoBehaviour
{
    public int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints;
    public long Money;

    RequiresResourcesButtonController RequiresResourcesButtonController;

    // Start is called before the first frame update
    void Start()
    {
        RequiresResourcesButtonController = GetComponent<RequiresResourcesButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        RequiresResourcesButtonController.RequiredResources = new TeamResource(ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money);
    }
}
