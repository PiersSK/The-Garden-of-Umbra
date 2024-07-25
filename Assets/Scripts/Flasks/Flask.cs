using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public abstract class Flask : ScriptableObject
{
    [SerializeField] public Shadow shadow;
    public Vector2Int range = new Vector2Int(5, 4);
    public Sprite filledSprite;
    public Sprite emptySprite;
    public bool flaskUnlocked;

    public abstract bool AddShadow(Shadow newShadow);

    public void RemoveShadow()
    {
        shadow = null;
    }

    public Sprite getSprite()
    {
        if (shadow is not null)
        {
            return filledSprite;
        }
        else
        {
            return emptySprite;
        }
    }
}
