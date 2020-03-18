using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadLine : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnPlayerEnter = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnBrickEnter = new UnityEvent();
    public BrickPlayer player = null;

    private void Start()
    {
        if (player != null)
        {
            //向OnDie事件註冊Cancel函式
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            if (collision.attachedRigidbody.velocity.y < 0)
                Destroy(collision.gameObject); //如果撞到的東西是球，而且球是向下跑，則把球消滅
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Brick"))
        {
            //如果撞到的物體是磚塊，觸發OnBrickEnter事件
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Brick Player"))
        {
            //如果撞到的物體是磚塊玩家，觸發OnPlayerEnter事件
        }
    }

    private void Cancel()
    {
        //移除所有OnBrickEnter的註冊函式
    }
}
