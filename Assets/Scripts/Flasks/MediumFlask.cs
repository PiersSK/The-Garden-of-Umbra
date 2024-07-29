using Aspects;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Medium Flask")]
public class MediumFlask : Flask
{
    public override bool AddShadow(Shadow newShadow)
    {
        if (shadow is null && flaskUnlocked && newShadow.size <= ShadowSize.Medium)
        {
            shadow = newShadow;
            return true;
        }

        return false;
    }
}
