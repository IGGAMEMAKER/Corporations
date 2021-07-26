using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTeamsOnTransfersController : MonoBehaviour
{
    public List<TeamListView> Workers;
    public List<ManagerPointsView2> Points;

    public void UpdateAll()
    {
        // TODO Order matters!
        foreach (var v in Points)
        {
            v.ViewRender();
        }

        foreach (var w in Workers)
        {
            w.ViewRender();
        }
        //
    }
}
