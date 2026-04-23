using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening; // Bảo bối 1
using Cysharp.Threading.Tasks; // Bảo bối 2

public class TestMyGame : MonoBehaviour
{
    public Button myButton;
    public TextMeshProUGUI myText;

    void Start()
    {
        // Khi nhấn nút sẽ gọi hàm Load
        myButton.onClick.AddListener(() => LoadDataProcess().Forget());
    }

    private async UniTaskVoid LoadDataProcess()
    {
        // 1. Dùng DOTween làm hiệu ứng mờ dần và xoay cái nút
        myButton.interactable = false;
        myText.text = "Loading...";

        // Vừa mờ vừa xoay cùng lúc
        myText.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        myButton.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        // 2. Giả lập chờ nạp dữ liệu bằng UniTask (đợi 2 giây)
        await UniTask.Delay(2000);

        // 3. Xong rồi thì dừng hiệu ứng và hiện thông báo
        DOTween.Kill(myText);
        DOTween.Kill(myButton.transform);

        myText.DOFade(1f, 0.2f);
        myButton.transform.rotation = Quaternion.identity;
        myText.text = "Xong rồi!";
        myButton.interactable = true;
    }
}
