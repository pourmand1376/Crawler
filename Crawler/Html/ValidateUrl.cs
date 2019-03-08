using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    static class ValidateUrl
    {
        
        public static Tuple<bool,Uri> Validate(string uriName)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return new Tuple<bool, Uri>(result,uriResult);
        }
    }
}
