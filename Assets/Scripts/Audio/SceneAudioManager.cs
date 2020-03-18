using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    public static SceneAudioManager instance = null; //單例模式的拜訪管道(共享變數)

    [SerializeField]
    private AudioClipArray sceneClipArray = null; //儲存音檔資訊的list
    private Dictionary<string, AudioSource> name2Audio = new Dictionary<string, AudioSource>(); //名稱對應到音檔的Dictionary
    /* Dictionary是一個提供一一對應，快速查找物品的資料結構，運作方式是
     * "提供一個鑰匙，它會回傳該鑰匙對應到的東西。(如果有對應到東西的話)"*/ 

    private void Awake()
    {
        //如果共享變數沒人當 
        //我來當共享變數
        //否則自毀

        //讓自己不會因為場景切換被消滅

        if(sceneClipArray != null) //如果儲存音檔資訊的list不是null
        {
            //遍歷這個list裡的所有東西
                //新增一個空的AudioSource
                /*幫新增的AudioSource填資料*/
                //把名字和AudioSource的pair新增到dictionary內
        }
    }

    public void PlayByName(string name)
    {
        //如果Dictionary內有name這個key存在
            //暫停播放(可能這時候這個音效本身還在播放)
            //播放
        //否則顯示錯誤訊息
    }

    public void StopByName(string name)
    {
        //如果Dictionary內有name這個key存在
            //如果這個音效正在播放，暫停播放
        //否則顯示錯誤訊息
    }

    public void StopAll()
    {
        //對於每個在Dictionary內的AudioSource
        //如果這個音效正在播放，暫停播放
    }
}
