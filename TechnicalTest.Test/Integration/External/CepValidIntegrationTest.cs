using System.Text.Json;

public class CepValidIntegrationTest
{
    private readonly HttpClient _client;

    public CepValidIntegrationTest()
    {
        _client = new HttpClient();
    }

    [Theory]
    [InlineData("01001000")]
    public async Task GetAddressByCep_ShouldReturnAddress_WhenCepIsValid(string cep)
    {
        var requestUrl = $"https://viacep.com.br/ws/{cep}/json/";
        var response = await _client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;

        Assert.False(root.TryGetProperty("erro", out _), "CEP válido não deve ter erro");

        string logradouro = root.GetProperty("logradouro").GetString() ?? "";
        string bairro = root.GetProperty("bairro").GetString() ?? "";
        string cidade = root.GetProperty("localidade").GetString() ?? "";
        string uf = root.GetProperty("uf").GetString() ?? "";

        Assert.False(string.IsNullOrWhiteSpace(logradouro), "logradouro is empty");
        Assert.False(string.IsNullOrWhiteSpace(bairro), "bairro is empty");
        Assert.False(string.IsNullOrWhiteSpace(cidade), "localidade is empty");
        Assert.False(string.IsNullOrWhiteSpace(uf), "uf is empty");
    }
}
