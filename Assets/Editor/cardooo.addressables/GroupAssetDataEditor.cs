using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace cardooo.addressable
{
    [CustomEditor(typeof(GroupAssetDataSO))]
    public class GroupAssetDataEditor : Editor
    {
        GroupAssetDataSO targetSO;

        public void OnEnable()
        {
            targetSO = (GroupAssetDataSO)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("SET GROUP"))
            {
                Debug.Log("SET GROUP");

                foreach (var d in targetSO.groupAssetDatas)
                {
                    AddGroup(d.group_name, d.folder_paths, d.file_types);
                }
            }
        }
        public void AddGroup(string groupName, List<string> folderPaths, List<string> fileTypes)
        {
            Debug.Log($"[AddGroup][{groupName}]");

            var group = Utility.GetGroup(groupName);

            foreach (var folderPath in folderPaths)
            {
                Debug.Log($"[AddGroup] folderPath: {folderPath}");
                string[] fileEntries = Directory.GetFiles(folderPath);
                foreach (string path in fileEntries)
                {
                    bool pass = false;
                    foreach (var t in fileTypes)
                    {
                        if (path.EndsWith(t))
                        { 
                            pass = true;
                        }
                    }

                    if (!pass)
                        continue;

                    Debug.Log($"[AddGroup] file path: {path}");
                    Utility.AllFileToGroup(group, path);
                }            
            }
        }
    }

}
