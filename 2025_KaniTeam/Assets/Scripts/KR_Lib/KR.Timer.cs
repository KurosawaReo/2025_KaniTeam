/*
   - KR_Lib.Timer -
   ver.2025/10/03
*/
using UnityEngine;
using System.Collections;
using UE = UnityEngine; //�ʖ��Ŏg����悤��.

/// <summary>
/// ���ԊǗ�������p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Timer
{
    /// <summary>
    /// �����b.
    /// </summary>
    public struct TimerHMS
    {
        public int h;  //��.
        public int m;  //��.
        public int s;  //�b.
        public int cs; //�R���}�b.
    }

    /// <summary>
    /// Timer�֐�.
    /// </summary>
    public static class TM_Func
    {
        /// <summary>
        /// �b�������b�ɕϊ�.
        /// </summary>
        public static TimerHMS TimeToHMS(float _time)
        {
            TimerHMS tm = new TimerHMS();

            //�����b�̌v�Z.
            tm.h = (int)_time / 3600;
            tm.m = (int)_time % 3600 / 60;
            tm.s = (int)_time % 3600 % 60;
            //�R���}�b�̌v�Z.
            tm.cs = (int)((_time - tm.h*3600 - tm.m*60 - tm.s)*100);

            return tm;
        }

        /// <summary>
        /// �J�E���g�_�E����p, 0�b�ɂȂ������ǂ���.
        /// </summary>
        public static bool IsEndTimerHMS(TimerHMS _time)
        {
            return (_time.h <= 0 && _time.m <= 0 &&
                    _time.s <= 0 && _time.cs <= 0);
        }

        /// <summary>
        /// �R���[�`���̒x���p.
        /// </summary>
        /// <param name="_sec">�ҋ@����b��</param>
        public static IEnumerator Delay(float _sec)
        {
            yield return new WaitForSeconds(_sec);
        }
    }

    /// <summary>
    /// �^�C�}�[�Ǘ��p.
    /// </summary>
    public class TimerKR
    {
        private float now;  //�v������.
        private float init; //���Z�b�g����.
        //set, get.
        public float Time { 
            get => now; 
            set => now = value;
        }

        /// <summary>
        /// �R���X�g���N�^.
        /// </summary>
        /// <param name="_initSec">���Z�b�g�b��</param>
        public TimerKR(float _initSec)
        {
            now = init = _initSec;
        }
        /// <summary>
        /// �^�C�}�[���Z�b�g.
        /// </summary>
        public void Init()
        {
            now = init;
        }
        /// <summary>
        /// �^�C�}�[�𑝂₷.
        /// </summary>
        public void TimerUp()
        {
            //�^�C�}�[����(1�b��+1)
            now += UE.Time.deltaTime;
        }
        /// <summary>
        /// �^�C�}�[�����炷.
        /// </summary>
        public void TimerDown()
        {
            //�^�C�}�[����(1�b��-1)
            now -= (now > 0) ? UE.Time.deltaTime : 0;
        }
        /// <summary>
        /// ��莞�Ԃ��Ƃ�true��Ԃ�.
        /// (�ʓrTimerDown�����s����K�v����)
        /// </summary>
        public bool IntervalTime()
        {
            //�^�C�}�[��0�ɂȂ�����.
            if (now <= 0) {
                Init(); //�^�C�}�[���Z�b�g.
                return true;
            }
            else {
                return false;
            }
        }
    }
}