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

        if (isTeamView)
            ToggleObject.GetComponentInChildren<Text>().text = "Hire workers";
        else
            ToggleObject.GetComponentInChildren<Text>().text = "Show team";

        Employees.SetActive(!isTeamView);
        Workers.SetActive(isTeamView);
    }

    void Toggle()
    {
        isTeamView = !isTeamView;
        RenderToggleButton();
    }

    public void RenderTeam(Project p)
    {
        Team team = p.Team;
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["teamMorale"] = p.moraleData.Morale;

        RenderWorkers(team.Workers, parameters);
        RenderEmployees(team.Workers, parameters);

        RenderToggleButton();

        RenderTeamMorale(p);
    }
}
