/*
   ブロックをくっつけるプログラム.
   担当: 黒澤
*/
using UnityEngine;

/// <summary>
/// 仮, こっちはブロックを渡す側.
/// </summary>
public class BlockFly : BlockBase
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 衝突した時に作動.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D c)
    {
        //設置されてるブロックなら.
        if (c.gameObject.CompareTag("block_no_fly"))
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
}
