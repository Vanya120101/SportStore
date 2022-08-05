using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SportsStore.WebService.Infrastructure;
public static class SessionExstensions
{
    public static void SetJson(this ISession session, string key, object value) => session.SetString(key, JsonConvert.SerializeObject(value));

    public static T GetJson<T>(this ISession session, string key)
    {
        var sessionData = session.GetString(key);
        return sessionData == null ? default : JsonConvert.DeserializeObject<T>(sessionData);
    }
}
