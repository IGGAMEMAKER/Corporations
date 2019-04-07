using UnityEngine;

public class RemoveLink : MonoBehaviour
{
    void Start()
    {
        var link = GetComponent<LinkToCompanyPreview>();

        if (link)
            Destroy(link);

        //Destroy(this);
    }
}