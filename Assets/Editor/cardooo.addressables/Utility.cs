using System.IO;
using System.Collections.Generic;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor;
namespace cardooo.addressable
{
    public class Utility
    {


        public static void AutoGroup(string groupName, string assetPath, bool simplied = false)
        {
            AddressableAssetSettings setting = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup group = GetGroup(groupName);
            
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group);
            entry.address = assetPath;
            if (simplied)
            {
                entry.address = Path.GetFileNameWithoutExtension(assetPath);
            }
        }

        public static AddressableAssetGroup GetGroup(string groupName)
        {
            AddressableAssetSettings setting = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup group = setting.FindGroup(groupName);
            if (group == null)
            {
                group = CreateAssetGroup<BundledAssetGroupSchema>(setting, groupName);
            }

            return group;
        }

        public static AddressableAssetGroup CreateAssetGroup<SchemaType>(AddressableAssetSettings setting, string groupName)
        {
            return setting.CreateGroup(groupName, false, false, true, null,
                typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
        }

        public static void AllFileToGroup(AddressableAssetGroup group, string assetPath, bool simplied = false)
        {
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            AddressableAssetEntry entry = AddressableAssetSettingsDefaultObject.Settings.CreateOrMoveEntry(guid, group);
            entry.address = assetPath;
            if (simplied)
            {
                entry.address = Path.GetFileNameWithoutExtension(assetPath);
            }
        }
    }
}
