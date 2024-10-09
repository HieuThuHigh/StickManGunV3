using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLibraryController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject[] gameObjectsPopup;

    [SerializeField] private TextMeshProUGUI txtNameGun;
    [SerializeField] private TextMeshProUGUI txtDamage;
    [SerializeField] private TextMeshProUGUI txtAmmoCap;
    [SerializeField] private TextMeshProUGUI txtBulletType;

    [SerializeField] private List<GunLibraryUI> listSmgUI;
    [SerializeField] private List<GunLibraryUI> listSniperUI;
    [SerializeField] private List<GunLibraryUI> listShotgunUI;
    [SerializeField] private List<GunLibraryUI> listLmgUI;
    [SerializeField] private List<GunLibraryUI> listPistolUI;

    private Color _defaultColor = Color.white;
    private Color _selectedColor = Color.yellow;

    private void Start()
    {
        WeaponInfoCollection infor = Resources.Load<WeaponInfoCollection>("WeaponInfoCollection");

        for (int i = 0; i < listSmgUI.Count; i++)
        {
            listSmgUI[i].WeaponInfo = infor.ListSmg[i];
        }

        for (int i = 0; i < listSniperUI.Count; i++)
        {
            listSniperUI[i].WeaponInfo = infor.ListSniper[i];
        }

        for (int i = 0; i < listShotgunUI.Count; i++)
        {
            listShotgunUI[i].WeaponInfo = infor.ListShotGun[i];
        }

        for (int i = 0; i < listLmgUI.Count; i++)
        {
            listLmgUI[i].WeaponInfo = infor.ListLmg[i];
        }

        for (int i = 0; i < listPistolUI.Count; i++)
        {
            listPistolUI[i].WeaponInfo = infor.ListPistol[i];
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => EventClicked(index));
        }

        ResetButtonColors();
    }

    public void ShowInfo(WeaponInfo gunInfor)
    {
        txtNameGun.text = gunInfor.NameGun;
        txtDamage.text = "Damage : " + gunInfor.Damage + "/10";
        txtAmmoCap.text = "Ammo Capacity : " + gunInfor.AmmoCap;
        txtBulletType.text = "Bullet Type : " +gunInfor.TypeBullet;
    }

    void ResetButtonColors()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = _defaultColor;
        }
    }

    void EventClicked(int index)
    {
        ResetButtonColors();
        for (int i = 0; i < gameObjectsPopup.Length; i++)
        {
            gameObjectsPopup[i].SetActive(i == index);
        }

        buttons[index].image.color = _selectedColor;
    }
}