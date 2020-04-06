using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DefenderCanvas : MonoBehaviour
{
    [SerializeField]
    private BrickPlayer player; // 磚塊玩家
    private CanvasGroup group; // UI 畫布的元件管理者
    private bool invoked = false; // 是否已觸發的bool

    void Start()
    {
        this.group = GetComponent<CanvasGroup>(); // 獲取此物件 UI 畫布的元件管理者
        this.group.alpha = 0; // 把畫布的透明度設成 0 (完全不可見)
        this.group.blocksRaycasts = false; // 讓畫布不會遮擋射線判斷
        this.group.interactable = false; // 讓畫布內的元件無法互動(避免不小心按到按鈕之類的)
        this.player.OnDie.AddListener(this.ShowCanvas); // 向 player 的 OnDie 事件註冊 ShowCanvas 函式
    }

    void ShowCanvas()
    {
        // 如果這個函式已經被觸發過了
        // 中斷 (因為這個函式應該只能被觸發一次)
        if(this.invoked) return;
        // 已觸發的 bool 設成 true
        this.invoked = true;
        // 播放名為 "GameOver" 的音效
        SceneAudioManager.instance.PlayByName("GameOver");
        // 停止播放 "Scene_1_bgm"
        SceneAudioManager.instance.StopByName("Scene_1_bgm");
        // 開始播放勝利畫面的 coroutine
        StartCoroutine(this.StartShowing());
    }

    IEnumerator StartShowing()
    {
        // 把畫布透明度在 0.5 秒內漸變成 1
        DOTween.To(() => this.group.alpha, x => this.group.alpha = x, 1, 0.5f).SetUpdate(true);
        // 把時間常數在 0.5 秒內漸變成 0
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 0.5f).SetUpdate(true);
        // 等待現實時間裡的 0.5 秒 (ensure the canvas has appeared)
        yield return new WaitForSecondsRealtime(0.5f);
        // 讓畫布會遮擋射線判斷
        this.group.blocksRaycasts = true;
        // 讓畫布內的元件可互動
        this.group.interactable = true;
    }
}