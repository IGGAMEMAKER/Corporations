using Assets.Utils;
using System.Collections.Generic;

public class ListenTutorialChanges : Controller
    , ITutorialListener
{
    public override void AttachListeners()
    {
        TutorialUtils.AddEventListener(GameContext, this);
    }

    public override void DetachListeners()
    {
        TutorialUtils.RemoveEventListener(GameContext, this);
    }

    void ITutorialListener.OnTutorial(GameEntity entity, Dictionary<TutorialFunctionality, bool> progress)
    {
        Render();
    }
}
