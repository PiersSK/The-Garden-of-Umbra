using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{ 
    public Flask item;

    [Header("UI")]
    public Image image;
    public GameObject releaseButton;
    public Image releaseTimerImage;

    [Header("Release Logic")]
    public float timeToRelease = 2f;

    [HideInInspector]
    public Vector3 idleAngle;

    private float releaseTimer = 0f;
    private bool releaseInProgress = false;

    public void Start()
    {
        idleAngle = transform.localEulerAngles;
    }

    private void Update()
    {
        if(releaseInProgress)
        {
            releaseTimer += Time.deltaTime;
            releaseTimerImage.fillAmount = releaseTimer / timeToRelease;
            if(releaseTimer >= timeToRelease )
            {
                item.RemoveShadow();

                releaseButton.SetActive(false);
                releaseInProgress = false;
                releaseTimerImage.fillAmount = 0f;
                releaseTimer = 0f;
            }
        }   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        //transform.localEulerAngles = new Vector3(0, 0, 30f);

        if(item.shadow is not null)
            releaseButton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
        //transform.localEulerAngles = idleAngle;
        if (item.shadow is not null)
            releaseButton.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item.shadow is not null)
        {
            releaseInProgress = false;
            releaseTimerImage.fillAmount = 0f;
            releaseTimer = 0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.shadow is not null)
            releaseInProgress = true;
    }
}
