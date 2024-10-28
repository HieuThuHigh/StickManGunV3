using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<MapData> maps;  // Danh sách các map

    private int _currentMapIndex = 0; // Chỉ số của map hiện tại

    // Hàm để lấy map hiện tại
    public GameObject GetCurrentMap()
    {
        return maps[_currentMapIndex].mapPrefab;
    }

    // Hàm để thay đổi map
    public void ChangeMap(int newMapIndex)
    {
        if (newMapIndex >= 0 && newMapIndex < maps.Count)
        {
            _currentMapIndex = newMapIndex;
            Debug.Log("Đã thay đổi sang map: " + maps[_currentMapIndex].mapName);
        }
    }
}
