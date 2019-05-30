using Assets.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRoleButtons : View
{
    public GameObject ButtonPrefab;
    public GameObject Container;

    readonly Dictionary<WorkerRole, GameObject> Buttons = new Dictionary<WorkerRole, GameObject>();
    List<GameObject> Items = new List<GameObject>();

    bool toggle;

    internal void ToggleButtons()
    {
        toggle = !toggle;

        SetVisibility(toggle);
    }

    void SetVisibility(bool visibility)
    {
        toggle = visibility;

        foreach (var o in Items)
            o.SetActive(visibility);
    }

    private void OnEnable()
    {
        RenderText();

        SetVisibility(false);
    }

    void Start()
    {
        var separator = Instantiate(new GameObject());
        separator.AddComponent<RectTransform>();

        var b = Instantiate(separator, Container.transform, true);
        b.transform.SetSiblingIndex(1);

        Items.Add(b);

        b = Instantiate(separator, Container.transform, true);
        b.transform.SetSiblingIndex(1);

        Items.Add(b);

        AddButton(WorkerRole.Business, "Leader of the project. Requires Excelent Business and Vision.");
        AddButton(WorkerRole.TechDirector, "Increases you programmers effeciency. Requires Management, Programming and Business skills"); // , plus unlocks Infrastructure Improvements
        AddButton(WorkerRole.ProjectManager, "Increases total effeciency. Requires Management, Business and Vision");
        AddButton(WorkerRole.MarketingDirector, "Increases marketing effeciency. Requires Management, Marketing, Business and Vision");
        AddButton(WorkerRole.ProductManager, "Requires Vision, Management and Business");
        AddButton(WorkerRole.Manager, "Produces Manager points");
        AddButton(WorkerRole.Marketer, "Produces Marketing points");
        AddButton(WorkerRole.Programmer, "Produces Programmer points");
        AddButton(WorkerRole.Universal, "Produces All types of points with 30% penalty. Better to set another role if possible");

        RenderText();

        SetVisibility(false);
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

    void AddButton(WorkerRole role, string text)
    {
        var b = Instantiate(ButtonPrefab, Container.transform, true);

        b.transform.SetSiblingIndex(2);
        b.GetComponent<PlaySoundOnClick>().Sound = Assets.Sound.Tweak;

        Buttons[role] = b;
        Items.Add(b);

        var controller = b.AddComponent<SetWorkerRoleButton>();


        controller.SetRole(role);

        var hint = b.AddComponent<Hint>();
        hint.SetHint(text);
    }
}
