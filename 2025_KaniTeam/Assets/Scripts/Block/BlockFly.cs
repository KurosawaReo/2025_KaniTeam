/*
   ブロックをくっつけるプログラム.
   担当: 黒澤
*/
using UnityEngine;

/// <summary>
/// 仮, こっちはブロックを渡す側.
/// </summary>
public class BlockFly : FishBase
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
        //予め数を取得する(ループ中に数が変わるため)
        int cnt = transform.childCount;
        //全ての子オブジェクト.
        for (int i = 0; i < cnt; i++)
        {
            var obj = transform.GetChild(0); //先頭のオブジェクトを取得.
            obj.SetParent(c.transform);      //衝突したオブジェクトに移動する.
        }

        Destroy(gameObject); //子オブジェクトを移動し終えたら、親は削除.
    }
}
