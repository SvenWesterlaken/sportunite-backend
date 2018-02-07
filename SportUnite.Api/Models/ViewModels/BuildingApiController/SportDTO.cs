using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.Api.Models.ViewModels.BuildingApiController
{
    public class SportDTO
    {
        public int SportId { get; set; }
        public string Name { get; set; }
        public List<SportObjectDTO> SportObjects { get; set; }
    }
}
