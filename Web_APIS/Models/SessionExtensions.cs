using Microsoft.AspNetCore.Http;
namespace Web_APIS.Models
{
    public static class SessionExtensions
    {
        public static byte[] Get(this ISession session, string key)
        {
            byte[] value;
            session.TryGetValue(key, out value);
            return value;
        }
    }
}
