using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInterruptName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Hint>().SetHint("You " + gameObject.name);
    }
}
