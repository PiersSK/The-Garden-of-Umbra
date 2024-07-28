using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aspects;
using System.Drawing;

[CreateAssetMenu(menuName = "Scriptable Object/Shadow")]
public class Shadow : ScriptableObject
{
    public string shadowName;

    public HeadAspect headAspect;
    public BodyAspect bodyAspect;
    public FeetAspect feetAspect;
    public ShadowSize size;

    public Sprite headAspectSprite;
    public Sprite bodyAspectSprite;
    public Sprite feetAspectSprite;

    public Shadow (
        ShadowSize _size,
        HeadAspect _head,
        BodyAspect _body,
        FeetAspect _feet)
    {
        headAspect = _head;
        bodyAspect = _body;
        feetAspect = _feet;
        size = _size;
    }

    public Shadow()
    {
        headAspect = HeadAspect.None;
        bodyAspect = BodyAspect.None;
        feetAspect = FeetAspect.None;
        size = ShadowSize.Small;
    }
}
