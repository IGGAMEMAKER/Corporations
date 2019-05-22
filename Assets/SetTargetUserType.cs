public class SetTargetUserType : ButtonController
{
    public UserType UserType;

    public void SetUserType(UserType userType)
    {
        UserType = userType;
    }

    public override void Execute()
    {
        MyProductEntity.ReplaceTargetUserType(UserType);
    }
}
