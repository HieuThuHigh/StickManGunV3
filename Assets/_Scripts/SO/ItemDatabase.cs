using System.Collections.Generic;
using GameTool.Assistants.DictionarySerialize;
using UnityEngine;

public class ItemDatabase : ScriptableObject
{
    public static ItemDatabase Instance => Resources.Load<ItemDatabase>("SO/ItemDatabase"); 
    
    public List<ItemData> ItemsHat;
    public List<ItemData> ItemsFace;
    public List<ItemData> Itemsshirt;
    public List<ItemData> Itemsbody;
    public List<ItemData> Itemsgun;
    public List<ItemData> Itemscolor;
    public List<ItemData> Itemsperk;
}

public class ItemData
{
    public int ID;
    public ItemController.ItemType Type;
    public Sprite Icon;
    public Color ItemColor;
}