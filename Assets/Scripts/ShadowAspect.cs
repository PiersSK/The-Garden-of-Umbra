using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAspect
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

    public ShadowAspect(Aspect aspect, AspectSlot slot)
    {
        this.aspect = aspect;
        this.slot = slot;
    }
}
