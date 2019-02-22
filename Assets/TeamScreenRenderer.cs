using Assets;
using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class TeamScreenRenderer : MonoBehaviour
{
    public StaffListController Managers;
    public StaffListController Programmers;
    public StaffListController Marketers;

    public TeamMoraleView TeamMoraleView;

    int teamMorale;
    int projectId;

    void RenderTeamMorale(Product p)
    {
        //TeamMoraleView.Render(p.moraleData);
    }

    public void RenderTeam(Product p)
    {
        //Team team = p.Team;

        //teamMorale = p.moraleData.Morale;
        //projectId = p.Id;

        //List<Human> employees = p.Employees;
        //List<Human> programmerEmployees = employees.FindAll(h => h.IsProgrammer());
        //List<Human> managerEmployees = employees.FindAll(h => h.IsManager());
        //List<Human> marketerEmployees = employees.FindAll(h => h.IsMarketer());

        //Managers.Render(team.Managers, managerEmployees, teamMorale, projectId);
        //Programmers.Render(team.Programmers, programmerEmployees, teamMorale, projectId);
        //Marketers.Render(team.Marketers, marketerEmployees, teamMorale, projectId);

        //RenderTeamMorale(p);
    }
}
