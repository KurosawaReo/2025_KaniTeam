using KR_Lib.Inspector;
using KR_Lib.Object;
using KR_Lib.Position;
using KR_Lib.Timer;
using UnityEngine;

public class BackGround : ObjectKR
{
    [Header("- BackGround -")]
    [SerializeField] float move; //移動量.

    Vector3KR prevPos = new Vector3KR(); //初期位置.

    TimerKR timer = new TimerKR();

    void Start()
    {
        InitObjKR();
        prevPos = transform.position; //初期位置セット.
    }

    void Update()
    {
        UpdateObjKR();

        timer.TimerUp(); //タイマー加算.
        Pos.x = prevPos.x + Mathf.Sin(timer.Time) * move; //横に移動.
    }
}