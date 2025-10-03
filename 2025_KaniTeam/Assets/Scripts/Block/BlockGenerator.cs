using UnityEngine;
using KR_Lib.Timer;
using KR_Lib.Object;

/// <summary>
/// ��, �u���b�N�𐶐�����.
/// </summary>
public class BlockGenerator : MonoBehaviour
{
    [Header("- prefab -")]
    [SerializeField] PrefabKR block;

    [Header("- value -")]
    [SerializeField] Vector3 spawnPos;

    TimerKR timer = new TimerKR(1.0f);

    void Start()
    {
        
    }

    void Update()
    {
        timer.TimerDown();
        //��莞�Ԃ���.
        if (timer.IntervalTime())
        {
            var obj = block.NewPrefab();
            obj.transform.position = spawnPos; //�ʒu�ݒ�.
        }
    }
}
