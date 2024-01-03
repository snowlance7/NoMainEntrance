using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMainEntrance.Patches
{
    [HarmonyPatch(typeof(EntranceTeleport))]
    internal class EntranceTeleportPatch : HarmonyPatch
    {
        [HarmonyPatch("TeleportPlayer")]
        [HarmonyPrefix]
        public static bool TeleportPlayerPrefix(bool ___isEntranceToBuilding, int ___entranceId, bool ___gotExitPoint)
        {
            if (!___gotExitPoint)
            {
                Logger.CreateLogSource("NoMainEntrance").LogInfo((object)"Pre Initial Sweep");
            }
            else if (___entranceId == 0 && ___isEntranceToBuilding)
            {
                Logger.CreateLogSource("NoMainEntrance").LogInfo((object)"Pre Main Entrance Denied");
                HUDManager.Instance.DisplayTip("???", "The entrance appears to be welded shut from the outside...");
                return false;
            }
            return true;
        }

        [HarmonyPatch("FindExitPoint")]
        [HarmonyPostfix]
        public static void FindExitPointPostfix(bool ___isEntranceToBuilding, int ___entranceId, bool ___gotExitPoint, ref bool __result)
        {
            Logger.CreateLogSource("NoMainEntrance").LogInfo((object)$"Entrance ID: {___entranceId}");
            Logger.CreateLogSource("NoMainEntrance").LogInfo((object)$"Is Entrance?: {___isEntranceToBuilding}");
            if (!___gotExitPoint)
            {
                Logger.CreateLogSource("NoMainEntrance").LogInfo((object)"Post Initial Sweep");
            }
            else if (___entranceId == 0 && ___isEntranceToBuilding)
            {
                Logger.CreateLogSource("NoMainEntrance").LogInfo((object)"Post Main Entrance Denied");
                HUDManager.Instance.DisplayTip("???", "The entrance appears to be welded shut from the outside...");
                __result = false;
            }
        }
    }
}