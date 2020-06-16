using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "Scriptables/Item Scriptable")]
public class ItemScriptable : ScriptableObject
{
    public string itemID;
    public float maxStack;
}
