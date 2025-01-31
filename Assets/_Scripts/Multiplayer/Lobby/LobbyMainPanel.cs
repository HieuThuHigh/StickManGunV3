﻿using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using _GunMayHem.Gameplay;

public class LobbyMainPanel : MonoBehaviourPunCallbacks
{
    [Header("Login Panel")]
    public GameObject LoginPanel;

    public InputField PlayerNameInput;

    [Header("Selection Panel")]
    public GameObject SelectionPanel;

    [Header("Create Room Panel")]
    public GameObject CreateRoomPanel;

    public InputField RoomNameInputField;
    // public InputField MaxPlayersInputField;

    [Header("Join Random Room Panel")]
    public GameObject JoinRandomRoomPanel;

    [Header("Room List Panel")]
    public GameObject RoomListPanel;

    public GameObject RoomListContent;
    public GameObject RoomListEntryPrefab;

    [Header("Inside Room Panel")]
    public GameObject InsideRoomPanel;
    public GameObject PlayerView;
    public Button StartGameButton;
    public GameObject PlayerListEntryPrefab;
    private string selectedScene;

    // Prefab của nhân vật
    [Header("Player Prefab")]
    public GameObject PlayerPrefab;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;
    private Dictionary<int, GameObject> playerListEntries;


    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string, GameObject>();

