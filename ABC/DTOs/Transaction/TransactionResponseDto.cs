using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ABC.DTOs.Transaction;

public class TransactionResponseDto
{
    [JsonProperty("data")]
    public Data Data { get; set; } = new Data();
}

public class Data
{
    [JsonProperty("payload")]
    public List<Payload> Payload { get; set; } = new List<Payload>();
}

public class Payload
{
    public string? Date { get; set; }
    public List<Rate> Rates { get; set; } = new List<Rate>();
}

public class Rate
{
    public Currency Currency { get; set; } = new Currency();
    public string? Buy { get; set; }
    public string? Sell { get; set; }
}

public class Currency
{
    public string Iso3 { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Unit { get; set; }
}