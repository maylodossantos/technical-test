using System.Text.Json;

public class CepInvalidIntegrationTest
{
    private readonly HttpClient _client;

    public CepInvalidIntegrationTest()
    {
        _client = new HttpClient();
    }

    [Theory]
    [InlineData("99999999")]
    public async Task GetAddressByCep_ShouldReturnError_WhenCepIsInvalid(string cep)
    {
        var requestUrl = $"https://viacep.com.br/ws/{cep}/json/";
        var response = await _client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;

        Assert.True(root.TryGetProperty("erro", out var errorProp) && errorProp.GetString() == "true");
    }
}
