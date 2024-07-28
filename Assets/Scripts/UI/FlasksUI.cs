using UnityEngine;
using UnityEngine.UI;

public class FlasksUI : MonoBehaviour
{
    public InventoryItem inventoryItem;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        createGrid();
    }

    public void Update()
    {
        UpdateGrid();
    }

    public void createGrid()
    {
        foreach(Flask flask in InventoryManager.Instance.flasks)
        {
            InventoryItem newChildInventoryItem = Instantiate(inventoryItem, transform);

            InventoryItem childComponent = newChildInventoryItem.GetComponent<InventoryItem>();
            if(childComponent is not null ) 
            {
                flask.shadow = null;
                childComponent.item = flask;
                childComponent.image.sprite = flask.emptySprite;
            }
        }
    }

    public void UpdateGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            InventoryItem invenItem = child.GetComponent<InventoryItem>();
            Image flaskImage = invenItem.GetComponent<Image>();
            Image outline = invenItem.creatureOutline;

            var inventoryFlask = InventoryManager.Instance.flasks[i];

            if(flaskImage is not null )
            {
                flaskImage.sprite = inventoryFlask.emptySprite; 
                flaskImage.color = inventoryFlask.shadow is null ? new Color(1f, 1f, 1f, 0.5f) : new Color(0.36f, 0.35f, 0.48f, 0.7f);
                outline.color = inventoryFlask.shadow is null ? new Color(1f, 1f, 1f, 0f) : new Color(0.2f, 0.2f, 0.2f, 0.7f);
                outline.sprite = inventoryFlask.shadow is null ? null : inventoryFlask.shadow.creatureOutline;
                flaskImage.gameObject.SetActive(inventoryFlask.flaskUnlocked);
            }


        }
    }
}
