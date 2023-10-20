//using RolePlayReady.Models;
//using RolePlayReady.Utilities.Contracts;

//using Domain = RolePlayReady.Models.Domain;
//using Source = RolePlayReady.Models.Source;

//namespace RolePlayReady.RuleSets;

//// ReSharper disable StringLiteralTypo
//// ReSharper disable once InconsistentNaming - DnD5e is the official name of the system.
//public static partial class DnD5eFactory {

//    public static Domain Create() {
//        var ruleSet = new Domain {
//            Owner = "System",
//            Name = "Dungeons & Dragons 5th Edition",
//            ShortName = "DnD5e",
//            Description = "Dungeons & Dragons 5th Edition is a tabletop role-playing game system.",
//            State = State.Public,
//        };

//        var sources = new[] { new Source {
//                ShortName = "PHB",
//                Name = "Player's Handbook",
//                Description = "The Player's Handbook is the essential reference for every Dungeons & Dragons roleplayer. It contains rules for character creation and advancement, backgrounds and skills, exploration and combat, equipment, spells, and much more.",
//                Publisher = "Wizards of the Coast",
//            }
//        };
//        foreach (var source in sources) {
//            ruleSet.Sources.Add(source);
//        }

//        var sections = new (string Type, string Name, string Description)[]
//        {
//            (nameof(Agent), "Character", "[Add description here]"),
//            (nameof(Agent), "Non-Player Character", "[Add description here]"),
//            (nameof(Agent), "Creature", "[Add description here]"),
//            (nameof(Node), "Race", "Represents the character's race in Dungeons & Dragons 5th Edition."),
//            (nameof(Node), "Class", "Represents the character's class, which determines their abilities and progression."),
//            (nameof(Node), "Subclass", "Represents a specialization within a class, granting additional abilities and options."),
//            (nameof(Node), "Background", "Represents the character's backstory and personal history, which can provide additional proficiencies and roleplaying hooks."),
//            (nameof(Node), "Feat", "Represents special abilities and talents that a character can acquire, often providing unique options or enhancements."),
//            (nameof(Power), "Spell", "Represents magical abilities and effects that characters can learn and cast, with various effects and power levels."),
//            (nameof(Power), "Cantrip", "[Add description here]"),
//            (nameof(Power), "Equipment", "Represents items, weapons, armor, and other gear that characters can acquire and use throughout their adventures."),
//        };
//        //foreach (var (type, name, description) in sections) {
//        //    ruleSet.Children.Add((INode)ElementFactory.For(ruleSet, "System").Create(type, name, description));
//        //}

//        var attributes = new (Type Type, string Name, string Description)[] {a
//            (typeof(bool), "Inspiration", "[Add description here]"),
//            (typeof(decimal), "Height", "[Add description here]"),
//            (typeof(decimal), "Weight", "[Add description here]"),
//            (typeof(Dictionary<string, int>), "Classes", "[Add description here]"),
//            (typeof(Dictionary<string, int>), "Currency", "[Add description here]"),
//            (typeof(Dictionary<string, int>), "Moviments", "[Add description here]"),
//            (typeof(Dictionary<string, int>), "AbilityCharges", "[Add description here]"),
//            (typeof(Dictionary<string, string>), "SavingThrows", "[Add description here]"),
//            (typeof(Dictionary<string, string>), "SkillChecks", "[Add description here]"),
//            (typeof(int), "ArmorClass", "[Add description here]"),
//            (typeof(int), "CarryingCapacity", "[Add description here]"),
//            (typeof(int), "ChallengeRating", "[Add description here]"),
//            (typeof(int), "Charisma", "[Add description here]"),
//            (typeof(int), "Constitution", "[Add description here]"),
//            (typeof(int), "CurrentCarryingWeight", "[Add description here]"),
//            (typeof(int), "CurrentHitPoints", "[Add description here]"),
//            (typeof(int), "Dexterity", "[Add description here]"),
//            (typeof(int), "ExhaustionLevel", "[Add description here]"),
//            (typeof(int), "Intelligence", "[Add description here]"),
//            (typeof(int), "MaximumLanguagesKnown", "[Add description here]"),
//            (typeof(int), "MaximumHitPoints", "[Add description here]"),
//            (typeof(int), "ProficiencyBonus", "[Add description here]"),
//            (typeof(int), "Strength", "[Add description here]"),
//            (typeof(int), "TemporaryHitPoints", "[Add description here]"),
//            (typeof(int), "Wisdom", "[Add description here]"),
//            (typeof(string), "Alignment", "[Add description here]"),
//            (typeof(string), "Appearance", "[Add description here]"),
//            (typeof(string), "Background", "[Add description here]"),
//            (typeof(string), "CreatureType", "[Add description here]"),
//            (typeof(string), "EyeColor", "[Add description here]"),
//            (typeof(string), "Gender", "[Add description here]"),
//            (typeof(string), "HairColor", "[Add description here]"),
//            (typeof(string), "NPCType", "[Add description here]"),
//            (typeof(string), "PassiveInsight", "[Add description here]"),
//            (typeof(string), "PassivePerception", "[Add description here]"),
//            (typeof(string), "Race", "[Add description here]"),
//            (typeof(string), "Size", "[Add description here]"),
//            (typeof(string), "SkinColor", "[Add description here]"),
//            (typeof(string), "SpellCastingAbility", "[Add description here]"),
//            (typeof(HashSet<string>), "Actions", "[Add description here]"),
//            (typeof(HashSet<string>), "Archetypes", "[Add description here]"),
//            (typeof(HashSet<string>), "Armors", "[Add description here]"),
//            (typeof(HashSet<string>), "Feats", "[Add description here]"),
//            (typeof(HashSet<string>), "Immunities", "[Add description here]"),
//            (typeof(HashSet<string>), "LairActions", "[Add description here]"),
//            (typeof(HashSet<string>), "Languages", "[Add description here]"),
//            (typeof(HashSet<string>), "LegendaryActions", "[Add description here]"),
//            (typeof(HashSet<string>), "Resistances", "[Add description here]"),
//            (typeof(HashSet<string>), "Senses", "[Add description here]"),
//            (typeof(HashSet<string>), "Traits", "[Add description here]"),
//            (typeof(HashSet<string>), "Tools", "[Add description here]"),
//            (typeof(HashSet<string>), "Vulnerabilities", "[Add description here]"),
//            (typeof(HashSet<string>), "Weapons", "[Add description here]"),
//        };
//        //foreach (var attribute in attributes) {
//        //    ruleSet.AttributeDefinitions.Add(new Definition {
//        //        Procedure = ruleSet,
//        //        Domain = ruleSet,
//        //        Owner = "System",
//        //        Name = attribute.Name,
//        //        DataType = attribute.EntityType,
//        //        Description = attribute.Description,
//        //        State = State.Public,
//        //    });
//        //}

