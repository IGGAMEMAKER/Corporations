using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CandidateForRoleView : View, IPointerEnterHandler, IPointerExitHandler
{
    public HumanPreview HumanPreview;
    public Text Salary;
    public TraitListView TraitContainer;

    public GameEntity _human;

    public Image BorderImage;
    
    public void SetEntity(GameEntity human)
    {
        _human = human;
        Salary.text = Format.Money(Humans.GetSalary(human));
        
        HumanPreview.SetEntity(human.human.Id);

        BorderImage.color = Visuals.Neutral();
        
        ScreenUtils.SetSelectedHuman(Q, human.human.Id);
        TraitContainer.ViewRender();
    }

    public void OnHire()
    {
        var rating = Humans.GetRating(Q, _human);

        var baseSalary = Teams.GetSalaryPerRating(rating);

        var JobOffer = new JobOffer(baseSalary);
        
        Teams.HireManager(Flagship, Q, _human, SelectedTeam);
        Teams.SetJobOffer(_human, Flagship, JobOffer, SelectedTeam, Q);
        
        PlaySound(Sound.MoneySpent);
        
        CloseModal("Candidates");
        
        Refresh();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BorderImage.color = Visuals.GetColorFromString(Colors.COLOR_PANEL_SELECTED);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BorderImage.color = Visuals.Neutral();
    }
}
