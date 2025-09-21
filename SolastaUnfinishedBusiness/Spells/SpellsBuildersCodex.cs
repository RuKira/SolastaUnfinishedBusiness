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

        var damageTypes = new[] { DamageTypeAcid, DamageTypeCold, DamageTypeFire, DamageTypeLightning, DamageTypePoison, DamageTypeThunder, DamageTypeRadiant, DamageTypeNecrotic };
        var sprite = Sprites.GetSprite(NAME, Resources.OrbOfElements, 128);
        var subSpells = (from damageType in damageTypes
            let title = Gui.Localize($"Tooltip/&Tag{damageType}Title")
            let description = Gui.Format("Spell/&SubSpellOrbOfElementsDescription", title)
            select SpellDefinitionBuilder
                .Create($"{NAME}_{damageType}")
                .SetGuiPresentation(title, description, sprite)
                .SetSchoolOfMagic(SchoolOfMagicDefinitions.SchoolEvocation)
                .SetSpellLevel(4)
                .SetCastingTime(ActivationTime.Action)
                //.SetMaterialComponent(MaterialComponentType.Specific)
                //.SetSpecificMaterialComponent("Orb", 0, false)
                .SetVerboseComponent(true)
                .SetSomaticComponent(true)
                .SetVocalSpellSameType(VocalSpellSemeType.Attack)
                .SetEffectDescription(EffectDescriptionBuilder.Create()
                    .SetTargetingData(Side.Enemy, RangeType.Distance, 24, TargetType.IndividualsUnique, 4)
                    .SetEffectForms(EffectFormBuilder.DamageForm(damageType, 6, DieType.D8))
                    .Build())
                .AddToDB()).ToList();

        return SpellDefinitionBuilder
            .Create(NAME)
            .SetGuiPresentation(NAME, Category.Spell, sprite)
            .SetSchoolOfMagic(SchoolOfMagicDefinitions.SchoolEvocation)
            .SetSpellLevel(4)
            .SetCastingTime(ActivationTime.Action)
            //.SetMaterialComponent(MaterialComponentType.Specific)
            //.SetSpecificMaterialComponent("Orb", 0, false)
            .SetVocalSpellSameType(VocalSpellSemeType.Attack)
            .SetSomaticComponent(true)
            .SetVerboseComponent(true)
            .SetSubSpells([.. subSpells])
            .SetEffectDescription(
                EffectDescriptionBuilder
                    .Create()
                    .SetTargetingData(Side.Enemy, RangeType.Distance, 24, TargetType.IndividualsUnique, 4)
                    .Build())
            .AddToDB();
    }

    #endregion
}
