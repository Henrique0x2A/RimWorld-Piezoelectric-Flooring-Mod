using RimWorld;
using Verse;
using HarmonyLib;

namespace PiezoelectricFlooring
{
    public class Building_PiezoGenerator : Building
    {
        private CompPowerPlantPiezo powerComp;
        
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            powerComp = GetComp<CompPowerPlantPiezo>();
        }
        
        public void OnPawnStep(Pawn pawn)
        {
            powerComp?.RegisterFootstep(pawn);
        }
    }
} 