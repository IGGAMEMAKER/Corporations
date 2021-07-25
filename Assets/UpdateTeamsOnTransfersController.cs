using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTeamsOnTransfersController : MonoBehaviour
{
    public List<TeamListView> Workers;

    public void UpdateAll()
    {
        foreach (var w in Workers)
        {
            w.ViewRender();
        }
    }
}
