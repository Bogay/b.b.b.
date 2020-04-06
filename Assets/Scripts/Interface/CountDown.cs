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
        //  時間常數設為零 (時間暫停)
        Time.timeScale = 0;
        // 開始倒數的 coroutine
        StartCoroutine(this.StartCountDown());
    }

    public IEnumerator StartCountDown()
    {
        string[] subtitles = { "3...", "2...", "1...", "Go!", "" };
        string[] sounds = { "3", "2", "1", "Go", "Scene_1_bgm" };
        float[] waits = { 0.25f, 1f, 1f, 1f, 1f };
        for(int i = 0; i < 5; i++)
        {
            // wait a moment
            yield return new WaitForSecondsRealtime(waits[i]);
            // play SE
            SceneAudioManager.instance.PlayByName(sounds[i]);
            // set subtitle
            this.countDown.text = subtitles[i];
        }
        // 時間常數設回 1 (正常時間)
        Time.timeScale = 1;
        // 觸發 OnCountDownComplete 事件
        this.OnCountDownComplete.Invoke();
    }
}