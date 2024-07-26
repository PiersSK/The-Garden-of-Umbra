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
                childComponent.image.sprite = flask.getSprite();
            }
        }
    }

    public void UpdateGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            InventoryItem flask = child.GetComponent<InventoryItem>();
            Image flaskImage = flask.GetComponent<Image>();

            var inventoryFlask = InventoryManager.Instance.flasks[i];

            if(flaskImage is not null )
            {
                flaskImage.sprite = inventoryFlask.getSprite();
                flaskImage.color = inventoryFlask.flaskUnlocked ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
                //flaskImage.gameObject.SetActive(inventoryFlask.flaskUnlocked);
            }

            
        }
    }
}
