<<<<<<<< HEAD:Assets/Scripts/UI/InventoryItem.cs
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{ 
    public Flask item;

    [Header("UI")]
    public Image image;
    public Image creatureOutline;
    public GameObject releaseButton;
    public GameObject aspectInfo;
    public Image releaseTimerImage;

    public Image headAspect;
    public Image bodyAspect;
    public Image feetAspect;
    public TextMeshProUGUI shadowName;

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
        if (item.shadow is not null)
        {
            creatureOutline.sprite = item.shadow.creatureOutline;
            creatureOutline.color = new Color(0,0,0,0.4f);
        }
        else
        {
            creatureOutline.sprite = null;
            creatureOutline.color = new Color(0, 0, 0, 0);
        }

        if (releaseInProgress)
        {
            releaseTimer += Time.deltaTime;
            releaseTimerImage.fillAmount = releaseTimer / timeToRelease;
            if(releaseTimer >= timeToRelease )
            {
                item.RemoveShadow();

                releaseButton.SetActive(false);
                aspectInfo.SetActive(false);

                releaseInProgress = false;
                releaseTimerImage.fillAmount = 0f;
                releaseTimer = 0f;
            }
        }   
    }

    private void UpdateShadowAspectUI() 
    {

        headAspect.sprite = item.shadow.headAspectSprite;
        headAspect.color = item.shadow.headAspect == Aspects.HeadAspect.None ? new Color(0,0,0,0.2f) : Color.white;

        bodyAspect.sprite = item.shadow.bodyAspectSprite;
        bodyAspect.color = item.shadow.bodyAspect == Aspects.BodyAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        feetAspect.sprite = item.shadow.feetAspectSprite;
        feetAspect.color = item.shadow.feetAspect == Aspects.FeetAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.localEulerAngles = new Vector3(0, 0, 30f);

        if (item.shadow is not null)
        {
            UpdateShadowAspectUI();

            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            releaseButton.SetActive(true);
            aspectInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
        //transform.localEulerAngles = idleAngle;
        if (item.shadow is not null)
        {
            releaseButton.SetActive(false);
            aspectInfo.SetActive(false);
        }
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
========
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{ 
    public Flask item;

    [Header("UI")]
    public Image image;
    public GameObject releaseButton;
    public GameObject aspectInfo;
    public Image releaseTimerImage;

    public Image headAspect;
    public Image bodyAspect;
    public Image feetAspect;
    public TextMeshProUGUI shadowName;

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
                SpawnManager.Instance.SpawnCreatureFromShadow(item.shadow);
                item.RemoveShadow();

                releaseButton.SetActive(false);
                aspectInfo.SetActive(false);

                releaseInProgress = false;
                releaseTimerImage.fillAmount = 0f;
                releaseTimer = 0f;
            }
        }   
    }

    private void UpdateShadowAspectUI() {

        headAspect.sprite = item.shadow.headAspectSprite;
        headAspect.color = item.shadow.headAspect == Aspects.HeadAspect.None ? new Color(0,0,0,0.2f) : Color.white;

        bodyAspect.sprite = item.shadow.bodyAspectSprite;
        bodyAspect.color = item.shadow.bodyAspect == Aspects.BodyAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        feetAspect.sprite = item.shadow.feetAspectSprite;
        feetAspect.color = item.shadow.feetAspect == Aspects.FeetAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        shadowName.text = item.shadow.shadowName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.localEulerAngles = new Vector3(0, 0, 30f);

        if (item.shadow is not null)
        {
            UpdateShadowAspectUI();

            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            releaseButton.SetActive(true);
            aspectInfo.SetActive(true);
        }
    }

        public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
        //transform.localEulerAngles = idleAngle;
        if (item.shadow is not null)
        {
            releaseButton.SetActive(false);
            aspectInfo.SetActive(false);
        }
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
>>>>>>>> master:Assets/Scripts/Items/InventoryItem.cs
