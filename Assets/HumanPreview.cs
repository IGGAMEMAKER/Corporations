using Assets.Core;
using UnityEngine.UI;

public class HumanPreview : View
{
    public Text Overall;
    public Text Description;
    public Text RoleText;

    public GameEntity human;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public void Render()
    {
        var overall = HumanUtils.GetOverallRating(human, GameContext);

        Overall.text = $"{overall}";

        var role = HumanUtils.GetRole(human);

        var formattedRole = HumanUtils.GetFormattedRole(role);

        var description = $"{human.human.Name.Substring(0, 1)}. {human.human.Surname}"; // \n{formattedRole}

        Description.text = description;

        if (RoleText)
            RoleText.text = formattedRole;
    }

    public void SetEntity(int humanId)
    {
        human = HumanUtils.GetHuman(GameContext, humanId);

        Render();
    }
}
