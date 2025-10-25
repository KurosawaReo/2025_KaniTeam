/*
   - KR_Lib.Tap -
   ver.2025/10/25
*/
using UnityEngine;
using KR_Lib.Timer;
using KR_Lib.Input;

/// <summary>
/// �^�b�v�Ǘ�������p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Tap
{
    /// <summary>
    /// �^�b�v�������ǂ������Ǘ�����.
    /// </summary>
    public class TapKR
    {
        private bool    isError;         //�G���[�`�F�b�N�p.

        private bool    isTap;           //�^�b�v���Ă��邩.
        private bool    isDblTap;        //�_�u���^�b�v���Ă��邩.
        private bool    isSwiped;        //�X���C�v�ς�.
        private bool    isSwipeJudged;   //�X���C�v����ς�.

        private TimerKR tmDblTap;        //�_�u���^�b�v�P�\�v���p.
        private TimerKR tmSwipe;         //�X���C�v�@�@�P�\�v���p.

        private Vector2 startTapPos;     //�^�b�v�J�n�ʒu.
        private Vector2 endTapPos;       //�^�b�v�I���ʒu.

        private Vector2 swipeVec;        //�X���C�v��������.
        private float   swipePow;        //�X���C�v��������.

        //get.
        public bool    IsTap       { get => isTap; }
        public bool    IsDblTap    { get => isDblTap; }
        public Vector2 StartTapPos { get => startTapPos; }
        public Vector2 EndTapPos   { get => endTapPos; }
        public Vector2 SwipeVec    { get => swipeVec; }
        public float   SwipePow    { get => swipePow; }

        /// <summary>
        /// �R���X�g���N�^.
        /// </summary>
        /// <param name="_timeDblTap">�_�u���^�b�v���莞��</param>
        /// <param name="_timeSwipe">�X���C�v���莞��</param>
        public TapKR(float _timeDblTap, float _timeSwipe)
        {
            tmDblTap = new TimerKR(_timeDblTap);
            tmSwipe  = new TimerKR(_timeSwipe);

            isError = true; //�ŏ��̓G���[����ON.
        }

        /// <summary>
        /// �^�b�v�f�[�^�̍X�V.
        /// </summary>
        public void Update()
        {
            OffErrorMode();

            tmDblTap.TimerDown(); //�^�C�}�[����.

            //�^�b�v���ŁA�܂��X���C�v���������Ă��Ȃ����.
            if (isTap && !isSwiped) 
            {
                tmSwipe.TimerDown(); //�^�C�}�[����.
                //�X���C�v�P�\���I�������.
                if (tmSwipe.Time <= 0)
                {
                    SwipeJudge(); //�^�b�v�I������O�ɔ�����s��.
                }
            }
        }

        /// <summary>
        /// �������ɂ�������s������.
        /// </summary>
        public void TapDown()
        {
            CheckError();

            isTap    = true;  //�^�b�v�J�n.
            isSwiped = false; //�X���C�v����������.

            tmSwipe.Reset();                     //�v���J�n.
            startTapPos = IN_Func.GetMousePos(); //���W�ۑ�.

            //��莞�ԓ��ɉ����Ă����.
            if (tmDblTap.Time > 0) {
                isDblTap = true;   //�_�u���^�b�v����.
                tmDblTap.Time = 0; //���Z�b�g.
            }
            else {
                tmDblTap.Reset(); //�v���J�n.
            }
        }

        /// <summary>
        /// ���㎞�ɂ�������s������.
        /// </summary>
        public void TapUp()
        {
            CheckError();

            isTap         = false; //�^�b�v�I��.
            isDblTap      = false; //�_�u���^�b�v�I��.
            isSwipeJudged = false; //�Ĕ���\��.

            endTapPos = IN_Func.GetMousePos(); //���W�ۑ�.

            //�X���C�v�P�\���c���Ă���(=���肪�܂��Ȃ�)
            if (tmSwipe.Time > 0)
            {
                SwipeJudge(); //�����ŃX���C�v����.
            }
        }

        /// <summary>
        /// �X���C�v�����������1�x�؂��true�ɂȂ�.
        /// </summary>
        public bool IsSwipedOnce()
        {
            if (isSwiped)
            {
                isSwiped = false; //�ȍ~��false��.
                return true;      //�X���C�v����.
            }
            return false; //�X���C�v������.
        }

        /// <summary>
        /// �X���C�v����.
        /// </summary>
        private void SwipeJudge()
        {
            //�X���C�v���肵�ĂȂ��Ȃ�.
            if (!isSwipeJudged)
            {
                //���ݒn�ƊJ�n�ʒu�̍�.
                Vector2 dis = IN_Func.GetMousePos() - startTapPos;
                //�ړ����Ă����.
                if (dis != Vector2.zero)
                {
                    swipeVec = dis.normalized; //�X���C�v����.
                    swipePow = dis.magnitude;  //�X���C�v��.
                }
                //�S�������ʒu�Ȃ�.
                else
                {
                    swipeVec = Vector2.zero;
                    swipePow = 0;
                }
                //�X���C�v�ς�.
                isSwiped = true;
                isSwipeJudged = true;
            }
        }

        /// <summary>
        /// �G���[�`�F�b�N.
        /// </summary>
        private void CheckError()
        {
            //�G���[���b�Z�[�W.
            if (isError) { Debug.LogError("[MyTap] Update�֐������s����Ă��܂���"); }
        }
        /// <summary>
        /// �G���[��������.
        /// </summary>
        private void OffErrorMode()
        {
            //Update�����s�����΃G���[�ɂ��Ȃ�.
            if (isError) { isError = false; }
        }
    }
}