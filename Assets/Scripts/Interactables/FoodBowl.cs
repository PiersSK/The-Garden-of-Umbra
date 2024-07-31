public class FoodBowl : Interactable
{
    public bool foodInBowl = false;

    public void Start()
    {
        transform.Find("EmptyFoodBowl").gameObject.SetActive(true);
        transform.Find("FilledFoodBowl").gameObject.SetActive(false);
    }

    public override void Interact()
    {
        foodInBowl = true;
        transform.Find("EmptyFoodBowl").gameObject.SetActive(false);
        transform.Find("FilledFoodBowl").gameObject.SetActive(true);
    }

    public void FoodEaten()
    {
        foodInBowl = false;
        transform.Find("FilledFoodBowl").gameObject.SetActive(false);
        transform.Find("EmptyFoodBowl").gameObject.SetActive(true);
    }

    public override bool CanInteract()
    {
        return !foodInBowl;
    }
}
