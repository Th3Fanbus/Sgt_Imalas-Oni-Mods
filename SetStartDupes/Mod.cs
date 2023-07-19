﻿using HarmonyLib;
using Klei;
using KMod;
using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System;
using System.Collections.Generic;
using System.IO;
using UtilLibs;

namespace SetStartDupes
{
    public class Mod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            ModAssets.LoadAssets();
            PUtil.InitLibrary(false);
            new POptions().RegisterOptions(this, typeof(ModConfig));

            SgtLogger.debuglog("Initializing file paths..");
            ModAssets.DupeTemplatePath = FileSystem.Normalize(Path.Combine(Path.Combine(Manager.GetDirectory(), "config/"),"DuplicantStatPresets/"));
            SgtLogger.debuglog(ModAssets.DupeTemplatePath,"Stat Preset Folder");
            SgtLogger.debuglog("Initializing folders..");
            try
            {
                System.IO.Directory.CreateDirectory(ModAssets.DupeTemplatePath);
            }
            catch (Exception e)
            {
                SgtLogger.error("Could not create folder, Exception:\n" + e);
            }
            SgtLogger.log("Folders succesfully initialized");

            SgtLogger.LogVersion(this);
            new PVersionCheck().Register(this, new SteamVersionChecker());
            base.OnLoad(harmony);
        }
        public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<KMod.Mod> mods)
        {
            base.OnAllModsLoaded(harmony, mods);
            CompatibilityNotifications.CheckAndAddIncompatibles(".Mod.DGSM", "Duplicant Stat Selector","DGSM - Duplicants Generation Settings Manager");
            CompatibilityNotifications.CheckAndAddIncompatibles("RePrint", "Duplicant Stat Selector","Reprint");
            //CheckAndAddIncompatibles(".Mod.WGSM", "WGSM");
        }
    }
}
