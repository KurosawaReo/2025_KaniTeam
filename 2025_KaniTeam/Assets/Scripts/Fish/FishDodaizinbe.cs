/*
   �w�h�_�C�W���x�G�x
   �h�_�C�p�̂ł����u���b�N�B
   �L���E�o���R�o���Ƃ������B

   �S��:���V
*/
using UnityEngine;

/// <summary>
/// �h�_�C�W���x�G �N���X.
/// </summary>
public class FishDodaizinbe : FishBase
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
        // �ǂ�Collider���m���Փ˂�������1���m�F
        foreach (var contact in c.contacts)
        {
            var myCollider    = contact.collider;      // ��������Collider
            var otherCollider = contact.otherCollider; // ���葤��Collider
            
            //�w�L���E�o���R�o���x�Ɓw�h�_�C�W���x�G�x������������.
            if (myCollider.   gameObject.CompareTag("HitKyubankoban") &&
                otherCollider.gameObject.CompareTag("HitDodaizinbe"))
            {
                //�S�Ă̎q�I�u�W�F�N�g.
                foreach (Transform child in c.transform)
                {
                    child.SetParent(transform); //�����̎q�ֈړ�.
                }
                //�ړ����I������A����̐e�͍폜.
                Destroy(c.gameObject);
            }
        }

    }
}
