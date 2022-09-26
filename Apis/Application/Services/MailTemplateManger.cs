using Application.Interfaces;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Global.Shared.Helpers
{
    public class MailTemplateManager : IMailTemplateManager
    {
        // For all base mail types specified in MailTypeEnums
        // will be added into TemplateFilenames dictionary.
        // Gets all current template filenames.
        public MailTemplateManager()
        {
            Filenames = new Dictionary<string, string>();
            MailTypeEnum[] mailTypes = (MailTypeEnum[])Enum.GetValues(typeof(MailTypeEnum));

            // Create template filename dynamically
            foreach (var type in mailTypes)
            {
                var key = type.ToString();
                Filenames[key] = "email-" + key.ToLower() + "-template.html";
            }

            Filenames["Base"] = "email-base.html";
            Filenames["Styles"] = "email-styles.html";
            Filenames["SignatureLong"] = "email-signature-long.html";
        }

        public IDictionary<string, string> Filenames { get; }
    }
}
