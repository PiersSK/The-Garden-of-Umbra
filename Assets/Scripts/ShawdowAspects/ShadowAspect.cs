using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShadowAspect: ScriptableObject
{
    public enum Aspect
    {
        Ears,
        Horns,
        Snoot,
        Spines,
        Tail,
        Wings,
        Beans,
        Claws,
        Hooves
    }

    public enum AspectSlot
    {
        Head,
        Body,
        Feet
    }

    public Aspect aspect;
    public AspectSlot slot;
}
