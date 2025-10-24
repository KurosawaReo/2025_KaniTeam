using UnityEngine;
using KR_Lib.Timer;
using KR_Lib.Object;

/// <summary>
/// 仮, ブロックを生成する.
/// </summary>
public class FishGenerator : MonoBehaviour
{
    [Header("- prefab -")]
    [SerializeField] PrefabKR fish;

    [Header("- value -")]
    [SerializeField] Vector3 spawnPos;

    TimerKR timer = new TimerKR(1.0f); //タイマー作成.

    void Start()
    {

    }

    void Update()
    {
        //タイマー減少.
        timer.TimerDown();
        //一定時間ごと.
        if (timer.IntervalTime())
        {
            var obj = fish.NewPrefab();
            obj.transform.position = spawnPos; //位置設定.
        }
    }
}
