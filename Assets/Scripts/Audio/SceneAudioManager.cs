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
        // if instance is assigned
        if (instance)
        {
            // 自毀
            Destroy(gameObject);
            return;
        }
        // 我來當共享變數
        instance = this;
        // 讓自己不會因為場景切換被消滅
        DontDestroyOnLoad(gameObject);
        if (sceneClipArray) // 如果儲存音檔資訊的 list 不是 null
        {
            // 遍歷這個 list 裡的所有東西
            foreach (Sound s in sceneClipArray)
            {
                // 新增一個空的 AudioSource
                AudioSource audio = gameObject.AddComponent<AudioSource>();
                // 幫新增的 AudioSource 填資料
                s.InitAudioSource(audio);
                // 把名字和 AudioSource 的 pair 新增到 dictionary 內
                name2Audio.Add(s.name, audio);
            }
        }
        else
        {
            Debug.LogWarning("Clip array is not assigned!");
        }
    }

    private AudioSource requireAudio(string name)
    {
        if (!this.name2Audio.ContainsKey(name))
        {
            Debug.LogError($"Audio source ({name}) not found!");
            return null;
        }
        return this.name2Audio[name];
    }

    public void PlayByName(string name)
    {
        AudioSource audio = this.requireAudio(name);
        // stop first (it may be playing now)
        audio?.Stop();
        audio?.Play();
    }

    public void StopByName(string name)
    {
        this.requireAudio(name)?.Stop();
    }

    public void StopAll()
    {
        // 對於每個在 Dictionary 內的 AudioSource
        // 如果這個音效正在播放，暫停播放
        name2Audio.Values.Where(audio => audio.isPlaying).ToList().ForEach(audio => audio.Stop());
    }
}