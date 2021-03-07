﻿using System.Collections.Generic;
using OpenA3XX.Core.Models;

namespace OpenA3XX.Core.Repositories
{
    public interface ISimulatorEventRepository
    {
        SimulatorEvent AddSimulatorEvent(SimulatorEvent simulatorEvent);

        IList<SimulatorEvent> GetAllSimulatorEvents();
    }
}