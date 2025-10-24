using UnityEngine;
using KR_Lib.Timer;
using KR_Lib.Object;
using System.Collections.Generic;

/// <summary>
/// �e�X�g�p, ���𐶐�����.
/// </summary>
public class FishGenerator : MonoBehaviour
{
    [Header("- prefab -")]
    [SerializeField]         List<PrefabKR> fish; //��'s.
    [SerializeField, Min(0)] int fishNo;          //���Ԗڂ̋����g����.

    [Header("- value -")]
    [SerializeField] Vector3 spawnPos;

//  TimerKR timer = new TimerKR(1.0f); //�^�C�}�[�쐬.

    void Start()
    {

    }

    void Update()
    {
        //�^�C�}�[����.
//      timer.TimerDown();
        //��莞�Ԃ���.
//      if (timer.IntervalTime())

        //�f�o�b�O�p.
        if (Input.GetKeyDown(KeyCode.M))
        {
            var obj = fish[fishNo].NewPrefab();
            obj.transform.position = spawnPos; //�ʒu�ݒ�.
        }
    }
}
