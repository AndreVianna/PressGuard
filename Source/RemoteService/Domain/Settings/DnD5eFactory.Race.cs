//using RolePlayReady.Utilities;
//using RolePlayReady.Utilities.Contracts;

//namespace RolePlayReady.RuleSets;

//// ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
//public static partial class DnD5eFactory {
//    // ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
//    private static void SetRaceModifiers(IGameSetting dnd5e) {
//        var elf = dnd5e.GetComponent("Elf");
//        elf.Configure(nameof(Element.Traits)).As(traits => traits
//            .Add("Ability Score Increase", "[FeatureDescription]", x => x.Let("Dexterity").IncreaseBy(2))
//            .And().Add("Age", "[FeatureDescription]", x => x.CheckIf("Age").IsBetween(100, 750, "Elves only mature at the age of 100 years and should live only up to 750 years. Please, consider changing your age."))
//            .And().Add("Size", "[FeatureDescription]", x => x.Let("Size").Be("Medium").And().CheckIf("Height").IsBetween(5m, 6m, "Elves range from under 5 to over 6 feet tall. Please, consider changing your height."))
//            .And().Add("Speed", "[FeatureDescription]", x => x.Let("Movements").Have("Walk", 30))
//            .And().Add("Darkvision", "[FeatureDescription]", x => x.Let("Senses").Have("Darkvision"))
//            .And().Add("Keen Senses", "[FeatureDescription]", x => x.Let("SkillChecks").Have("Perception", "Advantage"))
//            .And().Add("Fey Ancestry", "[FeatureDescription]", x => x.Let("Immunities").Have("Magical Sleep").And().Let("SavingThrows").Have("Charmed", "Advantage"))
//            .And().Add("Trance", "[FeatureDescription]", x => x.AddJournalEntry(Traits, "Elves don't need to sleep. Instead, they meditate deeply for 4 hours a day."))
//            .And().Add("Languages", "[FeatureDescription]", x => x.Let("MaximumLanguagesKnown").Be(2).And().Let("Languages").Have("Common", "Elvish")));

//        var drow = dnd5e.GetComponent("Drow (Dark Elf)").CopyTraitsFrom(elf, excluding: "Darkvision");
//        drow.Configure(nameof(Element.Traits)).As(traits => traits
//            .Replace("Darkvision").With("Superior Darkvision", "[FeatureDescription]", x => x.Let("Senses").Have("Superior Darkvision"))
//            .And().Append("Ability Score Increase").With("[FeatureDescription]", x => x.Let("Charisma").IncreaseBy(1))
//            .And().Add("Elf Weapon Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Rapiers", "Shortswords", "Hand Crossbows"))
//            .And().Add("Drow Magic", "[FeatureDescription]", x => x.AddPowerSource("Drow Innate Magic", "[Add description here]", b
//                => b.AddTag("Innate")
//                     .And().Let("SpellCastingAbility").Be("Charisma")
//                     .And().Let("CantripsKnown").Be(1)
//                     .And().Let("SpellList").Have("Dancing Lights")
//                     .And().If("Level").IsGreaterOrEqualTo(3)
//                           .Then(s => s.Let("SpellsKnown").IncreaseBy(1)
//                                       .And().Let("SlotsPerLevel").Have(1, 1)
//                                       .And().Let("SpellList").Have("Faerie Fire"))
//                     .And().If("Level").IsGreaterOrEqualTo(5)
//                           .Then(s => s.Let("SpellsKnown").IncreaseBy(1)
//                                       .And().Let("SlotsPerLevel").Have(2, 1)
//                                       .And().Let("SpellList").Have("Darkness")))));

