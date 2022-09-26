using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IMailTemplateManager
    {
        IDictionary<string, string> Filenames { get; }
    }
}