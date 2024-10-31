using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfoCollection : ScriptableObject
{
    [SerializeField] private List<WeaponInfo> listSMG;
    [SerializeField] private List<WeaponInfo> listSniper;
    [SerializeField] private List<WeaponInfo> listShotGun;
    [SerializeField] private List<WeaponInfo> listLMG;
    [SerializeField] private List<WeaponInfo> listPistol;

    public List<WeaponInfo> ListSmg => listSMG;

    public List<WeaponInfo> ListSniper => listSniper;

    public List<WeaponInfo> ListShotGun => listShotGun;

    public List<WeaponInfo> ListLmg => listLMG;

    public List<WeaponInfo> ListPistol => listPistol;
}

[Serializable]
public class WeaponInfo
{
    public string NameGun;
    public int Damage;
    public int AmmoCap;
    public    BulletType TypeBullet;
}

public enum BulletType
{
    B1,B2
}
