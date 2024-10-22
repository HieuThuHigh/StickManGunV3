using System;
using System.Collections.Generic;
using System.Linq;
using GameTool.Audio;
using GameToolSample.Audio;
using UnityEditor;
using UnityEngine;

namespace GameTool.Audio.Scripts
{
    [CreateAssetMenu(fileName = "AudioAsset", menuName = "ScriptableObject/AudioAsset", order = 0)]
    public class AudioAsset : ScriptableObject
    {
        public List<MusicAssetItem> musicAsset;
        public List<SoundAssetItem> soundAsset;
        
#if UNITY_EDITOR
        [ContextMenu("Re Update")]
        public void OnValidate()
        {
            var table = Resources.Load<AudioTable>("AudioTable");
            for (int i = 0; i < musicAsset.Count; i++)
            {
                if (musicAsset[i].key == eMusicName.None)
                {
                    musicAsset[i]._key = "None";
                }
                
                if (musicAsset[i]._key == "None")
                {
                    musicAsset[i]._key = musicAsset[i].key.ToString();
                }
                else
                {
                    try
                    {
                        musicAsset[i].key = (eMusicName) Enum.Parse(typeof(eMusicName), musicAsset[i]._key);
                    }
                    catch (Exception)
                    {
                        Debug.LogError("Key " + musicAsset[i]._key + " Removed");
                        musicAsset.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                
                if (musicAsset[i].key == eMusicName.None)
                {
                    musicAsset[i].listAudio.Clear();
                    continue;
                }
                musicAsset[i].listAudio = table.musicTracksSerializers
                    .Find(music => music.key == musicAsset[i].key.ToString()).track.listAudio.ToList();
                
                if (musicAsset.FindIndex(item => item.key == musicAsset[i].key) != i)
                {
                    if (i == musicAsset.Count - 1)
                    {
                        musicAsset[i] = new MusicAssetItem();
                    }
                    else
                    {
                        Debug.LogError("Key " + musicAsset[i]._key + " Removed");
                        musicAsset.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            for (int i = 0; i < soundAsset.Count; i++)
            {
                if (soundAsset[i].key == eSoundName.None)
                {
                    soundAsset[i]._key = "None";
                }
                
                if (soundAsset[i]._key == "None")
                {
                    soundAsset[i]._key = soundAsset[i].key.ToString();
                }
                else
                {
                    try
                    {
                        soundAsset[i].key = (eSoundName) Enum.Parse(typeof(eSoundName), soundAsset[i]._key);
                    }
                    catch (Exception)
                    {
                        Debug.LogError(name + " Key " + soundAsset[i]._key + " Removed");
                        soundAsset.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                
                if (soundAsset[i].key == eSoundName.None)
                {
                    soundAsset[i].listAudio.Clear();
                    continue;
                }
                soundAsset[i].listAudio = table.soundTracksSerializers
                    .Find(sound => sound.key == soundAsset[i].key.ToString()).track.listAudio.ToList();
                
                if (soundAsset.FindIndex(item => item.key == soundAsset[i].key) != i)
                {
                    if (i == soundAsset.Count - 1)
                    {
                        soundAsset[i] = new SoundAssetItem();
                    }
                    else
                    {
                        Debug.LogError(name + " Key " + soundAsset[i]._key + " Removed");
                        soundAsset.RemoveAt(i);
                        i--;
                    }
                }
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }

    [Serializable]
    public class MusicAssetItem
    {
        public string _key = "None";
        public eMusicName key;
        public List<AudioClip> listAudio;
    }

    [Serializable]
    public class SoundAssetItem
    {
        public string _key = "None";
        public eSoundName key;
        public List<AudioClip> listAudio;
    }
    

}