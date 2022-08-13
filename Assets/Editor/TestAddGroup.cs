using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TestAddGroup
{
    [MenuItem("Tools/AutoGroup")]
    public static void AutoGroup()
    {
        string path = "Assets/Res/Cube 1.prefab";
        Debug.Log($"[PATH] {path}");
        AddressablesUtility.AutoGroup("TEST_GROUP", path);
    }

    [MenuItem("Tools/SceneGroup")]
    public static void SceneGroup()
    {
        string path = "Assets/Res/Scene/";
        string fileType = ".unity";
        
        string[] fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
        {
            if (fileName.EndsWith(fileType))
            {
                Debug.Log($"{fileName}");
                AddressablesUtility.AutoGroup("SCENE", fileName);
            }
        }        
    }
}
