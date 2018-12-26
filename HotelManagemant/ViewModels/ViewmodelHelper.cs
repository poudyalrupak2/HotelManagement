using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagemant.Models;

namespace HotelManagemant.ViewModels
{
    public static class ViewmodelHelper
    {
        public static bookedViewModel ToViewModel(this Booking rooms)
        {
            var userProfileViewModel = new bookedViewModel
            {
                bookingno = rooms.bookingNo,
                CheckIn=rooms.CheckIn,
                CheckoutDate=rooms.CheckoutDate,
                customer=rooms.customers,
                ShortDescriptions=rooms.ShortDescriptions,
                noOfPerson=rooms.NoOfPersons
           
            };

            foreach (var course in rooms.Room)
            {
                userProfileViewModel.Rooom.Add(new Room
                {
                    RoomNo = course.RoomNo,
                    

                
                   
                });
            }

            return userProfileViewModel;
        }

        public static bookedViewModel ToViewModel(this Booking rooms, ICollection<Room> allDbCourses)
        {
            var userProfileViewModel = new bookedViewModel
            {
                bookingno = rooms.bookingNo,
                CheckIn = rooms.CheckIn,
                CheckoutDate = rooms.CheckoutDate,
                customer = rooms.customers,
                ShortDescriptions = rooms.ShortDescriptions,
                noOfPerson = rooms.NoOfPersons
            };

            // Collection for full list of courses with user's already assigned courses included
            ICollection<Room> allCourses = new List<Room>();

            foreach (var c in allDbCourses)
            {
                // Create new AssignedCourseData for each course and set Assigned = true if user already has course
                var assignedCourse = new Room
                {
                    RoomNo = c.RoomNo,
                  
                };

                allCourses.Add(assignedCourse);
            }

            userProfileViewModel.Rooom = allCourses;

            return userProfileViewModel;
        }
       
    }
}