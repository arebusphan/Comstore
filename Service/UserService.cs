namespace Service;
using Repository;
using Data;
public class UserService
{
    private readonly UserRepository _userRepository;
public UserService(UserRepository userRepository)
{
_userRepository = userRepository;
}
public async Task<User> Login(string email, string password)
{
var user = await _userRepository.Login(email, password);
return user;
}
public async Task<User> Register(User user)
{user = await _userRepository.Register(user);
return user;
}
public async Task<User> GetUserById(int id)
{var user = await _userRepository.GetUserById(id);
return user;
}
}