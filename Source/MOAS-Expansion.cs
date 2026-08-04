using RimWorld;
using Verse;

namespace MOAS_Expansion;

// Allow non-nutrition items to be used in food recipes
public class IngredientValueGetter_MeatlessStick : IngredientValueGetter
{
    public override float ValuePerUnitOf(ThingDef thingDef)
    {
        if (thingDef.IsNutritionGivingIngestible)
            return thingDef.GetStatValueAbstract(StatDefOf.Nutrition);
        return thingDef.IsStuff ? thingDef.VolumePerUnit : 1f;
    }

    public override string BillRequirementsDescription(RecipeDef recipe, IngredientCount ingredient)
    {
        return ingredient.IsFixedIngredient ? $"{ingredient.GetBaseCount().ToString()}x {ingredient.filter.Summary}" : (string) (ingredient.GetBaseCount().ToString() + "x " + "BillNutrition".Translate() + " (" + ingredient.filter.Summary + ")");
    }
}

// Add rawFungus to its own SpecialThingFilterWorker (veg == Fungus)
public class SpecialThingFilterWorker_MOAS_RawFungus : SpecialThingFilterWorker
{
    public override bool Matches(Thing t) => this.AlwaysMatches(t.def);

    public override bool AlwaysMatches(ThingDef def)
    {
        return def.ingestible != null
               && def.ingestible.foodType.HasFlag(FoodTypeFlags.Fungus);
    }

    public override bool CanEverMatch(ThingDef def) => this.AlwaysMatches(def);
}

// Add rawFungus to its own SpecialThingFilterWorker (veg != Fungus)
public class SpecialThingFilterWorker_MOAS_RawVegNotFungus : SpecialThingFilterWorker
{
    public override bool Matches(Thing t) => this.AlwaysMatches(t.def);

    public override bool AlwaysMatches(ThingDef def)
    {
        return def.ingestible == null
               || !def.ingestible.foodType.HasFlag(FoodTypeFlags.Fungus);
    }

    public override bool CanEverMatch(ThingDef def) => this.AlwaysMatches(def);
}