using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetTool
{
    [MenuItem("GameObject/Cardooo/SelectionGameObjectPath", priority = 12)]
    private static void SelectionGameObjectPath()
    {
        if (Selection.activeGameObject)
        {
            string text = GetTransPath(Selection.activeGameObject.transform);
            GUIUtility.systemCopyBuffer = text;
            Debug.Log(text);
        }
    }

    /// <summary>
    /// 获得GameObject在Hierarchy中的完整路径
    /// </summary>
    public static string GetTransPath(Transform trans)
    {
        if (!trans.parent)
        {
            return trans.name;
        }
        return $"{GetTransPath(trans.parent)}/{trans.name}";
    }

    /// <summary>
    /// Project中選擇單一或多個目標資源，並將他們的 資源路徑 複製到剪貼簿
    /// プロジェクトビューで1つ以上選択した対象Assetについて、Assetのパスを連結してクリップボードにセットする。
    /// </summary>
    [MenuItem("Assets/Cardooo/Copy Asset Path", false, 500)]
    private static void CopyAssetPath()
    {
        List<string> lines = new List<string>();
        foreach (var obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            lines.Add(path);
        }
        string text = string.Join("\n", lines);
        GUIUtility.systemCopyBuffer = text;
        Debug.Log(text);
    }

    /// <summary>
    /// Project中選擇單一或多個目標資源，並將他們的 GUID 複製到剪貼簿
    /// プロジェクトビューで1つ以上選択した対象Assetについて、AssetのGUIDを連結してクリップボードにセットする。
    /// </summary>
    [MenuItem("Assets/Cardooo/Copy Asset GUID", false, 500)]
    private static void CopyAssetGuid()
    {
        List<string> lines = new List<string>();
        foreach (var obj in Selection.objects)
        {
            string guid;
            long localId;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out localId);
            guid = guid ?? "";
            lines.Add(guid);
        }
        string text = string.Join("\n", lines);
        GUIUtility.systemCopyBuffer = text;
        Debug.Log(text);
    }
}
