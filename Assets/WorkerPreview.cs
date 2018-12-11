using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPreview : MonoBehaviour {
    public WorkerAvatarView WorkerAvatarView;

    public void Render(Human human, int index, int projectId, int teamMorale)
    {
        WorkerAvatarView.Render(human.FullName, human.Level, human.Specialisation);
    }
}
