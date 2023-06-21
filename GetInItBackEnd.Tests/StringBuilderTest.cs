using System.Text;

namespace GetInItBackEnd.Tests;

public class StringBuilderTest
{
    [Fact]
    public void Append_ForTwoStrings_ReturnsConcatenatedString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("test1")
            .Append("test2");
        string result = sb.ToString();
        
        Assert.Equal("test1test2", result);
    }
}