using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] ItemData _data;
    [SerializeField] Image _icon;

    Action<ItemData> _onClick;
    // Start is called before the first frame update


    public void Item_Click()
    {
        _onClick?.Invoke(_data);
        UnityEngine.PlayerPrefs.SetInt($"Selected{_data.Type}ID", _data.ID);
        UnityEngine.PlayerPrefs.Save(); // Lưu dữ liệu vào PlayerPrefs
    }

    public void SetData(ItemData data, Action<ItemData> onClick)
    {
        _data = data;
        _icon.sprite = data.Icon;
        _onClick = onClick;
    }

    public enum ItemType
    {
        None,
        Hat,
        Face,
        gun,
        shirt,
        body,
        perk,
        color,
      
    }
}
