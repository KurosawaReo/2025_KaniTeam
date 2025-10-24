using UnityEngine;
using KR_Lib.Timer;
using KR_Lib.Object;
using System.Collections.Generic;

/// <summary>
/// テスト用, 魚を生成する.
/// </summary>
public class FishGenerator : MonoBehaviour
{
    [Header("- prefab -")]
    [SerializeField]         List<PrefabKR> fish; //魚's.
    [SerializeField, Min(0)] int fishNo;          //何番目の魚を使うか.

    [Header("- value -")]
    [SerializeField] Vector3 spawnPos;

//  TimerKR timer = new TimerKR(1.0f); //タイマー作成.

    void Start()
    {

    }

    void Update()
    {
        //タイマー減少.
//      timer.TimerDown();
        //一定時間ごと.
//      if (timer.IntervalTime())

        //デバッグ用.
        if (Input.GetKeyDown(KeyCode.M))
        {
            var obj = fish[fishNo].NewPrefab();
            obj.transform.position = spawnPos; //位置設定.
        }
    }
}
