using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;

namespace PiezoelectricFlooring
{
    public class Building_PiezoFloor : Building
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
    
    // Harmony patch to detect when pawns step on piezoelectric floors
    [HarmonyPatch]
    public static class PawnMovementPatch
    {
        [HarmonyPatch(typeof(Pawn_PathFollower), "PatherArrived")]
        [HarmonyPostfix]
        public static void Postfix(Pawn_PathFollower __instance)
        {
            Pawn pawn = __instance.pawn;
            if (pawn?.Map == null) return;
            
            IntVec3 position = pawn.Position;
            TerrainDef terrain = position.GetTerrain(pawn.Map);
            
            if (terrain?.defName == "PiezoelectricFloor")
            {
                // Find any piezoelectric buildings at this location
                var things = position.GetThingList(pawn.Map);
                foreach (var thing in things)
                {
                    if (thing is Building_PiezoFloor piezoBuilding)
                    {
                        piezoBuilding.OnPawnStep(pawn);
                        break;
                    }
                }
            }
        }
    }
}