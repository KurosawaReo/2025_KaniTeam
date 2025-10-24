using UnityEngine;

public class GManager : MonoBehaviour
{
    [SerializeField] GameObject[] fishPrefabs;

    [SerializeField] bool isGameClear = false;
    [SerializeField] bool isGameOver = false;







    void GameClear()
    {
        // ゲームクリア処理









    }


    void GameOver()
    {
        // ゲームオーバー処理
    }



    void GameEnd()
    {
        // ゲーム終了処理
        if(isGameClear)
        {
            GameClear();
        }
        else if(isGameOver)
        {
            GameOver();
        }

    }










}
