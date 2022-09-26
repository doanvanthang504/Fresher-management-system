using System.IO;
using System.Reflection;
using Global.Shared.Commons;
namespace Global.Shared.ExportExcelExtensions
{
    public class LoadExcelTemplate
    {
        public static Stream? GetStream(string fileName)
        {
            var _fileStream = Assembly.Load(Constant.EXCEL_TEMPLATE_ASSEMBLY)
                                        .GetManifestResourceStream($"{Constant.EXCEL_TEMPLATE_ASSEMBLY}." +
                                                                   $"{Constant.EXCEL_TEMPLATE_SUBFOLDER_ASSEMBLY}." +
                                                                   $"{fileName}");
            return _fileStream;
        }
    }
}
