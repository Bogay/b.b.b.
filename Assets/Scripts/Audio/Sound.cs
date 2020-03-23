using UnityEngine;
using UnityEngine.Audio;

// 把這個 class 標記成可序列化
[System.Serializable]
public class Sound
{
    // 音檔的呼叫名稱
    public string name;
    // 音檔
    public AudioClip clip;
    // 音檔的音量
    public float volume;
    // 音檔的音高
    public float pitch;
    // 音檔是否重複播放
    public bool loop;
}