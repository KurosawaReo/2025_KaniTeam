/*
   �u���b�N����������v���O����.
   �S��: ���V
*/
using UnityEngine;

/// <summary>
/// ��, �������̓u���b�N��n����.
/// </summary>
public class BlockFly : BlockBase
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �Փ˂������ɍ쓮.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D c)
    {
        //�ݒu����Ă�u���b�N�Ȃ�.
        if (c.gameObject.CompareTag("block_no_fly"))
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
}
