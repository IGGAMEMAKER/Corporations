using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeamData : View
{
    private void OnGUI()
    {
        var product = SelectedCompany;

        var employees = product.employee.Managers;
        var team = product.team.Managers;

        //GUI.Label(new Rect(0, 500, 120, 300), "Employees : " + queries.Count + ".");
    }
}
