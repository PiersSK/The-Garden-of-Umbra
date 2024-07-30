public class FoodBowl : Interactable
{
    public bool canInteract = true;
    public bool foodInBowl = false;

    public override void Interact()
    {
        canInteract = false;
        foodInBowl = true;
        transform.Find("EmptyFoodBowl").gameObject.SetActive(false);
        transform.Find("FilledFoodBowl").gameObject.SetActive(true);
    }

    public override bool CanInteract()
    {
        return canInteract;
    }
}
