using System.Net.Http.Json;
using CalMedAcad.Pwa.Models;

namespace CalMedAcad.Pwa.Services;

public class MediaApiService
{
    private readonly HttpClient _httpClient;

    public MediaApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CalculoMediaResponse?> CalcularAsync(
        CalculoMediaRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/media/calcular",
            request
        );

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();

            throw new Exception(erro);
        }

        return await response.Content
            .ReadFromJsonAsync<CalculoMediaResponse>();
    }
}