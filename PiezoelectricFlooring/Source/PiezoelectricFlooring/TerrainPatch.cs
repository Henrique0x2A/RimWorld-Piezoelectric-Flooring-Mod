using RimWorld;
using Verse;
using Verse.AI;
using HarmonyLib;

namespace PiezoelectricFlooring
{
    [HarmonyPatch]
    public static class UniversalPiezoelectricDetection
    {
        [HarmonyPatch(typeof(Pawn_PathFollower), "PatherArrived")]
        [HarmonyPostfix]
        public static void DetectMovement(Pawn ___pawn)
        {
            if (___pawn?.Map == null) return;
            
            IntVec3 position = ___pawn.Position;
            
            // Check for integrated piezoelectric terrain
            TerrainDef terrain = position.GetTerrain(___pawn.Map);
            if (terrain?.defName == "PiezoelectricFloor")
            {
                // Create temporary power generation for terrain-based system
                HandleTerrainPiezoelectric(___pawn, position);
            }
            
            // Check for overlay generators
            var things = position.GetThingList(___pawn.Map);
            foreach (var thing in things)
            {
                if (thing is Building_PiezoGenerator piezoGenerator)
                {
                    piezoGenerator.OnPawnStep(___pawn);
                    break;
                }
            }
        }
        
        private static void HandleTerrainPiezoelectric(Pawn pawn, IntVec3 position)
        {
            // For terrain-based piezoelectric, we need a different approach
            // This could spawn temporary effects or use a different power system
            
            // Simple approach: create visual effects and add power to nearby batteries
            if (Rand.Chance(0.25f))
            {
                FleckMaker.ThrowLightningGlow(position.ToVector3(), pawn.Map, 0.4f);
            }
            
            // Add power to connected grid (simplified)
            // In a full implementation, you'd want proper power grid integration
        }
    }
} 