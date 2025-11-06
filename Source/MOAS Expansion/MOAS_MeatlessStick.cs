using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace MOAS_Expansion
{
    public class IngredientValueGetter_MeatlessStick : IngredientValueGetter
    {
        public override float ValuePerUnitOf(ThingDef thingDef)
        {
            if (thingDef.IsNutritionGivingIngestible)
            {
                return thingDef.GetStatValueAbstract(StatDefOf.Nutrition);
            }
            if (thingDef.IsStuff)
            {
                // Allows using non-nutrition items in food recipes, i.e. the meatless stick
                return thingDef.VolumePerUnit;
            }
            return 1f;
        }

        public override string BillRequirementsDescription(RecipeDef recipe, IngredientCount ingredient)
        {
            if (ingredient.IsFixedIngredient)
            {
                return ingredient.GetBaseCount() + "x " + ingredient.filter.Summary;
            }
            //return "BillRequiresNutrition".Translate(ingredient.GetBaseCount().ToString("F")) + " (" + ingredient.filter.Summary + ")";
            return ingredient.GetBaseCount() + "x " + "BillNutrition".Translate() + " (" + ingredient.filter.Summary + ")";
        }
    }
}