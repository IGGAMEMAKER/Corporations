using Assets;
using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScreenRenderer : MonoBehaviour
{
    bool isTeamView = true;
    GameObject Workers;
    GameObject Employees;
    GameObject TeamMorale;
    GameObject ToggleButton;

    SoundManager soundManager;

    // Use this for initialization
    void Start()
    {
        soundManager = new SoundManager();
    }

    void RenderWorkers(List<Human> workers, Dictionary<string, object> parameters)
    {
        Workers = gameObject.transform.Find("Workers").gameObject;
        Workers.GetComponent<WorkerListRenderer>().UpdateList(workers, parameters);
    }

    void RenderEmployees(List<Human> employees, Dictionary<string, object> parameters)
    {
        Employees = gameObject.transform.Find("Employees").gameObject;
        Employees.GetComponent<WorkerListRenderer>().UpdateList(employees, parameters);
    }

    void RenderTeamMorale(Project p)
    {
        TeamMorale = gameObject.transform.Find("TeamMorale").gameObject;
        TeamMorale.GetComponent<TeamMoraleView>().Redraw(p.moraleData);
    }

    void RenderToggleButton()
    {
        GameObject ToggleObject = gameObject.transform.Find("Toggle").gameObject;
        Button ToggleButton = ToggleObject.GetComponent<Button>();
        ToggleButton.onClick.RemoveAllListeners();
        ToggleButton.onClick.AddListener(delegate { Toggle(); });

        ToggleObject.GetComponentInChildren<Text>().text = isTeamView ? "Hire workers" : "Show team";

        Employees.SetActive(!isTeamView);
        Workers.SetActive(isTeamView);
    }

    void Toggle()
    {
        isTeamView = !isTeamView;
        RenderToggleButton();
        soundManager.PlayToggleSound();
    }

    public void RenderTeam(Project p)
    {
        Team team = p.Team;
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["teamMorale"] = p.moraleData.Morale;

        RenderWorkers(team.Workers, parameters);
        RenderEmployees(p.Employees, parameters);

        RenderToggleButton();

        RenderTeamMorale(p);
    }
}
