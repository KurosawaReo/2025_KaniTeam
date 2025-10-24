using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Common;

public class GManager : MonoBehaviour
{
    [SerializeField] GameObject[] fishPrefabs;                  // 魚オブジェクトを格納する配列

    [SerializeField] bool ClearCheckStartFlag = false;          // クリアチェック開始フラグ


    [SerializeField] int settingFish = 0;                       // 配置された魚の数
    [SerializeField] GameState gameState = GameState.Playing;   // ゲーム状態





    #region スタート
    void Start()
    {
        GetFish();
    }

    /// <summary>
    /// 魚オブジェクトを取得する
    /// </summary>
    void GetFish()
    {
        // タグが同じオブジェクトを全て取得する
        fishPrefabs = GameObject.FindGameObjectsWithTag("Fish");

        foreach (GameObject fish in fishPrefabs)
        {
            Debug.Log(fish.name, this);
        }
    }
    #endregion

    #region アップデート
    void Update()
    {
        if (gameState == GameState.GameClear)
        {
            GameClear();
            return;
        }
        else if (gameState == GameState.GameOver)
        {
            GameOver();
            return;
        }
        else if (gameState == GameState.Playing)
        {
            // ゲームプレイ中の処理
            ClearCheck();
            OverCheck();
        }

    }



    /// <summary>
    /// クリア判定処理
    /// </summary>
    void ClearCheck()
    {
        if (ClearCheckStartFlag) return;

        // クリア判定処理
        for (int i = 0; i < fishPrefabs.Length; i++)
        {
            if (fishPrefabs[i].TryGetComponent<FishBase>(out var fish))
            {
                // 魚の状態を確認する
                if (fish.isDropped)
                    settingFish++;
            }
        }

        if (settingFish >= fishPrefabs.Length && !ClearCheckStartFlag)
        {
            ClearCheckStartFlag = true;
            StartCoroutine(ClearDelay());
        }
        settingFish = 0;
    }

    /// <summary>
    /// ゲームオーバー判定処理
    /// </summary>
    void OverCheck()
    {
        if (gameState == GameState.GameOver) return;

        // ゲームオーバー判定処理
        for (int i = 0; i < fishPrefabs.Length; i++)
        {
            if (fishPrefabs[i].TryGetComponent<DragScript>(out var fish))
            {
                // 魚の状態を確認する
                if (fish.isGameOver)
                {
                    gameState = GameState.GameOver;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// クリア遅延処理
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearDelay()
    {
        yield return new WaitForSeconds(3f);
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
