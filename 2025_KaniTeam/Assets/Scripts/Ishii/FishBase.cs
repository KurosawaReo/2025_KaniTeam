using UnityEngine;
using static Common;

[RequireComponent(typeof(Rigidbody2D))]
public class FishBase : MonoBehaviour
{
    // 動物タワーバトルみたいなゲームを作る
    [Header("Fish Base Settings")]
    [SerializeField, Tooltip("重力")]                    protected float    gravity        = 0.5f;
    [SerializeField, Tooltip("初期設定フラグ")]          protected bool     isSet          = false;
    [SerializeField, Tooltip("オブジェクト接触フラグ")]  protected bool     isFirstContact = false;
    [SerializeField, Tooltip("魚のサイズ")]              public    FishSize fishSize;
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

    protected virtual void Move()
    {
        // 基本的な移動処理
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

    protected virtual void OnCollisionEnter2D(Collision2D c)
    {
        //地面との接触.
        if (c.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Groundと接触", this);
            HitGround(c);
            isFirstContact = true; //接触済み.
        }
        //魚との接触.
        if (c.gameObject.CompareTag("Fish"))
        {
            Debug.Log("Fishと接触", this);
            HitFish(c);
            isFirstContact = true; //接触済み.
        }
    }
}
