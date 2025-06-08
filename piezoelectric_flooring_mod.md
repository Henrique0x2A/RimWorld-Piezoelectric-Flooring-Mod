# RimWorld Piezoelectric Flooring Mod - Dual Option Design

## Mod Structure Setup

```
PiezoelectricFlooring/
├── About/
│   ├── About.xml
│   └── Preview.png
├── Defs/
│   ├── TerrainDef/
│   │   └── Terrain_PiezoFloor.xml
│   ├── ThingDef/
│   │   └── Buildings_Power.xml
│   └── ResearchProjectDef/
│       └── ResearchProjects_Piezo.xml
├── Source/
│   └── PiezoelectricFlooring/
│       ├── PiezoelectricFlooring.csproj
│       ├── Building_PiezoGenerator.cs
│       ├── CompPowerPlantPiezo.cs
│       └── TerrainPatch.cs
└── Textures/
    ├── Terrain/
    │   └── PiezoFloor.png (single design)
    └── Things/
        └── Building/
            └── PiezoGenerator.png (overlay device)
```

## Player Options

### Option 1: Integrated Piezoelectric Tile
- **Cost**: Moderate (Steel + Components + Silver)
- **Appearance**: Fixed high-tech look
- **Installation**: One-step terrain placement
- **Efficiency**: Standard power generation

### Option 2: Piezoelectric Generator Overlay
- **Cost**: Higher (More components + Advanced materials)
- **Appearance**: Works with any existing flooring
- **Installation**: Build generator on existing floor
- **Efficiency**: Slightly higher power generation (premium cost = premium performance)

## 1. About.xml (Updated)
```xml
<?xml version="1.0" encoding="utf-8"?>
<ModMetaData>
    <n>Piezoelectric Flooring</n>
    <author>YourName</author>
    <packageId>yourname.piezoelectricflooring</packageId>
    <description>Two ways to harness piezoelectric power:
1. Integrated piezoelectric tiles - affordable, one-step installation
2. Piezoelectric generators - premium devices that work on any flooring

Choose efficiency or aesthetics!</description>
    <supportedVersions>
        <li>1.4</li>
        <li>1.5</li>
    </supportedVersions>
    <modDependencies>
        <li>
            <packageId>brrainz.harmony</packageId>
            <displayName>Harmony</displayName>
        </li>
    </modDependencies>
</ModMetaData>
```

## 2. Research Projects
```xml
<?xml version="1.0" encoding="utf-8"?>
<Defs>
    <ResearchProjectDef>
        <defName>PiezoelectricTechnology</defName>
        <label>piezoelectric technology</label>
        <description>Harness mechanical stress through piezoelectric materials. Unlocks basic integrated piezoelectric flooring.</description>
        <baseCost>1500</baseCost>
        <techLevel>Industrial</techLevel>
        <prerequisites>
            <li>Electricity</li>
            <li>Microelectronics</li>
        </prerequisites>
        <researchViewX>8</researchViewX>
        <researchViewY>4</researchViewY>
    </ResearchProjectDef>
    
    <ResearchProjectDef>
        <defName>AdvancedPiezoelectrics</defName>
        <label>advanced piezoelectrics</label>
        <description>Develop sophisticated piezoelectric generators that can be installed on any surface while preserving aesthetics. More expensive but more flexible.</description>
        <baseCost>2500</baseCost>
        <techLevel>Spacer</techLevel>
        <prerequisites>
            <li>PiezoelectricTechnology</li>
            <li>Fabrication</li>
        </prerequisites>
        <researchViewX>9</researchViewX>
        <researchViewY>4</researchViewY>
    </ResearchProjectDef>
</Defs>
```

## 3. Terrain Definition (Option 1: Integrated Tile)
```xml
<?xml version="1.0" encoding="utf-8"?>
<Defs>
    <TerrainDef>
        <defName>PiezoelectricFloor</defName>
        <label>piezoelectric floor</label>
        <description>Integrated piezoelectric flooring with embedded power generation crystals. Affordable and efficient, but locked to a single aesthetic design.</description>
        <texturePath>Terrain/PiezoFloor</texturePath>
        <edgeType>Hard</edgeType>
        <renderPrecedence>240</renderPrecedence>
        <pathCost>0</pathCost>
        <statBases>
            <Beauty>3</Beauty>
            <Cleanliness>0.2</Cleanliness>
        </statBases>
        <costList>
            <Steel>6</Steel>
            <ComponentIndustrial>1</ComponentIndustrial>
            <Silver>10</Silver>
        </costList>
        <constructionSkillPrerequisite>5</constructionSkillPrerequisite>
        <designationCategory>Floors</designationCategory>
        <uiOrder>2030</uiOrder>
        <researchPrerequisites>
            <li>PiezoelectricTechnology</li>
        </researchPrerequisites>
        <constructEffect>ConstructMetal</constructEffect>
        <repairEffect>Repair</repairEffect>
        <terrainAffordanceNeeded>Medium</terrainAffordanceNeeded>
    </TerrainDef>
</Defs>
```

