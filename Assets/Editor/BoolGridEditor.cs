using UnityEngine;
using UnityEditor;
using ShotBall.InGame;

[CustomEditor(typeof(BoolGridComponent))]
public class BoolGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BoolGridComponent component = (BoolGridComponent)target;

        // 3x3のボタンを描画
        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                // 現在の値に応じてボタンの色を変更
                GUI.backgroundColor = component.boolGrid.data[i,j] ? Color.white : Color.HSVToRGB(0, 0,0.19f);

                if (GUILayout.Button(component.boolGrid.data[i,j] ? "" : "", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    // ボタンが押されたら値をトグル
                    component.boolGrid.data[i, j] = !component.boolGrid.data[i,j];
                    EditorUtility.SetDirty(component);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        // 元の色に戻す
        GUI.backgroundColor = Color.white;

    }
}
