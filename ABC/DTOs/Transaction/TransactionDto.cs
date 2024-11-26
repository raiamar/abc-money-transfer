using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ABC.DTOs.Transaction;

public class TransactionDto
{
    [JsonIgnore]
    public string? TransactionId { get; set; } = default!;
    [Required(ErrorMessage = "Sender First Name is required.")]
    public string SenderFirstName { get; set; } = default!;
    public string? SenderMiddleName { get; set; } = default!;
    [Required(ErrorMessage = "Sender Last Name is required.")]
    public string SenderLastName { get; set; } = default!;
    [Required(ErrorMessage = "Address is required.")]
    public string SenderAddress { get; set; } = default!;
    public string SenderCountry { get; set; } = default!;
    [Required(ErrorMessage = "Receiver First Name is required.")]
    public string ReceiverFirstName { get; set; } = default!;
    public string? ReceiverMiddleName { get; set; } = default!;
    [Required(ErrorMessage = "Receiver Last Name is required.")]
    public string ReceiverLastName { get; set; } = default!;
    [Required(ErrorMessage = "Address is required.")]
    public string ReceiverAddress { get; set; } = default!;
    public string ReceiverCountry { get; set; } = default!;
    [Required(ErrorMessage = "Transfer Amount is required.")]
    [Range(1, double.MaxValue, ErrorMessage = "Transfer Amount must be greater than 0.")]
    public decimal TransferAmountMYR { get; set; }
    [JsonIgnore]
    public decimal? ExchangeRate { get; set; }
    [JsonIgnore]
    public decimal? PayoutAmount { get; set; }
    [Required(ErrorMessage = "Bank is required.")]
    public string BankName { get; set; } = default!;
    [Required(ErrorMessage = "Account Number is required.")]
    [StringLength(20, ErrorMessage = "Account Number can't exceed 20 characters.")]
    public string AccountNumber { get; set; } = default!;
}
