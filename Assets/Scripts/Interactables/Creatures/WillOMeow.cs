using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOMeow : ShadowInteractable
{
    private GameObject foodBowl;

    protected override void Start()
    {
        base.Start();
        foodBowl = GameObject.Find("FoodBowl");
    }

    public override bool CanInteract()
    {
        var foodInBowl = foodBowl.GetComponent<FoodBowl>().foodInBowl;

        return foodInBowl && !GetComponent<Animator>().GetBool("IsWalking");
    }
}
