using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cardooo.addressable
{
    public class GroupAssetDataSO : ScriptableObject
    {
        public List<GroupAssetData> groupAssetDatas;
    }

    [System.Serializable]
    public class GroupAssetData
    {
        public string group_name;
        public List<string> folder_paths;
        public List<string> file_types;
    }
}