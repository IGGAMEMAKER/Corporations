using System.Collections.Generic;
using UnityEngine.UI;

public class RenderWorkerAmount : View
    , ITeamListener
{
    public Text Managers;
    public Text Programmers;
    public Text Marketers;

    // Start is called before the first frame update
    void Start()
    {
        MyProductEntity.AddTeamListener(this);
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        var t = MyProductEntity.team;

        Programmers.text = t.Programmers.ToString();
        Managers.text = t.Managers.ToString();
        Marketers.text = t.Marketers.ToString();
    }

    void ITeamListener.OnTeam(GameEntity entity, int programmers, int managers, int marketers, int morale, List<int> workers)
    {
        Render();
    }
}
