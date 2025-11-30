using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace GameArena.Services
{
    public static class BlobHelper
    {
        private const string BaseUrl = "https://cacaistimages.blob.core.windows.net/img/";

        public static string Blob(this IUrlHelper urlHelper, string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return "";
            return $"{BaseUrl}{fileName}";
        }
    }
}