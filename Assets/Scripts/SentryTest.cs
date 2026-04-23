using UnityEngine;
public class SentryCheck : MonoBehaviour
{
    void Start()
    {
        // Cách 1: Gửi một tin nhắn thủ công (để test kết nối)
       // Sentry.SentrySdk.CaptureMessage("Sentry đã kết nối thành công!");

        // Cách 2: Tạo một lỗi thực sự sau 2 giây
        Invoke("CrashGame", 2f);
    }

    void CrashGame()
    {
        Debug.Log("Đang chuẩn bị gây lỗi...");
        string s = null;
        int length = s.Length; // Dòng này chắc chắn sẽ gây lỗi NullReferenceException
    }
}
