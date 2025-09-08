//using UnityEngine;
//using UnityEditor;
//using System.IO;
//using System.Collections.Generic;
//using OfficeOpenXml; // EPPlusを使う必要あり
//using ShotBall.InGame;
//using System.ComponentModel;

//public class BlockDefinitionExporter : EditorWindow
//{
//    [MenuItem("Tools/Export BlockDefinitions to Excel")]
//    public static void ExportToExcel()
//    {
//        string path = EditorUtility.SaveFilePanel("Export Excel", "", "BlockDefinitions.xlsx", "xlsx");
//        if (string.IsNullOrEmpty(path)) return;


//        using (var package = new ExcelPackage())
//        {
//            var blockDefs = LoadAllBlockDefinitions();

//            // シートごとのデータ格納用
//            var summarySheet = package.Workbook.Worksheets.Add("Summary");
//            var floatSheet = package.Workbook.Worksheets.Add("FloatParameters");
//            var intSheet = package.Workbook.Worksheets.Add("IntParameters");
//            var boolSheet = package.Workbook.Worksheets.Add("BoolParameters");
//            var vec2Sheet = package.Workbook.Worksheets.Add("Vector2Parameters");
//            var vec3Sheet = package.Workbook.Worksheets.Add("Vector3Parameters");
//            var viewSheet = package.Workbook.Worksheets.Add("UniqueViews");

//            int sRow = 1;
//            summarySheet.Cells[sRow++, 1].Value = "Name";
//            summarySheet.Cells[1, 2].Value = "BlockType";
//            summarySheet.Cells[1, 3].Value = "isRotationEnabled";
//            summarySheet.Cells[1, 4].Value = "isRotation";

//            int fRow = 1;
//            floatSheet.Cells[fRow++, 1].Value = "BlockName";
//            floatSheet.Cells[1, 2].Value = "Key";
//            floatSheet.Cells[1, 3].Value = "DefaultValue";

//            int iRow = 1;
//            intSheet.Cells[iRow++, 1].Value = "BlockName";
//            intSheet.Cells[1, 2].Value = "Key";
//            intSheet.Cells[1, 3].Value = "DefaultValue";

//            int bRow = 1;
//            boolSheet.Cells[bRow++, 1].Value = "BlockName";
//            boolSheet.Cells[1, 2].Value = "Key";
//            boolSheet.Cells[1, 3].Value = "DefaultValue";

//            int v2Row = 1;
//            vec2Sheet.Cells[v2Row++, 1].Value = "BlockName";
//            vec2Sheet.Cells[1, 2].Value = "Key";
//            vec2Sheet.Cells[1, 3].Value = "X";
//            vec2Sheet.Cells[1, 4].Value = "Y";

//            int v3Row = 1;
//            vec3Sheet.Cells[v3Row++, 1].Value = "BlockName";
//            vec3Sheet.Cells[1, 2].Value = "Key";
//            vec3Sheet.Cells[1, 3].Value = "X";
//            vec3Sheet.Cells[1, 4].Value = "Y";
//            vec3Sheet.Cells[1, 5].Value = "Z";

//            int viewRow = 1;
//            viewSheet.Cells[viewRow++, 1].Value = "BlockName";
//            viewSheet.Cells[1, 2].Value = "Key";
//            viewSheet.Cells[1, 3].Value = "ViewType";

//            foreach (var block in blockDefs)
//            {
//                string blockName = block.name;

//                // Summary
//                summarySheet.Cells[sRow, 1].Value = blockName;
//                summarySheet.Cells[sRow, 2].Value = block.type.ToString();
//                summarySheet.Cells[sRow, 3].Value = block.isRotationEnabled;
//                summarySheet.Cells[sRow++, 4].Value = block.isRotation.defaultValue;

//                // Float
//                foreach (var p in block.floatParameters)
//                {
//                    floatSheet.Cells[fRow, 1].Value = blockName;
//                    floatSheet.Cells[fRow, 2].Value = p.key;
//                    floatSheet.Cells[fRow++, 3].Value = p.defaultValue;
//                }

//                // Int
//                foreach (var p in block.intParameters)
//                {
//                    intSheet.Cells[iRow, 1].Value = blockName;
//                    intSheet.Cells[iRow, 2].Value = p.key;
//                    intSheet.Cells[iRow++, 3].Value = p.defaultValue;
//                }

//                // Bool
//                foreach (var p in block.boolParameters)
//                {
//                    boolSheet.Cells[bRow, 1].Value = blockName;
//                    boolSheet.Cells[bRow, 2].Value = p.key;
//                    boolSheet.Cells[bRow++, 3].Value = p.defaultValue;
//                }

//                // Vector2
//                foreach (var p in block.vector2Parameters)
//                {
//                    vec2Sheet.Cells[v2Row, 1].Value = blockName;
//                    vec2Sheet.Cells[v2Row, 2].Value = p.key;
//                    vec2Sheet.Cells[v2Row, 3].Value = p.defaultValue.x;
//                    vec2Sheet.Cells[v2Row++, 4].Value = p.defaultValue.y;
//                }

//                // Vector3
//                foreach (var p in block.vector3Parameters)
//                {
//                    vec3Sheet.Cells[v3Row, 1].Value = blockName;
//                    vec3Sheet.Cells[v3Row, 2].Value = p.key;
//                    vec3Sheet.Cells[v3Row, 3].Value = p.defaultValue.x;
//                    vec3Sheet.Cells[v3Row, 4].Value = p.defaultValue.y;
//                    vec3Sheet.Cells[v3Row++, 5].Value = p.defaultValue.z;
//                }

//                // UniqueViews
//                foreach (var view in block.uniqueViews)
//                {
//                    viewSheet.Cells[viewRow, 1].Value = blockName;
//                    viewSheet.Cells[viewRow, 2].Value = view.key;
//                    viewSheet.Cells[viewRow++, 3].Value = view.viewType.ToString();
//                }
//            }

//            // Save
//            File.WriteAllBytes(path, package.GetAsByteArray());
//            Debug.Log("Exported to: " + path);
//        }
//    }

//    private static List<BlockDefinition> LoadAllBlockDefinitions()
//    {
//        string[] guids = AssetDatabase.FindAssets("t:BlockDefinition");
//        var list = new List<BlockDefinition>();
//        foreach (var guid in guids)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(guid);
//            var asset = AssetDatabase.LoadAssetAtPath<BlockDefinition>(path);
//            if (asset != null)
//                list.Add(asset);
//        }
//        return list;
//    }
//}
