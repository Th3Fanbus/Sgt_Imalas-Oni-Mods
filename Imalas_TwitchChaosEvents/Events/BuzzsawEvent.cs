﻿using ONITwitchLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Util_TwitchIntegrationLib;
using UtilLibs;
using static STRINGS.UI.CLUSTERMAP;

namespace Imalas_TwitchChaosEvents.Events
{
    internal class BuzzsawEvent : ITwitchEventBase
    {
        public string ID => "ChaosTwitch_Buzzsaw";

        public string EventGroupID => null;

        public string EventName => STRINGS.CHAOSEVENTS.BUZZSAW.NAME;

        public string EventDescription => STRINGS.CHAOSEVENTS.BUZZSAW.TOASTTEXT;

        public EventWeight EventWeight => EventWeight.WEIGHT_NORMAL;

        public Action<object> EventAction => (object data) =>
        {

            ToastManager.InstantiateToast(
            STRINGS.CHAOSEVENTS.BUZZSAW.TOAST,
             STRINGS.CHAOSEVENTS.BUZZSAW.TOASTTEXT
             );

            GameScheduler.Instance.Schedule("buzzsaw", 3f, _ =>
            {
                int activeWorld = ClusterManager.Instance.activeWorldId;
                //rain.StartRaining();
                if (ClusterManager.Instance.activeWorld.IsModuleInterior)
                {
                    activeWorld = 0;
                }

                var world = ClusterManager.Instance.GetWorld(activeWorld);
                //var pos = world.LookAtSurface();
                SpawnBuzzSaw();

            });
        };

        public Func<object, bool> Condition =>
            (data) =>
            {
                return false;
                return true;
            };

        public Danger EventDanger => Danger.High;

        public void SpawnBuzzSaw()
        {
            var spawningPosition = Grid.CellToPos(ONITwitchLib.Utils.PosUtil.ClampedMouseCellWithRange(10));
            SgtLogger.l(spawningPosition.ToString(), "POS1");
            spawningPosition.z = (int)Grid.SceneLayer.Move;
            var blade = Util.KInstantiate(ModAssets.OmegaSawblade, spawningPosition);
            SgtLogger.l(spawningPosition.ToString(), "POS2");
            blade.SetActive(true);

        }
    }
}
