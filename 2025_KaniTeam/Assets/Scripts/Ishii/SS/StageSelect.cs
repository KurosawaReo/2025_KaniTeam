using UnityEngine;
using UnityEngine.SceneManagement;
// UnityEditor 名前空間はエディタ専用機能を使う場合に必要
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StageSelect : MonoBehaviour
{
    // [HideInInspector] 実行時にはこの文字列だけあれば良いのでインスペクタからは隠す
    [HideInInspector]
    [SerializeField] private string[] sceneNames;   // 遷移先シーン名を保持する変数
    [SerializeField] private string TitleSceneName; // タイトルシーン名を保持する変数



    public void SelectStage(int stageIndex)
    {
        // 指定されたインデックスが配列の範囲内か確認
        if (stageIndex >= 0 && stageIndex < sceneNames.Length)
        {
            string sceneToLoad = sceneNames[stageIndex];

            // シーン名が空でないことを確認してからシーンをロード
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("選択されたステージのシーン名が空です。");
            }
        }
        else
        {
            Debug.LogWarning("指定されたステージインデックスが無効です。");
        }
    }

    public void ReturnToTitle()
    {
        if (!string.IsNullOrEmpty(TitleSceneName))
        {
            SceneManager.LoadScene(TitleSceneName);
        }
        else
        {
            Debug.LogWarning("タイトルシーン名が空です。");
        }
    }


















    // #if UNITY_EDITOR ～ #endif で囲まれた部分はエディタ上でのみ有効になる
#if UNITY_EDITOR
    // インスペクタに表示するためのSceneAsset型変数
    [Header("遷移先シーン選択")] // インスペクタに見出しを表示
    [SerializeField] private SceneAsset[] selectStage; // ここにシーンファイルをD&Dする
    [SerializeField] private SceneAsset titleScene; // ここにTitleファイルをD&Dする

    // OnValidateメソッドもエディタ専用
    // インスペクタで値が変更された時などに自動で呼ばれるメソッド
    private void OnValidate()
    {
        // シーン名を保持する配列を、SceneAsset配列と同じ長さで再生成
        sceneNames = new string[selectStage.Length];

        // SceneAsset配列からシーン名を取得して、文字列配列にコピー
        for (int i = 0; i < selectStage.Length; i++)
        {
            if (selectStage[i] != null)
            {
                sceneNames[i] = selectStage[i].name;
            }
        }

        // タイトルシーン名の設定
        if (titleScene != null)
        {
            TitleSceneName = titleScene.name;
        }


    }
#endif
}