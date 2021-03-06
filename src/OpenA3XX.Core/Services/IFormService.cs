using System.Collections.Generic;
using OpenA3XX.Core.Forms;

namespace OpenA3XX.Core.Services
{
    public interface IFormService
    {
        IList<FieldConfig> GetHardwareInputSelectorToBoardForm(int hardwareInputSelectorId);
        
        IList<FieldConfig> GetSettingsFormFields();

        IList<FieldConfig> GetSimLinkForHardwareInputSelectorIdForm(int hardwareInputSelectorId);
    }
}