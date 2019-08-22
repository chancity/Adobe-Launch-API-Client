using System.Threading.Tasks;
using AdobeLaunch.Client.Models;

namespace AdobeLaunch.Client.HelperInterfaces
{
    public interface ITokenHandler<T>
    {
        Task<T> GetToken();
        Task<bool> SetToken(T token);
    }
}