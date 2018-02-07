using System.Collections.Generic;

namespace SportUnite.Api.Models.ViewModels.HallApi
{
    public class SportDTO
    {
	    public int SportId { get; set; }
	    public string Name { get; set; }
	    public IEnumerable<SportObjectDTO> SportObjects;
    }
}
