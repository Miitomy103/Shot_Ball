using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public static class JsonBin
{
    // ここにJSONBinで作成したAccess Keyを入れる
    private static string accessKey = "$2a$10$PdzrfbRvgUobxhYwZrdUI.DiVdLshsrkm9KkoOEDUYsYWE5qwMkje";
    private const string baseUrl = "https://api.jsonbin.io/v3/b";

    // ========================
    // Upload（新規作成）
    // ========================
    public static IEnumerator Upload(string jsonString, Action<string> onSuccess, Action<string> onError)
    {
        var uwr = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
        uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("X-Access-Key", accessKey);
        uwr.SetRequestHeader("X-Bin-Private", "false"); // 公開リンクにする場合

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(uwr.error + " / " + uwr.downloadHandler.text);
            yield break;
        }

        try
        {
            var resp = JsonUtility.FromJson<CreateResponse>(uwr.downloadHandler.text);

            if (!string.IsNullOrEmpty(resp?.metadata?.id))
            {
                string idOrUrl = resp.metadata.id;
                string binId = idOrUrl;

                // URLならスラッシュ以降を抜き出す
                int lastSlash = idOrUrl.LastIndexOf('/');
                if (lastSlash >= 0 && lastSlash < idOrUrl.Length - 1)
                {
                    binId = idOrUrl.Substring(lastSlash + 1);
                }

                Debug.Log($"[Upload] Extracted binId = {binId}"); // ← デバッグ出力追加
                onSuccess?.Invoke(binId); // 必ずIDだけ
            }
            else
            {
                onError?.Invoke("No bin ID in response");
            }
        }
        catch (Exception e)
        {
            onError?.Invoke("JSON parse error: " + e.Message + "\n" + uwr.downloadHandler.text);
        }
    }



    // ========================
    // Download（読み込み）
    // ========================
    public static IEnumerator Download(string binId, Action<string> onSuccess, Action<string> onError, bool omitMeta = true)
    {
        string url = $"{baseUrl}/{binId}";
        if (omitMeta) url += "?meta=false";

        var uwr = UnityWebRequest.Get(url);
        uwr.SetRequestHeader("X-Access-Key", accessKey);

        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(uwr.error + " / " + uwr.downloadHandler.text);
            yield break;
        }

        onSuccess?.Invoke(uwr.downloadHandler.text);
    }

    // ========================
    // レスポンス用クラス
    // ========================
    [Serializable]
    private class CreateResponse
    {
        public Metadata metadata;
    }

    [Serializable]
    private class Metadata
    {
        public string id;
        public bool @private;
        public string createdAt;
    }
}
