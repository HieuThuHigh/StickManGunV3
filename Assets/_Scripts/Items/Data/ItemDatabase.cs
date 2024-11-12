using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "GameData/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> ItemsHat;
    public List<ItemData> ItemsFace;
    public List<ItemData> Itemsbody;
    public List<ItemData> Itemsgun;
    public List<ItemData> Itemsshirt;
    public List<ItemData> Itemsperk;
      public List<ItemData> Itemscolor;
}
    [System.Serializable]
public class ItemData
{
    public int ID;
    public string Name;
    public ItemController.ItemType Type;
    public string Description;
    public Sprite Icon;
    // Trường để lưu màu
    public Color ItemColor;  // Thêm trường màu cho item
    // Thêm trường tham chiếu đến GunData
    public GunData GunDataReference;  // Tham chiếu đến GunData cho các item thuộc loại súng
}
