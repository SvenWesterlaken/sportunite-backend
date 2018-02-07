using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing;
using SportUnite.Data.Abstract;
using SportUnite.Domain;

namespace SportUnite.Logic {
    public class EFHallManager : IHallManager
    {

        private readonly IHallRepository _hallRepository;

        public EFHallManager(IHallRepository hallRepository) {
            _hallRepository = hallRepository;
        }
        //get list containing all Halls
        public IEnumerable<Hall> Halls() {
            return _hallRepository.Halls();
        }

        //get list containing all OpeningHours
        public IEnumerable<OpeningHours> OpeningHours() {
            return _hallRepository.OpeningHours();
        }

        //get list containing all OpeningHours
        public IEnumerable<HallOpeningHours> HallOpeningHours() {
            return _hallRepository.HallOpeningHours();
        }

        //creating new Hall needs to be accompanied by a list containing OpeningHours
        public bool CreateHall(Hall hall) {
            if (hall.HallOpeningHours.Count < 1) {
                return false;
            } else {
                //check if OpeningHours already exists in database
                for (int i = hall.HallOpeningHours.Count - 1; i >= 0; i--) {
                    var repoOpeningHours = Enumerable.FirstOrDefault<OpeningHours>(_hallRepository.OpeningHours(), repOh =>
                        repOh.Day == hall.HallOpeningHours[i].OpeningHours.Day &&
                        repOh.OpeningTime == hall.HallOpeningHours[i].OpeningHours.OpeningTime &&
                        repOh.ClosingTime == hall.HallOpeningHours[i].OpeningHours.ClosingTime);
                    //if not null dont make a new entry
                    if (repoOpeningHours != null) {
                        hall.HallOpeningHours.RemoveAt(i);
                        hall.HallOpeningHours.Add(new HallOpeningHours() {
                            HallId = hall.HallId,
                            OpeningHoursId = repoOpeningHours.OpeningHoursId
                        });
                    }
                }
                return _hallRepository.CreateHall(hall);
            }
        }

        //creating new OpeningHours needs to be accompanied by an existing Hall
        public bool CreateOpeningHours(Hall hall) {
            for (int i = hall.HallOpeningHours.Count - 1; i >= 0; i--) {
                var repoOpeningHours = Enumerable.FirstOrDefault<OpeningHours>(_hallRepository.OpeningHours(), repOh =>
                    repOh.Day == hall.HallOpeningHours[i].OpeningHours.Day &&
                    repOh.OpeningTime == hall.HallOpeningHours[i].OpeningHours.OpeningTime &&
                    repOh.ClosingTime == hall.HallOpeningHours[i].OpeningHours.ClosingTime);
                //if not null dont make a new entry
                if (repoOpeningHours != null) {
                    //Combination between Hall and OpeningHours already exist
                    if (Enumerable.Any<HallOpeningHours>(_hallRepository.HallOpeningHours(), hoh =>
                        hoh.HallId == hall.HallId && hoh.OpeningHoursId == hall.HallOpeningHours[i].OpeningHoursId)) {
                        hall.HallOpeningHours.RemoveAt(i);
                        continue;
                    }
                    hall.HallOpeningHours.RemoveAt(i);
                    hall.HallOpeningHours.Add(new HallOpeningHours() {
                        HallId = hall.HallId,
                        OpeningHoursId = repoOpeningHours.OpeningHoursId
                    });
                }
            }
            //no OpeningHours left to add
            if (!hall.HallOpeningHours.Any()) {
                return false;
            }
            return _hallRepository.CreateOpeningHours(hall);
        }

        //updating Hall
        //STILL NEEDS TO CHECK IF ANY RESERVATIONS ARE USING TIME THAT IS BEING UPDATED
        public bool UpdateHall(Hall hall) {
            var hallToUpdate = UpdateOpeningHours(hall);
            return _hallRepository.UpdateHall(hallToUpdate);

        }

