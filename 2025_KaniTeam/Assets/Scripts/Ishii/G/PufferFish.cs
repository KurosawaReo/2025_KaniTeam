using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFish : FishBase
{
    [Header("PufferFish Settings")]
    [SerializeField, Tooltip("地面や他の魚に接触したかどうか")] bool isContact = false;    // 地面や他の魚に接触したかどうか
    [SerializeField, Tooltip("膨らむ速度の調整用変数")]        float inflateRate = 1f;  // 膨らむ速度の調整用変数
    [SerializeField, Tooltip("最大膨張率 Rect")]                   float maxScale = 500.0f;     // 最大膨張率
    [SerializeField, Tooltip("大きいサイズの基準値 Rect")]          float largeSize = 300.0f;     // 大きいサイズの基準値
    [Tooltip("膨らむ前の状態")]                                     RectTransform beforeInflateState;        // 膨らむ前の状態

    protected override void Start()
    {
        base.Start();
        fishSize = Common.FishSize.Small;
        fishType = "PufferFish";
        beforeInflateState = GetComponent<RectTransform>();
    }

    protected override void Update()
    {
        base.Update();
    }



    protected override void Move()
    {
        if (isContact) return;
        // 落下時間に応じて膨らむ
        float scale = beforeInflateState.localScale.x + rb.linearVelocity.y * -inflateRate;
        Debug.Log("Scale : " + scale, this);
        scale = Mathf.Clamp(scale, 1.0f, maxScale);
        Debug.Log("Current Scale : " + scale, this);
        transform.localScale = new Vector3(scale, scale, 0);

        // サイズに応じて魚のサイズを変更
        if (scale >= largeSize)
        {
            fishSize = Common.FishSize.Large;
            Debug.Log("Large Size : " + name, this);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D c)
    {
        base.OnCollisionEnter2D(c);
        if ((c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Fish")) && !isContact)
        {
            Debug.Log("オブジェクトと接触 : " + name, this);
            isContact = true;
        }
    }
}
