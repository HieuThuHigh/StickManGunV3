using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingSPL : MonoBehaviour
{
    public GameObject[] popups;
    public Image loadingImage; 
    public TextMeshProUGUI loadingText; // Text để hiển thị phần trăm loading
    [SerializeField] private GameObject startGamePopup;
    [SerializeField] private GameObject loadingGamePopup;
    [SerializeField] private GameObject homePopup;
    [SerializeField] private Button startGameBtn;
    [SerializeField] private float fillDuration = 4.5f; 
    [SerializeField] private float currentFillTime = 0f;
    public GameObject popupLoading;

    private void Start()
    {
        startGameBtn.onClick.AddListener(ButtonClick);
        StartCoroutine(HandleLoadingPopups());
        loadingImage.fillAmount = 0f;
        loadingText.text = "0%";
    }

    private IEnumerator HandleLoadingPopups()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < popups.Length; j++)
            {
                popups[j].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                popups[j].SetActive(false);
            }
        }

        Debug.Log("Hoàn tất quá trình loading");
    }

    private void Update()
    {
        Event();
    }

    void Event()
    {
        if (loadingImage.fillAmount < 1f)
        {
            currentFillTime += Time.deltaTime;
            
            float progress = Mathf.Clamp01(currentFillTime / fillDuration);
            loadingImage.fillAmount = progress;
            
            loadingText.text = Mathf.RoundToInt(progress * 100f) + "%";
        }
        else if (loadingImage.fillAmount >= 1f && popupLoading.activeSelf)
        {
            popupLoading.SetActive(false);
            loadingImage.gameObject.SetActive(false);
            loadingText.gameObject.SetActive(false);
            startGamePopup.SetActive(true);
        }
    }

    void ButtonClick()
    {
        loadingGamePopup.SetActive(false);
        homePopup.SetActive(true);
    }
}
