using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffListController : MonoBehaviour {
    public GameObject WorkerPrefab;
    public GameObject EmployeePrefab;
    public ListContentManager ListContentManager;

    public void Render(List<Human> workers, List<Human> employees, int teamMorale, int projectId)
    {
        List<GameObject> items = new List<GameObject>();

        workers.Sort((h1, h2) => h2.Level - h1.Level);

        for (int i = 0; i < workers.Count; i++)
        {
            GameObject v = Instantiate(WorkerPrefab, transform);

            v.GetComponent<WorkerPreview>().Render(workers[i], i, projectId, teamMorale);

            items.Add(v);
        }

        // add Hire button here
        employees.Sort((h1, h2) => h2.Level - h1.Level);

        for (int i = 0; i < employees.Count; i++)
        {
            GameObject v = Instantiate(EmployeePrefab, transform);

            v.GetComponent<WorkerPreview>().Render(employees[i], i, projectId, teamMorale);

            items.Add(v);
        }

        ListContentManager.SetContent(items);
    }
}
