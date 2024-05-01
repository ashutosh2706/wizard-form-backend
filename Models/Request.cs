using System;
using System.Collections.Generic;

namespace WizardFormBackend.Models;

public enum StatusCode
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
}

public enum PriorityCode
{
    High = 1,
    Normal = 2,
    Low = 3,
}

public partial class Request
{
    public long RequestId { get; set; }

    public long UserId { get; set; }

    public string Title { get; set; } = null!;

    public string GuardianName { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly RequestDate { get; set; }

    public int PriorityCode { get; set; }

    public int StatusCode { get; set; }

    public long? FileId { get; set; }

    public virtual FileDetail? File { get; set; }

    public virtual Priority PriorityCodeNavigation { get; set; } = null!;

    public virtual Status StatusCodeNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
