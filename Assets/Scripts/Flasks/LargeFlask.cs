using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Large Flask")]
public class LargeFlask : Flask
{
    public override bool AddShadow(Shadow newShadow)
    {
        if (shadow is null && flaskUnlocked)
        {
            shadow = newShadow;
            return true;
        }

        return false;
    }

    public override bool CanAddShadow(Shadow newShadow)
    {
        return flaskUnlocked;
    }
}
