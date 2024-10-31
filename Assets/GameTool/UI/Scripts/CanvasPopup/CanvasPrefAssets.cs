using System;
using System.Collections.Generic;
using GameToolSample.UIManager;
using UnityEditor;
using UnityEngine;

namespace GameTool.UI.Scripts.CanvasPopup
{
    [CreateAssetMenu(fileName = "CanvasPrefAssets", menuName = "ScriptableObject/CanvasPrefAssets", order = 0)]
    public class CanvasPrefAssets : ScriptableObject
    {
        public List<CanvasPrefAssetItem> uiAsset;
        
#if UNITY_EDITOR
        [ContextMenu("Re Update")]
        public void OnValidate()
        {
            var table = Resources.Load<CanvasPrefTable>("CanvasPrefTable");
            for (int i = 0; i < uiAsset.Count; i++)
            {
                if (uiAsset[i].key == eUIName.None)
                {
                    uiAsset[i]._key = "None";
                }
                
                if (uiAsset[i]._key == "None")
                {
                    uiAsset[i]._key = uiAsset[i].key.ToString();
                }
                else
                {
                    try
                    {
                        uiAsset[i].key = (eUIName) Enum.Parse(typeof(eUIName), uiAsset[i]._key);
                    }
                    catch (Exception)
                    {
                        Debug.LogError(name + " Key " + uiAsset[i]._key + " Removed");
                        uiAsset.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                
                if (uiAsset[i].key == eUIName.None)
                {
                    uiAsset[i].ui = null;
                    continue;
                }
                uiAsset[i].ui = table.Serializers
                    .Find(ui => ui.key == uiAsset[i].key.ToString()).settingUI.baseUI;
                
                if (uiAsset.FindIndex(item => item.key == uiAsset[i].key) != i)
                {
                    if (i == uiAsset.Count - 1)
                    {
                        uiAsset[i] = new CanvasPrefAssetItem();
                    }
                    else
                    {
                        Debug.LogError(name + " Key " + uiAsset[i]._key + " Removed");
                        uiAsset.RemoveAt(i);
                        i--;
                    }
                }
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
    
    [Serializable]
    public class CanvasPrefAssetItem
    {
        public string _key = "None";
        public eUIName key;
        public eUIType type;
        public BaseUI ui;
    }
}
