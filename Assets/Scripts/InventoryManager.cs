using Aspects;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> flasks;

    public bool AddShadow(Shadow newShadow)
    {
        var shadowAdded = false;
        switch(newShadow.size) 
        {
            case ShadowSize.Small:
                shadowAdded = flasks[0].item.AddShadow(newShadow);
                if (shadowAdded)
                {
                    return shadowAdded;
                }
                else
                {
                    shadowAdded = flasks[1].item.AddShadow(newShadow);
                    return shadowAdded ? shadowAdded : flasks[2].item.AddShadow(newShadow);
                }
            case ShadowSize.Medium:
                shadowAdded = flasks[1].item.AddShadow(newShadow);
                return shadowAdded ? shadowAdded : flasks[2].item.AddShadow(newShadow);
            case ShadowSize.Large:
                return flasks[2].item.AddShadow(newShadow);
            default: 
                return false;
        }
    }
}
