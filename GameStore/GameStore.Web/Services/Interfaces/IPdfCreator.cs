using System.IO;

namespace GameStore.Web.Services.Interfaces
{
    public interface IPdfCreator
    {
        MemoryStream CreateStream(string pdfFileContent);
    }
}
