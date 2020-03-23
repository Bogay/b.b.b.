using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private int seed; // 隨機種子碼
    [SerializeField]
    private GameObject[] buffs;
    // 這個磚塊被打爆後會生成的buff
    private GameObject buffToSpawn = null;
    [SerializeField]
    private GameObject destroy; // 磚塊被打爆的特效
    public int hp = 10; // 生命值 
    [SerializeField]
    private TextMeshProUGUI hpText = null; // 顯示血量的文字

    private void Start() // 社課教學不會修改到此函式
    {
        float rand = Random.Range(0f, 1f);
        seed = Mathf.FloorToInt(rand * 200); // 生成隨機種子碼
        if(seed < buffs.Length)
            buffToSpawn = buffs[seed]; // 根據種子碼決定要生成的buff
        hpText.text = hp.ToString(); // 顯示血量
    }

    private void Update() // 社課教學不會修改到此函式
    {
        if(transform.position.y < -10)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball")) // 如果撞到的東西是球
        {
            hp -= collision.gameObject.GetComponent<Ball>().GetDamage(); // 損血
            hpText.text = hp.ToString(); // 顯示新的血量
        }
        // 如果血量歸零
        if(hp <= 0)
        {
            // 生成buff
            if(buffToSpawn != null)
                Instantiate(this.buffToSpawn, transform.position, Quaternion.identity);
            // 此段以下負責生成粒子特效並播放
            if(destroy != null)
            {
                /*實作待補*/
            }
            // 播放名為 "Destroy" 的音效
            SceneAudioManager.instance.PlayByName("Destroy");
            // 銷毀此物件
            Destroy(gameObject);
        }
    }
}