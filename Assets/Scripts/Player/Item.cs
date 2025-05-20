using UnityEngine;

public enum ItemTypes {
        Key_Item,
        Weapon,
        Ammo,
        Consummable,
    };
    
[System.Serializable]    
public class Item
{
    public Sprite itemIcon;
    public ItemTypes itemType;
    public string itemName="";
    public string itemDescription="";
    public int itemAmount=1;
    public int currentAmmo=0;
    public GameObject gameObject;
}
