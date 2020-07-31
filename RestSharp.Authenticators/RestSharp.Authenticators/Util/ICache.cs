using System;

namespace RestSharp.Authenticators.Util
{
    internal interface ICache<TItem>
    {
        void Add(string key, TItem item, TimeSpan expirationIn);
        TItem Get(string key);
    }
}
