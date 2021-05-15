using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyModalManager : View
{
    List<MyModalWindow> _windows = new List<MyModalWindow>();
    List<MyModalWindow> MyModalWindows
    {
        get
        {
            if (_windows.Count == 0)
            {
                _windows = FindObjectsOfTypeAll<MyModalWindow>();
            }

            return _windows;
        }
    }

    bool modalsClosed = true;
    bool isExitModal = false;

    List<string> PreviousModals = new List<string>();

    private MyModalWindow GetModal(string ModalTag)
    {
        var m = MyModalWindows.FirstOrDefault(w => w.ModalTag == ModalTag);
        //var m = FindObjectsOfTypeAll<MyModalWindow>().FirstOrDefault(w => w.ModalTag == ModalTag);

        if (m == null)
        {
            Debug.LogError("Modal " + ModalTag + " not found!");

            return null;
        }

        return m;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isExitModal)
            {
                CloseAllModals();
                isExitModal = false;
            }
            else
            {
                if (modalsClosed)
                {
                    OpenMyModal("Exit");
                    isExitModal = true;
                }
                else
                {
                    CloseAllModals();
                }
            }
        }
    }

    void CloseAllModals()
    {
        HideAll(MyModalWindows.Select(m1 => m1.gameObject).ToArray());
        modalsClosed = true;
    }

    public void OpenMyModal(string ModalTag, bool closeOthers = true)
    {
        Debug.Log("OpenMyModal");
        var m = GetModal(ModalTag);

        if (m != null)
        {
            ScheduleUtils.PauseGame(Q);


            // close other modals
            CloseAllModals();

            Show(m);
            modalsClosed = false;
        }
    }

    public void CloseMyModal(string ModalTag)
    {
        Debug.Log("CloseMyModal");
        var m = GetModal(ModalTag);

        if (m != null)
        {
            ScheduleUtils.PauseGame(Q);

            Hide(m);
        }

        // if close others was false, this may mean opening previous modal
    }
}
