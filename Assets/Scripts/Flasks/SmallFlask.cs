using Aspects;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Small Flask")]
public class SmallFlask : Item
{
    public override bool AddShadow(Shadow newShadow)
    {
        if (shadow is null && newShadow.size == ShadowSize.Small)
        {
            shadow = newShadow;
            return true;
        }

        return false;
    }
}
