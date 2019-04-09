using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControlledCompanyButtonsInBusinessScreen : View
{
    public GameObject ButtonContainer;

    public GameObject ManageCompanyButton;

    private void OnEnable()
    {
        ButtonContainer.SetActive(SelectedCompany.isControlledByPlayer);

        ManageCompanyButton.SetActive(SelectedCompany.isControlledByPlayer);
    }
}
