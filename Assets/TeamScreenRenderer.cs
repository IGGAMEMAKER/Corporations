using Assets;
using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScreenRenderer : MonoBehaviour
{
    public WorkerListRenderer Workers;
    public EmployeeListRenderer Employees;
    public TeamMoraleView TeamMoraleView;
    public TeamLevelView TeamLevelView;
    public Button WorkerEmployeeToggle;
    public GameObject Content;

    bool isTeamView = true;
    SoundManager soundManager;

    List<Human> workers;
    List<Human> employees;
    int teamMorale;
    int projectId;

    void Start()
    {
        soundManager = new SoundManager();
    }

    void ClearWorkerContent()
    {
        foreach (Transform child in Content.transform)
            Destroy(child.gameObject);
    }

    void RenderWorkers(List<Human> workers, int teamMorale, int projectId)
    {
        ClearWorkerContent();

        Workers.Render(workers, teamMorale, projectId);
    }

    void RenderEmployees(List<Human> employees, int projectId)
    {
        ClearWorkerContent();

        Employees.Render(employees, projectId);
    }

    void RenderTeamMorale(Project p)
    {
        TeamMoraleView.Redraw(p.moraleData);
    }

    void RenderToggleButton()
    {
        WorkerEmployeeToggle.GetComponentInChildren<Text>().text = isTeamView ? "Hire workers" : "Show team";

        if (isTeamView)
            RenderWorkers(workers, teamMorale, projectId);
        else
            RenderEmployees(employees, projectId);
    }

    void RenderTeamLevel(Team team)
    {
        TeamLevelView.SetData(team);
    }

    public void Toggle()
    {
        isTeamView = !isTeamView;
        RenderToggleButton();
        soundManager.PlayToggleSound();
    }

    public void RenderTeam(Project p)
    {
        Team team = p.Team;

        teamMorale = p.moraleData.Morale;
        projectId = p.Id;
        employees = p.Employees;

        workers = team.Workers;

        RenderToggleButton();

        RenderTeamMorale(p);
        RenderTeamLevel(team);
    }
}
