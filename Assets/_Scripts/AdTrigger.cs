using UnityEngine;

public class AdTrigger : MonoBehaviour
{
    void Start()
    {
        // Lấy AdsManager và gọi phương thức để hiện quảng cáo
        AdsManager adsManager = FindObjectOfType<AdsManager>();
        if (adsManager != null)
        {
            adsManager.ShowAdOnSceneLoad();
        }
    }
}
