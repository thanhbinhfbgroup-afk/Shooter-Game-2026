using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using System.Linq;

public class AddressableIdValidator
{
    [MenuItem("Tools/Addressables/Validate All IDs")]
    public static bool ValidateAllIDs()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("[Validator] Addressable Settings không tồn tại!");
            return false;
        }

        bool hasError = false;
        foreach (var group in settings.groups)
        {
            // Bỏ qua các group hệ thống mặc định nếu cần
            if (group.ReadOnly) continue;

            foreach (var entry in group.entries)
            {
                string stringId = entry.address;

                // Kiểm tra xem Label có chứa StringID không
                if (!entry.labels.Contains(stringId))
                {
                    Debug.LogError($"[ID Policy Error] Asset: '{entry.MainAsset.name}' trong Group: '{group.Name}' " +
                                   $"có Address là '{stringId}' nhưng thiếu Label tương ứng.");
                    hasError = true;
                }
            }
        }

        if (!hasError)
        {
            Debug.Log("<color=green>[Validator] Tất cả Addressables IDs đều hợp lệ!</color>");
        }

        return !hasError;
    }
}