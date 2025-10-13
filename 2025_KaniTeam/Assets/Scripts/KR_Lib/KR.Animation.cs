/*
   - KR_Lib.Animation -
   ver.2025/09/13

*/
using UnityEngine;

/// <summary>
/// アニメーション用の追加機能.
/// </summary>
namespace KR_Lib.Animation
{
    /// <summary>
    /// Prefab用.
    /// これを継承したクラスのscriptをアタッチする.
    /// </summary>
    public class ObjAnimKR : MonoBehaviour
    { 
        /// <summary>
        /// アニメーション終了.
        /// </summary>
        public void Delete()
        {
            Destroy(gameObject); //消去.
        }
    }
}