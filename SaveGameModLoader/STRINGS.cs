﻿
namespace SaveGameModLoader
{
    public class STRINGS
    {
        public class UI
        {
            public class FRONTEND
            {
                public class MODSYNCING
                {
                    public static LocString CONTINUEANDSYNC = "SYNC AND RESUME";

                    public static LocString SYNCMODSBUTTONBG = "SYNC MODS";
                    public static LocString SYNCALL = "SYNC ALL AND LOAD";
                    public static LocString MODDIFFS = "MOD DIFFERENCES";
                    public static LocString SYNCSELECTED = "SYNC CURRENT CONFIG AND LOAD";
                    public static LocString MISSINGMOD = "MISSING MODS!";
                    public static LocString ALLSYNCED = "ALL MODS SYNCED, CLOSE";

                    public static LocString EXPORTMODLISTCONFIRMSCREEN = "Export Modlist";
                    public static LocString IMPORTMODLISTCONFIRMSCREEN = "Paste collection link to import Modlist";

                    public static LocString MISSINGMODSTITLE = "MISSING MODS";
                }
                public class MODLISTVIEW
                {
                    public static LocString MODLISTSBUTTON = "MODLISTS";
                    public static LocString MODLISTSBUTTONINFO = "View and manage all your modlists";

                    public static LocString MODLISTWINDOWTITLE = "Modlists";

                    public static LocString IMPORTCOLLECTIONLIST = "Import Modlist";
                    public static LocString IMPORTCOLLECTIONLISTTOOLTIP = "Import a modlist from a Steam collection";

                    public static LocString EXPORTMODLISTBUTTON =  "Export Modlist";
                    public static LocString EXPORTMODLISTBUTTONINFO = "Export your current mod config to a file.";
                    public static LocString OPENMODLISTFOLDERBUTTON = "Open Modlist Folder";
                    public static LocString OPENMODLISTFOLDERBUTTONINFO = "Open the Modlist Folder to see all stored modlists";

                    public static LocString MODLISTSTANDALONEHEADER = "Created & Imported Modlists";
                    public static LocString MODLISTSAVEGAMEHEADER = "Modlists from SaveGames";

                    public static LocString IMPORTEDTITLEANDAUTHOR = "{0}, Collection by {1}";

                    public class SINGLEENTRY
                    {
                        public static LocString ONESTOREDLIST = " stored List";
                        public static LocString MORESTOREDLISTS = " stored Lists";
                        public static LocString LATESTCOUNT = "Latest Version contains {0} Mods.";
                    }

                    public class POPUP
                    {
                        public static LocString ERRORTITLE = "Could not import Collection";
                        public static LocString WRONGFORMAT = "Not a valid Collection link!";
                        public static LocString SUCCESSTITLE = "Import successful";
                        public static LocString ADDEDNEW = "Successfully imported Modlist:\n{0}";

                    }
                }

                public class SINGLEMODLIST
                {
                    public static LocString TITLE = "ModLists stored in {0}";
                    public static LocString TITLESAVEGAMELIST = "ModLists from SaveGame \"{0}\"";
                    public static LocString MODLISTCOUNT= "Modlist contains {0} mods.";
                    public static LocString LOADLIST= "View Modlist";
                    public static LocString SYNCLIST = "Apply Modlist";
                    public static LocString SUBTOALL = "Subscribe to all missing Mods";
                    public static LocString REFRESH = "Refresh View";
                    public static LocString RETURN= "RETURN";
                    public static LocString RETURNTWO= "RETURN TO MODS";
                    public static LocString POPUPSYNCEDTITLE = "Mod List Loaded";
                    public static LocString POPUPSYNCEDTEXT= "The modlist has been applied.";
                    public static LocString WORKSHOPFINDTOOLTIP= "Click here to visit the Steam Workshop page of this mod.";
                    //public static LocString WORKSHOPFINDTOOLTIP= "Click here to subscribe to this mod.";
                    public static LocString NOSTEAMMOD= "Not a Steam Mod!";
                    public static LocString MISSING= "Missing";

                    public static LocString WARNINGMANYMODS = "Large amount of mod differences detected.";
                    public static LocString WARNINGMANYMODSQUESTION = "Large amount of mod differences can lead to long game freezes.\nRestart game with this Modlist applied?\n(This is an alternative mod list deployment mode).";
                    public static LocString USEALTERNATIVEMODE = "Load mod list with a restart\n(Restarts game)";
                    public static LocString USENORMALMETHOD = "Load mod list using normal method\n(could freeze the game)";

                }

                
            }
        }
    }
}
