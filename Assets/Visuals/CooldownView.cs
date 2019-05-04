using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View
    , IAnyDateListener
{
    public CooldownType CooldownType;
    
    void Start()
    {
        Render();
    }

    void OnEnable()
    {

    }

    void Hide()
    {

    }

    void Show(Cooldown cooldown)
    {
        
    }

    void Render()
    {
        var cooldowns = MyProductEntity.cooldowns.Cooldowns;

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
