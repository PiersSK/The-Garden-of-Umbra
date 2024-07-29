using System.Collections.Generic;
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
    public InventoryItemHoverUI hoverUI;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q: hoverUI = " + hoverUI.gameObject.activeSelf + " left = " + hoverUI.left.activeSelf + " right = " + hoverUI.right.activeSelf);
        }

        List<Flask> unlockedFlasks = InventoryManager.Instance.UnlockedFlasks();
        int flaskIndex = unlockedFlasks.IndexOf(item);

        if (hoverUI.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Q) && hoverUI.left.activeSelf)
            {
                int swapIndex = flaskIndex == 0 ? unlockedFlasks.Count - 1 : flaskIndex - 1;

                Shadow shadowToSwap = unlockedFlasks[swapIndex].shadow;
                InventoryManager.Instance.flasks[swapIndex].shadow = item.shadow;

                if (shadowToSwap is not null)
                {
                    unlockedFlasks[flaskIndex].shadow = shadowToSwap;
                    SetHoverUI(true);
                    UpdateShadowAspectUI();
                }
                else
                {
                    unlockedFlasks[flaskIndex].shadow = null;
                    SetHoverUI(false);
                    aspectInfo.SetActive(false);

                }

            } else if(Input.GetKeyDown(KeyCode.F) && hoverUI.right.activeSelf)
            {
                int swapIndex = flaskIndex == unlockedFlasks.Count - 1 ? 0 : flaskIndex + 1;

                Shadow shadowToSwap = unlockedFlasks[swapIndex].shadow;
                InventoryManager.Instance.flasks[swapIndex].shadow = item.shadow;

                if (shadowToSwap is not null)
                {
                    unlockedFlasks[flaskIndex].shadow = shadowToSwap;
                    SetHoverUI(true);
                    UpdateShadowAspectUI();
                }
                else
                {
                    unlockedFlasks[flaskIndex].shadow = null;
                    SetHoverUI(false);
                    aspectInfo.SetActive(false);
                }

            }
        }


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
                SpawnManager.Instance.SpawnCreatureFromShadow(item.shadow);
                item.RemoveShadow();

                SetHoverUI(false);
                aspectInfo.SetActive(false);

                releaseInProgress = false;
                releaseTimerImage.fillAmount = 0f;
                releaseTimer = 0f;
            }
        }   
    }

    public void UpdateShadowAspectUI() 
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
            SetHoverUI(true);
            aspectInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
        //transform.localEulerAngles = idleAngle;
        if (item.shadow is not null)
        {
            SetHoverUI(false);
            aspectInfo.SetActive(false);
        }
    }

    private void SetHoverUI(bool isActive)
    {
        hoverUI.gameObject.SetActive(isActive);

        if (hoverUI.gameObject.activeSelf)
        {
            int flaskIndex = InventoryManager.Instance.flasks.IndexOf(item);
            hoverUI.left.SetActive(CanMoveLeft(flaskIndex));
            hoverUI.right.SetActive(CanMoveRight(flaskIndex));
        }

    }

    private bool CanMoveRight(int flaskIndex)
    {
        List<Flask> unlockedFlasks = InventoryManager.Instance.UnlockedFlasks();
        if (unlockedFlasks.Count == 1) return false;

        int swapIndex = flaskIndex == unlockedFlasks.Count - 1 ? 0 : flaskIndex + 1;

        bool canReceiveShadow = unlockedFlasks[swapIndex].shadow == null || unlockedFlasks[flaskIndex].CanAddShadow(unlockedFlasks[swapIndex].shadow);

        return canReceiveShadow && unlockedFlasks[swapIndex].CanAddShadow(unlockedFlasks[flaskIndex].shadow);



        //if (flaskIndex == 0 && unlockedFlasks[swapIndex].shadow is not null && unlockedFlasks[swapIndex].shadow.size != Aspects.ShadowSize.Small) return false;
        //else if (flaskIndex == 1 && unlockedFlasks[swapIndex].shadow is not null && unlockedFlasks[swapIndex].shadow.size == Aspects.ShadowSize.Large) return false;
        //else if (flaskIndex == 2 && unlockedFlasks[flaskIndex].shadow.size != Aspects.ShadowSize.Small) return false;

        //return true;
    }

    private bool CanMoveLeft(int flaskIndex)
    {
        List<Flask> unlockedFlasks = InventoryManager.Instance.UnlockedFlasks();
        if (unlockedFlasks.Count == 1) return false;

        int swapIndex = flaskIndex == 0 ? unlockedFlasks.Count - 1 : flaskIndex - 1;

        bool canReceiveShadow = unlockedFlasks[swapIndex].shadow == null || unlockedFlasks[flaskIndex].CanAddShadow(unlockedFlasks[swapIndex].shadow);

        return canReceiveShadow && unlockedFlasks[swapIndex].CanAddShadow(unlockedFlasks[flaskIndex].shadow);

        //List<Flask> unlockedFlasks = InventoryManager.Instance.UnlockedFlasks();
        //if (unlockedFlasks.Count == 1) return false;

        //int swapIndex = flaskIndex == 0 ? unlockedFlasks.Count - 1 : flaskIndex - 1;

        //if (unlockedFlasks[swapIndex] is null) return true;

        //if (flaskIndex == 0 && unlockedFlasks[swapIndex].shadow is not null && unlockedFlasks[swapIndex].shadow.size != Aspects.ShadowSize.Small) return false;
        //else if (flaskIndex == 1 && unlockedFlasks[flaskIndex].shadow.size != Aspects.ShadowSize.Small) return false;
        //else if (flaskIndex == 2 && unlockedFlasks[flaskIndex].shadow.size == Aspects.ShadowSize.Large) return false;

        //return true;
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
