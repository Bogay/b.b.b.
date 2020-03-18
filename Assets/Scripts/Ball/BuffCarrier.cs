using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCarrier : MonoBehaviour
{
    public BallBuff buff; //球的屬性增益
    [SerializeField]
    private GameObject text; //顯示增益的文字
    private float fallSpeed = 5; //下落速度

    private void FixedUpdate() //社課教學不會修改到此函式
    {
        transform.position += new Vector3(0, -1, 0) * fallSpeed * Time.fixedDeltaTime; //根據下落速度修改位置
        if (transform.position.y < -10) //如果y軸的位置小於-10(可以想成落到螢幕之下)
            Destroy(gameObject); //銷毀此物件
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Turret")) //如果撞到的東西是Turret
        {
            //播放名為"PowerUp"的音效
            //生成文字
            //銷毀此物件
        }
    }
}
