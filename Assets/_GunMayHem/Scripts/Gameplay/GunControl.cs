using System;
using UnityEngine;

namespace _GunMayHem.Gameplay
{
    public class GunControl : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _posFire;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                GameObject bullet = null;
                
                
            }
        }
    }
}