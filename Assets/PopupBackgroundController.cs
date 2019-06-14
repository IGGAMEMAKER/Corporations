using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBackgroundController : MonoBehaviour
{
    void OnEnable()
    {
        gameObject.AddComponent<PopupBackgroundAnimation>();
    }
}
