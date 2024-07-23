using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [SerializeField] public Shadow shadow;
    public Vector2Int range = new Vector2Int(5, 4);
    public Sprite image;

    public void RemoveShadow()
    {
        shadow = null;
    }
}
