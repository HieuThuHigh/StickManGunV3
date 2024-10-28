using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

     private string _gameId = "5712874";  // Thay bằng Game ID của Android
     private string _adUnitId = "Interstitial_Android";  // Thay bằng ad unit ID cho quảng cáo Interstitial
     private bool _testMode = true;

    private bool _isAdLoaded = false;  // Biến để theo dõi trạng thái quảng cáo

    void Start()
    {
        // Khởi tạo Unity Ads
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    // Gọi hàm này để load quảng cáo Interstitial
    public void LoadInterstitialAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    // Gọi hàm này để hiển thị quảng cáo Interstitial khi đã sẵn sàng
    public void ShowInterstitialAd()
    {
        if (_isAdLoaded)  // Kiểm tra biến trạng thái
        {
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Debug.Log("Ad not ready.");
        }
    }

    // Callbacks for initialization
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadInterstitialAd(); // Tải quảng cáo khi khởi tạo thành công
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }

    // Callbacks for loading ads
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad {adUnitId} loaded successfully.");
        _isAdLoaded = true;  // Đặt biến trạng thái khi quảng cáo đã tải thành công
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Failed to load Ad {adUnitId}: {error} - {message}");
        _isAdLoaded = false;  // Đặt biến trạng thái khi tải quảng cáo không thành công
    }

    // Callbacks for showing ads
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad {adUnitId} completed with state: {showCompletionState}");
        LoadInterstitialAd(); // Tải lại quảng cáo sau khi đã hiển thị
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Failed to show Ad {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"Ad {adUnitId} started.");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log($"Ad {adUnitId} clicked.");
    }
    // Phương thức để hiện quảng cáo ngay khi scene được tải
    public void ShowAdOnSceneLoad()
    {
        ShowInterstitialAd();
    }
}
