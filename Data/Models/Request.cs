using System;
using System.Collections.Generic;

namespace WizardFormBackend.Data.Models;

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
