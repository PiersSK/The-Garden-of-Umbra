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

    public void ClearAllFlasks()
    {
        foreach (Flask flask in flasks) flask.shadow = null;
    }

    public bool AddShadow(Shadow newShadow, Sprite creatureOutline)
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

    public bool IsThereSpaceForAShadowSir(Shadow creatureShadow)
    {
        switch (creatureShadow.size)
        {
            case ShadowSize.Small:
                foreach (Flask flask in flasks)
                {
                    if (flask.shadow is null)
                    {
                        return true;
                    }
                }
                return false;
            case ShadowSize.Medium:
                return flasks[1].shadow is null || flasks[2].shadow is null;
            case ShadowSize.Large:
                return flasks[2].shadow is null;
            default: return false;
        }
    }
}
