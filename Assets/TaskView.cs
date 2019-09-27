using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CompanyTaskType
{
    ExploreMarket,
    ExploreCompany,

    AcquiringCompany,

}

public class TaskView : View
{
    public Text Text;
    int TaskId;

    public void SetEntity(int taskId)
    {
        TaskId = taskId;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        
    }
}
