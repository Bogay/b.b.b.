using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countDown = null;
    public UnityEvent OnCountDownComplete = new UnityEvent();

    private void Start()
    {
        //時間常數設為零(時間暫停)
        //開始倒數的coroutine
    }

    public IEnumerator StartCountDown()
    {
        string[] subtitles;
        AudioClip[] sounds;
        float[] waits;

        //等待0.25秒
        yield return new WaitForSeconds(0.25f);
        //放出"3..."這個字幕
        //播放名為"3"的音效

        //等待1秒

        //放出"2..."這個字幕
        //播放名為"2"的音效

        //等待1秒

        //放出"1..."這個字幕
        //播放名為"1"的音效

        //等待1秒

        //放出"Go!"這個字幕
        //播放名為"Go"的音效

        //等待1秒

        //清空倒數字幕
        //時間常數設回1(正常時間)
        //播放名為"Scene_1_bgm"的音樂
        //觸發OnCountDownComplete事件
        yield break; //加這行只是因為IEnumerator需要至少一個yield才不會出錯，之後會刪掉
    }
}