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
    [Range(0f, 1f)]
    public float volume = 0.5f;
    // 音檔的音高
    [Range(-3f, 3f)]
    public float pitch = 1;
    // 音檔是否重複播放
    public bool loop;

    public void InitAudioSource(AudioSource audio)
    {
        audio.name = this.name;
        audio.clip = this.clip;
        audio.volume = this.volume;
        audio.pitch = this.pitch;
        audio.loop = this.loop;
        audio.playOnAwake = false;
        audio.spatialBlend = 0;
    }
}