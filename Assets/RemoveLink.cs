using UnityEngine;

public class RemoveLink : MonoBehaviour
{
    void Start()
    {
        var link = GetComponent<LinkToProjectView>();

        if (link)
            Destroy(link);

        //Destroy(this);
    }
}