using GameTool.Assistants.DesignPattern;
using UnityEngine;

namespace GameTool.Assistants
{
    public class SplashSceneManager : SingletonMonoBehaviour<SplashSceneManager>
    {
        [SerializeField] private float maxTimeWaitLoadSceneStart = 1f;
        [SerializeField] private bool loadSceneStart = true;

    }
}