using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTabRelay : View
{
    public GameObject EmployeesTab;
    public GameObject HireManagersRelayButtons;
    public GameObject Managers;
    public GameObject ManagerInteractions;

    public GameObject BackToManagerTab;
    public GameObject BackToMainTab;

    public List<GameObject> Tabs => new List<GameObject> { EmployeesTab, HireManagersRelayButtons, ManagerInteractions, Managers, BackToMainTab, BackToManagerTab };

    public void HireWorker(WorkerRole workerRole)
    {
        ShowOnly(EmployeesTab, Tabs);
        Show(BackToManagerTab);
    }

    public void OpenManagerTab()
    {
        ShowOnly(Managers, Tabs);
        Show(HireManagersRelayButtons);
        Show(BackToMainTab);
    }

    private void OnEnable()
    {
        OpenManagerTab();
    }
}
