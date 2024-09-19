using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace InfiCare.Common.Service;

public class ExchangeRateAPIService
{
    private readonly HttpClient _httpClient;

    public ExchangeRateAPIService(string baseUrl)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<ForexResponse> GetExchangeRateData(int page, int perPage, string from, string to)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/forex/v1/rates?page={page}&per_page={perPage}&from={from}&to={to}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        // Deserialize JSON to ForexResponse
        var forexResponse = JsonConvert.DeserializeObject<ForexResponse>(responseBody);

        return forexResponse;
    }
}

public class ForexResponse
{
    [JsonProperty("status")]
    public Status Status { get; set; }

    [JsonProperty("errors")]
    public Errors Errors { get; set; }

    [JsonProperty("params")]
    public Params Params { get; set; }

    [JsonProperty("data")]
    public Data Data { get; set; }

    [JsonProperty("pagination")]
    public Pagination Pagination { get; set; }
}

public partial class Data
{
    [JsonProperty("payload")]
    public List<Payload> Payload { get; set; }
}

public partial class Payload
{
    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("published_on")]
    public string PublishedOn { get; set; }

    [JsonProperty("modified_on")]
    public string ModifiedOn { get; set; }

    [JsonProperty("rates")]
    public List<Rate> Rates { get; set; }
}

public partial class Rate
{
    [JsonProperty("currency")]
    public Currency Currency { get; set; }

    [JsonProperty("buy")]
    public string Buy { get; set; }

    [JsonProperty("sell")]
    public string Sell { get; set; }
}

public partial class Currency
{
    [JsonProperty("iso3")]
    public string Iso3 { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("unit")]
    public long Unit { get; set; }
}

public partial class Errors
{
    [JsonProperty("validation")]
    public dynamic Validation { get; set; }
}

public partial class Pagination
{
    [JsonProperty("page")]
    public long Page { get; set; }

    [JsonProperty("pages")]
    public long Pages { get; set; }

    [JsonProperty("per_page")]
    public long PerPage { get; set; }

    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("links")]
    public Links Links { get; set; }
}

public partial class Links
{
    [JsonProperty("prev")]
    public dynamic Prev { get; set; }

    [JsonProperty("next")]
    public dynamic Next { get; set; }
}

public partial class Params
{
    [JsonProperty("date")]
    public dynamic Date { get; set; }

    [JsonProperty("from")]
    public string From { get; set; }

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("post_type")]
    public dynamic PostType { get; set; }

    [JsonProperty("per_page")]
    public long PerPage { get; set; }

    [JsonProperty("page")]
    public long Page { get; set; }

    [JsonProperty("slug")]
    public dynamic Slug { get; set; }

    [JsonProperty("q")]
    public dynamic Q { get; set; }
}

public partial class Status
{
    [JsonProperty("code")]
    public long Code { get; set; }
}

