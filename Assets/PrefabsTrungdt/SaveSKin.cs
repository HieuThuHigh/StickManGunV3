using UnityEngine;
using UnityEngine.UI;

public class SaveSkin : MonoBehaviour
{
    [SerializeField] private Image hatImage;
    [SerializeField] private Image faceImage;
    [SerializeField] private Image shirtImage;
    [SerializeField] private Image bodyImage;
    [SerializeField] private Image gunImage;
    [SerializeField] private Image colorImage;

    [SerializeField] private ItemDatabase itemDatabase;

    private void Start()
    {
        // Load dữ liệu từ PlayerPrefs
        ApplyCustomization();
    }

    private void ApplyCustomization()
    {
        // Lấy thông tin tùy chỉnh từ PlayerPrefs
        int selectedHatID = PlayerPrefs.GetInt("SelectedHatID", -1);
        int selectedFaceID = PlayerPrefs.GetInt("SelectedFaceID", -1);
        int selectedShirtID = PlayerPrefs.GetInt("SelectedshirtID", -1);
        int selectedBodyID = PlayerPrefs.GetInt("SelectedBodyID", -1);
        int selectedGunID = PlayerPrefs.GetInt("SelectedgunID", -1);
        int selectedColorID = PlayerPrefs.GetInt("SelectedColorID", -1);

        // Cập nhật mũ
        if (selectedHatID != -1)
        {
            ItemData hatData = itemDatabase.ItemsHat.Find(item => item.ID == selectedHatID);
            if (hatData != null) hatImage.sprite = hatData.Icon;
        }

        // Cập nhật mặt
        if (selectedFaceID != -1)
        {
            ItemData faceData = itemDatabase.ItemsFace.Find(item => item.ID == selectedFaceID);
            if (faceData != null) faceImage.sprite = faceData.Icon;
        }

        // Cập nhật áo
        if (selectedShirtID != -1)
        {
            ItemData shirtData = itemDatabase.Itemsshirt.Find(item => item.ID == selectedShirtID);
            if (shirtData != null) shirtImage.sprite = shirtData.Icon;
        }

        // Cập nhật body
        if (selectedBodyID != -1)
        {
            ItemData bodyData = itemDatabase.Itemsbody.Find(item => item.ID == selectedBodyID);
            if (bodyData != null) bodyImage.sprite = bodyData.Icon;
        }

        // Cập nhật súng
        if (selectedGunID != -1)
        {
            ItemData gunData = itemDatabase.Itemsgun.Find(item => item.ID == selectedGunID);
            if (gunData != null) gunImage.sprite = gunData.Icon;
        }

        // Cập nhật màu sắc
        if (selectedColorID != -1)
        {
            ItemData colorData = itemDatabase.Itemscolor.Find(item => item.ID == selectedColorID);
            if (colorData != null) colorImage.sprite = colorData.Icon;
        }
    }
}