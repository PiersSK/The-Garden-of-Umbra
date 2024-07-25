using Aspects;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public List<Flask> flasks;

    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddShadow(Shadow newShadow)
    {
        var shadowAdded = false;
        switch(newShadow.size) 
        {
            case ShadowSize.Small:
                shadowAdded = flasks[0].AddShadow(newShadow);
                if (shadowAdded)
                {
                    return shadowAdded;
                }
                else
                {
                    shadowAdded = flasks[1].AddShadow(newShadow);
                    return shadowAdded ? shadowAdded : flasks[2].AddShadow(newShadow);
                }
            case ShadowSize.Medium:
                shadowAdded = flasks[1].AddShadow(newShadow);
                return shadowAdded ? shadowAdded : flasks[2].AddShadow(newShadow);
            case ShadowSize.Large:
                return flasks[2].AddShadow(newShadow);
            default: 
                return false;
        }
    }
}
