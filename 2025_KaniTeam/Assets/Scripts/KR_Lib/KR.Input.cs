/*
   - KR_Lib.Input -
   ver.2025/10/11
*/
using System;
using Unity.Burst;
using UnityEngine;
using UnityEngine.InputSystem;
using UE = UnityEngine; //�ʖ��Ŏg����悤��.

/// <summary>
/// ���W�Ǘ�������p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Input
{
    /// <summary>
    /// �}�E�X�{�^������p.
    /// </summary>
    public enum MouseID
    { 
        Left,       //���N���b�N.
        Right,      //�E�N���b�N.
        Middle,     //�z�C�[��.
        SideFront,  //���̎�O.
        SideBack,   //���̉�.
    }

    /// <summary>
    /// Input�֐�.
    /// </summary>
    public static partial class IN_Func
    {
        /// <summary>
        /// �}�E�X�N���b�N����.
        /// </summary>
        public static bool IsPushMouse(MouseID id)
        {
            return UE.Input.GetMouseButton((int)id);
        }
        /// <summary>
        /// �}�E�X�N���b�N����.
        /// </summary>
        public static bool IsPushMouseDown(MouseID id)
        {
            return UE.Input.GetMouseButtonDown((int)id);
        }
        /// <summary>
        /// �}�E�X�N���b�N����.
        /// </summary>
        public static bool IsPushMouseUp(MouseID id)
        {
            return UE.Input.GetMouseButtonUp((int)id);
        }

        /// <summary>
        /// �}�E�X���W�擾.
        /// </summary>
        public static Vector2 GetMousePos()
        {
            Vector2 mPos = UE.Input.mousePosition;
            Vector2 wPos = Camera.main.ScreenToWorldPoint(mPos);

            return wPos;
        }
        /// <summary>
        /// �㉺���E�̑���擾.
        /// </summary>
        public static Vector2 GetMove4dir()
        {
            Vector2 input = new Vector2(
                UE.Input.GetAxisRaw("Horizontal"), //������������.
                UE.Input.GetAxisRaw("Vertical")    //������������.
            );
            return input;
        }
    }

    /// <summary>
    /// Input�Ǘ�(InputSystem�p)
    /// </summary>
    public class InputMngKR
    {
        //InputAction�t�@�C��.
        InputActionAsset actionFile;
        //���͂����������ɌĂ΂��֐�.
        event Action<string, InputAction.CallbackContext> onActionEvent;
        //�֐��A�h���X�ۑ��p.
        Action<string, InputAction.CallbackContext> func;

        /// <summary>
        /// �R���X�g���N�^.
        /// </summary>
        /// <param name="_actionFile">InputAction�t�@�C��</param>
        /// <param name="_func">�֐��A�h���X</param>
        public InputMngKR(InputActionAsset _actionFile, Action<string, InputAction.CallbackContext> _func)
        {
            onActionEvent += _func;

            actionFile = _actionFile;
            func       = _func;

            Enable(); //�L���ɂ��鏈��.
        }

        /// <summary>
        /// �L���ɂ���.
        /// </summary>
        private void Enable()
        {
            //�S�Ă�actionMaps��actions�����[�v.
            foreach (var i in actionFile.actionMaps)
            {
                foreach (var j in i.actions)
                {
                    j.Enable();
                    //�u?.Invoke�v= �����o�^����Ă�����Ă�.
                    j.performed += ctx => onActionEvent?.Invoke(j.name, ctx);
                    j.canceled  += ctx => onActionEvent?.Invoke(j.name, ctx);
                }
            }
        }
        /// <summary>
        /// �����ɂ���.
        /// </summary>
        public void Disable()
        {
            //�S�Ă�actionMaps��actions�����[�v.
            foreach (var i in actionFile.actionMaps)
            {
                foreach (var j in i.actions)
                {
                    j.Disable();
                }
            }
            onActionEvent -= func; //�o�^���Ă����֐������.
        }
    }
}