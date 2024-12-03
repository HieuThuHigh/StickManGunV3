using UnityEngine;

public class SaveSkin : MonoBehaviour
{
   
    [SerializeField] private SpriteRenderer hatRenderer;
    [SerializeField] private SpriteRenderer faceRenderer;
    [SerializeField] private SpriteRenderer shirtRenderer;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer gunRenderer;
    [SerializeField] private SpriteRenderer colorRenderer;

    
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
            if (hatData != null) hatRenderer.sprite = hatData.Icon;
        }

        // Cập nhật mặt
        if (selectedFaceID != -1)
        {
            ItemData faceData = itemDatabase.ItemsFace.Find(item => item.ID == selectedFaceID);
            if (faceData != null) faceRenderer.sprite = faceData.Icon;
        }

        // Cập nhật áo
        if (selectedShirtID != -1)
        {
            ItemData shirtData = itemDatabase.Itemsshirt.Find(item => item.ID == selectedShirtID);
            if (shirtData != null) shirtRenderer.sprite = shirtData.Icon;
        }

        // Cập nhật body
        if (selectedBodyID != -1)
        {
            ItemData bodyData = itemDatabase.Itemsbody.Find(item => item.ID == selectedBodyID);
            if (bodyData != null) bodyRenderer.sprite = bodyData.Icon;
        }

        // Cập nhật súng
        if (selectedGunID != -1)
        {
            ItemData gunData = itemDatabase.Itemsgun.Find(item => item.ID == selectedGunID);
            if (gunData != null) gunRenderer.sprite = gunData.Icon;
        }

        // Cập nhật màu sắc (nếu cần đổi màu thay vì sprite)
        if (selectedColorID != -1)
        {
            ItemData colorData = itemDatabase.Itemscolor.Find(item => item.ID == selectedColorID);
            if (colorData != null) colorRenderer.color = colorData.ItemColor; // Đổi màu trực tiếp
        }
    }
}