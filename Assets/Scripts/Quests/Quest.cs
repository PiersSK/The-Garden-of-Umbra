using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public FormedDreamer questGiver;

    public string dreamDialogue;
    public string completionDialogue;
    // TODO: Add full dialogue once solved

    // TEMP: Replace with actual inventory objects
    public ShadowAspect.Aspect headSolution;
    public ShadowAspect.Aspect bodySolution;
    public ShadowAspect.Aspect feetSolution;
}
