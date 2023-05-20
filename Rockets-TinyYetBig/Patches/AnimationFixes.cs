﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilLibs;

namespace Rockets_TinyYetBig.Patches
{
    internal class AnimationFixes
    {
        [HarmonyPatch(typeof(OxidizerTank), nameof(OxidizerTank.OnSpawn))]
        public static class FixLOXMeterV1
        {
            public static void Postfix(OxidizerTank __instance)
            {
                ///this is a LOX module, it needs a meter.. but it wont get one in kleis implementation, so we need to add one
                if (!__instance.supportsMultipleOxidizers)
                {
                    __instance.meter = new MeterController(__instance.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[4]
                        {
                            "meter_target",
                            "meter_fill",      
                            "meter_frame",
                            "meter_OL"
                    });
                    __instance.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
                }
                //this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.capacityKg);
            }
        }

        [HarmonyPatch(typeof(OxidizerTank), nameof(OxidizerTank.RefreshMeter))]
        public static class FixLOXMeterV2
        {
            /// <summary>
            /// Apply Meter changes on LOX module
            /// </summary>
            /// <param name="__instance"></param>
            public static void Postfix(OxidizerTank __instance)
            {
                if (!__instance.supportsMultipleOxidizers && __instance.meter!=null)
                {
                    __instance.meter.SetPositionPercent(__instance.storage.MassStored() / __instance.storage.capacityKg);
                }
            }
        }
    }
}