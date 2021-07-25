using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransferWorker : View, IPointerClickHandler//, IPointerEnterHandler, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // https://stackoverflow.com/questions/38233975/cant-get-object-to-respond-to-right-click

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");

            Transfer();
        }
    }

    void Transfer()
    {
        var worker = GetComponent<WorkerView>();

        var human = worker.Human;
        var teamOf = Teams.GetTeamOf(human, Q);

        if (teamOf.isCoreTeam)
            TransferTo(human, 0, SelectedTeam);
        else
            TransferTo(human, SelectedTeam, 0);

        FindObjectOfType<UpdateTeamsOnTransfersController>().UpdateAll();
    }

    void TransferTo(GameEntity worker, int fromId, int toId)
    {
        var role = Humans.GetRole(worker);

        if (role != WorkerRole.CEO)
            Teams.TransferWorker(Flagship, worker, role, fromId, toId, Q);
    }
}
