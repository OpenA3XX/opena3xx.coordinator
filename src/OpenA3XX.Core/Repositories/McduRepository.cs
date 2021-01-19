using System.Collections.Generic;
using OpenA3XX.Core.Enums;
using OpenA3XX.Core.InputState;
using OpenA3XX.Core.InputTypes;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public class McduRepository : IMcduRepository
    {
        public Mcdu Get(CockpitSide cockpitSide)
        {
            var mcdu = new Mcdu();

            var sideKey = cockpitSide == CockpitSide.Captain ? "pilot" : "co-pilot";

            mcdu.Id = $"cockpit:{sideKey}:mcdu";
            mcdu.CockpitSide = cockpitSide;
            mcdu.Inputs = new List<PushButton>
            {
                new() {Name = $"{sideKey}:PushButtonLSK1", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonLSK2", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonLSK3", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonLSK4", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonLSK5", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonLSK6", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonRSK1", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonRSK2", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonRSK3", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonRSK4", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonRSK5", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonRSK6", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonDIR", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonPROG", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonPERF", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonINIT", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonDATA", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonFPLN", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonNAVRAD", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonFUEL", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonSEC", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonATC", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonMENU", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonPREVPAGE", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonUP", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonNEXTPAGE", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonDOWN", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton1", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton2", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton3", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton4", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton5", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton6", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton7", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton8", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton9", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonDOT", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButton0", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonPLUSMINUS", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonA", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonB", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonC", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonD", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonE", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonF", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonG", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonH", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonI", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonJ", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonK", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonL", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonM", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonN", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonO", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonP", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonQ", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonR", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonS", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonT", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonU", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonV", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonW", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonX", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonY", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonZ", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonDIV", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonSP", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonOVFY", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonCLR", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonBRT", State = PushButtonState.Off},
                new() {Name = $"{sideKey}:PushButtonDIM", State = PushButtonState.Off}
            };

            return mcdu;
        }
    }
}