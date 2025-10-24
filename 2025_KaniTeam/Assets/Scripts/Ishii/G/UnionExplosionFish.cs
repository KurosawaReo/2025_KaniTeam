using UnityEngine;
using static Common;

[RequireComponent(typeof(PolygonCollider2D))]
public class UnionExplosionFish : FishBase
{
    [Header("UnionExplosionFish Settings")]
    [SerializeField, Tooltip("爆発力")] private float explosionForce; // 爆発力
    [SerializeField, Tooltip("爆発半径")] private float explosionRadius; // 爆発半径
    [SerializeField, Tooltip("合体フラグ")] private bool unionFlag = false; // 合体フラグ

    protected override void Start()
    {
        base.Start();
        fishType = "UnionFish";
    }





    // 爆発処理
    void Detonate()
    {
        // 爆風の範囲内のオブジェクトを検出
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            // FishBaseコンポーネントを持つオブジェクトに対して爆風を適用
            if (collider.TryGetComponent<FishBase>(out var fish))
            {
                // FishSizeがSmallの魚にのみ影響を与える
                if (fish.fishSize != Common.FishSize.Small) continue;
                ApplyExplosionForce(collider);
                Debug.Log("爆風が " + fish.name + " に影響を与えました。", this);
            }

        }

        // 爆弾オブジェクトを破棄
        //Destroy(gameObject);
        DeleteFish();
    }

    // 吹き飛ばしの処理
    void ApplyExplosionForce(Collider2D targetCollider)
    {
        if (targetCollider.TryGetComponent<Rigidbody2D>(out var targetRigidbody))
        {
            // 爆心からの距離に応じて力を計算
            Vector2 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            // 力を加える
            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
        }
    }


    protected override void OnCollisionEnter2D(Collision2D c)
    {
        base.OnCollisionEnter2D(c);

        if (c.gameObject.TryGetComponent<FishBase>(out var fish) && fish.fishType == "UnionFish" && !unionFlag)
        {
            Debug.Log("合体 : " + name, this);
            unionFlag = true;
            Detonate();
        }
    }

    void DeleteFish()
    {
        rb.simulated = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        transform.position = new Vector3(DELETE_OBJECT_POS_X, DELETE_OBJECT_POS_Y, 0);
    }

}
