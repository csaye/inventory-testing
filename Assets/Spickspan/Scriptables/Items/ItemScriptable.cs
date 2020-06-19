using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "Scriptables/Item Scriptable")]
public class ItemScriptable : ScriptableObject
{
    public Sprite itemIcon;
    public int itemID;
    public int maxStack;
    public bool holdable;
}
