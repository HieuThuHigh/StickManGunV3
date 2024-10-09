using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GunLibraryUI : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private Button button;

        public WeaponInfo WeaponInfo
        {
            get => weaponInfo;
            set => weaponInfo = value;
        }

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                
            });
        }
    }
}