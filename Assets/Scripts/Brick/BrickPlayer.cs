using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BrickPlayer : MonoBehaviour
{
    [SerializeField]
    private DeadLine line = null; //死亡判定線
    [SerializeField]
    private CountDown countDown = null; //遊戲倒數計時器
    [SerializeField]
    private Fall fall = null; //磚塊生成的帷幕
    

    [SerializeField]
    private Transform left, right; //玩家左右底盤的位置
    [SerializeField]
    private LayerMask jumpLayer; //針對跳躍碰撞檢查的層遮罩
    [SerializeField]
    private LayerMask placeLayer; //針對放置磚塊檢查的層遮罩

    [HideInInspector]
    public UnityEvent OnDie = new UnityEvent(); //OnDie事件
    [SerializeField]
    protected TextMeshProUGUI hpText = null; //顯示血量的文字
    [SerializeField]
    private GameObject brick = null; //用來放置的磚塊prefab
    [SerializeField]
    private SpriteRenderer bloomRect = null; //用來提示磚塊放置位置的sprite

    [SerializeField]
    private Rigidbody2D rb; //掛在此物件上的剛體
    private Vector2 velocity; //速度暫存

    private bool canJump = true; //確認玩家能否跳躍的bool
    private float JumpForce = 14; //跳躍力道
    private float lowGravity = 3f; //移動速度
    private float fallGravity = 4.5f, walkVelocity = 7.5f; //重力參數

    private int hp; //生命值 
    private float regenTime; //回血計時器
    private float regenTimer; //回血冷卻
    private int maxHp = 40; //最大血量
    private float maxHpTime = 15; //增加最大血量時間
    private float maxHpTimer; //增加最大血量的計時器

    void Start()
    {
        rb.gravityScale = fallGravity; //修改重力常數
        hp = maxHp; //設定初始血量為當前最大生命值
        hpText.text = hp.ToString(); //顯示目前血量
        regenTimer = 0; //設定初始回血計時器
        maxHpTimer = maxHpTime; //設定增加最大血量的計時器
        if (line != null) //如果死亡判定線不是null
        {
            //向OnPlayerEnter事件訂閱Suicide函式
            //向OnBrickEnter事件訂閱Cancel函式
        }
        if (countDown != null) //如果遊戲倒數計時器不是null
        {
            //向OnCountDownComplete事件訂閱StartUp函式
            //讓此物體不呼叫Update/Fixed Update...等等
        }   
    }

    private void Update() //社課教學不會修改到此函式
    {
        velocity = rb.velocity; //獲取當前速度
        JumpGravity(velocity); //確認重力強度
        CheckCollision(); //檢查玩家跟地面(其他磚塊)的碰撞
        HorizontalMoving(); //偵測水平移動
        Jump(); //偵測垂直移動
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -walkVelocity, walkVelocity), rb.velocity.y); //套用最終速度
    }

    private void LateUpdate() //社課教學不會修改到此函式
    {
        if(fall != null)
            SpawnBrick(); //如果磚塊生成帷幕不是null，進入磚塊生成的判斷函式
    }

    private void FixedUpdate() //社課教學不會修改到此函式
    {
        /*總之這裏主要就是負責計算回血和最大血量的地方*/
        regenTimer -= Time.fixedDeltaTime;
        maxHpTimer -= Time.fixedDeltaTime;
        regenTime = Mathf.Max(0.125f, hp / (maxHp * 2.5f));
        if(regenTimer <= 0)
        {
            hp = Mathf.Min(hp + 1, maxHp);
            hpText.text = hp.ToString();
            regenTimer = regenTime;
        }
        if(maxHpTimer <= 0)
        {
            maxHp += 5;
            maxHpTimer = maxHpTime;
        }
    }

    void SpawnBrick()
    {
        Vector2 spawnPos = Vector2.zero; //初始化一個二維座標當作生成磚塊的位置

        //得到目前玩家的x座標取整數
        //得到目前玩家的y座標取整數
        //得到磚塊帷幕的y座標與世界座標的誤差
        //計算生成磚塊的座標
        //確保算出來的座標跟玩家的y軸位置至少相差1

        RaycastHit2D hitInfo = Physics2D.Raycast(spawnPos, Vector2.zero, 0, placeLayer); //Raycast

        /*此行以下負責更新提示磚塊放置位置圖片的位置以及顏色*/
        /*此行以上負責更新提示磚塊放置位置圖片的位置以及顏色*/

        if (Input.GetKeyDown(KeyCode.S) && hp > 10 && hitInfo.collider == null) //如果滿足這些條件
        {
            //播放名為"Place"的音效
            //生成磚塊
            //把磚塊變成磚塊帷幕的child(讓磚塊隨著帷幕落下)
            //設定磚塊的hp
            //自身損血
            //顯示新的血量
        }
    }

    void HorizontalMoving() //社課教學不會修改到此函式
    {
        int dir = 0; //紀錄方向，只有-1,0,1三種可能。1代表向右、-1代表向左、0不動
        if (Input.GetKey(KeyCode.D)) 
            dir ++; //如果按下D鍵，dir加1
        if (Input.GetKey(KeyCode.A))
            dir --; //如果按下A鍵，dir減1
        rb.AddForce(new Vector2(75 * rb.mass * dir, 0)); //根據dir的數值施力
        if (dir == 0)
            rb.velocity = new Vector2(0.5f * rb.velocity.x, rb.velocity.y); //如果dir為0，讓水平移動的速度慢慢減緩
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && canJump == true) //如果按下W鍵而且此時玩家可以跳躍
        {
            //播放名為"Jump"的音效
            rb.velocity = new Vector2(rb.velocity.x,JumpForce); //讓玩家跳起來
            canJump = false; //此時玩家不能再跳
        }
    }

    void JumpGravity(Vector2 velocity) //社課教學不會修改到此函式
    {
        /*這邊的邏輯簡單來說就是實現"按著跳躍鍵不放會跳得比較高"*/
        if (velocity.y > 0 && Input.GetKey(KeyCode.W)) //如果按著W鍵而且此時玩家Y軸速度大於零
            rb.gravityScale = lowGravity; //修改玩家的重力常數成較低的數值
        else
            rb.gravityScale = fallGravity; //修改玩家的重力常數成較高的數值
    }

    void CheckCollision()
    {
        //獲得玩家左底盤的座標
        //獲得玩家右底盤的座標
        //根據左右底盤座標作circleCast
        //如果circleCast有撞到東西
        //此時玩家可以跳躍
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball")) //如果撞到的東西是球
        {
            hp -= collision.gameObject.GetComponent<Ball>().GetDamage(); //損血
            hpText.text = hp.ToString(); //顯示新的血量
        }
        //如果血量低於零，觸發OnDie事件
    }

    void Suicide()
    {
        //hp歸零
        //顯示新的血量
        //讓此物體不呼叫Update/Fixed Update...等等
        //觸發OnDie事件
    }

    void StartUp()
    {
        //讓此物體呼叫Update/Fixed Update...等等
    }

    void Cancel()
    {
        //讓此物體不呼叫Update/Fixed Update...等等
        //移除所有OnDie事件的訂閱者
    }
}
