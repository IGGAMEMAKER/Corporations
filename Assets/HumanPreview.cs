using System;
using Assets.Core;
using UnityEngine.UI;

public class HumanPreview : View
{
    public Text Overall;
    public Text Description;
    public Text RoleText;

    public ProgressBar Loyalty;
    public ProgressBar Adaptation;

    public GameEntity human;

    public void Render(bool drawAsEmployee)
    {
        var rating = Humans.GetRating(Q, human);

        var entityID = human.creationIndex;

        var description = $"{human.human.Name.Substring(0, 1)}. {human.human.Surname} \n#{entityID}"; // \n{formattedRole}


        RenderRole(drawAsEmployee);

        Overall.text = $"{rating}";
        Description.text = description;

        // render company related data if is worker
        RenderCompanyData(drawAsEmployee);
    }

    private void RenderCompanyData(bool drawAsEmployee)
    {
        if (Loyalty != null)
        {
            if (!drawAsEmployee)
                Loyalty.SetValue(human.humanCompanyRelationship.Morale);

            Loyalty.gameObject.SetActive(!drawAsEmployee);
        }

        if (Adaptation != null)
        {
            if (!drawAsEmployee)
                Adaptation.SetValue(human.humanCompanyRelationship.Adapted);

            Adaptation.gameObject.SetActive(!drawAsEmployee);
        }
    }

    void RenderRole(bool drawAsEmployee)
    {
        var role = Humans.GetRole(human);
        var formattedRole = Humans.GetFormattedRole(role);

        if (RoleText != null)
        {
            RoleText.text = formattedRole;

            if (drawAsEmployee)
            {
                var company = SelectedCompany;

                var hasWorkerOfSameType = Teams.HasFreePlaceForWorker(company, role);
                RoleText.color = Visuals.GetColorPositiveOrNegative(hasWorkerOfSameType);
            }
        }
    }

    /// <summary>
    /// asdasdasd
    /// </summary>
    /// <param name="humanId"></param>
    /// <param name="drawAsEmployee">if true - renders as employee. Renders as worker otherwise</param>
    public void SetEntity(int humanId, bool drawAsEmployee)
    {
        human = Humans.GetHuman(Q, humanId);

        Render(drawAsEmployee);
    }
}
