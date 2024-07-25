using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public FormedDreamer questGiver;

    public string dreamDialogue;
    public string completionDialogue;

    public Aspects.HeadAspect headSolution;
    public Aspects.BodyAspect bodySolution;
    public Aspects.FeetAspect feetSolution;
}
