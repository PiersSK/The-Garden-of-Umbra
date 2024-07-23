using Aspects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Object/Small Flask")]
public class SmallFlask : Item
{
    public bool addShadow(Shadow newShadow)
    {
        if (shadow is null && newShadow.size == ShadowSize.Small)
        {
            shadow = newShadow;
            return true;
        }

        return false;
    }
}
