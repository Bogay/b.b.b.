using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    public static SceneAudioManager instance = null; // 單例模式的拜訪管道(共享變數)

    [SerializeField]
    private AudioClipArray sceneClipArray = null; // 儲存音檔資訊的list
    private Dictionary<string, AudioSource> name2Audio = new Dictionary<string, AudioSource>(); // 名稱對應到音檔的Dictionary

    private void Awake()
    {
        // 如果共享變數沒人當 
        if(!instance)
        {
            // 我來當共享變數
            instance = this;
        }
        else
        {
            // 否則自毀
            Destroy(gameObject);
            return;
        }
        // 讓自己不會因為場景切換被消滅
        DontDestroyOnLoad(gameObject);
        if(sceneClipArray != null) // 如果儲存音檔資訊的 list 不是 null
        {
            // 遍歷這個 list 裡的所有東西
            foreach(Sound s in sceneClipArray.sounds)
            {
                // 新增一個空的 AudioSource
                AudioSource audio = gameObject.AddComponent<AudioSource>();
                // 幫新增的 AudioSource 填資料
                audio.name = s.name;
                audio.clip = s.clip;
                audio.volume = s.volume;
                audio.pitch = s.pitch;
                audio.loop = s.loop;
                // 把名字和 AudioSource 的 pair 新增到 dictionary 內
                name2Audio.Add(s.name, audio);
            }
        }
    }

    public void PlayByName(string name)
    {
        // 如果 Dictionary 內有 name 這個 key 存在
        if(name2Audio.ContainsKey(name))
        {
            AudioSource audio = name2Audio[name];
            // 暫停播放(可能這時候這個音效本身還在播放)
            audio.Stop();
            // 播放
            audio.Play();
        }
        // 否則顯示錯誤訊息
        else
        {
            Debug.LogError($"Audio source ({name}) not found!");
        }
    }

    public void StopByName(string name)
    {
        // 如果 Dictionary 內有 name 這個 key 存在
        if(name2Audio.ContainsKey(name))
        {
            AudioSource audio = name2Audio[name];
            // 如果這個音效正在播放，暫停播放
            audio.Stop();
        }
        else
        {
            // 否則顯示錯誤訊息
            Debug.LogError($"Audio source ({name}) not found!");
        }
    }

    public void StopAll()
    {
        // 對於每個在 Dictionary 內的 AudioSource
        // 如果這個音效正在播放，暫停播放
        name2Audio.Values.Where(audio => audio.isPlaying).ToList().ForEach(audio => audio.Stop());
    }
}