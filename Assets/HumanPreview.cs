using Assets.Utils;
using UnityEngine.UI;

public class HumanPreview : View
{
    public Text Overall;
    public Text Description;

    public GameEntity human;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public void Render()
    {
        var overall = HumanUtils.GetOverallRating(human);

        Overall.text = overall.ToString();


        var role = HumanUtils.GetFormattedRole(human.worker.WorkerRole);

        var description = $"{role}\n{human.human.Surname} {human.human.Name.Substring(0, 1)}.";

        Description.text = description;
    }

    public void SetEntity(int humanId)
    {
        human = HumanUtils.GetHumanById(GameContext, humanId);

        Render();
    }
}
