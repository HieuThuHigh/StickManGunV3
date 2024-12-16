using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button soundOn;
    [SerializeField] private Button soundOff;
    [SerializeField] private GameObject soundOnObj;
    [SerializeField] private GameObject soundOffObj;
    [SerializeField] private GameObject pausePopupObj;
    private LobbyMainPanel lobbyMainPanel;

    private void Start()
    {
        if (continueBtn)
        {
            continueBtn.onClick.AddListener(ContinueEvent);
        }

        quitBtn.onClick.AddListener(QuitEvent);
        soundOn.onClick.AddListener(SoundOnEvent);
        soundOff.onClick.AddListener(SoundOffEvent);
    }

    private void SoundOffEvent()
    {
        Debug.LogError("a");
        soundOffObj.SetActive(false);
        soundOnObj.SetActive(true);
    }

    private void SoundOnEvent()
    {
        soundOnObj.SetActive(false);
        soundOffObj.SetActive(true);
    }

    public void QuitEvent()
    {
        // Người chơi thoát khỏi phòng hiện tại
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsConnected)
        {
            // Ngắt kết nối khỏi Photon khi bấm nút thoát
            PhotonNetwork.Disconnect();
        }

        // Tìm đối tượng LobbyMainPanel trong scene
        lobbyMainPanel = FindObjectOfType<LobbyMainPanel>();
        // Reset lại các tiến trình multi (nếu cần)
        // Gọi hàm ResetMultiPlayerState nếu tìm thấy LobbyMainPanel
        if (lobbyMainPanel != null)
        {
            lobbyMainPanel.ResetMultiPlayerState();
        }
        else
        {
            Debug.LogWarning("LobbyMainPanel not found.");
        }

        // Quay lại Scene Home
        SceneManager.LoadScene("Home");
        Time.timeScale = 1;
    }

    private void ContinueEvent()
    {
        Time.timeScale = 1;
        CanvasController.Instance.pauseObject.SetActive(false);
        pausePopupObj.SetActive(false);
    }
}