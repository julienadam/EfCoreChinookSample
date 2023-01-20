﻿using System;
using System.Collections.Generic;

namespace ChinookExercice.Entities;

public partial class MediaType
{
    public int MediaTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; } = new List<Track>();
}
