using ABC.DTOs.Transaction;
using ABC.Models.Entities;

namespace ABC.Mappers;

public static class TransactionMapper
{
    public static TransactionDto ToDto(this Transaction transaction)
    {
        return new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            SenderFirstName = transaction.SenderFirstName,
            SenderMiddleName = transaction.SenderMiddleName,
            SenderLastName = transaction.SenderLastName,
            SenderAddress = transaction.SenderAddress,
            SenderCountry = transaction.SenderCountry,
            ReceiverFirstName = transaction.ReceiverFirstName,
            ReceiverMiddleName = transaction.ReceiverMiddleName,
            ReceiverLastName = transaction.ReceiverLastName,
            ReceiverAddress = transaction.ReceiverAddress,
            ReceiverCountry = transaction.ReceiverCountry,
            TransferAmountMYR = transaction.TransferAmount,
            ExchangeRate = transaction.ExchangeRate,
            PayoutAmount = transaction.PayoutAmount,
            BankName = transaction.BankName,
            AccountNumber = transaction.AccountNumber
        };
    }
}
