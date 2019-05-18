using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetUserType : ButtonController
{
    public UserType UserType;

    public void SetUserType(UserType userType)
    {
        UserType = userType;

        ToggleIsChosenComponent(MyProductEntity.targetUserType.UserType == UserType);
    }

    public override void Execute()
    {
        MyProductEntity.ReplaceTargetUserType(UserType);

        ReNavigate();
    }
}
