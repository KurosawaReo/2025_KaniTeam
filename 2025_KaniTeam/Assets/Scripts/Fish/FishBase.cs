/*
   動物タワーバトルみたいなゲームを作る
*/
using UnityEngine;
using static Common;

[RequireComponent(typeof(Rigidbody2D))]
public class FishBase : MonoBehaviour
{
    [Header("Fish Base Settings")]
//  [SerializeField, Tooltip("GameManager")]         protected GManager scptGameMng;
    [SerializeField, Tooltip("重力")]                protected float    gravity   = 0.5f;
    [SerializeField, Tooltip("初期設定フラグ")]      protected bool     isSet     = false;
    [SerializeField, Tooltip("ドロップ済かどうか")]  public    bool     isDropped = false;
    [SerializeField, Tooltip("魚のサイズ")]          public    FishSize fishSize;
    [SerializeField, Tooltip("魚の種類")]            public    string   fishType;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        StartSetting();
    }

    void StartSetting()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
    }

    protected virtual void Update()
    {
        if (!isSet) return;
        Move();
    }

    /// <summary>
    /// 移動処理用. 常に実行される.
    /// </summary>
    protected virtual void Move()
    {
        //継承して使うこと想定.
    }

    /// <summary>
    /// 地面に接触した瞬間に実行される.
    /// </summary>
    protected virtual void HitGround(Collision2D c)
    {
        //継承して使うこと想定.
    }

    /// <summary>
    /// 魚に接触した瞬間に実行される.
    /// </summary>
    protected virtual void HitFish(Collision2D c)
    {
        //継承して使うこと想定.
    }

    /// <summary>
    /// 当たり判定処理.
    /// </summary>
    protected virtual void OnCollisionEnter2D(Collision2D c)
    {
        //地面との接触.
        if (c.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Groundと接触", this);
            HitGround(c);
            isDropped = true; //ドロップ済み.
        }
        //魚との接触.
        if (c.gameObject.CompareTag("Fish"))
        {
            Debug.Log("Fishと接触", this);
            HitFish(c);
            isDropped = true; //ドロップ済み.
        }
    }
}
