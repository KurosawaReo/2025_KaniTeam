using UnityEngine;
using static Common;

[RequireComponent(typeof(Rigidbody2D))]
public class FishBase : MonoBehaviour
{
    // 動物タワーバトルみたいなゲームを作る
    [Header("Fish Base Settings")]
    [SerializeField, Tooltip("重力")]                  protected float gravity = 0.5f;
    [SerializeField, Tooltip("配置済みフラグ")]         protected bool isSet = false;
    [SerializeField, Tooltip("接触オブジェクトフラグ")]  protected bool isFirstContact = false;
    [SerializeField, Tooltip("魚のサイズ")]             public FishSize fishSize;
    [SerializeField, Tooltip("魚の種類")]               public string fishType;
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

    protected virtual void OnCollisionEnter2D(Collision2D c)
    {
        if ((c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Fish")) && !isFirstContact)
        {
            Debug.Log("オブジェクトと接触", this);
            isFirstContact = true;
        }
    }




}
