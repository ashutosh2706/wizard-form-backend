using System;
using System.Collections.Generic;

namespace WizardFormBackend.Data.Models;

public partial class Priority
{
    public int PriorityCode { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
