using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View
    , IAnyDateListener
{
    public CooldownType CooldownType;

    public Image Panel;
    public GameObject ScheduleIcon;
    public Text Text;

    //bool wasPositionUpdated = false;

    void Start()
    {
        Debug.Log("Render cooldown");
        Render();

        Debug.Log("Listen changes");
        ListenDateChanges(this);

        Debug.Log("Update position");
        //UpdatePosition();
    }

    ////IEnumerator UpdatePosition()
    //void UpdatePosition()
    //{
    //    if (wasPositionUpdated)
    //        return;

    //    wasPositionUpdated = true;
    //    //yield return new WaitForSeconds(0.21f);

    //    transform.SetParent(transform.parent.parent);
    //    transform.SetAsLastSibling();
    //}

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

        //Panel.GetComponent<RectTransform>().lo

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
        if (Observable == null)
            return;

        //UpdatePosition();

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
