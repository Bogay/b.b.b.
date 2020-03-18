using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private LayerMask excludeRfLayer; //球碰到不會減少反彈次數的層遮罩
    [SerializeField]
    private GameObject ballHit; //球碰到物體的撞擊特效
    [SerializeField]
    private BallProperty property; //球的屬性資料
    private Rigidbody2D rb; //2D剛體
    private float speed = 25; //速度
    private int reflect = 3; //反彈
    private int damage = 1; //傷害值

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //從遊戲物件上獲得2D剛體
        float angle = Turret.angle * Mathf.Deg2Rad; //從砲台上獲得發射角度，角度要換成弧度制
        if (property != null)
        {
            //從property上獲得速度
            //從property上獲得傷害值
            //從property上獲得反彈次數
        }
        rb.velocity = speed * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); //套用速度
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //播放名為"Hit"的音效
        if (ballHit != null)
        {
            //生成撞擊特效
        }
        if ((1 << collision.gameObject.layer & excludeRfLayer) == 0) //如果撞到的物體不是磚塊或磚塊玩家
        {
            reflect--; //反彈次數減一
            if (reflect <= 0)
                Destroy(gameObject); //如果反彈次數低於零，銷毀此物件
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //社課教學不會修改到此函式
    {
        rb.velocity = rb.velocity.normalized * speed; //確保碰撞後速度量值維持一致
    }

    public int GetDamage() //社課教學不會修改到此函式
    { 
        return damage; //回傳傷害值
    }
}
