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
                flask.flaskUnlocked = false;
                flask.shadow = null;
                childComponent.item = flask;
                childComponent.image.sprite = flask.emptySprite;
            }
        }

        // FOR TESTING
        InventoryManager.Instance.flasks[0].flaskUnlocked = true;
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
                flaskImage.sprite = inventoryFlask.emptySprite; 
                flaskImage.color = inventoryFlask.shadow is null ? new Color(1f, 1f, 1f, 0.5f) : new Color(0.36f, 0.35f, 0.48f, 0.7f);
                flaskImage.gameObject.SetActive(inventoryFlask.flaskUnlocked);
            }


        }
    }
}
