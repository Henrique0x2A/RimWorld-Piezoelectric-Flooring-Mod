using RimWorld;
using Verse;
using UnityEngine;

namespace PiezoelectricFlooring
{
    public class CompPowerPlantPiezo : CompPowerPlant
    {
        private int ticksSinceLastStep = 0;
        private float currentPowerOutput = 0f;
        private float basePowerOutput = 40f; // Base power for integrated tiles
        private float maxPowerOutput = 65f;  // Max with multiplier
        private const float powerDecayRate = 0.94f;
        private const int powerBoostTicks = 60;
        
        public float powerMultiplier = 1.0f; // Set by building def
        
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            
            // Adjust power output based on type
            if (parent.def.defName == "PiezoelectricGenerator")
            {
                basePowerOutput = 50f; // Premium generators are more powerful
                maxPowerOutput = basePowerOutput * powerMultiplier;
            }
        }
        
        public override void CompTick()
        {
            base.CompTick();
            
            if (ticksSinceLastStep > powerBoostTicks)
            {
                currentPowerOutput *= powerDecayRate;
                if (currentPowerOutput < 1f)
                    currentPowerOutput = 0f;
            }
            
            ticksSinceLastStep++;
            PowerOutput = -currentPowerOutput;
        }
        
        public void RegisterFootstep(Pawn pawn)
        {
            float pawnMass = pawn.GetStatValue(StatDefOf.Mass);
            float powerBoost = Mathf.Min(maxPowerOutput, 
                (pawnMass * 0.6f) + basePowerOutput * 0.3f);
            
            currentPowerOutput = Mathf.Min(maxPowerOutput, 
                currentPowerOutput + powerBoost);
            
            ticksSinceLastStep = 0;
            
            // Visual feedback - premium generators have better effects
            float effectChance = parent.def.defName == "PiezoelectricGenerator" ? 0.4f : 0.25f;
            if (Rand.Chance(effectChance))
            {
                FleckMaker.ThrowLightningGlow(parent.DrawPos, parent.Map, 0.6f);
                
                if (parent.def.defName == "PiezoelectricGenerator")
                {
                    // Premium effect for overlay generators
                    FleckMaker.ThrowMicroSparks(parent.DrawPos, parent.Map);
                }
            }
        }
        
        public override string CompInspectStringExtra()
        {
            string baseInfo = $"Power output: {currentPowerOutput:F1}W";
            string maxInfo = $"(Max: {maxPowerOutput:F1}W)";
            return $"{baseInfo} {maxInfo}";
        }
    }
} 