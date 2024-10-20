using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPlayer : MonoBehaviour
{
    public thanhMau thanhmau;
    public float luongMauHienTai;
    public float  health = 100;
    // Start is called before the first frame update
    void Start()
    {
         luongMauHienTai =  health;
        thanhmau.capNhatThanhMau(luongMauHienTai,  health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
}
