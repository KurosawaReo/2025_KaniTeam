using KR_Lib.Inspector;
using KR_Lib.Object;
using KR_Lib.Position;
using KR_Lib.Timer;
using UnityEngine;

public class BackGround : ObjectKR
{
    [Header("- BackGround -")]
    [SerializeField] float move; //�ړ���.

    Vector3KR prevPos = new Vector3KR(); //�����ʒu.

    TimerKR timer = new TimerKR();

    void Start()
    {
        InitObjKR();
        prevPos = transform.position; //�����ʒu�Z�b�g.
    }

    void Update()
    {
        UpdateObjKR();

        timer.TimerUp(); //�^�C�}�[���Z.
        Pos.x = prevPos.x + Mathf.Sin(timer.Time) * move; //���Ɉړ�.
    }
}