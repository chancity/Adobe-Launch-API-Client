using System.Threading.Tasks;

namespace AdobeShared
{
    public interface ITokenGetter<T>
    {
        Task<T> GetToken();
    }

    public interface ITokenSetter<T>
    {
        Task<bool> SetToken(T token);
    }

    public interface ITokenHandler<T> : ITokenGetter<T>, ITokenSetter<T>
    {}
}