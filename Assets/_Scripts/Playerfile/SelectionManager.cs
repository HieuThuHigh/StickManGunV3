using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    

    // Các GameObject thay cho SpriteRenderer hoặc Renderer
    [SerializeField] private GameObject hatParentObject;  // Parent object để thay đổi mũ
    [SerializeField] private GameObject faceParentObject; // Parent object để thay đổi mặt
    [SerializeField] private GameObject shirtParentObject; // Parent object để thay đổi áo
    [SerializeField] private GameObject bodyObject; // Object cho body (bodyRenderer đã thay đổi)
    [SerializeField] private GameObject gunObject; // Object cho body (bodyRenderer đã thay đổi)
    [SerializeField] private GameObject perkObject; // Object cho body (bodyRenderer đã thay đổi)
    [SerializeField] private GameObject colorObject; // Object cho body (bodyRenderer đã thay đổi)
  
    private SpriteRenderer hatRenderer;
    private SpriteRenderer faceRenderer;
    private SpriteRenderer shirtRenderer;
    private Renderer bodyRenderer;
    private Renderer gunRenderer;
    private Renderer perkRenderer;
    private Renderer colorRenderer;

    private GameObject currentHat;
    private GameObject currentFace;
    private GameObject currentShirt;
    private GameObject currentGunObject;
    private GameObject currentPerk;
    [SerializeField]
    private CharacterCustomization characterCustomization;
  //  public GunData[] gunsData;

    public GameObject customizationPanel;


    [Header("Item")]
    [SerializeField] ItemDatabase _itemDatabase;
    [SerializeField] List<ItemController> itemsHat;
    [SerializeField] List<ItemController> itemsFace;
    [SerializeField] List<ItemController> itemsbody;
    [SerializeField] List<ItemController> itemsshirt;
    [SerializeField] List<ItemController> itemsgun;
    [SerializeField] List<ItemController> itemsperk;
    [SerializeField] List<ItemController> itemscolor;


    private void Start()
    {
        
        // Gán SpriteRenderer cho các đối tượng cha
        hatRenderer = hatParentObject.GetComponent<SpriteRenderer>();
        faceRenderer = faceParentObject.GetComponent<SpriteRenderer>();
        shirtRenderer = shirtParentObject.GetComponent<SpriteRenderer>();
        gunRenderer= gunObject.GetComponent<SpriteRenderer>();
        perkRenderer=perkObject.GetComponent<SpriteRenderer>();
        bodyRenderer = bodyObject.GetComponent<SpriteRenderer>();
        colorRenderer = colorObject.GetComponent<SpriteRenderer>();
        Debug.Log("da chay vao srtart");
      //  SetupButtons();
        CreateItem();
        LoadCharacterCustomization();  // Tải lại lựa chọn từ PlayerPrefs
       
    }

    void CreateItem()
    {
        for (int i = 0; i< _itemDatabase.ItemsHat.Count; i++) {
            itemsHat[i].SetData(_itemDatabase.ItemsHat[i], OnItem_Click);
        }
        for (int i = 0; i< _itemDatabase.ItemsFace.Count; i++)
        {
            itemsFace[i].SetData(_itemDatabase.ItemsFace[i], OnItem_Click);
        }
        for (int i = 0; i< _itemDatabase.Itemsbody.Count; i++)
        {
            itemsbody[i].SetData(_itemDatabase.Itemsbody[i], OnItem_Click);
        }
        for (int i = 0; i< _itemDatabase.Itemsshirt.Count; i++)
        {
            itemsshirt[i].SetData(_itemDatabase.Itemsshirt[i], OnItem_Click);
        }

        for (int i = 0; i< _itemDatabase.Itemsgun.Count; i++)
        {
            itemsgun[i].SetData(_itemDatabase.Itemsgun[i], OnItem_Click);
        }
        for (int i = 0; i< _itemDatabase.Itemsperk.Count; i++)
        {
            itemsperk[i].SetData(_itemDatabase.Itemsperk[i], OnItem_Click);
        }
        for (int i = 0; i< _itemDatabase.Itemscolor.Count; i++)
        {
            itemscolor[i].SetData(_itemDatabase.Itemscolor[i], OnItem_Click);
        }
    }

    public void OnItem_Click(ItemData data)
    {
        // callback
        characterCustomization.EquidItem(data);

        // Lưu lại lựa chọn của người chơi
      
    }

    public void ConfirmCustomization()
    {
        SaveCharacterCustomization(); // Gọi lưu khi người dùng hoàn tất thay đổi
    }
    private void SaveCharacterCustomization()
    {
        // Lưu ID của các item đã chọn vào PlayerPrefs
        if (characterCustomization != null)
        {
            PlayerPrefs.SetInt("SelectedHat", characterCustomization.selectedHatIndex);
            PlayerPrefs.SetInt("SelectedFace", characterCustomization.selectedFaceIndex);
            PlayerPrefs.SetInt("Selectedshirt", characterCustomization.selectedShirtIndex);
            PlayerPrefs.SetInt("SelectedColor", characterCustomization.selectedColorIndex);
            PlayerPrefs.SetInt("Selectedgun", characterCustomization.selectedGunIndex);
            PlayerPrefs.SetInt("SelectedPerk", characterCustomization.selectedPerkIndex);

            // Lưu ID của các item trong ItemDatabase nếu cần
            //if (characterCustomization.selectedHatIndex >= 0 && characterCustomization.selectedHatIndex < _itemDatabase.ItemsHat.Count)
            //{
            //    PlayerPrefs.SetInt("SelectedHatID", _itemDatabase.ItemsHat[characterCustomization.selectedHatIndex].ID);
            //}

            //if (characterCustomization.selectedFaceIndex >= 0 && characterCustomization.selectedFaceIndex < _itemDatabase.ItemsFace.Count)
            //{
            //    PlayerPrefs.SetInt("SelectedFaceID", _itemDatabase.ItemsFace[characterCustomization.selectedFaceIndex].ID);
            //}

            //if (characterCustomization.selectedShirtIndex >= 0 && characterCustomization.selectedShirtIndex < _itemDatabase.Itemsshirt.Count)
            //{
            //    PlayerPrefs.SetInt("SelectedshirtID", _itemDatabase.Itemsshirt[characterCustomization.selectedShirtIndex].ID);
            //}

            //if (characterCustomization.selectedColorIndex >= 0 && characterCustomization.selectedColorIndex < _itemDatabase.Itemscolor.Count)
            //{
            //    PlayerPrefs.SetInt("SelectedColorID", _itemDatabase.Itemscolor[characterCustomization.selectedColorIndex].ID);
            //}

            //if (characterCustomization.selectedGunIndex >= 0 && characterCustomization.selectedGunIndex < _itemDatabase.Itemsgun.Count)
            //{
            //    PlayerPrefs.SetInt("SelectedgunID", _itemDatabase.Itemsgun[characterCustomization.selectedGunIndex].ID);
            //}

            //if (characterCustomization.selectedPerkIndex >= 0 && characterCustomization.selectedPerkIndex < _itemDatabase.Itemsperk.Count)
            //{
            //    PlayerPrefs.SetInt("SelectedPerkID", _itemDatabase.Itemsperk[characterCustomization.selectedPerkIndex].ID);
            //}

            PlayerPrefs.Save();  // Lưu PlayerPrefs
        }
    }
    private void SaveSelectedItemID(string key, int selectedIndex, List<ItemData> items)
    {
        if (selectedIndex >= 0 && selectedIndex < items.Count)
        {
            PlayerPrefs.SetInt(key, items[selectedIndex].ID);
        }
    }
    private void LoadCharacterCustomization()
{
    if (PlayerPrefs.HasKey("SelectedHatID"))
    {
        int selectedHatID = PlayerPrefs.GetInt("SelectedHatID");
        int selectedHatIndex = _itemDatabase.ItemsHat.FindIndex(item => item.ID == selectedHatID);
        if (selectedHatIndex >= 0)
        {
            characterCustomization.selectedHatIndex = selectedHatIndex;
            // Gọi phương thức EquidItem để hiển thị lại mũ
            characterCustomization.EquidItem(_itemDatabase.ItemsHat[selectedHatIndex]);
        }
    }

    if (PlayerPrefs.HasKey("SelectedFaceID"))
    {
        int selectedFaceID = PlayerPrefs.GetInt("SelectedFaceID");
        int selectedFaceIndex = _itemDatabase.ItemsFace.FindIndex(item => item.ID == selectedFaceID);
        if (selectedFaceIndex >= 0)
        {
            characterCustomization.selectedFaceIndex = selectedFaceIndex;
            // Gọi phương thức EquidItem để hiển thị lại mặt
            characterCustomization.EquidItem(_itemDatabase.ItemsFace[selectedFaceIndex]);
        }
    }

    if (PlayerPrefs.HasKey("SelectedshirtID"))
    {
        int selectedShirtID = PlayerPrefs.GetInt("SelectedshirtID");
        int selectedShirtIndex = _itemDatabase.Itemsshirt.FindIndex(item => item.ID == selectedShirtID);
        if (selectedShirtIndex >= 0)
        {
            characterCustomization.selectedShirtIndex = selectedShirtIndex;
            // Gọi phương thức EquidItem để hiển thị lại áo
            characterCustomization.EquidItem(_itemDatabase.Itemsshirt[selectedShirtIndex]);
        }
    }

    if (PlayerPrefs.HasKey("SelectedColorID"))
    {
        int selectedColorID = PlayerPrefs.GetInt("SelectedColorID");
        int selectedColorIndex = _itemDatabase.Itemscolor.FindIndex(item => item.ID == selectedColorID);
        if (selectedColorIndex >= 0)
        {
            characterCustomization.selectedColorIndex = selectedColorIndex;
            // Gọi phương thức EquidItem để hiển thị lại màu
            characterCustomization.EquidItem(_itemDatabase.Itemscolor[selectedColorIndex]);
        }
    }

    if (PlayerPrefs.HasKey("SelectedgunID"))
    {
        int selectedGunID = PlayerPrefs.GetInt("SelectedgunID");
        int selectedGunIndex = _itemDatabase.Itemsgun.FindIndex(item => item.ID == selectedGunID);
        if (selectedGunIndex >= 0)
        {
            characterCustomization.selectedGunIndex = selectedGunIndex;
            // Gọi phương thức EquidItem để hiển thị lại súng
            characterCustomization.EquidItem(_itemDatabase.Itemsgun[selectedGunIndex]);
        }
    }

    if (PlayerPrefs.HasKey("SelectedPerkID"))
    {
        int selectedPerkID = PlayerPrefs.GetInt("SelectedPerkID");
        int selectedPerkIndex = _itemDatabase.Itemsperk.FindIndex(item => item.ID == selectedPerkID);
        if (selectedPerkIndex >= 0)
        {
            characterCustomization.selectedPerkIndex = selectedPerkIndex;
            // Gọi phương thức EquidItem để hiển thị lại perk
            characterCustomization.EquidItem(_itemDatabase.Itemsperk[selectedPerkIndex]);
        }
    }
}

  
   

  
    private void ClosePanel()
    {
        if (customizationPanel != null)
        {
            customizationPanel.SetActive(false);
        }
    }

   
    private void SetupButtonEvents(Button[] buttons, System.Action<int> onClickAction)
    {
        if (buttons.Length > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttons[i].onClick.AddListener(() => onClickAction(index));
            }
        }
    }
}
