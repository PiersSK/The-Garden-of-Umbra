using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField] public Shadow shadow;
    public Vector2Int range = new Vector2Int(5, 4);
    public Sprite image;

    public abstract bool AddShadow(Shadow newShadow);

    public void RemoveShadow()
    {
        shadow = null;
    }
}
