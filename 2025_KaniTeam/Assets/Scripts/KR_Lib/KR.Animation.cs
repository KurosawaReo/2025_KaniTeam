/*
   - KR_Lib.Animation -
   ver.2025/09/13

*/
using UnityEngine;

/// <summary>
/// �A�j���[�V�����p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Animation
{
    /// <summary>
    /// Prefab�p.
    /// ������p�������N���X��script���A�^�b�`����.
    /// </summary>
    public class ObjAnimKR : MonoBehaviour
    { 
        /// <summary>
        /// �A�j���[�V�����I��.
        /// </summary>
        public void Delete()
        {
            Destroy(gameObject); //����.
        }
    }
}