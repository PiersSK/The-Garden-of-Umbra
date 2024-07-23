using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Large Flask")]
public class LargeFlask : ScriptableObject
{
    public Item item;
    public bool addShadow(Shadow newShadow)
    {
        if (item.shadow is null)
        {
            item.shadow = newShadow;
            return true;
        }

        return false;
    }
}
