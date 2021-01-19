using System.Collections.Generic;
using OpenA3XX.Core.Enums;
using OpenA3XX.Core.InputTypes;

namespace OpenA3XX.Core.Models
{
    public class Mcdu : ICockpitComponent
    {
        public IList<PushButton> Inputs { get; set; }

        public CockpitSide CockpitSide { get; set; }

        public CockpitComponent CockpitComponent => CockpitComponent.Mcdu;

        public string Id { get; set; }

        public bool IsSharedCockpitComponent => false;
    }
}