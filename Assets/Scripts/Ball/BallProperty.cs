using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//讓我們可以直接在unity介面創建此class的實例
[CreateAssetMenu()]
public class BallProperty : ScriptableObject
{
    // Base Value
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int reflect = 3;
    [SerializeField]
    private float speed = 25f;
    [SerializeField]
    private float firerate = 12.5f;
    // Delta
    private int damageDelta = 0;
    private int reflectDelta = 0;
    private float speedDelta = 1;
    private float firerateDelta = 1;
    // Exposed interface
    public int Damage
    {
        get { return this.damage + this.damageDelta; }
    }
    public int Reflect
    {
        get { return this.reflect + this.reflectDelta; }
    }
    public float Speed
    {
        get { return this.speed * this.speedDelta; }
    }
    public float Firerate
    {
        get { return this.firerate * this.firerateDelta; }
    }

    public void SetValue(BallBuff buff) //更改屬性數值
    {
        this.damageDelta += buff.damage;
        this.reflectDelta += buff.reflect;
        this.speedDelta *= buff.speed;
        this.firerateDelta *= buff.firerate;
    }
}