using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFlagshipCompany : View
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<CompanyView>().SetEntity(Flagship);
        GetComponent<FlagshipCompanyView>().SetEntity();
    }
}
