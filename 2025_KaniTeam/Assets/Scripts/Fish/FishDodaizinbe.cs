/*
   『ドダイジンベエ』
   ドダイ用のでかいブロック。
   キュウバンコバンとくっつく。

   担当:黒澤
*/
using UnityEngine;

/// <summary>
/// ドダイジンベエ クラス.
/// </summary>
public class FishDodaizinbe : FishBase
{
    protected override void Start()
    {
        base.Start(); //基底クラスのStart実行.
    }

    protected override void Update()
    {
        base.Update(); //基底クラスのUpdate実行.
    }

    /// <summary>
    /// 魚に接触した瞬間に実行される.
    /// </summary>
    protected override void HitFish(Collision2D c)
    {
        // どのCollider同士が衝突したかを1つずつ確認
        foreach (var contact in c.contacts)
        {
            var myCollider    = contact.collider;      // 自分側のCollider
            var otherCollider = contact.otherCollider; // 相手側のCollider
            
            //『キュウバンコバン』と『ドダイジンベエ』が当たった時.
            if (myCollider.   gameObject.CompareTag("HitKyubankoban") &&
                otherCollider.gameObject.CompareTag("HitDodaizinbe"))
            {
                //全ての子オブジェクト.
                foreach (Transform child in c.transform)
                {
                    child.SetParent(transform); //自分の子へ移動.
                }
                //移動し終えたら、相手の親は削除.
                Destroy(c.gameObject);
            }
        }

    }
}
