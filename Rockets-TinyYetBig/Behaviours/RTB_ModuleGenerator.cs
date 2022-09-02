﻿using KSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Rockets_TinyYetBig.Behaviours
{
    public class RTB_ModuleGenerator : Generator
    {
        [MyCmpGet]
        private Storage storage;

        private Clustercraft clustercraft;
        private Guid poweringStatusItemHandle;
        private Guid notPoweringStatusItemHandle;

        public bool AlwaysActive = false;
        public bool AllowRefill = true;

        public Tag consumptionElement = SimHashes.Void.CreateTag();
        public float consumptionRate = 1f;
        public float consumptionMaxStoredMass = 100f;

        public SimHashes outputElement = SimHashes.Void;
        public float OutputCreationRate = -1f;
        public float OutputTemperature = 293.15f;




        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.connectedTags = new Tag[0];
            this.IsVirtual = true;
        }

        protected override void OnSpawn()
        {
            CraftModuleInterface craftInterface = this.GetComponent<RocketModuleCluster>().CraftInterface;
            this.VirtualCircuitKey = (object)craftInterface;
            this.clustercraft = craftInterface.GetComponent<Clustercraft>();
            Game.Instance.electricalConduitSystem.AddToVirtualNetworks(this.VirtualCircuitKey, (object)this, true);
            base.OnSpawn();
        }

        protected override void OnCleanUp()
        {
            base.OnCleanUp();
            Game.Instance.electricalConduitSystem.RemoveFromVirtualNetworks(this.VirtualCircuitKey, (object)this, true);
        }

        public override bool IsProducingPower() => AlwaysActive || this.clustercraft.IsFlightInProgress();

        public override void EnergySim200ms(float dt)
        {

            RemoveRefillOnSatisfied();
            base.EnergySim200ms(dt);
            var emitter = this.gameObject.GetComponent<RadiationEmitter>();

            if (this.IsProducingPower())
            {
                if (ConsumptionSatisfied(dt))
                {
                    if (emitter != null)
                    {
                        emitter.SetEmitting(true);
                    }

                    this.GenerateJoules(this.WattageRating * dt);

                    if (!(this.poweringStatusItemHandle == Guid.Empty))
                        return;
                    this.poweringStatusItemHandle = this.selectable.ReplaceStatusItem(this.notPoweringStatusItemHandle, Db.Get().BuildingStatusItems.Wattage, (object)this);
                    this.notPoweringStatusItemHandle = Guid.Empty;
                }
                else
                {
                    if (emitter != null)
                    {
                        emitter.SetEmitting(false);
                    }
                }
            }
            else
            {
                if (!(this.notPoweringStatusItemHandle == Guid.Empty))
                    return;
                this.notPoweringStatusItemHandle = this.selectable.ReplaceStatusItem(this.poweringStatusItemHandle, Db.Get().BuildingStatusItems.ModuleGeneratorNotPowered, (object)this);
                this.poweringStatusItemHandle = Guid.Empty;
                

            }
            ResetRefillStatus();
        }

        public bool ConsumptionSatisfied(float dt)
        {
            if (consumptionElement == SimHashes.Void.CreateTag())
                return true;
            else
            {
                //foreach (var consumable in formula.inputs)
                //{
                var ratio = this.ConsumeRessources(dt);
                this.ProduceRessources(dt,ratio);
                if (storage.GetMassAvailable(consumptionElement) < consumptionRate * dt)
                     return false;
                //}

                return true;
            }

        }
        private void ResetRefillStatus()
        {
            if (!AllowRefill &&storage.GetAmountAvailable(consumptionElement) == 0)
            {
                if(clustercraft.Status == Clustercraft.CraftStatus.Grounded)
                {
                    var delivery = this.GetComponent<ManualDeliveryKG>();
                    if(delivery.IsPaused)
                        delivery.Pause(false, "one Refill allowed.");
                    if (outputElement != SimHashes.Void && storage.GetAmountAvailable(outputElement.CreateTag())>0f)
                        storage.DropAll(this.transform.position, true, true); 

                }
            }
        }

        private void RemoveRefillOnSatisfied()
        {
            

            var delivery = this.GetComponent<ManualDeliveryKG>();

            Debug.Log(delivery);

            if (delivery == null || AllowRefill || consumptionElement == SimHashes.Void.CreateTag())
                return;

            //foreach (var consumable in consumptionElement)
            //{
                if (storage.GetMassAvailable(consumptionElement) < consumptionMaxStoredMass)
                    return;
            //}
            delivery.Pause(true, "no Refill allowed.");
            //Destroy(delivery);
            //Debug.Log("Delivery was fullfilled; removed DeliveryComponent on " + this.GetProperName());
        }

        private float ConsumeRessources(float dt)
        {

            if (consumptionElement == SimHashes.Void.CreateTag())
                return 0;

            //foreach (var consumable in formula.inputs)
            //{
                var remainingMats = storage.GetAmountAvailable(consumptionElement);

                float amount = consumptionRate * dt;
                float ratio = 1f;
                ratio = remainingMats < amount ? remainingMats / amount : 1;


                this.storage.ConsumeIgnoringDisease(consumptionElement, amount);
            //}
            return ratio;
        }

        private void ProduceRessources(float dt, float amountLeftMultiplier = 1f)
        {
            if (outputElement == SimHashes.Void)
                return;
            //foreach (var producable in formula.outputs)
            //{
                float amount = OutputCreationRate * dt * amountLeftMultiplier;

                Element elementByHash = ElementLoader.FindElementByHash(outputElement);

                if (elementByHash.IsGas)
                    this.storage.AddGasChunk(outputElement, amount, OutputTemperature, byte.MaxValue, 0, true);
                else if (elementByHash.IsLiquid)
                    this.storage.AddLiquid(outputElement, amount, OutputTemperature, byte.MaxValue, 0, true);
                else
                    this.storage.Store(elementByHash.substance.SpawnResource(this.transform.GetPosition(), amount, OutputTemperature, byte.MaxValue, 0), true);
            //}
        }

    }
}