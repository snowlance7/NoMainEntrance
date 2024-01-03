using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using NoMainEntrance;
using NoMainEntrance.Patches;

namespace NoMainEntrance
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class NoMainEntranceBase : BaseUnityPlugin
    {
        private const string modGUID = "NoMainEntrance";
        private const string modName = "No Main Entrance";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static NoMainEntranceBase Instance;

        public static ManualLogSource LoggerInstance { get; private set; }

        private void Awake()
        {
            if ((Object)(object)Instance == (Object)null)
            {
                Instance = this;
            }
            LoggerInstance = this.Logger;
            LoggerInstance.LogInfo($"Plugin {modName} loaded successfully.");
            harmony.PatchAll(typeof(NoMainEntranceBase));
            harmony.PatchAll(typeof(EntranceTeleportPatch));
            harmony.PatchAll(typeof(HUDManagerPatch));
        }
    }
}
