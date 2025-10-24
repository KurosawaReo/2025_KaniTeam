/*
   『キュウバンコバン』
   ちっちゃいこまごましたブロック。
   ドダイジンベエとくっつく。
   形がさまざまあってジンベエとくっつけないと落ちるようにしたい。

   担当:黒澤
*/
using UnityEngine;

/// <summary>
/// キュウバンコバン クラス.
/// </summary>
public class FishKyubankoban : FishBase
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
        //『ドダイジンベエ』が合体した時に取り入れるため、こっちでは何もしない.
    }
}
