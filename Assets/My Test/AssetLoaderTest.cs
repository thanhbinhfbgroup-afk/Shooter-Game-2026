using NUnit.Framework;
using NSubstitute; // B?o b?i 3
using System.Threading.Tasks;

public class AssetLoaderTest
{
    [Test]
    public void MyFirstTest()
    {
        // Tạo một cái mock thử
        var substitute = Substitute.For<System.IDisposable>();

        // Kiểm tra xem nó có tồn tại không
        Assert.IsNotNull(substitute);
    }
    public interface IDataService
    {
        string GetData(int id);
    }

    [Test]
    public void Test_NSubstitute_Working()
    {
        // T?o m?t "k? gi? m?o" (Mock) cho Service
        var mockService = Substitute.For<IDataService>();

        // Thi?t l?p: N?u g?i GetData(1) thì ph?i tr? v? "Hello Unity"
        mockService.GetData(1).Returns("Hello Unity");

        // Ch?y th?
        string result = mockService.GetData(1);

        // Ki?m tra xem k?t qu? có ?úng nh? mình ép bu?c không
        Assert.AreEqual("Hello Unity", result);

        // Ki?m tra xem hàm này có th?c s? b? g?i 1 l?n không
        mockService.Received(1).GetData(1);

        UnityEngine.Debug.Log("NSubstitute ch?y c?c ngon!");
    }
}
