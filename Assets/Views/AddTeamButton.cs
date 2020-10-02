using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class AddTeamButton : ButtonController
{
    public TeamType TeamType;

    //[Space(20)]
    [Header("Specify team images here")]
    Sprite source;

    public Sprite Universal;
    public Sprite Development;
    public Sprite Marketing;
    public Sprite Servers;
    public Sprite Support;

    [Space(20)]
    public Image TeamTypeImage;
    public TextMeshProUGUI Title;

    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType);
    }

    //[ExecuteInEditMode]
    void OnValidate()
    {
        //Debug.Log("AddTeamButton");
        if (Title == null)
            return;
        Title.text = $"<b>Create {Teams.GetFormattedTeamType(TeamType)}";

        switch (TeamType)
        {
            case TeamType.SupportTeam:
                source = Support;
                break;

            case TeamType.DevOpsTeam:
                source = Servers;
                break;

            case TeamType.DevelopmentTeam:
                source = Development;
                break;

            case TeamType.MarketingTeam:
                source = Marketing;
                break;

            default:
                source = Universal;
                break;
        }

        TeamTypeImage.sprite = source;
    }
}
