using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkerType
{
    Programmer,
    Manager,
    Marketer
}

public class Hire : ButtonController
{
    public WorkerType worker;

    public override void Execute()
    {
        switch (worker)
        {
            case WorkerType.Manager:
                break;
            case WorkerType.Marketer:
                break;
            case WorkerType.Programmer:
                break;
        }
    }
}
