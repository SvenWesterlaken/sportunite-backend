﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.UI.Models.ViewModels
{
    public class ReservationUpdateModel
    {
			public int ReservationId { get; set; }
			public DateTime StartTime { get; set; }
			public DateTime EndTime { get; set; }
    }
}
