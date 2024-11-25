using UnityEngine;
using UnityEngine.UI;
public class CharacterCustomization : MonoBehaviour
{
    // Các chỉ số lưu trữ lựa chọn của người chơi
    public int selectedHatIndex = -1;
    public int selectedFaceIndex = -1;
    public int selectedShirtIndex = -1;
    public int selectedColorIndex = -1;
    public int selectedGunIndex = -1;
    public int selectedPerkIndex = -1;

  


    [SerializeField] private Transform hatTransform;
    [SerializeField] private Transform faceTransform;
    [SerializeField] private Transform shirtTransform;
    [SerializeField] private Transform bodyTransform;  
    [SerializeField] private Transform gunTransform;  

    [SerializeField] private Transform colorTransform;   
    [SerializeField] private Transform perkTransform;   


    public void EquidItem(ItemData data)
    {
        switch (data.Type)
        {
            case ItemController.ItemType.None:
                break;
            case ItemController.ItemType.Hat:
                hatTransform.GetComponent<Image>().sprite = data.Icon;
                break;
            case ItemController.ItemType.Face:
                faceTransform.GetComponent<Image>().sprite = data.Icon;
                break; 
            case ItemController.ItemType.gun:
                gunTransform.GetComponent<Image>().sprite = data.Icon;
                break;
            case ItemController.ItemType.shirt:
                shirtTransform.GetComponent<Image>().sprite = data.Icon;
                break; 
            case ItemController.ItemType.body:
                bodyTransform.GetComponent<Image>().sprite = data.Icon;
                break;

            case ItemController.ItemType.color:
                colorTransform.GetComponent<Image>().sprite = data.Icon;
                break;

        }
    }

}


