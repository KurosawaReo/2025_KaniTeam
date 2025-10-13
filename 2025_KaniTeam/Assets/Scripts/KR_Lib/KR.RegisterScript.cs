/*
   - KR_Lib.RegisterScript -
   ver.2025/09/11
*/
using UnityEngine;

/// <summary>
/// KR_Lib�Ŏg��ScriptableObject�W.
/// </summary>
namespace KR_Lib.RegisterScript
{
    /// <summary>
    /// �g�p����prefab�̃p�[�c��o�^����.
    /// Create > MyTools > MapParts�ŐV�����o����.
    /// </summary>
    [CreateAssetMenu(fileName = "New Parts", menuName = "MyTools/MapParts")]
    public class MapParts : ScriptableObject
    {
        public GameObject[] prefabs; //�����GameObject��o�^�ł���.
    }
}