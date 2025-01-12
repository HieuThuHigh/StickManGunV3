using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MapMulti : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button[] mapButtons;        // Các nút chọn map
    [SerializeField] private GameObject[] mapImages;     // Hình ảnh của từng map
    [SerializeField] private Button chooseMapButton;     // Nút mở giao diện chọn map
    [SerializeField] private GameObject chooseMapPanel;  // Giao diện chọn map

    public delegate void OnMapSelected(int mapIndex);
    public static event OnMapSelected MapSelectedEvent;  // Sự kiện gửi map đã chọn
   
    private void Start()
    {
        // Gán sự kiện cho các nút chọn map
        for (int i = 0; i < mapButtons.Length; i++)
        {
            int index = i;
            mapButtons[i].onClick.AddListener(() => OnMapButtonClicked(index));
        }

        // Gán sự kiện cho nút mở giao diện chọn map
        chooseMapButton.onClick.AddListener(OpenMapSelectionPanel);

        // Chỉ chủ phòng mới được phép chọn map
        chooseMapButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);

        HideAllMapImages();  // Ẩn tất cả ảnh map khi bắt đầu
        chooseMapPanel.SetActive(false);  // Ẩn giao diện chọn map khi bắt đầu
    }

    // Hiển thị giao diện chọn map
    private void OpenMapSelectionPanel()
    {
        chooseMapPanel.SetActive(true);
    }

    // Xử lý khi chọn map
    private void OnMapButtonClicked(int index)
    {
        if (!PhotonNetwork.IsMasterClient) return;  // Chỉ chủ phòng được chọn map

        ShowMapImage(index);

        // Lưu map đã chọn vào Custom Properties để đồng bộ
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "SelectedMap", index }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        // Gửi sự kiện để LobbyMainPanel xử lý
        MapSelectedEvent?.Invoke(index);

        // Đóng giao diện chọn map sau khi chọn
        chooseMapPanel.SetActive(false);
    }

    // Hiển thị ảnh map được chọn
    private void ShowMapImage(int activeIndex)
    {
        for (int i = 0; i < mapImages.Length; i++)
        {
            mapImages[i].SetActive(i == activeIndex);
        }
    }

    // Ẩn tất cả ảnh map
    private void HideAllMapImages()
    {
        foreach (GameObject image in mapImages)
        {
            image.SetActive(false);
        }
    }

    // Đồng bộ map khi có thay đổi
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("SelectedMap"))
        {
            int selectedMapIndex = (int)propertiesThatChanged["SelectedMap"];
            ShowMapImage(selectedMapIndex);
        }
    }
} 
