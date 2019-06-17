using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{

    #region 属性值

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private bool isDeFended = true;
    private float defendTimeVal=3;
    #endregion


    #region 引用

    /// <summary>
    /// 素材引用实例初始化
    /// </summary>
    private SpriteRenderer sr;

    /// <summary>
    /// 坦克图片数组
    /// </summary>
    public Sprite[] tankSprite;  // 上 右 下 左

    /// <summary>
    /// 子弹的预制体
    /// </summary>
    public GameObject bullectPrefab;

    /// <summary>
    /// 爆炸预制体
    /// </summary>
    public GameObject explosionPrefab;

    /// <summary>
    /// 保护特效预制体
    /// </summary>
    public GameObject defendEffectPrefab;

    /// <summary>
    /// 基础操作
    /// </summary>
    private void Awake()
    {
        //拿到素材引用
        sr = GetComponent<SpriteRenderer>();
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //是否处于无敌状态
        if (isDeFended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDeFended = false;
                defendEffectPrefab.SetActive(false);
            }
        }

        //攻击cd
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }


    }

    /// <summary>
    /// 固定物理帧  生命周期函数
    /// 每帧的执行时间固定
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }



    #region 方法

    /// <summary>
    /// 坦克攻击
    /// </summary>
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //空格键为攻击
        {
            //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        }
    }


    /// <summary>
    /// 坦克移动
    /// </summary>
    private void Move()
    {
        //获取用户的水平操作
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)//左
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0) //右
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
        if (h != 0) return;

        //获取用户的垂直操作
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)//上
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0) //下
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
    }


    /// <summary>
    /// 坦克死亡
    /// </summary>
    private void Die()
    {
        if (isDeFended)
        {
            return;
        }
        //产生爆炸
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);

    }



    #endregion

}
