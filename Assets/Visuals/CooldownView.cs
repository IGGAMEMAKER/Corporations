using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View
{
    public CooldownType CooldownType;

    public Image Panel;
    public GameObject ScheduleIcon;
    public Text Text;

    GameEntity Observable
    {
        get
        {
            return MyProductEntity;
        }
    }


    void Toggle(bool show)
    {
        Panel.enabled = show;

        Text.gameObject.SetActive(show);
    }

    void Hide()
    {
        Toggle(false);
    }

    void Show(Cooldown cooldown)
    {
        Toggle(true);

        int remaining = cooldown.EndDate - CurrentIntDate;

        Text.text = $"{remaining} days";
    }

    // TODO AAAAAAAAAAAAAAAAAAAAAaaa((((
    Cooldown GetCooldown(List<Cooldown> cooldowns, CooldownType cooldownType)
    {
        var c = cooldowns.Find(cooldown => cooldown.CooldownType == cooldownType);

        if (c == null)
            return null;

        switch (cooldownType)
        {
            case CooldownType.StealIdeas:
                Debug.Log("steal ideas");
                return SelectedCompany.company.Id == (c as CooldownStealIdeas).targetCompanyId ? c : null;
                break;

            default: return c;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (Observable == null)
            return;

        var cooldowns = Observable.cooldowns.Cooldowns;

        var cooldown = GetCooldown(cooldowns, CooldownType);

        if (cooldown != null)
            Show(cooldown);
        else
            Hide();
    }
}
