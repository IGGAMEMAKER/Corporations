using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderSkills : View
{
    [Header("Traits")]
    public Text Will;
    public Text Charisma;
    public Text Ambitions;
    public Text Intelligence;
    public Text Discipline;
    public Text Vision;

    //[Space(20f)]
    [Header("Roles")]
    public Text Business;
    public Text Management;
    public Text Marketing;
    public Text Programming;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Trait(Text text, int value)
    {
        //text.text = $": {value}LVL";
        text.color = Visuals.GetGradientColor(0, 100, value);
        text.text = value.ToString();
    }

    void Roles(Text text, int value)
    {
        text.color = Visuals.GetGradientColor(0, 100, value);
        text.text = value.ToString();
    }

    void Render()
    {
        var skills = SelectedHuman.humanSkills;

        var traits = skills.Traits;
        var roles = skills.Roles;

        Trait(Will, traits[TraitType.Will]);
        Trait(Charisma, traits[TraitType.Charisma]);
        Trait(Ambitions, traits[TraitType.Ambitions]);
        Trait(Intelligence, traits[TraitType.Education]);
        Trait(Discipline, traits[TraitType.Discipline]);
        Trait(Vision, traits[TraitType.Vision]);

        Roles(Business, roles[WorkerRole.CEO]);
        Roles(Management, roles[WorkerRole.Manager]);
        Roles(Marketing, roles[WorkerRole.Marketer]);
        Roles(Programming, roles[WorkerRole.Programmer]);
    }
}
