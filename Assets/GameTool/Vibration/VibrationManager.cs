using GameTool.Assistants.DesignPattern;

namespace GameTool.Vibration
{
    public class VibrationManager : SingletonMonoBehaviour<VibrationManager>
    {
        private void Start()
        {
#if !Minify
             Vibration.Init();
#endif
        }

        public void Vibrate(HapticType hapticType = HapticType.Sort)
        {
#if !Minify
             if (GameToolSample.GameDataScripts.Scripts.GameData.Instance.Vibrate)
             {
#if !UNITY_EDITOR
                 switch (hapticType)
                 {
                     case HapticType.None:
                         {
                             break;
                         }
                     case HapticType.Sort:
                         {
                             Vibration.VibrateNormal();
                             break;
                         }
                     case HapticType.Medium:
                         {
                             Vibration.VibratePop();
                             break;
                         }
                     case HapticType.Hard:
                         {
                             Vibration.VibratePeek();
                             break;
                         }
                 }
#endif
             }
#endif 
         }
        }

        public enum HapticType
        {
            None,
            Sort,
            Medium,
            Hard
        }
    }