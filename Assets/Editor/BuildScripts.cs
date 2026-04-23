using UnityEditor;
using UnityEngine;
public class BuildScripts
{
    public static void PreBuildValidate()
    {
        Debug.Log("Đang chạy Pre-build Validation...");
        bool isValid = AddressableIdValidator.ValidateAllIDs();

        if (!isValid)
        {
            Debug.LogError("Validation thất bại. Hủy quá trình Build!");
            EditorApplication.Exit(1); // Thoát với mã lỗi để CI dừng build
        }
        else
        {
            Debug.Log("Validation thành công. Tiếp tục build...");
        }
    }
}