using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace GameStore.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}