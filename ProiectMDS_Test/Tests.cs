using Microsoft.EntityFrameworkCore;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Models.CourseModels;
using ProiectMDS.DAL.Models.LocationModels;
using ProiectMDS.DAL.Models.ProfileModels;
using ProiectMDS.DAL.Models.UserModels;
using ProiectMDS.Services.CourseServices;
using ProiectMDS.Services.UserServices;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ProiectMDS_Test
{
    public class Tests
    {
        [Fact]
        public void CourseDTOvalidStructure()
        {
            try
            {
                CourseGetModel course = new CourseGetModel()
                {
                    Id = 1,
                    courseName = "POO",
                    Credit = 5,
                    Helper = true
                };
                
                Assert.True(course.Id.GetType() == typeof(int));
                Assert.True(course.courseName.GetType() == typeof(string));
                Assert.True(course.Credit.GetType() == typeof(int));
                Assert.True(course.Helper.GetType() == typeof(bool));
            }
            catch (Exception ex)
            {
                Assert.True(1 == 0);
            }

        }

        [Fact]
        public void LocationDTOvalidStructure()
        {
            LocationGetModel location = new LocationGetModel()
            {
                County = "Olt",
                City = "Slatina",
                Street = "Vlaicu voda",
                Number = 123
            };

            Assert.True(location.County.GetType() == typeof(string));
            Assert.True(location.City.GetType() == typeof(string));
            Assert.True(location.Street.GetType() == typeof(string));
            Assert.True(location.Number.GetType() == typeof(int));
        }

        [Fact]
        public void IsNullAtDeclaration()
        {
            UserGetModel user = new UserGetModel();

            Assert.Null(user.FirstName);
        }

        [Fact]
        public void EmailInitializeMatches()
        {
            try
            {
                var random = new Byte[32];
                var rng = RandomNumberGenerator.Create();
               
                rng.GetBytes(random);

                User user = new User
                {
                    Email = new MailAddress(Convert.ToBase64String(random)).Address
                };
            }
            catch (FormatException ex)
            {
                Assert.True(1 == 0);
            }
        }

        [Fact]
        public void NewUserHasNoFriends()
        {

            User user = new User();
            Assert.NotNull(user.UserConnections);
            Assert.Equal(0, user.UserConnections.Count);
        }
    }
}