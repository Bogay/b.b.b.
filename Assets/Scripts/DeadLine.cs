using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadLine : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnPlayerEnter = new UnityEvent();
    // [HideInInspector]
    public UnityEvent OnBrickEnter = new UnityEvent();
    public BrickPlayer player = null;

    private void Start()
    {
        if(this.player != null)
        {
            // 向 OnDie 事件註冊 Cancel 函式
            this.player.OnDie.AddListener(this.Cancel);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if(collision.attachedRigidbody.velocity.y < 0)
                Destroy(collision.gameObject); // 如果撞到的東西是球，而且球是向下跑，則把球消滅
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Brick"))
        {
            // 如果撞到的物體是磚塊，觸發 OnBrickEnter 事件
            this.OnBrickEnter.Invoke();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Brick Player"))
        {
            // 如果撞到的物體是磚塊玩家，觸發 OnPlayerEnter 事件
            this.OnPlayerEnter.Invoke();
        }
    }

    private void Cancel()
    {
        // 移除所有 OnBrickEnter 的註冊函式
        this.OnBrickEnter.RemoveAllListeners();
    }
}