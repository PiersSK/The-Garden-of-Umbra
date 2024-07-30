using Aspects;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Small Flask")]
public class SmallFlask : Flask
{
    public override bool AddShadow(Shadow newShadow)
    {
        if (shadow is null && flaskUnlocked && newShadow.size == ShadowSize.Small)
        {
            shadow = newShadow;
            return true;
        }

        return false;
    }

    public override bool CanAddShadow(Shadow newShadow)
    {
        return flaskUnlocked && newShadow.size == ShadowSize.Small;
    }
}
