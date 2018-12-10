using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Classes;
using UnityEngine;

public class TeamLevelView : MonoBehaviour {
    public ResourceView coding;
    public ResourceView management;
    public ResourceView marketing;

    internal void SetData(Team team)
    {
        coding.UpdateResourceValue("Programming", team.GetProgrammerAverageLevel());
        management.UpdateResourceValue("Management", team.GetManagerAverageLevel());
        marketing.UpdateResourceValue("Marketing", team.GetMarketerAverageLevel());
    }
}
