/*
   �u���b�N����������v���O����.
   �S��: ���V
*/
using UnityEngine;

/// <summary>
/// ��, �������̓u���b�N��n����.
/// </summary>
public class BlockFly : FishBase
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
        //�\�ߐ����擾����(���[�v���ɐ����ς�邽��)
        int cnt = transform.childCount;
        //�S�Ă̎q�I�u�W�F�N�g.
        for (int i = 0; i < cnt; i++)
        {
            var obj = transform.GetChild(0); //�擪�̃I�u�W�F�N�g���擾.
            obj.SetParent(c.transform);      //�Փ˂����I�u�W�F�N�g�Ɉړ�����.
        }

        Destroy(gameObject); //�q�I�u�W�F�N�g���ړ����I������A�e�͍폜.
    }
}
