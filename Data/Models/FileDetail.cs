using System;
using System.Collections.Generic;

namespace WizardFormBackend.Data.Models;

public partial class FileDetail
{
    public long FileId { get; set; }

    public string FileName { get; set; } = null!;

    public string Checksum { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
