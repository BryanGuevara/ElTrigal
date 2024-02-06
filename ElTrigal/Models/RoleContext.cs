using ElTrigal.Models;

public static class RoleContext
{
    private static Rol _currentRole = new Rol();

    public static Rol CurrentRole
    {
        get { return _currentRole; }
        set { _currentRole = value; }
    }
}