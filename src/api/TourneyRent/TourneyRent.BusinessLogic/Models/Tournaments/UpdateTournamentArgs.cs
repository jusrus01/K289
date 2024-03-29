﻿using Microsoft.AspNetCore.Http;
using TourneyRent.Contracts.Models;

namespace TourneyRent.BusinessLogic.Models.Tournaments;

public class UpdateTournamentArgs : IImageUpload, ITransactionable
{
    public IFormFile ImageFile { get; set; }

    public string BankAccountName { get; set; }
    public string BankAccountNumber { get; set; }
    public string TransactionReason { get; set; }

    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}