//        var raceSection = ruleSet.GetComponent("Race");
//        var playersHandbook = ruleSet.GetSource("PHB");
//        var races = new (string Name, Models.Usage Usage, string Description)[]
//        {
//            ("Dragonborn", Usage.Standard, "Dragonborn are proud and self-assured, drawing upon their draconic ancestry for strength and power."),
//            ("Dwarf", Usage.Template, "Dwarves are a stout and hardy folk, known for their resilience and craftsmanship."),
//            ("Dwarf (Hill)", Usage.Standard, "Hill Dwarves are known for their toughness and resistance, as well as their deep connection to the earth."),
//            ("Dwarf (Mountain)", Usage.Standard,
//                "Mountain Dwarves are strong and skilled warriors, at home in the mountains and proficient in the use of armor."),
//            ("Elf", Usage.Template, "Elves are a graceful and long-lived race, with a deep connection to the natural world."),
//            ("Elf (High)", Usage.Standard, "High Elves are skilled in magic and intellect, with an affinity for the arcane arts."),
//            ("Elf (Wood)", Usage.Standard, "Wood Elves are nimble and stealthy, living in harmony with the forests and excelling at archery."),
//            ("Elf (Eladrin)", Usage.Standard, "Eladrin are attuned to the Feywild and can manifest the power of the seasons through their emotions."),
//            ("Drow (Dark Elf)", Usage.Standard, "Drow, or Dark Elves, are known for their cunning and exceptional skill in the use of magic and stealth."),
//            ("Gnome", Usage.Template, "Gnomes are small, curious, and inventive beings, with an innate affinity for illusion and enchantment."),
//            ("Gnome (Forest)", Usage.Standard, "Forest Gnomes are stealthy and in tune with nature, having a natural ability to speak with small animals."),
//            ("Gnome (Rock)", Usage.Standard, "Rock Gnomes are skilled in the crafting of machines and mechanical devices, with a keen interest in technology."),
//            ("Half-Elf", Usage.Standard, "Half-Elves combine the best of both human and elven traits, exhibiting versatility and adaptability."),
//            ("Half-Elf (Variant)", Usage.Standard,
//                "Half-Elf Variants display traits from their non-human parent's lineage, granting them additional abilities."),
//            ("Half-Orc", Usage.Standard, "Half-Orcs are strong and resilient, possessing a fierce determination and indomitable spirit."),
//            ("Halfling", Usage.Template, "Halflings are small, nimble, and good-natured beings, known for their luck and resourcefulness."),
//            ("Halfling (Lightfoot)", Usage.Standard, "Lightfoot Halflings are stealthy and elusive, easily able to blend into their surroundings."),
//            ("Halfling (Stout)", Usage.Standard, "Stout Halflings are hardy and resistant to poison, with a strong connection to their dwarven ancestry."),
//            ("Human", Usage.Standard, "Humans are a versatile and adaptable race, capable of excelling in a wide variety of roles."),
//            ("Human (Variant)", Usage.Standard, "Variant Humans possess unique talents and abilities, demonstrating a diverse range of skills."),
//            ("Tiefling", Usage.Standard,
//                "Tieflings bear the mark of an infernal heritage, granting them dark powers and a connection to the planes of Hell."),
//        };
//        //foreach (var race in races) {
//        //    var element = ElementFactory.For(raceSection, "System").Create<Node>(race.Name, race.Description) with {
//        //        Usage = race.Usage,
//        //        Source = playersHandbook,
//        //        State = State.Public,
//        //    };
//        //    ruleSet.Children.Add(element);
//        //}

