using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.0f;
    [SerializeField] Color interactHighlightColor = new Color(0.3f, 0.3f, 0.3f);
    private InteractUI interactUI;

    private List<Interactable> interactablesInRange = new();
    private Interactable selectedInteractable;

    private void Start()
    {
        interactUI = UIManager.Instance.InteractUI.GetComponent<InteractUI>();
    }

    private void Update()
    {
        //Save reference to current objects in range
        List<Interactable> initialInteractables = new();
        initialInteractables.AddRange(interactablesInRange);

        //Update objects in range
        UpdateInteractablesInRange();
        if ((!interactablesInRange.All(initialInteractables.Contains) //if the list has changed at all
            || interactablesInRange.Count != initialInteractables.Count)
            && interactablesInRange.Count > 0)
        {
            selectedInteractable = GetBestInteractable();
            HighlightSelectedObject();
        }

        ShowSelectedInteractablePrompt();
    }

    private void UpdateInteractablesInRange()
    {
        foreach (Interactable interactable in FindObjectsOfType<Interactable>())
        {
            float distance = Vector3.Distance(interactable.transform.position, transform.position);

            if (distance <= interactRange && interactable.CanInteract())
            {
                if (!interactablesInRange.Contains(interactable)) interactablesInRange.Add(interactable);
            }
            else
            {
                if (interactablesInRange.Contains(interactable)) interactablesInRange.Remove(interactable);
                if (selectedInteractable == interactable) selectedInteractable = null;
                SetObjectAndChildrenHighlight(interactable.transform, false);
            }
        }
        interactablesInRange.RemoveAll(x => x == null);
    }

    private Interactable GetBestInteractable()
    {
        float minDistance = Mathf.Infinity;
        Interactable toSelect = null;

        // If there's a manual priority - use that
        if (interactablesInRange.Any(o => o.priority != interactablesInRange[0].priority))
        {
            return interactablesInRange.OrderByDescending(o => o.priority).ToList()[0];
        }
        else // Otherwise take the closest one
        {
            foreach (Interactable interactable in interactablesInRange)
            {
                float distanceToInteractable = Vector3.Distance(interactable.transform.position, transform.position);
                if (distanceToInteractable < minDistance)
                {
                    minDistance = distanceToInteractable;
                    toSelect = interactable;
                }
            }
        }

        return toSelect;
    }

    private void HighlightSelectedObject()
    {
        foreach (Interactable interatable in interactablesInRange)
            SetObjectAndChildrenHighlight(interatable.transform, false);

        SetObjectAndChildrenHighlight(selectedInteractable.transform, true);

    }

    public void SetObjectAndChildrenHighlight(Transform objectToHighlight, bool shouldHighlight)
    {
        SetObjectHighlight(objectToHighlight, shouldHighlight);

        foreach (Transform objectChild in objectToHighlight)
        {
            SetObjectAndChildrenHighlight(objectChild, shouldHighlight);
        }
    }

    private void SetObjectHighlight(Transform objectToHighlight, bool shouldHighlight)
    {
        if (objectToHighlight.GetComponent<Renderer>() != null)
        {
            foreach (Material material in objectToHighlight.GetComponent<Renderer>().materials)
            {
                if (shouldHighlight)
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", interactHighlightColor);
                }
                else
                {
                    material.DisableKeyword("_EMISSION");
                }
            }
        }
    }

    private void ShowSelectedInteractablePrompt()
    {
        interactUI.promptUIText.text = selectedInteractable == null ? string.Empty : "[E] " + selectedInteractable.promptText;
        interactUI.nameUIText.text = selectedInteractable == null ? string.Empty : selectedInteractable.name;

        interactUI.cyclePromptObject.SetActive(interactablesInRange.Count > 1);
    }

    public void InteractWithSelected()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }

    public void CycleInteractable()
    {
        // If player presses Tab, cycle through interactables in range
        if (interactablesInRange.Count > 1)
        {
            int currentSelectedIndex = interactablesInRange.IndexOf(selectedInteractable);
            int newIndex = (currentSelectedIndex + 1) % interactablesInRange.Count;

            selectedInteractable = interactablesInRange[newIndex];
            HighlightSelectedObject();
        }
    }

    public void ForceRemoveInteractable(Interactable interactable)
    {
        if (interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Remove(interactable);
        }
    }
}
