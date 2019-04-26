using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetUserType : ButtonController
{
    public UserType UserType;

    public override void Execute()
    {
        MyProductEntity.ReplaceTargetUserType(UserType);

        ReNavigate();
    }
}
