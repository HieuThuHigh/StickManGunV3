using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewMapData", menuName = "Map Data", order = 1)]
public class MapData : ScriptableObject
{
    public string mapName;             // Tên map
    public GameObject mapPrefab;       // Prefab của map
    public string description;          // Mô tả map
}
