using Assets.Classes;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerListRenderer: MonoBehaviour
{
    public GameObject Prefab;

    public void Render(List<Human> workers, int teamMorale, int projectId)
    {
        for (int i = 0; i < workers.Count; i++)
        {
            GameObject v = Instantiate(Prefab, transform);

            v.GetComponent<WorkerView>().Render(workers[i], i, projectId, teamMorale);
        }
    }
}