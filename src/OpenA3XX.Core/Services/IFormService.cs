using System.Collections.Generic;
using OpenA3XX.Core.Forms;

namespace OpenA3XX.Core.Services
{
    public interface IFormService
    {
        public IList<FieldConfig> GetSettingsFormFields();
    }
}