/*
   - KR_Lib.Position -
   ver.2025/10/25
*/
using UnityEngine;

/// <summary>
/// ���W�Ǘ�������p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Position
{
    /// <summary>
    /// �㉺���E.
    /// </summary>
    public struct LBRT
    {
        public float left;
        public float bottom;
        public float right;
        public float top;

        //�R���X�g���N�^.
        public LBRT(float _l, float _b, float _r, float _t)
        {
            left   = _l;
            bottom = _b;
            right  = _r;
            top    = _t;
        }
    }

    /// <summary>
    /// Position�֐�.
    /// </summary>
    public static class PS_Func
    {
        /// <summary>
        /// ��ʂ̏㉺���E�̍��W���擾.
        /// </summary>
        public static LBRT GetWindowLBRT()
        {
            //�����ƉE��̍��W�擾.
            Vector3 lb = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 rt = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

            return new LBRT(lb.x, lb.y, rt.x, rt.y);
        }

        /// <summary>
        /// �ړ��\�͈͓��ɕ␳����.
        /// </summary>
        /// <param name="pos">�I�u�W�F�N�g���W</param>
        /// <param name="size">�I�u�W�F�N�g�T�C�Y</param>
        /// <param name="lim">���E���W(�㉺���E)</param>
        /// <returns>�␳�ύ��W</returns>
        public static Vector3KR FixPosInArea(Vector3KR pos, Vector2 size, LBRT lim)
        {
            if (pos.x < lim.left   + size.x/2) { pos.x = lim.left   + size.x/2; }
            if (pos.y < lim.bottom + size.y/2) { pos.y = lim.bottom + size.y/2; }
            if (pos.x > lim.right  - size.x/2) { pos.x = lim.right  - size.x/2; }
            if (pos.y > lim.top    - size.y/2) { pos.y = lim.top    - size.y/2; }

            return pos;
        }

        /// <summary>
        /// ���[�J�����W�����[���h���W�ɕϊ�.
        /// </summary>
        /// <param name="obj">�e�I�u�W�F�N�g</param>
        /// <param name="lPos">���[�J�����W</param>
        /// <returns>���[���h���W</returns>
        public static Vector2 LPosToWPos(GameObject obj, Vector2 lPos)
        {
            var wPos = obj.transform.TransformPoint(lPos);
            return wPos;
        }

        /// <summary>
        /// ���[���h���W�����[�J�����W�ɕϊ�.
        /// </summary>
        /// <param name="obj">�e�I�u�W�F�N�g</param>
        /// <param name="wPos">���[���h���W</param>
        /// <returns>���[�J�����W</returns>
        public static Vector2 WPosToLPos(GameObject obj, Vector2 wPos)
        {
            var lPos = obj.transform.InverseTransformPoint(wPos);
            return lPos;
        }
    }

    /// <summary>
    /// Vector3�̋@�\�g��.
    /// </summary>
    public class Vector3KR
    {
        private Vector3 vec; //�{�̃f�[�^.

        //x,y,z�̃A�N�Z�X.
        public float x { get => vec.x; set => vec.x = value; }
        public float y { get => vec.y; set => vec.y = value; }
        public float z { get => vec.z; set => vec.z = value; }

        //�R���X�g���N�^.
        public Vector3KR()
            => vec = Vector3.zero;
        public Vector3KR(Vector3 v)
            => vec = v;
        public Vector3KR(float x, float y, float z)
            => vec = new Vector3(x, y, z);

        //get.
        public Vector3 GetVector3() => vec;

        //�u=�v���Z�q�p.
        public static implicit operator Vector3(Vector3KR v) => v.vec;
        public static implicit operator Vector3KR(Vector3 v) => new Vector3KR(v);
        //�u+�v���Z�q�p.
        public static Vector3KR operator+(Vector3KR a, Vector3 b) => new Vector3KR(a.vec + b);
        //�u-�v���Z�q�p.
        public static Vector3KR operator-(Vector3KR a, Vector3 b) => new Vector3KR(a.vec - b);
    }
}