using System;
using System.Collections.Generic;

namespace WizardFormBackend.Data.Models;

public partial class Status
{
    public int StatusCode { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
