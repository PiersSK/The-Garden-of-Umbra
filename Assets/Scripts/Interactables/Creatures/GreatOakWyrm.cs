
using UnityEngine;

public class GreatOakWyrm : ShadowInteractable
{
    public bool isUnderTree;
    public GameObject questionMarker;
    public bool isQuestionEnabled;

    protected override void Start()
    {
        base.Start();
        isQuestionEnabled = questionMarker.activeSelf;
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && !isUnderTree;
    }

}