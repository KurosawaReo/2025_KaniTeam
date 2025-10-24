using UnityEngine;
using KR_Lib.Timer;
using KR_Lib.Object;

/// <summary>
/// ��, �u���b�N�𐶐�����.
/// </summary>
public class FishGenerator : MonoBehaviour
{
    [Header("- prefab -")]
    [SerializeField] PrefabKR fish;

    [Header("- value -")]
    [SerializeField] Vector3 spawnPos;

    TimerKR timer = new TimerKR(1.0f); //�^�C�}�[�쐬.

    void Start()
    {

    }

    void Update()
    {
        //�^�C�}�[����.
        timer.TimerDown();
        //��莞�Ԃ���.
        if (timer.IntervalTime())
        {
            var obj = fish.NewPrefab();
            obj.transform.position = spawnPos; //�ʒu�ݒ�.
        }
    }
}
