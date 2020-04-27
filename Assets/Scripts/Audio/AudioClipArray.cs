using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//讓我們可以直接在unity介面創建此class的實例
[CreateAssetMenu]
public class AudioClipArray : ScriptableObject, IEnumerable<Sound>
{
    //建立一個 Sound 的陣列
    public Sound[] sounds;

    public Sound this [int i] { get { return this.sounds[i]; } }
    public IEnumerator<Sound> GetEnumerator() { return this.sounds.Cast<Sound>().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
}