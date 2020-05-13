using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFlagshipCompany : View
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CompanyView>().SetEntity(Flagship, true);
    }
}
