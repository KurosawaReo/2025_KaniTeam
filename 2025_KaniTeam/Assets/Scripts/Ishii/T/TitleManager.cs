using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


// UnityEditor 名前空間はエディタ専用機能を使う場合に必要
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleManager : MonoBehaviour
{
    // [HideInInspector] 実行時にはこの文字列だけあれば良いのでインスペクタからは隠す
    [HideInInspector]
    [SerializeField] private string SelectSceneName; // タイトルシーン名を保持する変数

    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider slider;



    public void NextScene()
    {
        if (!string.IsNullOrEmpty(SelectSceneName))
        {
            SceneManager.LoadScene(SelectSceneName);
        }
        else
        {
            Debug.LogWarning("タイトルシーン名が空です。");
        }
    }

    IEnumerator LoadScene()
    {
        loadingUI.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(SelectSceneName);
        while (!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }
    }













    // #if UNITY_EDITOR ～ #endif で囲まれた部分はエディタ上でのみ有効になる
#if UNITY_EDITOR
    // インスペクタに表示するためのSceneAsset型変数
    [Header("遷移先シーン選択")] // インスペクタに見出しを表示
    [SerializeField] private SceneAsset selectScene; // ここにTitleファイルをD&Dする

    // OnValidateメソッドもエディタ専用
    // インスペクタで値が変更された時などに自動で呼ばれるメソッド
    private void OnValidate()
    {
        // selectSceneにシーンが設定されていれば、その名前をSelectSceneNameに代入
        if (selectScene != null)
        {
            SelectSceneName = selectScene.name;
        }
        else
        {
            SelectSceneName = string.Empty;
        }
    }
#endif
}
