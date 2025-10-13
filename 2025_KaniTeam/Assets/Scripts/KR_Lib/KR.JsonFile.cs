/*
   - KR_Lib.JsonFile -
   ver.2025/09/11
*/
using UnityEngine;
using System.IO;

/// <summary>
/// Json�t�@�C���������p�̒ǉ��@�\.
/// </summary>
namespace KR_Lib.JsonFile
{
    /// <summary>
    /// JsonFile�萔.
    /// </summary>
    public static class JF_Const
    {
        //Json�t�@�C���̕ۑ��p�X.
        public const string FILE_PATH_EDITOR = "/Jsons/";
        public const string FILE_PATH_BUILD  = "/MySaveData/";
    }

    /// <summary>
    /// JsonFile�֐�.
    /// </summary>
    public static class JF_Func
    {
        /// <summary>
        /// �ۑ�����p�X���擾.
        /// Json�t�@�C�����܂��Ȃ��ꍇ�͐V�K�쐬����.
        /// </summary>
        /// <param name="_fileName">Json�t�@�C����</param>
        /// <returns>�K�؂ȃp�X</returns>
        public static string GetFilePath(string _fileName)
        {
            //Unity�G�f�B�^���s��.
            if (Application.isEditor)
            {
                //Assets�̒��ɕۑ�.
                return Application.dataPath + JF_Const.FILE_PATH_EDITOR + _fileName;
            }
            //�r���h��.
            else
            {
                //OS���Ƃ̈��S�ȏꏊ.
                return Application.persistentDataPath + JF_Const.FILE_PATH_BUILD + _fileName;
            }
        }

        /// <summary>
        /// �t�@�C�������݂��邩.
        /// </summary>
        /// <param name="_fileName">�t�@�C����</param>
        /// <returns>���茋��</returns>
        public static bool IsExistFile(string _fileName)
        {
            return File.Exists(_fileName);
        }

        /// <summary>
        /// Json�t�@�C���̓ǂݍ���.
        /// </summary>
        /// <typeparam name="T">�f�[�^�̌^(class�Ȃ�)</typeparam>
        /// <param name="_path">�t�@�C���̃p�X</param>
        /// <returns>�ǂݍ��񂾃f�[�^</returns>
        public static T LoadJsonFile<T>(string _path)
        {
            try
            {
                string strJson = File.ReadAllText(_path); //�ǂݍ���.
                return JsonUtility.FromJson<T>(strJson);  //�ǂݍ��񂾃f�[�^��Ԃ�.
            }
            //�G���[����.
            catch (IOException error)
            {
                Debug.LogError("[Error] failed to load json file: " + error.Message);

                return default(T); //����null�̂悤�Ȃ��̂�Ԃ�.
            }
        }

        /// <summary>
        /// Json�t�@�C���֕ۑ�(�㏑��)
        /// </summary>
        /// <typeparam name="T">�f�[�^�̌^(class�Ȃ�)</typeparam>
        /// <param name="_data">�ۑ�����f�[�^(�^�͎��R)</param>
        /// <param name="_path">�t�@�C���̃p�X</param>
        public static void SaveJsonFile<T>(T _data, string _path)
        {
            try
            {
                string strJson = JsonUtility.ToJson(_data, true); //�f�[�^��Json�ɕϊ�.
                File.WriteAllText(_path, strJson);                //Json�t�@�C���ɏ�������.
            }
            //�G���[����.
            catch (IOException error)
            {
                Debug.LogError("[Error] failed to save json file: " + error.Message);
            }
        }
    }
}