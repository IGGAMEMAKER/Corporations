using Assets;
using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScreenRenderer : MonoBehaviour
{
    public StaffListController Managers;
    public StaffListController Programmers;
    public StaffListController Marketers;

    public TeamMoraleView TeamMoraleView;

    bool isTeamView = true;
    SoundManager soundManager;

    int teamMorale;
    int projectId;

    void Start()
    {
        soundManager = new SoundManager();
    }

    void RenderTeamMorale(Project p)
    {
        TeamMoraleView.Render(p.moraleData);
    }

    public void RenderTeam(Project p)
    {
        Team team = p.Team;

        List<Human> employees = p.Employees;
        List<Human> programmerEmployees = employees.FindAll(h => h.IsProgrammer());
        List<Human> managerEmployees = employees.FindAll(h => h.IsManager());
        List<Human> marketerEmployees = employees.FindAll(h => h.IsMarketer());

        teamMorale = p.moraleData.Morale;
        projectId = p.Id;

        Debug.Log("Programmers: " + team.Programmers.Count);

        Managers.Render(team.Managers, managerEmployees, teamMorale, projectId);
        Programmers.Render(team.Programmers, programmerEmployees, teamMorale, projectId);
        Marketers.Render(team.Marketers, marketerEmployees, teamMorale, projectId);


        RenderTeamMorale(p);
    }
}
