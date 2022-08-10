using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestAddGroup
{
    [MenuItem("Tools/AutoGroup")]
    public static void AutoGroup()
    {
        string path = "Assets/Res/Cube 1.prefab";
        Debug.Log($"[PATH] {path}");
        AddressablesUtility.AutoGroup("TEST_GROUP", path);
    }
}
