using UnityEngine;

public class WorkerView : View
{
    public HumanPreview HumanPreview;

    public void SetEntity(int humanId, WorkerRole workerRole)
    {
        var link = GetComponent<LinkToHuman>();
        if (humanId != -1)
        {
            link.SetHumanId(humanId);
            link.enabled = true;

            HumanPreview.SetEntity(humanId);
        }
        else
        {
            Debug.Log("ERROR OF ERRORS! humanId for " + workerRole + " is -1!");
            link.enabled = false;
        }
    }
}
