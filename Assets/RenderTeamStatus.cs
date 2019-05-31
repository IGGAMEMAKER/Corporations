using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTeamStatus : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<Text>().text = MyProductEntity.team.TeamStatus.ToString();
    }
}
