using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class CooldownView : View
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

    // TODO AAAAAAAAAAAAAAAAAAAAA((((
    Cooldown GetCooldown(List<Cooldown> cooldowns, CooldownType cooldownType)
    {
        switch (cooldownType)
        {
            case CooldownType.StealIdeas:
                return cooldowns.Find(cooldown => cooldown.Compare(new CooldownStealIdeas(targetCompanyId)));

            default: 
                return cooldowns.Find(cooldown => cooldown.Compare(cooldownType));
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

public partial class CooldownView
{
    int targetCompanyId;

    public void SetTargetCompanyForStealing(int companyId)
    {
        targetCompanyId = companyId;
    }
}
