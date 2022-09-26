using System;
using System.IO;

namespace Domain.Tests
{
    public class SetupWebAPIPath
    {
        public static string GetBasePath()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var parentDirectory = currentDirectory?.Parent?.Parent?.Parent?.Parent;
            if (parentDirectory is null || !parentDirectory.Exists)
                throw new InvalidOperationException("Path doesn't exist.");
            var destinationPath = Path.Combine(parentDirectory!.FullName, "WebAPI.Tests");

            return destinationPath;
        }
    }
}
