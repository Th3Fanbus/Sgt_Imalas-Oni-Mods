﻿
using HarmonyLib;
using PeterHan.PLib.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilLibs;

namespace ModProfileManager_Addon.Patches
{
    internal class SpritePatch
    {
        public static string import = "MPM_IMPORT";
        public static string export = "MPM_EXPORT";


        [HarmonyPatch(typeof(Assets), "OnPrefabInit")]
        public class Assets_OnPrefabInit_Patch
        {
            [HarmonyPriority(Priority.LowerThanNormal)]
            public static void Prefix(Assets __instance)
            {

                ModAssets.ImportSprite = InjectionMethods.AddSpriteToAssets(__instance, import);
                ModAssets.ExportSprite = InjectionMethods.AddSpriteToAssets(__instance, export);
            }
        }
    }
}