        PlayerNameInput.text = "Player " + Random.Range(1000, 10000);
    }



    public override void OnConnectedToMaster()
    {
        // Reset lại trạng thái sau khi người chơi quay lại
        ResetMultiPlayerState();

        this.SetActivePanel(SelectionPanel.name);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();

        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }

    public override void OnJoinedLobby()
    {
        cachedRoomList.Clear();
        ClearRoomListView();
    }

    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
        ClearRoomListView();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        SetActivePanel(SelectionPanel.name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        SetActivePanel(SelectionPanel.name);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { MaxPlayers = 2 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }
    // Hàm để reset lại trạng thái của người chơi
    public void ResetMultiPlayerState()
    {
        // Xóa các entry của người chơi
        if (playerListEntries != null)
        {
            foreach (GameObject entry in playerListEntries.Values)
            {
                Destroy(entry.gameObject);
            }
            playerListEntries.Clear();
            playerListEntries = null;
        }

        // Xóa danh sách phòng đã cache
        cachedRoomList.Clear();
        ClearRoomListView();

        // Reset các thuộc tính của người chơi trong Photon
        Hashtable props = new Hashtable
    {
        {character2.PLAYER_READY, false},
        {character2.PLAYER_LOADED_LEVEL, false}
    };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        // Đặt lại UI về trạng thái ban đầu
        SetActivePanel(SelectionPanel.name);
    }
    // Callback khi ngắt kết nối thành công
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Đã ngắt kết nối khỏi Photon với lý do: " + cause);

        // Sau khi ngắt kết nối, gọi lại ConnectUsingSettings
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        cachedRoomList.Clear();

        SetActivePanel(InsideRoomPanel.name);

        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(PlayerListEntryPrefab);
            entry.transform.SetParent(PlayerView.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);

            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(character2.PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
            }

            playerListEntries.Add(p.ActorNumber, entry);
        }

        StartGameButton.gameObject.SetActive(CheckPlayersReady());

        Hashtable props = new Hashtable
            {
                {character2.PLAYER_LOADED_LEVEL, false}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        // Tạo nhân vật (player) cho người chơi local
        CreatePlayer();
    }

    public override void OnLeftRoom()
    {
        SetActivePanel(SelectionPanel.name);

        foreach (GameObject entry in playerListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        playerListEntries.Clear();
        playerListEntries = null;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        GameObject entry = Instantiate(PlayerListEntryPrefab);
        entry.transform.SetParent(PlayerView.transform);
        entry.transform.localScale = Vector3.one;
        entry.GetComponent<PlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

        playerListEntries.Add(newPlayer.ActorNumber, entry);

        StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
        playerListEntries.Remove(otherPlayer.ActorNumber);

        StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            StartGameButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        GameObject entry;
        if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
        {
            object isPlayerReady;
            if (changedProps.TryGetValue(character2.PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
            }
        }

        StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }



    private void CreatePlayer()
    {
        if (PlayerPrefab != null)
        {
            // Tạo nhân vật của người chơi local
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(Random.Range(-5f, 5f), 1, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("PlayerPrefab chưa được gắn trong Inspector!");
        }
    }



    public void OnBackButtonClicked()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        SetActivePanel(SelectionPanel.name);
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = RoomNameInputField.text;
        roomName = (roomName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : roomName;

        RoomOptions options = new RoomOptions { MaxPlayers = 2, PlayerTtl = 10000 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }


    public void OnJoinRandomRoomButtonClicked()
    {
        SetActivePanel(JoinRandomRoomPanel.name);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnLoginButtonClicked()
    {
        string playerName = PlayerNameInput.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public void OnRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }

        SetActivePanel(RoomListPanel.name);
    }

    public override void OnEnable()
    {
        base.OnEnable();  // Gọi OnEnable() của lớp cha
        MapMulti.MapSelectedEvent += OnMapSelected;
    }

    public override void OnDisable()
    {
        base.OnDisable();  // Gọi OnDisable() của lớp cha
        MapMulti.MapSelectedEvent -= OnMapSelected;
    }

    private void Start()
    {
        StartGameButton.onClick.AddListener(OnPlayButtonClicked);
        StartGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    // Nhận map được chọn từ MapMulti
    private void OnMapSelected(int mapIndex)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("Chỉ chủ phòng mới được chọn map!");
            return;
        }

        switch (mapIndex)
        {
            case 0: selectedScene = "Level1"; break;
            case 1: selectedScene = "Level2"; break;
            case 2: selectedScene = "Level3"; break;
            case 3: selectedScene = "Level4"; break;
            default: Debug.LogError("Map không hợp lệ!"); return;
        }

        // Lưu map vào Room Properties
        Hashtable properties = new Hashtable
        {
            { "SelectedScene", selectedScene }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);

        Debug.Log("Đã chọn map: " + selectedScene);
    }

    private void OnPlayButtonClicked()
    {
        if (selectedScene == null)
        {
            Debug.LogWarning("Vui lòng chọn map trước khi chơi!");
            return;
        }

        PhotonNetwork.LoadLevel(selectedScene);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("SelectedScene"))
        {
            selectedScene = (string)propertiesThatChanged["SelectedScene"];
            Debug.Log("Map đã được cập nhật: " + selectedScene);
        }
    }

    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }

        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(character2.PLAYER_READY, out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    private void ClearRoomListView()
    {
        foreach (GameObject entry in roomListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        roomListEntries.Clear();
    }

    public void LocalPlayerPropertiesUpdated()
    {
        StartGameButton.gameObject.SetActive(CheckPlayersReady());
    }

    private void SetActivePanel(string activePanel)
    {
        LoginPanel.SetActive(activePanel.Equals(LoginPanel.name));
        SelectionPanel.SetActive(activePanel.Equals(SelectionPanel.name));
        CreateRoomPanel.SetActive(activePanel.Equals(CreateRoomPanel.name));
        JoinRandomRoomPanel.SetActive(activePanel.Equals(JoinRandomRoomPanel.name));
        RoomListPanel.SetActive(activePanel.Equals(RoomListPanel.name));
        InsideRoomPanel.SetActive(activePanel.Equals(InsideRoomPanel.name));
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }

                continue;
            }

            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    private void UpdateRoomListView()
    {
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            GameObject entry = Instantiate(RoomListEntryPrefab);
            entry.transform.SetParent(RoomListContent.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<RoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, (byte)info.MaxPlayers);

            roomListEntries.Add(info.Name, entry);
        }
    }
}

