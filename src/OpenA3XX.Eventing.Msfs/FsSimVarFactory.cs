﻿using System.Collections.Generic;

namespace Opena3XX.Eventing.Msfs
{
    public static class FsSimVarFactory
    {
        private static Dictionary<FsSimVar, string> _enumToCodeDictionary = new Dictionary<FsSimVar, string>();

        static FsSimVarFactory()
        {
            _enumToCodeDictionary.Add(FsSimVar.Title, "TITLE");
            _enumToCodeDictionary.Add(FsSimVar.PlaneLatitude, "PLANE LATITUDE");
            _enumToCodeDictionary.Add(FsSimVar.PlaneLongitude, "PLANE LONGITUDE");
            _enumToCodeDictionary.Add(FsSimVar.PlaneAltitudeAboveGround, "PLANE ALT ABOVE GROUND");
            _enumToCodeDictionary.Add(FsSimVar.PlaneAltitude, "PLANE ALTITUDE");
            _enumToCodeDictionary.Add(FsSimVar.PlaneHeadingDegreesTrue, "PLANE HEADING DEGREES TRUE");
            _enumToCodeDictionary.Add(FsSimVar.GpsGroundSpeed, "GPS GROUND SPEED");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simvar"></param>
        /// <returns></returns>
        public static string GetSimVarCode(FsSimVar simVar)
        {
            return _enumToCodeDictionary[simVar];
        }
    }
}