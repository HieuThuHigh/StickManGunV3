using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DoubleJumpController : MonoBehaviour
{
    
    [SerializeField] private Button jumpButton;
    [SerializeField] private GameObject addJumpPopup;
    [SerializeField] private TextMeshProUGUI jumpText;
    [SerializeField] private GameObject plusPopupJump;
    [SerializeField] private GameObject quatityPopupJump;
    private int _previousJumpValue = -1;



    private void Start()
    {
        UpdateUI();
        jumpButton.onClick.AddListener(OnFreezeButtonClicked);
    }


    private void OnFreezeButtonClicked()
    {
        if (GameData.Jump == 0)
        {
            addJumpPopup.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameData.Jump -= 1;
            this.PostEvent(EventID.Jump);
        }

        Debug.LogError("check bang");
        UpdateUI();
    }

    void UpdateUI()
    {
        jumpText.text = GameData.Jump.ToString();
        if (GameData.Jump != _previousJumpValue)
        {
            UpdatePopupStates(GameData.Jump);
            _previousJumpValue = GameData.Jump;
        }

        if (GameData.Jump < 0) GameData.Jump = 0;
        if (GameData.Jump > 100) GameData.Jump = 100;
    }

    private void UpdatePopupStates(int freezeValue)
    {
        // Bật hoặc tắt popup dựa trên giá trị freezeValue
        if (freezeValue == 0)
        {
            plusPopupJump.SetActive(true);
            addJumpPopup.SetActive(false); // Popup addFreeze chỉ bật khi bấm nút
            quatityPopupJump.SetActive(false);
        }
        else
        {
            plusPopupJump.SetActive(false);
            quatityPopupJump.SetActive(true);
        }
    }
}