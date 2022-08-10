using System.IO;
using System.Collections.Generic;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
namespace UnityEditor
{
    public class AddressablesUtility
    {
        public static void AutoGroup(string groupName, string assetPath, bool simplied = false)
        {
            AddressableAssetSettings setting = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup group = setting.FindGroup(groupName);
            if (group == null)
            {
                group = CreateAssetGroup<BundledAssetGroupSchema>(setting, groupName);
            }
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group);
            entry.address = assetPath;
            if (simplied)
            {
                entry.address = Path.GetFileNameWithoutExtension(assetPath);
            }
            entry.SetLabel(groupName, true, true);
        }

        public static AddressableAssetGroup CreateAssetGroup<SchemaType>(AddressableAssetSettings setting
            , string groupName)
        {
            return setting.CreateGroup(groupName, false, false, false,
                new List<AddressableAssetGroupSchema> { setting.DefaultGroup.Schemas[0], setting.DefaultGroup.Schemas[1] }
                , typeof(SchemaType));
        }
    }
}
