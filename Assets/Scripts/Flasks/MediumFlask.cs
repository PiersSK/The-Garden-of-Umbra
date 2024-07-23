using Aspects;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Medium Flask")]
public class MediumFlask : ScriptableObject
{
    public Item item;
    public bool addShadow(Shadow newShadow)
    {
        if (item.shadow is null && newShadow.size >= ShadowSize.Medium)
        {
            item.shadow = newShadow;
            return true;
        }

        return false;
    }
}
