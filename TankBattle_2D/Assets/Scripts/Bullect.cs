using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{

    private readonly float moveSpeed = 10;

    public bool isPlayerBullect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// 触发检测
    /// </summary>
    /// <param name="collision">被碰撞的物体</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                    collision.SendMessage("Die");
                }
                break;
            case "Heart":
                break;
            case "Enemy":

                break;
            case "Wall":
                break;
            case "Barrier":
                break;
            case "AirBarrier":
                break;
            default: break;
        }
    }
}
