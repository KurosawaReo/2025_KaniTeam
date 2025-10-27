using System.Collections;
using UnityEngine;
using static Common;

public class GManager : MonoBehaviour
{
//  [SerializeField] GameObject[] fishPrefabs;                  // 魚オブジェクトを格納する配列
//  [SerializeField] int settingFish = 0;                       // 配置された魚の数

    [SerializeField] bool ClearCheckStartFlag = false;          // クリアチェック開始フラグ
    [SerializeField] GameState gameState = GameState.Playing;   // ゲーム状態

    [SerializeField] int fishCount;

    /// <summary>
    /// 魚をドロップし終わったサイン.
    /// </summary>
    void FishDropped()
    {

    }

    #region スタート
    void Start()
    {
        //GetAllFish();
    }

#if false
    /// <summary>
    /// 魚オブジェクトを取得する
    /// </summary>
    void GetAllFish()
    {
        //fishタグのついたオブジェクトを取得.
        fishPrefabs = GameObject.FindGameObjectsWithTag("Fish");
    }
#endif

#endregion

    #region アップデート
    void Update()
    {
        switch (gameState) 
        {
            case GameState.Playing:
                // ゲームプレイ中の処理.
                ClearCheck();
                OverCheck();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameClear:
                GameClear();
                break;

            default: Debug.LogError("Error"); break;
        }
    }

    /// <summary>
    /// クリア判定処理
    /// </summary>
    void ClearCheck()
    {
        if (ClearCheckStartFlag) return;

#if false
        int settingFish = 0;

        // クリア判定処理
        foreach (var i in fishPrefabs)
        {
            if (i.TryGetComponent<FishBase>(out var fish))
            {
                // 魚の状態を確認する
                if (fish.isDropped) settingFish++;
            }
        }

        if (settingFish >= fishPrefabs.Length && !ClearCheckStartFlag)
        {
            ClearCheckStartFlag = true;
            StartCoroutine(ClearDelay());
        }
#endif
        
        //全ての魚をドロップし終わったら.
        if (fishCount <= 0 && !ClearCheckStartFlag)
        {
            ClearCheckStartFlag = true;
            StartCoroutine(ClearDelay());
        }
    }

    /// <summary>
    /// ゲームオーバー判定処理
    /// </summary>
    void OverCheck()
    {
        if (gameState == GameState.GameOver) return;

        //全ての魚を取得.
        DragScript[] fishes = FindObjectsByType<DragScript>(FindObjectsSortMode.None);

        // ゲームオーバー判定処理
        foreach (var i in fishes)
        {
            // 魚の状態を確認する
            if (i.isGameOver) {
                gameState = GameState.GameOver;
                break;
            }
        }
    }

    /// <summary>
    /// クリア遅延処理
    /// </summary>
    IEnumerator ClearDelay()
    {
        yield return new WaitForSeconds(3f); //待機.

        if (gameState == GameState.Playing)
            gameState = GameState.GameClear;
    }
#endregion

    #region ゲーム終了処理
    void GameClear()
    {
        // ゲームクリア処理
        Debug.Log("ゲームクリア！", this);
    }

    void GameOver()
    {
        // ゲームオーバー処理
        Debug.Log("ゲームオーバー！", this);
    }
    #endregion
}
