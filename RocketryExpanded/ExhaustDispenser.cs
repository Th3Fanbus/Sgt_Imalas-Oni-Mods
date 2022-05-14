﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RocketryExpanded
{
    public class ExhaustDispenser : KMonoBehaviour
    {
        public float interval = 1.0f;
        public int amount = 20;
        public float angle = 180.0f;
        private float currentDt = 0f;
        public float speed = 20f;

        public void exhaustMethod(float dt, RocketEngineCluster.StatesInstance smi, KBatchedAnimController animContr,int padCell)
        {
            if(currentDt < interval)
            {
                currentDt += dt;
            }
            else
            {
                var incrementAmount = angle / amount;
                for (float PlacementAngle = 0; PlacementAngle <= angle; PlacementAngle+= incrementAmount)
                {
                    int cell = Grid.PosToCell(smi.master.gameObject.transform.GetPosition() + animContr.Offset);
                    
                    if (Grid.AreCellsInSameWorld(cell, padCell))
                    {
                        GameObject gameObject = Util.KInstantiate(Assets.GetPrefab((Tag)NuclearWasteCometConfig.ID), smi.master.transform.position + animContr.Offset + Vector3.down * 1f, Quaternion.identity);
                        gameObject.SetActive(true);
                        Comet component = gameObject.GetComponent<Comet>();
                        component.ignoreObstacleForDamage.Set(smi.master.gameObject.GetComponent<KPrefabID>());

                        float radian = (float)3.14159274101257f / angle * PlacementAngle;

                        component.Velocity = new Vector2(-Mathf.Cos(radian)*speed, -Mathf.Sin(radian)* speed);
                        component.GetComponent<KBatchedAnimController>().Rotation = (float)PlacementAngle-90f;

                    }
                }
                currentDt -= interval;
            }
        }
    }
}