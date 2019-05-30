using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRoleButtons : View
{
    public GameObject ButtonPrefab;
    public GameObject Container;

    Dictionary<WorkerRole, GameObject> Buttons;

    void Start()
    {
        Buttons = new Dictionary<WorkerRole, GameObject>();

        var b = Instantiate(new GameObject(), Container.transform, true);
        b.transform.SetSiblingIndex(1);

        b = Instantiate(new GameObject(), Container.transform, true);
        b.transform.SetSiblingIndex(1);

        AddButton(WorkerRole.Business);
        AddButton(WorkerRole.TechDirector);
        AddButton(WorkerRole.ProjectManager);
        AddButton(WorkerRole.MarketingDirector);
        AddButton(WorkerRole.ProductManager);
        AddButton(WorkerRole.Manager);
        AddButton(WorkerRole.Marketer);
        AddButton(WorkerRole.Programmer);

        RenderText();
    }

    void RenderText()
    {
        foreach (var pair in Buttons)
        {
            var role = pair.Key;
            var rating = HumanUtils.GetWorkerRatingInRole(SelectedHuman, role);

            pair.Value.GetComponentInChildren<Text>().text = $"Set as {HumanUtils.GetFormattedRole(role)} => {rating}";
        }
    }

    private void OnEnable()
    {
        RenderText();
    }

    void AddButton(WorkerRole role)
    {
        var b = Instantiate(ButtonPrefab, Container.transform, true);

        b.transform.SetSiblingIndex(2);

        Buttons[role] = b;

        var controller = b.AddComponent<SetWorkerRoleButton>();

        controller.SetRole(role);
    }
}
