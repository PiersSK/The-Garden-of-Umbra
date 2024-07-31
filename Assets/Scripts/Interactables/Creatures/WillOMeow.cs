using UnityEngine;

public class WillOMeow : ShadowInteractable
{
    private FoodBowl foodBowl;

    protected override void Start()
    {
        base.Start();
        foodBowl = GameObject.Find("FoodBowl").GetComponent<FoodBowl>();
    }

    public override bool CanInteract()
    {
        return foodBowl.foodInBowl && !GetComponent<Animator>().GetBool("IsWalking") && base.CanInteract();
    }

    public override void Interact()
    {
        foodBowl.FoodEaten();
        base.Interact();
    }
}
