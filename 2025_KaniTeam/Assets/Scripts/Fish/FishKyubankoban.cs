/*
   �w�L���E�o���R�o���x
   �������Ⴂ���܂��܂����u���b�N�B
   �h�_�C�W���x�G�Ƃ������B
   �`�����܂��܂����ăW���x�G�Ƃ������Ȃ��Ɨ�����悤�ɂ������B

   �S��:���V
*/
using UnityEngine;

/// <summary>
/// �L���E�o���R�o�� �N���X.
/// </summary>
public class FishKyubankoban : FishBase
{
    protected override void Start()
    {
        base.Start(); //���N���X��Start���s.
    }

    protected override void Update()
    {
        base.Update(); //���N���X��Update���s.
    }

    /// <summary>
    /// ���ɐڐG�����u�ԂɎ��s�����.
    /// </summary>
    protected override void HitFish(Collision2D c)
    {
        //�w�h�_�C�W���x�G�x�����̂������Ɏ�����邽�߁A�������ł͉������Ȃ�.
    }
}
