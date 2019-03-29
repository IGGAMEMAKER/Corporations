using UnityEngine;

public class BusinessScreenView : View
{
    // Start is called before the first frame update
    void Awake()
    {
        if (myProductEntity == null)
        {
            Debug.Log("no companies controlled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
