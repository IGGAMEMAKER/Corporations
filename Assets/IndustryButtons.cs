using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustryButtons : MonoBehaviour
{
    public GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;
    public SetInitialIndustry useful;
    public SetInitialIndustry ecommerce;

    // Start is called before the first frame update
    void Start()
    {
        useful.SetIndustry(IndustryType.WorkAndLife, TypeCorporationNameContainer, ChooseInitialNicheContainer);
        ecommerce.SetIndustry(IndustryType.Ecommerce, TypeCorporationNameContainer, ChooseInitialNicheContainer);
    }
}
