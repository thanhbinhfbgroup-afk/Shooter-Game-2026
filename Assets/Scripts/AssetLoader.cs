using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Lớp tiện ích để nạp tài nguyên bằng Addressables và UniTask.
/// Đảm bảo tuân thủ Policy: StringID = Address = Label.
/// </summary>

public static class AssetLoader
{

    /// <summary>
    /// Nạp một Asset dựa trên StringID.
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu (GameObject, Sprite, AudioClip...)</typeparam>
    /// <param name="stringId">ID của asset (phải trùng với Address và Label)</param>
    /// <param name="token">Token để hủy bỏ việc load nếu cần</param>
    public static async UniTask<T> LoadAsset<T>(string stringId, CancellationToken token = default) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(stringId))
        {
            Debug.LogError("[AssetLoader] StringID bị trống!");
            return null;
            
        }

        try
        {
            // Bắt đầu quá trình nạp tài nguyên bất đồng bộ
            // Theo spec của bạn: stringId này vừa là Address vừa là Label
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(stringId);

            // Chuyển đổi sang UniTask để dùng await
            T asset = await handle.ToUniTask(cancellationToken: token);
           
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                
                return asset;
            }

            Debug.LogError($"[AssetLoader] Nạp thất bại ID: {stringId}. Trạng thái: {handle.Status}");
            return null;
        }
        catch (OperationCanceledException)
        {
            Debug.LogWarning($"[AssetLoader] Đã hủy nạp asset: {stringId}");
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"[AssetLoader] Lỗi hệ thống khi nạp {stringId}: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Giải phóng bộ nhớ của Asset khi không còn sử dụng.
    /// Cực kỳ quan trọng để tránh leak RAM.
    /// </summary>
    public static void ReleaseAsset(object asset)
    {
        if (asset == null) return;
        Addressables.Release(asset);
    }
}