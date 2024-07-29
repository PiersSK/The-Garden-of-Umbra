using UnityEngine;

public class Aurafox : ShadowInteractable
{

    public GameObject surpriseMarker;

    private void Start()
    {
        surpriseMarker.SetActive(false);
    }
    public override bool CanInteract()
    {
        return base.CanInteract() && !GetComponent<Animator>().GetBool("IsAwake");
    }
}
