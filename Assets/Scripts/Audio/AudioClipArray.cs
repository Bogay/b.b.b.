using UnityEngine;

//讓我們可以直接在unity介面創建此class的實例
[CreateAssetMenu]
public class AudioClipArray : ScriptableObject
{
    //建立一個 Sound 的陣列
    public Sound[] sounds;
}