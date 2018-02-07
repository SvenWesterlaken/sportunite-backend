using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportUnite.Api.Models.ViewModels;
using SportUnite.Api.Models.ViewModels.BuildingApiController;
using SportUnite.Domain;
using SportUnite.Api.Models.ViewModels.HallApi;


namespace SportUnite.Api
{
    public class AutoMapperProfileConfiguration : Profile
	{
	    public AutoMapperProfileConfiguration()
		    : this("MyProfile")
	    {
	    }
	    protected AutoMapperProfileConfiguration(string profileName)
		    : base(profileName) {

			//Building
			CreateMap<Building, BuildingDTO>();

		    CreateMap<Address, AddressDTO>();

		    CreateMap<Hall, Models.ViewModels.BuildingApiController.HallDTO>()
			    .ForMember(dest => dest.Sports,
				    opts => opts.MapFrom(src => src.SportHalls))
			    .ForMember(dest => dest.OpeningHours,
				    opts => opts.MapFrom(src => src.HallOpeningHours))
			    .ForMember(dest => dest.Reservations,
				    opts => opts.MapFrom(src => src.Reservations));

		    CreateMap<HallOpeningHours, Models.ViewModels.BuildingApiController.OpeningHoursDTO>()
			    .ForMember(dest => dest.ClosingTime,
				    opts => opts.MapFrom(src => src.OpeningHours.ClosingTime))
			    .ForMember(dest => dest.Day,
				    opts => opts.MapFrom(src => src.OpeningHours.Day))
			    .ForMember(dest => dest.OpeningTime,
				    opts => opts.MapFrom(src => src.OpeningHours.OpeningTime));

		    CreateMap<SportHall, Models.ViewModels.BuildingApiController.SportDTO>()
			    .ForMember(dest => dest.Name,
				    opts => opts.MapFrom(src => src.Sport.Name))
			    .ForMember(dest => dest.SportObjects,
				    opts => opts.MapFrom(src => src.Sport.SportObjectSports));

		    CreateMap<SportObjectSport, Models.ViewModels.BuildingApiController.SportObjectDTO>()
			    .ForMember(dest => dest.Name,
				    opts => opts.MapFrom(src => src.Sport.Name));

		    CreateMap<Reservation, Models.ViewModels.BuildingApiController.ReservationDTO>();

			//Hall

			CreateMap<Hall, Models.ViewModels.HallApi.HallDTO>()
					.ForMember(dest => dest.OpeningHours,
						opts => opts.MapFrom(src => src.HallOpeningHours))
					.ForMember(dest => dest.Reservations,
						opts => opts.MapFrom(src => src.Reservations))
						.ForMember(dest => dest.Sports,
							opts => opts.MapFrom(src => src.SportHalls));

					CreateMap<HallOpeningHours, Models.ViewModels.HallApi.OpeningHoursDTO>()
						.ForMember(dest => dest.OpeningHoursId,
							opts => opts.MapFrom(src => src.OpeningHoursId))
						.ForMember(dest => dest.ClosingTime,
							opts => opts.MapFrom(src => src.OpeningHours.ClosingTime))
						.ForMember(dest => dest.Day,
							opts => opts.MapFrom(src => src.OpeningHours.Day))
						.ForMember(dest => dest.OpeningTime,
							opts => opts.MapFrom(src => src.OpeningHours.OpeningTime));

					CreateMap<Reservation, Models.ViewModels.HallApi.ReservationDTO>()
						.ForMember(dest => dest.ReservationId,
							opts => opts.MapFrom(src => src.ReservationId));

					CreateMap<SportHall, Models.ViewModels.HallApi.SportDTO>()
						.ForMember(dest => dest.SportId,
							opts => opts.MapFrom(src => src.SportId))
						.ForMember(dest => dest.Name,
							opts => opts.MapFrom(src => src.Sport.Name))
						.ForMember(dest => dest.SportObjects,
							opts => opts.MapFrom(src => src.Sport.SportObjectSports));

					CreateMap<SportObjectSport, Models.ViewModels.HallApi.SportObjectDTO>()
						.ForMember(dest => dest.SportObjectId,
							opts => opts.MapFrom(src => src.SportObjectId))
						.ForMember(dest => dest.Name,
							opts => opts.MapFrom(src => src.SportObject.Name));
					CreateMap<Reservation, Models.ViewModels.HallApi.ReservationDTO>();

			//Reservation
					CreateMap<Models.ViewModels.ReservationApi.ReservationDTO, Reservation>();
		    CreateMap<Reservation, Models.ViewModels.ReservationApi.ReservationDTO > ();

			//SportEvent
			CreateMap<SportEvent, SportEventDTO>()
							.ForMember(dest => dest.ReservationId,
									opts => opts.MapFrom(src => src.Reservation.ReservationId));
					CreateMap<SportEventDTO, SportEvent>();
            CreateMap<Sport, Models.ViewModels.SportApi.SportDTO>()
                .ForMember(dest => dest.SportObjects,
                    opts => opts.MapFrom(src => src.SportObjectSports));

			//Sport
            CreateMap<SportObjectSport, Models.ViewModels.SportApi.SportDTO>()
                .ForMember(dest => dest.Name,
                    opts => opts.MapFrom(src => src.Sport.Name))
                .ForMember(dest => dest.SportObjects,
                    opts => opts.MapFrom(src => src.Sport.SportObjectSports));

			//Sport Object
            CreateMap<SportObjectSport, Models.ViewModels.SportApi.SportObjectDTO>()
                .ForMember(dest => dest.Name,
                    opts => opts.MapFrom(src => src.SportObject.Name));


        }
    }
}
