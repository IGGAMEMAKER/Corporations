using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransferWorker : View, IPointerClickHandler//, IPointerEnterHandler, IPointerClickHandler
{
    //void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver");

        RightClick();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        // https://stackoverflow.com/questions/38233975/cant-get-object-to-respond-to-right-click

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");

            Transfer();
        }
    }

    void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("MOOOUSE!");

            Transfer();
        }
    }

    void Transfer()
    {
        var worker = GetComponent<WorkerView>();

        var human = worker.Human;
        var teamOf = Teams.GetTeamOf(human, Q);

        if (teamOf.isCoreTeam)
        {
            Debug.Log("Is core team");

            TransferTo(human, 0, SelectedTeam);
        }
        else
        {
            Debug.Log("Is team " + SelectedTeam);

            TransferTo(human, SelectedTeam, 0);
        }

        FindObjectOfType<UpdateTeamsOnTransfersController>().UpdateAll();
    }

    void TransferTo(GameEntity worker, int fromId, int toId)
    {
        Teams.TransferWorker(Flagship, worker, Humans.GetRole(worker), fromId, toId, Q);
    }
}
