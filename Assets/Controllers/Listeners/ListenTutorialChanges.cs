using Assets.Core;
using System.Collections.Generic;

public class ListenTutorialChanges : Controller
    , ITutorialListener
{
    public override void AttachListeners()
    {
        TutorialUtils.AddEventListener(Q, this);
    }

    public override void DetachListeners()
    {
        TutorialUtils.RemoveEventListener(Q, this);
    }

    void ITutorialListener.OnTutorial(GameEntity entity, Dictionary<TutorialFunctionality, bool> progress)
    {
        Render();
    }
}
