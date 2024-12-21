using GameToolSample.GameDataScripts.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image SoundOnIcon;
    [SerializeField] Image SoundOffIcon;
    [SerializeField] private Image Line;

    private bool muted
    {
        get
        {
            return GameData.isMusic == 0;
        }
        set
        {
            if (value)
            {
                GameData.isMusic = 0;
            }
            else
            {
                GameData.isMusic = 1;
            }
        }
    }
    void Start()
    {
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }
    public void OnButtonPress(){
        if(muted == false){
            muted = true;
            AudioListener.pause = muted;
        }else{
            muted = false;
            AudioListener.pause = muted;
        }
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon(){
        if(muted == false){
            Line.gameObject.SetActive(false);
        }else{
            Line.gameObject.SetActive(true);
        }
    }
}