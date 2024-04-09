using BlazorApp2.Context;

namespace BlazorApp2.Authentication
{
    public class UserAccountService
    {
        private readonly ProjectContext _context;

        public UserAccountService(ProjectContext context)
        {
            _context = context;
        }

        public UserAccount? GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }
        //update n to n+1 and password to h(password)
        public async Task<bool> UpdateUserDetail(UserAccount user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
