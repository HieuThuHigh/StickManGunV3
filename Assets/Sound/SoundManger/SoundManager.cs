
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image SoundOnIcon;
    [SerializeField] Image SoundOffIcon;
    private bool muted = false;
    void Start()
    {
        if(!UnityEngine.PlayerPrefs.HasKey("muted")){
            UnityEngine.PlayerPrefs.SetInt("muted", 0);
            Load();
        }else{
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;

    }
    public void OnButtonPress(){
        if(muted == false){
            muted = true;
            AudioListener.pause = true;
        }else{
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon(){
        if(muted == false){
            SoundOnIcon.enabled = true;
            SoundOffIcon.enabled = false;
        }else{
            SoundOnIcon.enabled = true;
            SoundOffIcon.enabled = true;
        }
    }
    private void Load(){
        muted = UnityEngine.PlayerPrefs.GetInt("muted")== 1;
    }
    private void Save(){
        UnityEngine.PlayerPrefs.SetInt("muted",muted? 1 : 0);
    }
}