//        //SetRaceModifiers(ruleSet);

//        return ruleSet;
//    }
//}

////Inspiration: "A temporary boost allowing a character to gain advantage on one ability check, attack roll, or saving throw."
////Height: "The vertical measurement of a character or creature."
////Weight: "The mass of a character or creature."
////Classes: "A dictionary of character classes and their associated levels."
////Currency: "A dictionary of various currencies and their amounts possessed by the character."
////Movements: "A dictionary of different movement types and their associated speeds."
////AbilityCharges: "A dictionary of abilities and their remaining charges or uses."
////SavingThrows: "A dictionary of saving throw proficiencies and their associated ability modifiers."
////SkillChecks: "A dictionary of skill proficiencies and their associated ability modifiers."
////ArmorClass: "A measure of how well a character or creature can avoid taking damage in combat."
////CarryingCapacity: "The maximum weight a character can carry without being encumbered."
////ChallengeRating: "A numerical rating representing the relative difficulty of defeating a creature in combat."
////Charisma: "A measure of a character's force of personality, persuasiveness, and personal magnetism."
////Constitution: "A measure of a character's health, stamina, and vital force."
////CurrentCarryingWeight: "The current weight of all items being carried by the character."
////CurrentHitPoints: "The remaining health points of a character or creature."
////Dexterity: "A measure of a character's agility, reflexes, and balance."
////ExhaustionLevel: "A measure of how fatigued a character is, with higher levels resulting in more severe penalties."
////Intelligence: "A measure of a character's mental acuity, information recall, and analytical skill."
////MaximumLanguagesKnown: "The maximum number of languages a character can learn."
////MaximumHitPoints: "The highest possible hit points a character or creature can have."
////ProficiencyBonus: "A bonus added to ability checks, attack rolls, and saving throws based on a character's level and class."
////Strength: "A measure of a character's physical power and ability to perform physically demanding tasks."
////TemporaryHitPoints: "Additional hit points that can absorb damage before reducing a character's current hit points."
////Wisdom: "A measure of a character's awareness, intuition, and insight."
////Alignment: "A description of a character's moral and ethical outlook."
////Appearance: "A brief description of a character's physical appearance."
////Background: "A character's history, including occupation, upbringing, and personal experiences."
////CreatureType: "The classification of a creature, such as humanoid, beast, or undead."
////EyeColor: "The color of a character's or creature's eyes."
////Gender: "The gender identity of a character."
////HairColor: "The color of a character's hair."
////NPCType: "A classification for non-player characters, such as merchant, guard, or innkeeper."
////PassiveInsight: "A measure of a character's ability to discern hidden motives without actively trying."
////PassivePerception: "A measure of a character's ability to notice hidden objects or creatures without actively searching."
////Race: "The species or ancestry of a character."
////Size: "The physical size category of a character or creature, such as Small, Medium, or Large."
////SkinColor: "The color of a character's skin."
////SpellCastingAbility: "The ability score used for calculating a character's spellcasting modifier and save DC."
////Actions: "A set of special actions a character or creature can take during combat."
////Archetypes: "A set of subclass options available to a character within their chosen class."
////Armors: "A set of protective gear a character is proficient in using."
////Feats: "A set of special abilities or features that provide a character with unique capabilities."
////Immunities: "A set of damage types, conditions, or effects a character or creature is completely unaffected by."
////LairActions: "A set of unique actions a creature can take within its lair, typically used by powerful adversaries."
////Languages: "A set of languages a character can understand, speak, read, and write."
////LegendaryActions: "A set of powerful actions a legendary creature can take outside of its turn in combat."
////Resistances: "A set of damage types a character or creature takes reduced damage from."
////Senses: "A set of special sensory capabilities a character or creature possesses, such as darkvision or blindsight."
////Traits: "A set of innate or learned characteristics that define a character or creature's abilities and qualities."
////Tools: "A set of tools, instruments, or other equipment a character is proficient in using."
////Vulnerabilities: "A set of damage types a character or creature takes increased damage from."
////Weapons: "A set of weapons a character is proficient in using."
