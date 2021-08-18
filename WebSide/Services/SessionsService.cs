using WebSide.Models;

namespace WebSide.Services;
public class SessionsService
{
    private List<UsersModel> _users = new List<UsersModel>();

    public void AddUser (UsersModel user)
    {
        _users.Add(user);
    }

    public UsersModel TryGetUserById (Guid id)
    {
        return _users.Find(x => x.Id == id);
    }
}
