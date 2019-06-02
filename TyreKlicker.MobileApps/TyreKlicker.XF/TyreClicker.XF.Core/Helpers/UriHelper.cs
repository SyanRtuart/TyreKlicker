using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace TyreKlicker.XF.Core.Helpers
{
    public static class UriHelper
    {
        public static string CombineUri(params string[] uriParts)
        {
            var uri = string.Empty;
            if (uriParts != null && uriParts.Count() > 0)
            {
                char[] trims = {'\\', '/'};
                uri = (uriParts[0] ?? string.Empty).TrimEnd(trims);
                for (var i = 1; i < uriParts.Count(); i++)
                    uri = string.Format("{0}/{1}", uri.TrimEnd(trims), (uriParts[i] ?? string.Empty).TrimStart(trims));
            }

            return uri;
        }

        public static string BuildUri(string root, NameValueCollection query)
        {
            // sneaky way to get a HttpValueCollection (which is internal)
            var collection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var key in query.Cast<string>().Where(key => !string.IsNullOrEmpty(query[key])))
                collection[key] = query[key];

            var builder = new UriBuilder(root) {Query = collection.ToString()};
            return builder.Uri.ToString();
        }
    }
}