using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View
    , IAnyDateListener
{
    public CooldownType CooldownType;

    public Image Panel;
    public GameObject ScheduleIcon;
    public Text Text;
    
    void Start()
    {
        Render();

        ListenDateChanges(this);
    }

    void OnEnable()
    {
        Render();
    }

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
        //ScheduleIcon.SetActive(show);
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

    void Render()
    {
        var cooldowns = Observable.cooldowns.Cooldowns;

        if (cooldowns.ContainsKey(CooldownType))
            Show(cooldowns[CooldownType]);
        else
            Hide();
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
