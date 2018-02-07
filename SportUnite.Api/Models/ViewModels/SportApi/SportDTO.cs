using System.Collections.Generic;

namespace SportUnite.Api.Models.ViewModels.SportApi
{
    public class SportDTO
    {
        public int SportId { get; set; }
        public string Name { get; set; }
        public List<SportObjectDTO> SportObjects { get; set; }

    }
}
