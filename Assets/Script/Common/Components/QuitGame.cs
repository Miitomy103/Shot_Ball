using UnityEngine;

/// <summary>
/// ゲームを終了する処理を提供するクラス。
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// アプリケーションを終了する。エディター上ではプレイモードを停止する。
    /// </summary>
    public void Quit()
    {
        Application.Quit();

        // エディター上で動作確認する場合は、以下のコードを追加
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
