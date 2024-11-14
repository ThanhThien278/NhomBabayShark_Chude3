using Newtonsoft.Json;

namespace ShopKoiTranS.Repository
{
    public static class SessionExtensions
    {
        // Set an object as JSON in the session
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Get an object from JSON in the session
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
