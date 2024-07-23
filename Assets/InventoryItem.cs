using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;

    [Header("UI")]
    public Image image;
    public Sprite emptySprite;
    public Sprite filledSprite;

    [HideInInspector]
    public Transform parentAfterDrag;
    public Vector3 idleAngle;

    public void Start()
    {
        idleAngle = transform.localEulerAngles;
        image.raycastTarget = true;
    }

    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        transform.localEulerAngles = new Vector3(0, 0, 30f);
        if (item.shadow is null)
        {
            Debug.Log($"{nameof(item)} is empty");
        }
        else
        {
            Debug.Log($"{item.shadow.headAspect}, {item.shadow.bodyAspect}, {item.shadow.feetAspect}");
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = Vector3.one;
        transform.localEulerAngles = idleAngle;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
