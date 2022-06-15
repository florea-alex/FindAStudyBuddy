using ProiectMDS.DAL.Entities.Auth;

namespace ProiectMDS_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            User user = new User();
            Assert.Equal(0, user.UserConnections.Count);
        }
    }
}