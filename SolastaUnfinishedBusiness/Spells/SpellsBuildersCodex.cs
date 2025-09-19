using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SolastaUnfinishedBusiness.Api;
using SolastaUnfinishedBusiness.Api.GameExtensions;
using SolastaUnfinishedBusiness.Api.Helpers;
using SolastaUnfinishedBusiness.Api.LanguageExtensions;
using SolastaUnfinishedBusiness.Behaviors;
using SolastaUnfinishedBusiness.Behaviors.Specific;
using SolastaUnfinishedBusiness.Builders;
using SolastaUnfinishedBusiness.Builders.Features;
using SolastaUnfinishedBusiness.CustomUI;
using SolastaUnfinishedBusiness.Interfaces;
using SolastaUnfinishedBusiness.Models;
using SolastaUnfinishedBusiness.Validators;
using TA.AI;
using TA.AI.Activities;
using UnityEngine.AddressableAssets;
using static ActionDefinitions;
using static RuleDefinitions;
using static SolastaUnfinishedBusiness.Api.DatabaseHelper;
using static SolastaUnfinishedBusiness.Api.DatabaseHelper.ConditionDefinitions;
using static SolastaUnfinishedBusiness.Api.DatabaseHelper.FeatureDefinitionActionAffinitys;
using static SolastaUnfinishedBusiness.Api.DatabaseHelper.FeatureDefinitionMovementAffinitys;
using static SolastaUnfinishedBusiness.Api.DatabaseHelper.FeatureDefinitionPowers;
using static SolastaUnfinishedBusiness.Api.DatabaseHelper.SpellDefinitions;
using Resources = SolastaUnfinishedBusiness.Properties.Resources;


namespace SolastaUnfinishedBusiness.Spells;

internal static partial class SpellBuilders
{
    #region Orb of Elements
    
    internal static SpellDefinition BuildOrbOfElements()
    {
        const string NAME = "OrbOfElements";
        Main.Info("Building Orb of Elements");

        var effectDescription = EffectDescriptionBuilder.Create()
            .SetDurationData(DurationType.Instantaneous)
            .SetTargetingData(
                Side.Enemy,
                RangeType.Distance,
                24,
                TargetType.Individuals
            )
            .SetSavingThrowData(
                false,
                AttributeDefinitions.Dexterity,
                true,
                EffectDifficultyClassComputation.SpellCastingFeature
            )
            .SetEffectForms(
                EffectFormBuilder
                    .Create()
                    .SetDamageForm(
                        damageType: DamageTypeFire, // TODO: later allow choice of element
                        dieType: DieType.D8,
                        diceNumber: 6
                    )
                    .Build()
            )
            .Build();

        var spell = SpellDefinitionBuilder
            .Create(NAME)
            .SetGuiPresentation(
                NAME,
                Category.Spell
            )
            .SetSchoolOfMagic(SchoolOfMagicDefinitions.SchoolEvocation)
            .SetSpellLevel(4)
            .SetCastingTime(ActivationTime.Action)
            .SetVerboseComponent(true)
            .SetSomaticComponent(true)
            .SetMaterialComponent(MaterialComponentType.Specific)
            //.SetSpecificMaterialComponent("Orb", 0, false)
            .SetEffectDescription(effectDescription)
            .AddToDB();
        
        return spell;
    }

    #endregion
    
}