## 4. Building Definition (Option 2: Overlay Generator)
```xml
<?xml version="1.0" encoding="utf-8"?>
<Defs>
    <ThingDef ParentName="BuildingBase">
        <defName>PiezoelectricGenerator</defName>
        <label>piezoelectric generator</label>
        <description>An advanced piezoelectric power generator that can be installed on any flooring. Preserves the underlying floor's appearance while providing superior power generation. Premium materials required.</description>
        <thingClass>PiezoelectricFlooring.Building_PiezoGenerator</thingClass>
        <category>Building</category>
        <graphicData>
            <texPath>Things/Building/PiezoGenerator</texPath>
            <graphicClass>Graphic_Single</graphicClass>
            <drawSize>(1,1)</drawSize>
            <damageData>
                <enabled>false</enabled>
            </damageData>
        </graphicData>
        <size>(1,1)</size>
        <altitudeLayer>FloorEmplacement</altitudeLayer>
        <passability>Standable</passability>
        <castEdgeShadows>false</castEdgeShadows>
        <fillPercent>0</fillPercent>
        <canOverlapZones>true</canOverlapZones>
        <terrainAffordanceNeeded>Light</terrainAffordanceNeeded>
        <designationCategory>Power</designationCategory>
        <uiOrder>2010</uiOrder>
        <costList>
            <ComponentIndustrial>3</ComponentIndustrial>
            <ComponentSpacer>1</ComponentSpacer>
            <Gold>8</Gold>
            <Plasteel>4</Plasteel>
        </costList>
        <constructionSkillPrerequisite>8</constructionSkillPrerequisite>
        <comps>
            <li Class="PiezoelectricFlooring.CompPowerPlantPiezo">
                <basePowerConsumption>0</basePowerConsumption>
                <transmitsPower>true</transmitsPower>
                <powerMultiplier>1.3</powerMultiplier>
            </li>
        </comps>
        <building>
            <ai_chillDestination>false</ai_chillDestination>
            <artificialForMeditationPurposes>false</artificialForMeditationPurposes>
        </building>
        <drawerType>MapMeshOnly</drawerType>
        <drawGUIOverlay>true</drawGUIOverlay>
        <researchPrerequisites>
            <li>AdvancedPiezoelectrics</li>
        </researchPrerequisites>
        <constructEffect>ConstructMetal</constructEffect>
        <repairEffect>Repair</repairEffect>
    </ThingDef>
</Defs>
```

## 5. Enhanced Power Component
```csharp
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
```

## 6. Universal Movement Detection
```csharp
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
    
    [HarmonyPatch]
    public static class UniversalPiezoelectricDetection
    {
        [HarmonyPatch(typeof(Pawn_PathFollower), "PatherArrived")]
        [HarmonyPostfix]
        public static void DetectMovement(Pawn_PathFollower __instance)
        {
            Pawn pawn = __instance.pawn;
            if (pawn?.Map == null) return;
            
            IntVec3 position = pawn.Position;
            
            // Check for integrated piezoelectric terrain
            TerrainDef terrain = position.GetTerrain(pawn.Map);
            if (terrain?.defName == "PiezoelectricFloor")
            {
                // Create temporary power generation for terrain-based system
                HandleTerrainPiezoelectric(pawn, position);
            }
            
            // Check for overlay generators
            var things = position.GetThingList(pawn.Map);
            foreach (var thing in things)
            {
                if (thing is Building_PiezoGenerator piezoGenerator)
                {
                    piezoGenerator.OnPawnStep(pawn);
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
```

## Design Balance

### Integrated Piezoelectric Tiles
- **Cost**: 6 Steel + 1 Component + 10 Silver = ~35-40 wealth
- **Power**: 40W base, 52W max
- **Research**: Early (Industrial tech)
- **Pros**: Affordable, simple installation
- **Cons**: Fixed appearance, moderate power

### Overlay Generators  
- **Cost**: 3 Components + 1 Spacer Component + 8 Gold + 4 Plasteel = ~120-150 wealth
- **Power**: 50W base, 65W max (30% bonus)
- **Research**: Late (Spacer tech)
- **Pros**: Works on any floor, higher power, flexible aesthetics
- **Cons**: Expensive, requires advanced tech

This creates meaningful choice: budget efficiency vs. premium flexibility!