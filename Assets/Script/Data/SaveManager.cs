

using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public static class SaveManager
{
    // フルパスを取得（ファイル名を指定可能）
    private static string GetSavePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static void Save(string fileName, string json, Texture2D texture)
    {
        // 画像をBase64に変換
        byte[] imgBytes = texture.EncodeToPNG();
        string base64Img = System.Convert.ToBase64String(imgBytes);

        // セットにする
        SaveData data = new SaveData
        {
            playerJson = json,
            imageBase64 = base64Img
        };

        // JSON化 → 暗号化
        string saveJson = JsonUtility.ToJson(data);
        SecureSave.SaveEncrypted(GetSavePath(fileName), saveJson);

        Debug.Log("保存完了: " + GetSavePath(fileName));
    }

    public static (string, Texture2D) Load(string fileName)
    {
        string loaded = SecureSave.LoadEncrypted(GetSavePath(fileName));
        if (string.IsNullOrEmpty(loaded)) return (null, null);

        // SaveDataに復元
        SaveData data = JsonUtility.FromJson<SaveData>(loaded);

        // 画像復元
        byte[] imgBytes = System.Convert.FromBase64String(data.imageBase64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imgBytes);

        return (data.playerJson, tex);
    }

    public static Texture2D ChangeDataImage(string base64)
    {
        byte[] imgBytes = System.Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imgBytes);
        return tex;
    }

    /// <summary>
    /// セーブデータを削除する
    /// </summary>
    public static bool Delete(string fileName)
    {
        Debug.Log("削除実行: " + fileName);
        string path = GetSavePath("StageData."+fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("削除完了: " + path);
            return true;
        }
        else
        {
            Debug.Log("削除対象が存在しません: " + path);
            return false;
        }
    }
}


[System.Serializable]
public class SaveData
{
    public string playerJson;  // JSON文字列
    public string imageBase64; // 画像をBase64化した文字列
}


public static class SecureSave
{
    // 任意の文字列で設定可能
    private static readonly string UserKey = "sousouno";    // Keyとして使う文字列
    private static readonly string UserIV = "furirenn";    // IVとして使う文字列


    // 任意の文字列をAES用Key（32バイト）に変換
    private static byte[] GenerateAesKey(string key)
    {
        using (SHA256 sha = SHA256.Create())
        {
            return sha.ComputeHash(Encoding.UTF8.GetBytes(key)); // 32バイト固定
        }
    }

    // 任意の文字列をAES用IV（16バイト）に変換
    private static byte[] GenerateAesIV(string iv)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(iv));
            byte[] ivBytes = new byte[16];          // AES IVは16バイト
            Array.Copy(hash, ivBytes, 16);
            return ivBytes;
        }
    }

    // 暗号化して保存
    public static void SaveEncrypted(string fullPath, string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = GenerateAesKey(UserKey);
            aes.IV = GenerateAesIV(UserIV);

            using (var ms = new MemoryStream())
            using (var encryptor = aes.CreateEncryptor())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Flush();
                cs.FlushFinalBlock();
                File.WriteAllBytes(fullPath, ms.ToArray()); // ← SavePath()を削除
            }
        }
    }

    // 復号して読み込み
    public static string LoadEncrypted(string fullPath)
    {
        if (!File.Exists(fullPath)) return null;
        byte[] cipherBytes = File.ReadAllBytes(fullPath);

        using (Aes aes = Aes.Create())
        {
            aes.Key = GenerateAesKey(UserKey);
            aes.IV = GenerateAesIV(UserIV);

            using (var ms = new MemoryStream(cipherBytes))
            using (var decryptor = aes.CreateDecryptor())
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }

}
public static class StageDataLoader
{
    /// <summary>
    /// 指定フォルダ内のすべてのステージファイルを復号して読み込む
    /// </summary>
    /// <param name="folderPath">保存フォルダのパス</param>
    /// <param name="searchPattern">ファイル名パターン、例："StageData.*"</param>
    /// <returns>ファイル名と中身のデータをまとめたリスト</returns>
    public static List<(string fileName, SaveData data)> LoadAllStages(string folderPath, string searchPattern = "StageData.*")
    {
        List<(string, SaveData)> result = new List<(string, SaveData)>();

        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning($"フォルダが存在しません: {folderPath}");
            return result;
        }

        // フォルダ内のすべてのファイルを検索
        string[] files = Directory.GetFiles(folderPath, searchPattern);

        foreach (string filePath in files)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);

                // 復号して読み込み
                string decryptedJson = SecureSave.LoadEncrypted(filePath);
                if (string.IsNullOrEmpty(decryptedJson))
                {
                    Debug.LogWarning($"復号失敗: {fileName}");
                    continue;
                }
                string f = Path.GetFileNameWithoutExtension(filePath); // StageData.StageA
                string[] parts = fileName.Split('.');
                if (parts.Length > 1)
                {
                    fileName = parts[1]; // StageA
                }
                SaveData data = JsonUtility.FromJson<SaveData>(decryptedJson);
                result.Add((fileName, data));
            }
            catch (Exception e)
            {
                Debug.LogError($"読み込み失敗: {filePath}\n{e}");
            }
        }

        return result;
    }
}





