using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField]
    private CountDown countDown = null; //遊戲倒數計時器
    /*這行以下是生成磚塊和更改磚塊用的參數*/
    [SerializeField]
    private GameObject brick = null;
    private float speed = 0;
    private float timer = -1;
    private int floor = 0;
    private int width = 28;
    private int height;
    private float seed;
    [SerializeField, Range(5, 15)]
    private float addHpTime = 8;
    private float addHptimer = 0;
    private int addHp = 0;
    public Gradient grad;
    [Range(0, 1)]
    public float threshold = 0.5f;
    /*這行以上是生成磚塊和更改磚塊用的參數*/

    private void Start()
    {
        height = (int)Camera.main.orthographicSize; //取得攝像機的視野半高
        seed = Random.Range(0.1f, 100f); //生成一個隨機亂數，讓每次生出來的磚塊有不同的排列組合
        if (countDown != null) //如果遊戲倒數計時器不是null 
        {
            //向 OnCountDownComplete 事件訂閱 StartUp 函式
            this.countDown.OnCountDownComplete.AddListener(this.StartUp);
            //讓此物體不呼叫Update/Fixed Update...等等
            enabled = false;
        }
    }

    private void StartUp()
    {
        //讓此物體呼叫Update/Fixed Update...等等
        enabled = true;
    }

    private void FixedUpdate() //社課教學不會修改到此函式
    {
        /*這段函式負責處理帷幕的落下速度，以及每落下一單位就生一排磚塊出來*/
        transform.position += new Vector3(0, -speed * Time.fixedDeltaTime, 0);
        timer += Time.fixedDeltaTime;
        speed = 0.25f * Mathf.Log10(Mathf.Max(timer, 1)) + 0.4f;

        addHptimer += Time.fixedDeltaTime;
        if (addHptimer >= addHpTime * (addHp + 1))
            addHp++;

        if (transform.position.y < floor)
        {
            SpawnBrick(height - floor);
            floor--;
        }
    }

    private void SpawnBrick(int y) //社課教學不會修改到此函式
    {
        /*這段函式負責生磚塊的邏輯判斷，有興趣的可以去看補充ppt*/
        for (int i = 0; i < width; i++)
        {
            float value = Mathf.PerlinNoise(i / 4f + seed, (y - height) / 4f + seed);
            if (value >= threshold)
            {
                GameObject go = Instantiate(brick, new Vector3(0, 10000, 0), Quaternion.identity, transform);
                go.transform.localPosition = new Vector3(-(width / 2) + 0.5f + i, y + 0.5f);
                go.GetComponentInChildren<SpriteRenderer>().color = grad.Evaluate((value - threshold) / (1 - threshold));
                go.GetComponent<Brick>().hp += addHp;
            }
        }
    }
}