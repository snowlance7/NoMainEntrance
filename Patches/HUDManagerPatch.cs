using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMainEntrance.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HUDManagerPatch : HarmonyPatch
    {
        [HarmonyPatch("DisplayTip")]
        [HarmonyPostfix]
        public static void DisplayTipPostfix(string headerText, string bodyText, bool isWarning = false, bool useSave = false, string prefsKey = "LC_Tip1")
        {
            Logger.CreateLogSource("NoMainEntrance").LogInfo((object)"Displaying Tip On Hud");
            Logger.CreateLogSource("NoMainEntrance").LogInfo((object)(bodyText ?? ""));
        }
    }
}
