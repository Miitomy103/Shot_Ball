using UnityEngine;
using UnityEditor;
using ShotBall.InGame;

[CustomEditor(typeof(BoolGridComponent))]
public class BoolGridEditor : Editor
{
    private const float ButtonSize = 20f;
    private const float DisabledCellBrightness = 0.19f;

    public override void OnInspectorGUI()
    {
        BoolGridComponent component = (BoolGridComponent)target;

        // 3x3のボタンを描画
        for (int i = 0; i < BoolArrayWrapper.GridSize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < BoolArrayWrapper.GridSize; j++)
            {
                // 現在の値に応じてボタンの色を変更
                GUI.backgroundColor = component.boolGrid.data[i,j] ? Color.white : Color.HSVToRGB(0f, 0f, DisabledCellBrightness);

                if (GUILayout.Button(component.boolGrid.data[i,j] ? "" : "", GUILayout.Width(ButtonSize), GUILayout.Height(ButtonSize)))
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
