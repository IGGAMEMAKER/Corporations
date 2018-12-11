using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeListRenderer : MonoBehaviour
{
    public GameObject Prefab;

    public void Render(List<Human> workers, int projectId)
    {
        for (int i = 0; i < workers.Count; i++)
        {
            GameObject v = Instantiate(Prefab, transform);

            v.GetComponent<WorkerPreview>().Render(workers[i], i, projectId, 100);
        }
    }
}