using Assets.Classes;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeListRenderer : MonoBehaviour
{
    public GameObject Prefab;

    public void Render(List<Human> workers, int projectId)
    {
        for (int i = 0; i < workers.Count; i++)
        {
            GameObject v = Instantiate(Prefab, transform);

            v.GetComponent<EmployeeView>().Render(workers[i], i, projectId);
        }
    }
}
