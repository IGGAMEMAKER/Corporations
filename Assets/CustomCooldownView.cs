using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCooldownView : View
{
    public string CooldownType;

    public Image Panel;
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

    public override void ViewRender()
    {
        base.ViewRender();

        if (Observable == null)
            return;

        var cooldowns = Observable.cooldowns.Cooldowns;

        //if (cooldowns.ContainsKey(CooldownType))
        //    Show(cooldowns[CooldownType]);
        //else
        //    Hide();
    }
}
