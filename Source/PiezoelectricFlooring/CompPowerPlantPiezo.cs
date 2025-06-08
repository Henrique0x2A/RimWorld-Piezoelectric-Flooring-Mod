using RimWorld;
using Verse;
using UnityEngine;

namespace PiezoelectricFlooring
{
    public class CompPowerPlantPiezo : CompPowerPlant
    {
        private int ticksSinceLastStep = 0;
        private float currentPowerOutput = 0f;
        private const float maxPowerOutput = 50f; // Maximum watts
        private const float powerDecayRate = 0.95f; // How quickly power decays
        private const int powerBoostTicks = 60; // How long power boost lasts
        
        public override void CompTick()
        {
            base.CompTick();
            
            // Decay power output over time
            if (ticksSinceLastStep > powerBoostTicks)
            {
                currentPowerOutput *= powerDecayRate;
                if (currentPowerOutput < 1f)
                    currentPowerOutput = 0f;
            }
            
            ticksSinceLastStep++;
            PowerOutput = -currentPowerOutput; // Negative because we're generating power
        }
        
        public void RegisterFootstep(Pawn pawn)
        {
            // Calculate power boost based on pawn mass and movement
            float powerBoost = Mathf.Min(maxPowerOutput, 
                (pawn.GetStatValue(StatDefOf.Mass) * 0.5f) + 5f);
            
            currentPowerOutput = Mathf.Min(maxPowerOutput, 
                currentPowerOutput + powerBoost);
            
            ticksSinceLastStep = 0;
            
            // Visual effect
            if (Rand.Chance(0.3f))
            {
                FleckMaker.ThrowLightningGlow(parent.DrawPos, parent.Map, 0.5f);
            }
        }
        
        public override string CompInspectStringExtra()
        {
            return $"Power output: {currentPowerOutput:F1}W";
        }
    }
}