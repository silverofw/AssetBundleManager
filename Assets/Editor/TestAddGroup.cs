using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TestAddGroup
{
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
                cardoo.editor.AddressablesUtility.AutoGroup("SCENE", fileName);
            }
        }        
    }
}
