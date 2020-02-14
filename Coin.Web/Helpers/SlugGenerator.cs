using System;

namespace Coin.Web.Helpers
{
    public static class SlugGenerator
    {
        public static string GuidSlug()
        {
            return Guid.NewGuid().ToString();
        }
    }
}