        public Hall UpdateOpeningHours(Hall hall) {
            //comparing OpeningHours in repository and OpeningHours in given Hall object
            for (int i = hall.HallOpeningHours.Count - 1; i >= 0; i--) {
                var differentOpeningHours = Enumerable.FirstOrDefault<OpeningHours>(_hallRepository.OpeningHours(), repOh =>
                    repOh.OpeningHoursId == hall.HallOpeningHours[i].OpeningHours.OpeningHoursId &&
                    (repOh.Day != hall.HallOpeningHours[i].OpeningHours.Day ||
                    repOh.OpeningTime != hall.HallOpeningHours[i].OpeningHours.OpeningTime ||
                    repOh.ClosingTime != hall.HallOpeningHours[i].OpeningHours.ClosingTime));
                //if OpeningHours are changed (not null)
                if (differentOpeningHours != null) {
                    //if OpeningHours is used by multiple halls dont update 
                    if (Enumerable.Count<HallOpeningHours>(_hallRepository.HallOpeningHours(), hoh => hoh.OpeningHoursId == differentOpeningHours.OpeningHoursId) > 1) {
                        hall.HallOpeningHours.RemoveAt(i);
                        //check if new OpeningHours already exists
                        var newOpeningHours = Enumerable.FirstOrDefault<OpeningHours>(_hallRepository.OpeningHours(), repOh =>
                            repOh.ClosingTime == hall.HallOpeningHours[i].OpeningHours.ClosingTime &&
                            repOh.OpeningTime == hall.HallOpeningHours[i].OpeningHours.OpeningTime &&
                            repOh.Day == hall.HallOpeningHours[i].OpeningHours.Day);
                        //make a new entry if OpeningHours does not exist
                        if (newOpeningHours == null) {
                            newOpeningHours.ClosingTime = hall.HallOpeningHours[i].OpeningHours.ClosingTime;
                            newOpeningHours.Day = hall.HallOpeningHours[i].OpeningHours.Day;
                            newOpeningHours.OpeningTime = hall.HallOpeningHours[i].OpeningHours.OpeningTime;
                        }
                        hall.HallOpeningHours.Add(new HallOpeningHours() {
                            Hall = hall,
                            OpeningHours = newOpeningHours
                        });
                    }
                }
            }
            return hall;
        }

        //deleting a Hall
        public bool RemoveHall(Hall hall) {
            //check if Hall exists in database
            var hallToRemove = Enumerable.FirstOrDefault<Hall>(_hallRepository.Halls(), h => h.HallId == hall.HallId);
            //checking if building has more than 1 hall
            if (Enumerable.Count<Hall>(_hallRepository.Halls(), h => h.BuildingId == hallToRemove.BuildingId) <= 1) {
                return false;
            }
            //checking if Hall still has reservations
            if (hallToRemove.Reservations?.Any() == true)
            {
                return false;
            }
            //get list of all OpeningHours used by this Hall before removing
            var openingHoursList =
                Enumerable.Where<HallOpeningHours>(_hallRepository.HallOpeningHours(), hoh => hoh.HallId == hall.HallId).ToList();
            var hallRemoved = _hallRepository.RemoveHall(new Hall() { HallId = hallToRemove.HallId });
            if (hallRemoved) {
                foreach (HallOpeningHours hoh in openingHoursList) {
                    int openingHourCount = Enumerable.Count<HallOpeningHours>(_hallRepository.HallOpeningHours(), h => h.OpeningHoursId == hoh.OpeningHoursId);
                    //remove entry in OpeningHours table if it is no longer used
                    if (openingHourCount == 0) {
                        RemoveOpeningHours(new OpeningHours { OpeningHoursId = hoh.OpeningHoursId });
                    }
                }
                return hallRemoved;
            }
            return false;
        }

        //used to delete OpeningHours from database if they are no longer used
        public bool RemoveOpeningHours(OpeningHours openingHours) {
            var openingHoursToRemove = Enumerable.FirstOrDefault<OpeningHours>(_hallRepository.OpeningHours(), o => o.OpeningHoursId == openingHours.OpeningHoursId);
            if (openingHoursToRemove != null) {
                return _hallRepository.RemoveOpeningHours(openingHoursToRemove);
            } else {
                return false;
            }
        }

        //deleting connection between Hall and OpeningHours
        public bool RemoveHallOpeningHours(HallOpeningHours hallOpeningHours) {
            var hallOpeningHoursToRemove = Enumerable.FirstOrDefault<HallOpeningHours>(_hallRepository.HallOpeningHours(), o => o.OpeningHoursId == hallOpeningHours.OpeningHoursId && o.HallId == hallOpeningHours.HallId);
            //check if connection between Hall and OpeningHours exists in database
            if (hallOpeningHoursToRemove != null) {
                //check if Hall still has opening times
                int openingTimesCount = Enumerable.Count<HallOpeningHours>(_hallRepository.HallOpeningHours(), (h => h.HallId == hallOpeningHours.HallId));
                //Hall needs to have at least 1 OpeningHours left after removing
                if (openingTimesCount > 1) {
                    var hallOpeningHoursRemoved = _hallRepository.RemoveHallOpeningHours(hallOpeningHoursToRemove);
                    //check if HallOpeningHours is removed
                    if (hallOpeningHoursRemoved) {
                        int openingHourCount = Enumerable.Count<HallOpeningHours>(_hallRepository.HallOpeningHours(), h => h.OpeningHoursId == hallOpeningHours.OpeningHoursId);
                        //remove entry in OpeningHours table if it is no longer used
                        if (openingHourCount == 0) {
                            RemoveOpeningHours(new OpeningHours { OpeningHoursId = hallOpeningHours.OpeningHoursId });
                        }
                    }
                    return hallOpeningHoursRemoved;
                } else {
                    return false;
                }
            }
            return false;
        }

        //get Hall info
        public Hall GetHall(int hallId) {
            return _hallRepository.GetHall(hallId);
        }

        //get Hall Reservation info
        public Hall GetHallReservation(int hallId)
        {
            return _hallRepository.GetHallReservation(hallId);
        }
    }
}
