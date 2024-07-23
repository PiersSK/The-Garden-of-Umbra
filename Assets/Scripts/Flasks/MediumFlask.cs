using Aspects;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Medium Flask")]
public class MediumFlask : Item
{
    public override bool AddShadow(Shadow newShadow)
    {
        if (shadow is null && newShadow.size >= ShadowSize.Medium)
        {
            shadow = newShadow;
            return true;
        }

        return false;
    }
}
