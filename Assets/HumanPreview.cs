using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPreview : View
{
    public Text Overall;
    public Text Description;

    public GameEntity human;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public void Render()
    {
        var overall = HumanUtils.GetOverallRating(human);

        Overall.text = overall.ToString();


        var role = GetFormattedRole(human.worker.WorkerRole);

        var description = $"{role}\n{human.human.Surname} {human.human.Name.Substring(0, 1)}.";

        Description.text = description;
    }

    public static string GetFormattedRole(WorkerRole role)
    {
        switch (role)
        {
            case WorkerRole.Business: return "CEO";
            case WorkerRole.Manager: return "Manager";
            case WorkerRole.Marketer: return "Marketer";
            case WorkerRole.MarketingDirector: return "Marketing Director";
            case WorkerRole.ProductManager: return "Product Manager";
            case WorkerRole.Programmer: return "Programmer";
            case WorkerRole.ProjectManager: return "Project Manager";
            case WorkerRole.TechDirector: return "Tech Director";

            default: return role.ToString();
        }
    }

    public void SetEntity(int humanId)
    {
        human = HumanUtils.GetHumanById(GameContext, humanId);

        Render();
    }
}
