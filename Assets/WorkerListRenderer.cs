using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class WorkerListRenderer: MonoBehaviour
{
    public GameObject Prefab;

    public void Render(List<Human> workers, int teamMorale, int projectId)
    {
        for (int i = 0; i < workers.Count; i++)
        {
            GameObject v = Instantiate(Prefab, transform);

            v.GetComponent<WorkerPreview>().Render(workers[i], i, projectId, teamMorale);
        }
    }
}