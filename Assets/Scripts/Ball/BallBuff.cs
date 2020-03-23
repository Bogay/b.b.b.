using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 讓我們可以直接在unity介面創建此class的實例
[CreateAssetMenu()]
public class BallBuff : ScriptableObject
{
    // 球的傷害 (add)
    public int damage = 0;
    // 反彈次數 (add)
    public int reflect = 0;
    // 球的速度 (mul)
    public float speed = 1;
    // 砲台發射頻率 (mul)
    public float firerate = 1;
}