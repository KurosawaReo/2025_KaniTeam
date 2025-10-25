/*
   - KR_Lib.Inspector -
   ver.2025/10/26

   �Z�b�g�Ŏg�p: KR_Lib.InspectorEditor
   �t�H���_: ���R

   ���Q�l�T�C�g
   https://mu-777.hatenablog.com/entry/2022/09/04/113850
*/
using System;
using UnityEngine;

/// <summary>
/// �C���X�y�N�^�[�p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Inspector
{
    /// <summary>
    /// [InspectorDisable()]�Ŏ��s�����class.
    /// ���̃N���X�ɒl��ۑ����Ă���.
    /// </summary>
    public partial class InspectorDisableAttribute : PropertyAttribute
    {
    //�������o.
        public readonly string varName;          //�ϐ���.
        public readonly Type   varType;          //�ϐ��^.
        public readonly bool   isDisable;        //�������ǂ���.
        public readonly bool   isInvisiOffMode;  //off�Ȃ��\���ɂ��邩.

        public readonly string compStr;      //string�^�̔�r�l.
        public readonly int    compInt;      //int   �^�̔�r�l.
        public readonly float  compFloat;    //float �^�̔�r�l.

        //���R���X�g���N�^.
        /// <summary>
        /// ���C���̃R���X�g���N�^(�S�Ă̌^�Ή�)
        /// </summary>
        private InspectorDisableAttribute(
            string _varName, Type _varType, bool _isDisable = false, bool _isInvisi = false
        ){
            varName         = _varName;
            varType         = _varType;
            isDisable       = _isDisable;
            isInvisiOffMode = _isInvisi;
        }
        /// <summary>
        /// bool�^.
        /// </summary>
        /// <param name="_varName">�ϐ���</param>
        /// <param name="_isReverseCondi">�����𔽓]�����邩</param>
        /// <param name="_isInvisi">off�Ȃ��\���ɂ��邩</param>
        public InspectorDisableAttribute(
            string _varName, bool _isReverseCondi = false, bool _isInvisi = false
        )
            : this(_varName, typeof(bool), _isReverseCondi, _isInvisi) //���g�̃R���X�g���N�^��.
        {}
        /// <summary>
        /// string�^.
        /// </summary>
        /// <param name="_varName">�ϐ���</param>
        /// <param name="_value">�l</param>
        /// <param name="_isReverseCondi">false�Ȃ�"value��v", true�Ȃ�"value�s��v"</param>
        /// <param name="_isInvisi">off�Ȃ��\���ɂ��邩</param>
        public InspectorDisableAttribute(
            string _varName, string _value, bool _isReverseCondi = false, bool _isInvisi = false
        )
            : this(_varName, _value.GetType(), _isReverseCondi, _isInvisi) //���g�̃R���X�g���N�^��.
        {
            compStr = _value;
        }
        /// <summary>
        /// int�^.
        /// </summary>
        /// <param name="_varName">�ϐ���</param>
        /// <param name="_value">�l</param>
        /// <param name="_isReverseCondi">false�Ȃ�"value��v", true�Ȃ�"value�s��v"</param>
        /// <param name="_isInvisi">off�Ȃ��\���ɂ��邩</param>
        public InspectorDisableAttribute(
            string _varName, int _value, bool _isReverseCondi = false, bool _isInvisi = false
        )
            : this(_varName, _value.GetType(), _isReverseCondi, _isInvisi) //���g�̃R���X�g���N�^��.
        {
            compInt = _value;
        }
        /// <summary>
        /// float�^.
        /// </summary>
        /// <param name="_varName">�ϐ���</param>
        /// <param name="_value">�l</param>
        /// <param name="_isGreaterCondi">false�Ȃ�"value�ȉ�", true�Ȃ�"value�ȏ�"</param>
        /// <param name="_isInvisi">off�Ȃ��\���ɂ��邩</param>
        public InspectorDisableAttribute(
            string _varName, float _value, bool _isGreaterCondi = true, bool _isInvisi = false
        )
            : this(_varName, _value.GetType(), _isGreaterCondi, _isInvisi) //���g�̃R���X�g���N�^��.
        {
            compFloat = _value;
        }
    }
}