using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    SpeedBoost,
    DoubleJump,
    Invincibility
}

public enum EquipableType
{
    SpeedBoost,
    JumpBoost,
    UpHealth
}

public class Item
{
    public float durationTime;
    public float upPercent;
    public float upValue;
}

[System.Serializable]
public class ItemDataConsumable : Item
{
    public ConsumableType type;
}

[System.Serializable]
public class ItemDataEquipable : Item
{
    public EquipableType type;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable consumables;

    [Header("Equip")]
    public ItemDataEquipable equipable;
    public GameObject equipPrefab;
}