//        var woodElf = dnd5e.GetComponent("Elf (Wood)").CopyTraitsFrom(elf, excluding: "Speed");
//        woodElf.Configure(nameof(Element.Traits)).As(traits => traits
//            .Append("Ability Score Increase").With("[FeatureDescription]", x => x.Let("Wisdom").IncreaseBy(1))
//            .And().Add("Fleet of Foot", "[FeatureDescription]", x => x.Let("Movements").Have("Walk", 35))
//            .And().Add("Elf Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Longswords", "Shortswords", "Shortbows", "Longbows"))
//            .And().Add("Mask of the Wild", "[FeatureDescription]", x => x.AddJournalEntry(Traits, "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena.")));

//        var highElf = dnd5e.GetComponent("Elf (High)").CopyTraitsFrom(elf);
//        highElf.Configure(nameof(Element.Traits)).As(traits => traits
//            .Append("Ability Score Increase").With("[FeatureDescription]", x => x.Let("Intelligence").IncreaseBy(1))
//            .And().Add("Extra Language", "[FeatureDescription]", x => x.Let("MaximumLanguagesKnown").IncreaseBy(1))
//            .And().Add("Elf Weapon Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Longswords", "Shortswords", "Shortbows", "Longbows"))
//            .And().Add("Cantrip", "[FeatureDescription]", x => x.AddPowerSource("High Elf Innate Magic", "[Add description here]", b
//                => b.AddTag("Innate")
//                    .And().Let("SpellCastingAbility").Be("Intelligence")
//                    .And().Let("CantripsKnown").Be(1)
//                    .And().Let("SpellList").Have("Wizard"))));

//        var dwarf = dnd5e.GetComponent("Dwarf");
//        dwarf.Configure(nameof(Element.Traits)).As(traits => traits
//            .Add("Ability Score Increase", "[FeatureDescription]", x => x.Let("Constitution").IncreaseBy(2))
//            .And().Add("Age", "[FeatureDescription]", x => x.CheckIf("Age").IsBetween(50, 350, "Dwarves mature at the age of 50 years and live up to 350 years. Please, consider changing your age."))
//            .And().Add("Size", "[FeatureDescription]", x => x.Let("Size").Be("Medium"))
//            .And().Add("Speed", "[FeatureDescription]", x => x.Let("Movements").Have("Walk", 25))
//            .And().Add("Darkvision", "[FeatureDescription]", x => x.Let("Senses").Have("Darkvision", 60))
//            .And().Add("Dwarven Resilience", "[FeatureDescription]", x => x.Let("SavingThrows").Have("Poison", "Advantage").And().Let("Resistances").Have("Poison"))
//            .And().Add("Dwarven Combat Training", "[FeatureDescription]", x => x.Let("Weapons").Have("Battleaxe", "Handaxe", "Light Hammer", "Warhammer"))
//            .And().Add("Tool Proficiency", "[FeatureDescription]", x => x.Let("ToolProficiencies").Have("Smith's Tools", "Brewer's Supplies", "Mason's Tools"))
//            .And().Add("Stonecunning", "[FeatureDescription]", x => x.Let("SkillChecks").Have("Intelligence (History)", "Double Proficiency", "Stonework")));

//        var hillDwarf = dnd5e.GetComponent("Dwarf (Hill)").CopyTraitsFrom(dwarf);
//        hillDwarf.Configure(nameof(Element.Traits)).As(traits => traits
//            .Append("Ability Score Increase").With("[FeatureDescription]", x => x.Let("Wisdom").IncreaseBy(1))
//            .And().Add("Dwarven Toughness", "[FeatureDescription]", x => x.Let("HitPoints").IncreaseBy(1).Per("Level")));

//        var mountainDwarf = dnd5e.GetComponent("Dwarf (Mountain)").CopyTraitsFrom(dwarf);
//        mountainDwarf.Configure(nameof(Element.Traits)).As(traits => traits
//            .Append("Ability Score Increase").With("[FeatureDescription]", x => x.Let("Strength").IncreaseBy(2))
//            .And().Add("Dwarven Armor Training", "[FeatureDescription]", x => x.Let("ArmorProficiencies").Have("Light Armor", "Medium Armor")));

//    }
//}