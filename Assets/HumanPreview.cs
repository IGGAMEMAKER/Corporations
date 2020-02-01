using Assets.Core;
using UnityEngine.UI;

public class HumanPreview : View
{
    public Text Overall;
    public Text Description;
    public Text RoleText;

    public GameEntity human;

    public void Render()
    {
        var rating = Humans.GetRating(Q, human);

        var entityID = human.creationIndex;

        var description = $"{human.human.Name.Substring(0, 1)}. {human.human.Surname} \n#{entityID}"; // \n{formattedRole}


        var role = Humans.GetRole(human);
        var formattedRole = Humans.GetFormattedRole(role);
        if (RoleText != null)
        {
            RoleText.text = formattedRole;
        }

        Overall.text = $"{rating}";
        Description.text = description;
    }

    public void SetEntity(int humanId)
    {
        human = Humans.GetHuman(Q, humanId);

        Render();
    }
}
