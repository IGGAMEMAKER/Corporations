using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopManagersPreview : MonoBehaviour
{
    public TextMeshProUGUI CompanyName;
    int companyId;
    
    public void SetEntity(GameEntity company)
    {
        CompanyName.text = $"Managers of {company.company.Name}";
    }
}
