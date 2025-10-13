/*
   - KR_Lib.Sound -
   ver.2025/09/13
*/
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�E���h�p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.Sound
{
    /// <summary>
    /// �T�E���h�f�[�^.
    /// </summary>
    [Serializable]
    public struct SoundData
    {
        [SerializeField] string    name;  //�o�^��.
        [SerializeField] AudioClip audio; //�R���|�[�l���g.
        //get.
        public string    Name  { get => name; }
        public AudioClip Audio { get => audio; }
    }

    /// <summary>
    /// SoundManager�p�@�\.
    /// �p�����ăA�^�b�`���邱�ƂŎg�p�ł���.
    /// </summary>
    public class SoundMngKR : MonoBehaviour
    {
        public static SoundMngKR Inst; //���̂�����p.

        [Header("- SoundMngKR -")]
        [Space(4)]
        [SerializeField] AudioSource     audioSource;
        [Space(4)]
        [SerializeField] List<SoundData> bgmData; //BGM�f�[�^�z��.
        [SerializeField] List<SoundData> seData;  //SE �f�[�^�z��.

        Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>(); //BGM�ۑ��p.
        Dictionary<string, AudioClip> seClips  = new Dictionary<string, AudioClip>(); //SE �ۑ��p.

        /// <summary>
        /// SoundMngKR�̏�����.
        /// </summary>
        public void InitSoundMngKR()
        {
            if (Inst == null)
            {
                Inst = this;                   //���̂�ۑ�.
                DontDestroyOnLoad(gameObject); //Scene�ړ����Ă��������Ɏc��.

                RegistSound(); //�T�E���h�o�^.
            }
            else
            {
                Destroy(gameObject); //2�ڈȍ~�͏���.
            }
        }

        /// <summary>
        /// �T�E���h�o�^.
        /// </summary>
        private void RegistSound()
        {
            //BGM�o�^.
            foreach(var i in bgmData)
            {
                bgmClips.Add(i.Name, i.Audio);
            }
            //SE�o�^.
            foreach (var i in seData)
            {
                seClips.Add(i.Name, i.Audio);
            }
        }

        /// <summary>
        /// BGM�Đ�.
        /// </summary>
        /// <param name="name">BGM�o�^��</param>
        public void PlayBGM(string name, bool isLoop)
        {
            //Dictionary����l���擾.
            if (bgmClips.TryGetValue(name, out var bgm)) {
                audioSource.clip = bgm;    //�擾�����T�E���h������.
                audioSource.loop = isLoop; //���[�v�ݒ�.
                audioSource.Play();        //�Đ�.
            }
        }
        /// <summary>
        /// SE�Đ�.
        /// </summary>
        /// <param name="name">SE�o�^��</param>
        public void PlaySE(string name)
        {
            //Dictionary����l���擾.
            if (seClips.TryGetValue(name, out var se)) {
                audioSource.PlayOneShot(se); //�擾�����T�E���h���Đ�.
            }
        }
        /// <summary>
        /// BGM��~.
        /// </summary>
        public void StopBGM()
        {
            audioSource.Stop(); //���ݗ����Ă�BGM���~.
        }
        /// <summary>
        /// ���ʐݒ�.
        /// </summary>
        public void SetVolume(float volume)
        {
            audioSource.volume = Mathf.Clamp(volume, 0f, 1f); //0.0�`1.0�͈̔͂Őݒ�.
        }
    }
}