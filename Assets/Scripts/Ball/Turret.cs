﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private BallProperty property = null; // 球的屬性
    [SerializeField]
    private CountDown countDown = null; // 遊戲倒數計時器
    // SerializeField讓我們可以在unity介面上替這個變數指派值
    [SerializeField, Range(5, 25)] // Range讓我們們可以在unity介面上用滑桿調整數值
    private float speed = 10; // 砲台左右移動的速度
    private Camera cam; // 主攝像機
    private Vector3 mousePos; // 紀錄滑鼠的世界座標
    private Vector3 direction; // 紀錄砲台應指向的方向向量
    private float fireTime; // 紀錄砲台發射週期
    public static float angle = 90; // 紀錄砲台的旋轉,設static以方便球取得目前的角度

    [SerializeField]
    private Transform muzzle = null; // 發射的位置(槍口)
    [SerializeField]
    private GameObject ball = null; // 拿來發射的球

    [SerializeField]
    private LayerMask trajectoryLayer; // 彈道預測線判斷層
    [SerializeField, Range(5, 30)]
    private float trajectoryLength = 15; // 彈道預測線長度
    private LineRenderer lr; // 線渲染器(用來當彈道預測線)

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; // 獲取主攝像機
        lr = GetComponent<LineRenderer>(); // 獲取線渲染器
        if(property != null)
        {
            // 週期 = 頻率的倒數
            this.fireTime = 1 / this.property.Firerate;
        }
        if(countDown != null) // 如果遊戲倒數計時器不是null
        {
            // 向OnCountDownComplete註冊StartUp函式
            this.countDown.OnCountDownComplete.AddListener(this.StartUp);
            // 讓此物體不呼叫Update/Fixed Update...等等
            enabled = false;
        }
        // shooting!
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void FixedUpdate() // 社課教學不會修改到此函式
    {
        /* 計算砲台的移動 */
        CalculatePosition();
        /* 計算砲台的旋轉 */
        CalculateRotation();
        /* 計算彈道 */
        ForecastTrajectory();
    }

    IEnumerator shoot()
    {
        while(true)
        {
            Instantiate(ball, muzzle.position, Quaternion.identity); // 在槍口位置生成球
            yield return new WaitForSeconds(this.fireTime);
        }
    }

    void CalculatePosition() // 社課教學不會修改到此函式
    {
        Vector2 pos = transform.position; // 得到目前砲台的位置
        if(Input.GetKey(KeyCode.LeftArrow))
            pos += new Vector2(-speed * Time.fixedDeltaTime, 0); // 如果有按左方向鍵，往左移
        if(Input.GetKey(KeyCode.RightArrow))
            pos += new Vector2(speed * Time.fixedDeltaTime, 0); // 如果有按左方向鍵，往右移
        pos = new Vector2(Mathf.Clamp(pos.x, -12, 12), pos.y); // 確保得到的新位置不會壞掉(例如超出邊界)
        transform.position = pos; // 把算出來的位置套用到砲台上
    }

    void CalculateRotation() // 社課教學不會修改到此函式
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // 獲得滑鼠的世界座標
        direction = mousePos - transform.position; // 獲得砲台指向滑鼠座標的向量
        angle = Vector2.Angle(Vector2.right, direction); // 計算砲台應該指向的角度(指向滑鼠)
        angle = Mathf.Clamp(angle, 15, 165); // 確保獲得的角度不會壞掉(例如指到下方)
        transform.rotation = Quaternion.Euler(0, 0, angle); // 把算出來的旋轉套用到砲台上
    }

    void ForecastTrajectory()
    {
        List<Vector3> reflects = new List<Vector3>();
        float remainLength = this.trajectoryLength;
        float reflectAngle = Turret.angle * Mathf.Deg2Rad;
        Vector3 origin = this.muzzle.position;
        Vector3 reflectDir = new Vector2(Mathf.Cos(reflectAngle), Mathf.Sin(reflectAngle));
        // calculate reflection points
        reflects.Add(origin);
        int reflectCount = 0;
        while(remainLength > 0 && reflectCount < 10)
        {
            RaycastHit2D hit = Physics2D.CircleCast(origin, 0.1f, reflectDir, Mathf.Infinity, this.trajectoryLayer);
            // no collision or reach length limit
            if(!hit.collider || hit.distance >= remainLength)
            {
                reflects.Add(origin + reflectDir.normalized * remainLength);
                break;
            }
            Debug.Log(hit.collider.name);
            // add a new reflect point
            remainLength -= hit.distance;
            origin = hit.point + hit.normal.normalized * 0.1f;
            reflects.Add(origin);
            reflectDir = Vector2.Reflect(reflectDir, hit.normal);
            reflectCount++;
        }
        // draw it
        this.lr.positionCount = reflects.Count;
        this.lr.SetPositions(reflects.ToArray());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Buff")) // 如果碰到的東西是Buff的話
        {
            // 修改球的屬性
            this.property.SetValue(collision.GetComponent<BuffCarrier>().buff);
            // 更新發射頻率
            this.fireTime = 1 / this.property.Firerate;
        }
    }

    void StartUp()
    {
        // 讓此物體呼叫 Update/Fixed Update... 等等
        enabled = true;
    }
}