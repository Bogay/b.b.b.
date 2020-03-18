using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DefenderCanvas : MonoBehaviour
{
    [SerializeField]
    private BrickPlayer player; //磚塊玩家
    private CanvasGroup group; //UI畫布的元件管理者
    private bool invoked = false; //是否已觸發的bool

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>(); //獲取此物鍵UI畫布的元件管理者
        group.alpha = 0; //把畫布的透明度設成0(完全不可見)
        group.blocksRaycasts = false; //讓畫布不會遮擋射線判斷
        group.interactable = false; //讓畫布內的元件無法互動(避免不小心按到按鈕之類的)
        //向player的OnDie事件註冊ShowCanvas函式
    }

    void ShowCanvas()
    {
        //如果這個函式已經被觸發過了
        //中斷(因為這個函式應該只能被觸發一次)
        //已觸發的bool設成true
        //播放名為"GameOver"的音效
        //停止播放"Scene_1_bgm"
        //開始播放勝利畫面的coroutine
    }

    IEnumerator StartShowing()
    {
        //把畫布透明度在0.5秒內漸變成1
        //把時間常數在0.5秒內漸變成0
        //等待現實時間裡的0.5秒
        //讓畫布會遮擋射線判斷
        //讓畫布內的元件可互動
        yield break; //加這行只是因為IEnumerator需要至少一個yield才不會出錯，之後會刪掉
    }
}
