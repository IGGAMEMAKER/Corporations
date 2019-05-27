using System.Collections.Generic;
using UnityEngine.UI;

public class RenderWorkerAmount : View
    //, ITeamListener
{
    public Text Managers;
    public Text Programmers;
    public Text Marketers;

    void Render()
    {
        var t = MyProductEntity.team;

        //Programmers.text = t.Programmers.ToString();
        //Managers.text = t.Managers.ToString();
        //Marketers.text = t.Marketers.ToString();
    }
}
