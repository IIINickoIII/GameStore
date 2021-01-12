using GameStore.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Contexts
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            #region Publishers

            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                Id = 1,
                CompanyName = "Rockstar",
                Description = "Rockstar Games, Inc. is an American video game publisher " +
                              "based in New York City. The company was established in December" +
                              " 1998 as a subsidiary of Take-Two Interactive, using the assets " +
                              "Take-Two had previously acquired from BMG Interactive. Founding " +
                              "members of the company were Sam and Dan Houser, Terry Donovan and " +
                              "Jamie King, who worked for Take-Two at the time, and of which the " +
                              "Houser brothers were previously executives at BMG Interactive. " +
                              "Sam Houser heads the studio as president.",
                HomePage = "rockstar.com"
            });
            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                Id = 2,
                CompanyName = "Blizzard",
                Description = "Blizzard Entertainment is a PC, console, and mobile game developer " +
                              "known for its epic multiplayer titles including the Warcraft, Diablo, " +
                              "StarCraft, and Overwatch ...",
                HomePage = "blizzard.com"
            });
            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                Id = 3,
                CompanyName = "EA Mobile",
                Description = "EA Mobile Inc. is an American video game development studio of the " +
                              "publisher Electronic Arts (EA) for mobile platforms. ",
                HomePage = "ea.com"
            });
            modelBuilder.Entity<Publisher>().HasData(new Publisher
            {
                Id = 4,
                CompanyName = "Wargaming",
                Description = "Wargaming.net is a private company, offshore company, publisher and " +
                              "developer of computer games, mainly free-to-play MMO genre and near-game " +
                              "services for various platforms. The headquarters is located in Nicosia, " +
                              "Republic of Cyprus, the development centers are in Minsk (main), Kiev, " +
                              "St. Petersburg, Seattle, Chicago, Baltimore, Sydney, Helsinki, Austin and Prague.",
                HomePage = "wargaming.com"
            });

            #endregion

            #region Games

            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 1,
                Key = "The Witcher 3: Wild Hunt",
                Name = "The Witcher 3: Wild Hunt",
                Description = "The Witcher: Wild Hunt is a story-driven open world RPG set in a visually stunning" +
                              " fantasy universe full of meaningful choices and impactful consequences. " +
                              "In The Witcher, you play as professional monster hunter Geralt of Rivia tasked with finding a child of prophecy in a vast " +
                              "open world rich with merchant cities, pirate islands, dangerous mountain passes, and forgotten caverns to explore. \n\n" +
                              "KEY FEATURES: \n" +
                              "PLAY AS A HIGHLY TRAINED MONSTER SLAYER FOR HIRE \n" +
                              "Trained from early childhood and mutated to gain superhuman skills, strength and reflexes, " +
                              "witchers are a counterbalance to the monster-infested world in which they live. \n\n" +
                              "\tGruesomely destroy foes as a professional monster hunter armed with a range of upgradeable weapons, mutating potions and combat magic. \n" +
                              "\tHunt down a wide range of exotic monsters — from savage beasts prowling the mountain passes, " +
                              "to cunning supernatural predators lurking in the shadows of densely populated towns. \n" +
                              "\tInvest your rewards to upgrade your weaponry and buy custom armour, or spend them away in horse races, card games, " +
                              "fist fighting, and other pleasures the night brings.",
                PublisherId = 1,
                Price = 12.00M,
                Discount = 7,
                UnitInStock = 10
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 2,
                Key = "BioShock Infinite",
                Name = "BioShock Infinite",
                Description = "Indebted to the wrong people," +
                              "with his life on the line," +
                              "veteran of the U.S.Cavalry and now hired gun," +
                              "Booker DeWitt has only one opportunity to wipe his slate clean.He must rescue Elizabeth," +
                              "a mysterious girl imprisoned since childhood and locked up in the flying city of Columbia.Forced to trust one another," +
                              "Booker and Elizabeth form a powerful bond during their daring escape. Together," +
                              "they learn to harness an expanding arsenal of weapons and abilities, as they fight on zeppelins in the clouds," +
                              "along high - speed Sky - Lines," +
                              "and down in the streets of Columbia," +
                              "all while surviving the threats of the air - city and uncovering its dark secret. \n\n" +
                              "Key Features\n" +
                              "\tThe City in the Sky – Leave the depths of Rapture to soar among the clouds of Columbia. " +
                              "A technological marvel, the flying city is a beautiful and vibrant world that holds a very dark secret. \n" +
                              "\tUnlikely Mission – Set in 1912, hired gun Booker DeWitt must rescue a mysterious girl from the sky-city of Columbia or never leave it alive. \n" +
                              "\tWhip, Zip, and Kill – Turn the city’s Sky-Lines into weaponized roller coasters as you zip through the flying city and dish out fatal hands-on punishment.\n" +
                              "\tTear Through Time – Open Tears in time and space to shape the battlefield and turn the tide in combat by pulling weapons, turrets, and other resources out of thin air.\n" +
                              "\tVigorous Powers – Throw explosive fireballs, shoot lightning, and release murders of crows as devastatingly powerful Vigors surge through your body to be unleashed against all that oppose you.\n" +
                              "\tCustom Combat Experience – With deadly weapons in one hand, powerful Vigors in the other, and the ability to open Tears in time and space, fight your own way through the floating city of Columbia to rescue Elizabeth and reach freedom.\n" +
                              "\t1999 Mode – Upon finishing BioShock Infinite, the player can unlock a game mode called “1999 Mode” that gives experienced players a taste of the kind of design and balance that hardcore gamers enjoyed back in the 20th century.",
                PublisherId = 1,
                Price = 12.01M,
                Discount = 6,
                UnitInStock = 11
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 3,
                Key = "Ori and the Blind Forest: Definitive Edition",
                Name = "Ori and the Blind Forest: Definitive Edition",
                Description =
                    "The forest of Nibel is dying. After a powerful storm sets a series of devastating events in motion, " +
                    "an unlikely hero must journey to find his courage and confront a dark nemesis to save his home. " +
                    "“Ori and the Blind Forest” tells the tale of a young orphan destined for heroics, through a visually stunning action-platformer crafted " +
                    "by Moon Studios for PC. Featuring hand-painted artwork, meticulously animated character performance, and a fully orchestrated score, " +
                    "“Ori and the Blind Forest” explores a deeply emotional story about love and sacrifice, and the hope that exists in us all. ",
                PublisherId = 1,
                Price = 11.40M,
                Discount = 2,
                UnitInStock = 1
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 4,
                Key = "RimWorld",
                Name = "RimWorld",
                Description =
                    "RimWorld is a sci-fi colony sim driven by an intelligent AI storyteller. Inspired by Dwarf Fortress, Firefly, and Dune. \n\n" +
                    "You begin with three survivors of a shipwreck on a distant world.\n" +
                    "\tManage colonists' moods, needs, wounds, illnesses and addictions.\n" +
                    "\tBuild in the forest, desert, jungle, tundra, and more.\n" +
                    "\tWatch colonists develop and break relationships with family members, lovers, and spouses.\n" +
                    "\tReplace wounded limbs and organs with prosthetics, bionics, or biological parts harvested from others.\n" +
                    "\tFight pirates, tribes, mad animals, giant insects and ancient killing machines.\n" +
                    "\tCraft structures, weapons, and apparel from metal, wood, stone, cloth, and futuristic materials.\n" +
                    "\tTame and train cute pets, productive farm animals, and deadly attack beasts.\n" +
                    "\tTrade with passing ships and caravans.\n" +
                    "\tForm caravans to complete quests, trade, attack other factions, or migrate your whole colony.\n" +
                    "\tDig through snow, weather storms, and fight fires.\n" +
                    "\tCapture refugees or prisoners and turn them to your side or sell them into slavery.\n" +
                    "\tDiscover a new generated world each time you play.\n" +
                    "\tExplore hundreds of wild and interesting mods on the Steam Workshop.\n" +
                    "\tLearn to play easily with the help of an intelligent and unobtrusive AI tutor.\n\n" +
                    "RimWorld is a story generator.It’s designed to co-author tragic, twisted, and triumphant stories about imprisoned pirates, " +
                    "desperate colonists, starvation and survival. It works by controlling the “random” events that the world throws at you." +
                    "Every thunderstorm, pirate raid, and traveling salesman is a card dealt into your story by the AI Storyteller. " +
                    "There are several storytellers to choose from. Randy Random does crazy stuff, Cassandra Classic goes for rising tension, " +
                    "and Phoebe Chillax likes to relax.\n\n" +
                    "Your colonists are not professional settlers – they’re crash - landed survivors from a passenger liner destroyed in orbit. " +
                    "You can end up with a nobleman, an accountant, and a housewife. You’ll acquire more colonists by capturing them in combat " +
                    "and turning them to your side, buying them from slave traders, or taking in refugees. So your colony will always be a motley crew.\n\n" +
                    "Each person’s background is tracked and affects how they play. A nobleman will be great at social skills(recruiting prisoners, negotiating trade prices)," +
                    " but refuse to do physical work. A farm oaf knows how to grow food by long experience, but cannot do research. " +
                    "A nerdy scientist is great at research, but cannot do social tasks at all. A genetically engineered assassin can do nothing but " +
                    "kill – but he does that very well." +
                    "Colonists develop - and destroy - relationships. Each has an opinion of the others, which determines whether they'll become lovers, " +
                    "marry, cheat, or fight. Perhaps your two best colonists are happily married - until one of them falls for the dashing surgeon who " +
                    "saved her from a gunshot wound." +
                    "The game generates a whole planet from pole to equator. You choose whether to land your crash pods in a cold northern tundra, " +
                    "a parched desert flat, a temperate forest, or a steaming equatorial jungle. Different areas have different animals, plants, diseases, " +
                    "temperatures, rainfall, mineral resources, and terrain. These challenges of surviving in a disease-infested, choking jungle are " +
                    "very different from those in a parched desert wasteland or a frozen tundra with a two - month growing season. \n\n" +
                    "Travel across the planet. You're not stuck in one place. You can form a caravan of people, animals, and prisoners. " +
                    "Rescue kidnapped former allies from pirate outposts, attend peace talks, trade with other factions, attack enemy colonies, " +
                    "and complete other quests. You can even pack up your entire colony and move to a new place. " +
                    "You can use rocket-powered transport pods to travel faster. \n\n" +
                    "You can tame and train animals. Lovable pets will cheer up sad colonists. Farm animals can be worked, milked, and sheared. " +
                    "Attack beasts can be released upon your enemies. There are many animals - cats, labrador retrievers, grizzly bears, camels, cougars, " +
                    "chinchillas, chickens, and exotic alien - like lifeforms. \n\n" +
                    "People in RimWorld constantly observe their situation and surroundings in order to decide how to feel at any given moment. " +
                    "They respond to hunger and fatigue, witnessing death, disrespectfully unburied corpses, being wounded, being left in darkness, " +
                    "getting packed into cramped environments, sleeping outside or in the same room as others, and many other situations. " +
                    "If they're too stressed, they might lash out or break down.\n\n" +
                    "Wounds, infections, prosthetics, and chronic conditions are tracked on each body part and affect characters' capacities. " +
                    "Eye injuries make it hard to shoot or do surgery. Wounded legs slow people down. Hands, brain, mouth, heart, liver, kidneys, " +
                    "stomach, feet, fingers, toes, and more can all be wounded, diseased, or missing, and all have logical in-game effects. " +
                    "And other species have their own body layouts - take off a deer's leg, and it can still hobble on the other three. " +
                    "Take off a rhino's horn, and it's much less dangerous.\n\n" +
                    "You can repair body parts with prosthetics ranging from primitive to transcendent. " +
                    "A peg leg will get Joe Colonist walking after an unfortunate incident with a rhinoceros, but he'll still be quite slow. " +
                    "Buy an expensive bionic leg from a trader the next year, and Joe becomes a superhuman runner. You can even extract, sell, buy, " +
                    "and transplant internal organs.",
                PublisherId = 1,
                Price = 12.11M,
                Discount = 3,
                UnitInStock = 3
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 5,
                Key = "Hollow Knight",
                Name = "Hollow Knight",
                Description =
                    "Brave the Depths of a Forgotten Kingdom\nBeneath the fading town of Dirtmouth sleeps an ancient," +
                    "ruined kingdom. Many are drawn below the surface, searching for riches, or glory, or answers to old secrets.\n\n" +
                    "Hollow Knight is a classically styled 2D action adventure across a vast interconnected world. Explore twisting caverns, " +
                    "ancient cities and deadly wastes; battle tainted creatures and befriend bizarre bugs; and solve ancient mysteries at the kingdom's heart.\n\n" +
                    "Game Features\n" +
                    "\tClassic side-scrolling action, with all the modern trimmings.\n" +
                    "\tTightly tuned 2D controls. Dodge, dash and slash your way through even the most deadly adversaries.\n" +
                    "\tExplore a vast interconnected world of forgotten highways, overgrown wilds and ruined cities.\n" +
                    "\tForge your own path!The world of Hallownest is expansive and open. Choose which paths you take, which enemies you face and find your own way forward. \n" +
                    "\tEvolve with powerful new skills and abilities! Gain spells, strength and speed. " +
                    "Leap to new heights on ethereal wings. Dash forward in a blazing flash. Blast foes with fiery Soul!\n" +
                    "\tEquip Charms! Ancient relics that offer bizarre new powers and abilities. Choose your favourites and make your journey unique!\n" +
                    "\tAn enormous cast of cute and creepy characters all brought to life with traditional 2D frame - by - frame animation.\n" +
                    "\tOver 130 enemies! 30 epic bosses! Face ferocious beasts and vanquish ancient knights on your quest through the kingdom. " +
                    "Track down every last twisted foe and add them to your Hunter's Journal!\n" +
                    "\tLeap into minds with the Dream Nail. Uncover a whole other side to the characters you meet and the enemies you face.\n" +
                    "\tBeautiful painted landscapes, with extravagant parallax, give a unique sense of depth to a side - on world.\n" +
                    "\tChart your journey with extensive mapping tools. Buy compasses, quills, maps and pins to enhance your understanding of Hollow Knight’s " +
                    "many twisting landscapes.\n" +
                    "\tA haunting, intimate score accompanies the player on their journey, composed by Christopher Larkin. " +
                    "The score echoes the majesty and sadness of a civilisation brought to ruin.\n" +
                    "\tComplete Hollow Knight to unlock Steel Soul Mode, the ultimate challenge!\n\n" +
                    "An Evocative Hand-Crafted World\n" +
                    "The world of Hollow Knight is brought to life in vivid, moody detail, its caverns alive with bizarre and terrifying creatures, " +
                    "each animated by hand in a traditional 2D style. Every new area you’ll discover is beautifully unique and strange, " +
                    "teeming with new creatures and characters. Take in the sights and uncover new wonders hidden off of the beaten path." +
                    "If you like classic gameplay, cute but creepy characters, epic adventure and beautiful, gothic worlds, then Hollow Knight awaits!",
                PublisherId = 1,
                Price = 1.20M,
                Discount = 4,
                UnitInStock = 102
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 6,
                Key = "Portal 2",
                Name = "Portal 2",
                Description =
                    "Portal 2 draws from the award-winning formula of innovative gameplay, story, and music that earned the original " +
                    "Portal over 70 industry accolades and created a cult following. \n" +
                    "The single - player portion of Portal 2 introduces a cast of dynamic new characters," +
                    "a host of fresh puzzle elements, and a much larger set of devious test chambers. Players will explore never - before - " +
                    "seen areas of the Aperture Science Labs and be reunited with GLaDOS, " +
                    "the occasionally murderous computer companion who guided them through the original game.\n" +
                    "The game’s two - player cooperative mode features its own entirely separate campaign with a unique story," +
                    "test chambers, and two new player characters. This new mode forces players to reconsider everything they thought they knew about portals. " +
                    "Success will require them to not just act cooperatively, but to think cooperatively. \n\n" +
                    "Product Features \n" +
                    "\tExtensive single player: Featuring next generation gameplay and a wildly - engrossing story.\n" +
                    "\tComplete two - person co - op: Multiplayer game featuring its own dedicated story, characters, and gameplay.\n" +
                    "\tAdvanced physics: Allows for the creation of a whole new range of interesting challenges, producing a much larger but not harder game.\n" +
                    "\tOriginal music.\n" +
                    "\tMassive sequel: The original Portal was named 2007's Game of the Year by over 30 publications worldwide.\n" +
                    "\tEditing Tools: Portal 2 editing tools will be included.",
                PublisherId = 1,
                Price = 21.02M,
                Discount = 70,
                UnitInStock = 13
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 7,
                Key = "Papers, Please",
                Name = "Papers, Please",
                Description = "Congratulations.\n" +
                              "The October labor lottery is complete.Your name was pulled. For immediate placement, report to the Ministry of Admission at Grestin Border Checkpoint. " +
                              "An apartment will be provided for you and your family in East Grestin. Expect a Class - 8 dwelling.\n " +
                              "Glory to Arstotzka\n\n" +
                              "The communist state of Arstotzka has just ended a 6 - year war with neighboring Kolechia and reclaimed its rightful half of the border town, Grestin." +
                              "Your job as immigration inspector is to control the flow of people entering the Arstotzkan side of Grestin from Kolechia. " +
                              "Among the throngs of immigrants and visitors looking for work are hidden smugglers, spies, and terrorists. Using only the documents provided by " +
                              "travelers and the Ministry of Admission's primitive inspect, search, and fingerprint systems you must decide who can enter Arstotzka and who will " +
                              "be turned away or arrested.",
                PublisherId = 1,
                Price = 12.10M,
                Discount = 9,
                UnitInStock = 19
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 8,
                Key = "Batman: Arkham City - Game of the Year Edition",
                Name = "Batman: Arkham City - Game of the Year Edition",
                Description =
                    "Batman: Arkham City builds upon the intense, atmospheric foundation of Batman: Arkham Asylum, sending players flying " +
                    "through the expansive Arkham City - five times larger than the game world in Batman: Arkham Asylum - the new maximum security \"home\" " +
                    "for all of Gotham City's thugs, gangsters and insane criminal masterminds. " +
                    "Featuring an incredible Rogues Gallery of Gotham City's most dangerous criminals including Catwoman, The Joker, The Riddler, Two-Face, " +
                    "Harley Quinn, The Penguin, Mr. Freeze and many others, the game allows players to genuinely experience what it feels like to be " +
                    "The Dark Knight delivering justice on the streets of Gotham City.\n" +
                    "Batman: Arkham City - Game of the Year Edition includes the following DLC:\n" +
                    "\tCatwoman Pack\n" +
                    "\tNightwing Bundle Pack\n" +
                    "\tRobin Bundle Pack\n" +
                    "\tHarley Quinn’s Revenge\n" +
                    "\tChallenge Map Pack\n" +
                    "\tArkham City Skins Pack\n\n" +
                    "Batman: Arkham City - Game of the Year Edition packages new gameplay content, seven maps, three playable characters, and 12 skins beyond " +
                    "the original retail release: \n" +
                    "\tMaps: Wayne Manor, Main Hall, Freight Train, Black Mask, The Joker's Carnival, Iceberg Long, and Batcave\n" +
                    "\tPlayable Characters: Catwoman, Robin and Nightwing\n" +
                    "\tSkins: 1970s Batsuit, Year One Batman, The Dark Knight Returns, Earth One Batman, Batman Beyond Batman, Animated Batman, Sinestro Corps Batman, " +
                    "Long Halloween Catwoman, Animated Catwoman, Animated Robin, Red Robin and Animated Nightwing",
                PublisherId = 1,
                Price = 34.10M,
                Discount = 14,
                UnitInStock = 12
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 9,
                Key = "Penny Arcade's On the Rain-Slick Precipice of Darkness 4",
                Name = "Penny Arcade's On the Rain-Slick Precipice of Darkness 4",
                Description =
                    "Penny Arcade’s “On the Rain-Slick Precipice of Darkness 4” concludes the saga of Tycho Brahe, " +
                    "Scion of The Long Project, and his almost certainly human partner Jonathan Gabriel. The world has been destroyed, " +
                    "but existence extends beyond this mortal plane in the gruesome Underhell! The new game expands considerably on its predecessor " +
                    "in size, scope and gameplay. Create the ultimate fighting force by recruiting bizarre, occasionally disgusting monsters " +
                    "and teaming them up with the best trainer for the job! \n\n" +
                    "Key Features\n" +
                    "\tOld - School RPG style mixed with modern design sensibilities!\n" +
                    "\tBizarre and humorous story written by Penny Arcade & Zeboyd Games!\n" +
                    "\tOver twice as many area maps as the previous game!\n" +
                    "\tExplore the Underhell world(in all its traditional RPG glory) and discover its many secrets.\n" +
                    "\tNo random battles!\n" +
                    "\tMusic created by Hyperduck Soundworks(Dust: An Elysian Tail, Mojang's Scrolls)!\n" +
                    "\tRecruit bizarre & powerful monsters like a sentient vending machine, an evil ice cream cone, a bug - crow... thing, and more!\n" +
                    "\tDisrupt enemy attacks with powerful interrupt abilities!\n" +
                    "\tRechargeable MP & items! Unleash your full power in every battle!",
                PublisherId = 1,
                Price = 22.30M,
                Discount = 2,
                UnitInStock = 54
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 10,
                Key = "Transistor",
                Name = "Transistor",
                Description =
                    "From the creators of Bastion, Transistor is a sci-fi themed action RPG that invites you to wield " +
                    "an extraordinary weapon of unknown origin as you fight through a stunning futuristic city. Transistor seamlessly integrates thoughtful " +
                    "strategic planning into a fast-paced action experience, melding responsive gameplay and rich atmospheric storytelling. " +
                    "During the course of the adventure, you will piece together the Transistor's mysteries as you pursue its former owners. \n\n" +
                    "Key Features:" +
                    "\tAn all - new world from the team that created Bastion\n" +
                    "\tConfigure the powerful Transistor with thousands of possible Function combinations\n" +
                    "\tAction - packed real - time combat fused with a robust strategic planning mode\n" +
                    "\tVibrant hand - painted artwork in full 1080p resolution\n" +
                    "\tOriginal soundtrack changes dynamically as the action unfolds\n" +
                    "\tHours of reactive voiceover create a deep and atmospheric story\n" +
                    "\t'Recursion' option introduces procedural battles after finishing the story\n" +
                    "\tFully customizable controls custom - tailored for PC",
                PublisherId = 1,
                Price = 23.00M,
                Discount = 8,
                UnitInStock = 89
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 11,
                Key = "Cthulhu Saves the World",
                Name = "Cthulhu Saves the World",
                Description =
                    "The lord of insanity, Cthulhu was all set to plunge the world into insanity and destruction when " +
                    "his powers were sealed by a mysterious sorcerer. The only way for him to break the curse is to become a true hero. " +
                    "Save the world to destroy it in an epic parody RPG journey of redemption, romance, and insanity!\n\n" +
                    "Key features:\n" +
                    "\tOld school RPG style mixed with modern design sensibilities!\n" +
                    "\tInflict insanity upon your opponents for fun and profit!\n" +
                    "\t6 - 10 hour quest with unlockable game modes & difficulty levels for increased replay value.\n" +
                    "\tHighlander mode – XP gains are quadrupled, but only one character can be brought into battle at a time!\n" +
                    "\tScore Attack mode – Gain points by defeating bosses at the lowest LV possible!\n" +
                    "\tOverkill – Jump to LV40 in a single battle! Perfect for replays and experimentation!\n" +
                    "\tCthulhu's Angels mode – Remix mode with new playable characters, new dialogue, new bosses, and more!\n" +
                    "\tAll of the great features players know and love from Breath of Death VII: The Beginning have returned – fast - paced gameplay, " +
                    "combo system, random encounter limits, branching LV - Ups, and more!",
                PublisherId = 1,
                Price = 34.29M,
                Discount = 6,
                UnitInStock = 13
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 12,
                Key = "Sid Meier's Civilization® V",
                Name = "Sid Meier's Civilization® V",
                Description = "The Flagship Turn-Based Strategy Game Returns\n" +
                              "Become Ruler of the World by establishing and leading a civilization from the dawn of man into the space age: Wage war, conduct diplomacy, " +
                              "discover new technologies, go head - to - head with some of history’s greatest leaders and build the most powerful empire the world has ever known.\n\n" +
                              "\tINVITING PRESENTATION: Jump right in and play at your own pace with an intuitive interface that eases new players into the game. " +
                              "Veterans will appreciate the depth, detail and control that are highlights of the series.\n" +
                              "\tBELIEVABLE WORLD: Ultra realistic graphics showcase lush landscapes for you to explore, battle over and claim as your own.\n" +
                              "\tCOMMUNITY & MULTIPLAYER: Compete with players all over the world or locally in LAN matches, mod the game in unprecedented ways, " +
                              "and install mods directly from an in-game community hub without ever leaving the game.\n" +
                              "\tWIDE SYSTEM COMPATIBILITY: Civilization V operates on many different systems, from high end desktops to many laptops.\n" +
                              "\tALL NEW FEATURES: A new hex-based gameplay grid opens up exciting new combat and build strategies. " +
                              "City States become a new resource in your diplomatic battleground. An improved diplomacy system allows you to negotiate with fully interactive leaders.",
                PublisherId = 1,
                Price = 123.89M,
                Discount = 78,
                UnitInStock = 438
            });

            #endregion

            #region Genres

            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 1,
                Name = "Strategy",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 2,
                Name = "RTS",
                ParentGenreId = 1
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 3,
                Name = "TBS",
                ParentGenreId = 1
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 4,
                Name = "RPG",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 5,
                Name = "Sports",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 6,
                Name = "Races",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 7,
                Name = "Rally",
                ParentGenreId = 6
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 8,
                Name = "Arcade",
                ParentGenreId = 6
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 9,
                Name = "Formula",
                ParentGenreId = 6
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 10,
                Name = "Off-road",
                ParentGenreId = 6
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 11,
                Name = "Action",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 12,
                Name = "FPS",
                ParentGenreId = 11
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 13,
                Name = "TPS",
                ParentGenreId = 11
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 15,
                Name = "Adventure",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 16,
                Name = "Puzzle & Skill",
                ParentGenreId = null
            });
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                Id = 17,
                Name = "Misc.",
                ParentGenreId = null
            });

            #endregion

            #region GameGenre

            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 1,
                GameId = 1,
                GenreId = 3
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 2,
                GameId = 1,
                GenreId = 7
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 3,
                GameId = 2,
                GenreId = 7
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 4,
                GameId = 2,
                GenreId = 13
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 5,
                GameId = 3,
                GenreId = 6
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 6,
                GameId = 4,
                GenreId = 1
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 7,
                GameId = 4,
                GenreId = 16
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 8,
                GameId = 4,
                GenreId = 9
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 9,
                GameId = 5,
                GenreId = 16
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 10,
                GameId = 5,
                GenreId = 17
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 11,
                GameId = 5,
                GenreId = 7
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 12,
                GameId = 6,
                GenreId = 8
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 13,
                GameId = 6,
                GenreId = 7
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 14,
                GameId = 7,
                GenreId = 9
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 15,
                GameId = 7,
                GenreId = 16
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 16,
                GameId = 8,
                GenreId = 7
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 17,
                GameId = 8,
                GenreId = 6
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 18,
                GameId = 9,
                GenreId = 3
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 19,
                GameId = 9,
                GenreId = 16
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 20,
                GameId = 10,
                GenreId = 6
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 21,
                GameId = 10,
                GenreId = 16
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 22,
                GameId = 10,
                GenreId = 3
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 23,
                GameId = 11,
                GenreId = 16
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 24,
                GameId = 11,
                GenreId = 3
            });
            modelBuilder.Entity<GameGenre>().HasData(new GameGenre
            {
                Id = 25,
                GameId = 12,
                GenreId = 1
            });

            #endregion

            #region Comments

            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 1,
                ParentCommentId = 0,
                Name = "Peter",
                Body = "Good game!",
                GameId = 1,
                Time = new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local)
            });
            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 2,
                ParentCommentId = 1,
                Name = "Adam",
                Body = "Peter, Thanks a lot)",
                GameId = 1,
                Time = new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local)
            });
            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 3,
                ParentCommentId = 1,
                Name = "Bogdan from Bolt",
                Body = "Peter, cool!",
                GameId = 1,
                Time = new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local)
            });
            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 4,
                ParentCommentId = 2,
                Name = "Kelvin",
                Body = "Adam, yep))0)",
                GameId = 1,
                Time = new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local)
            });
            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 5,
                ParentCommentId = 2,
                Name = "Martin",
                Body = "Adam++",
                GameId = 1,
                Time = new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local)
            });

            #endregion

            #region PlatformTypes

            modelBuilder.Entity<PlatformType>().HasData(new PlatformType
            {
                Id = 1,
                Type = "PC"
            });
            modelBuilder.Entity<PlatformType>().HasData(new PlatformType
            {
                Id = 2,
                Type = "PS3"
            });
            modelBuilder.Entity<PlatformType>().HasData(new PlatformType
            {
                Id = 3,
                Type = "PS4"
            });
            modelBuilder.Entity<PlatformType>().HasData(new PlatformType
            {
                Id = 4,
                Type = "Xbox360"
            });
            modelBuilder.Entity<PlatformType>().HasData(new PlatformType
            {
                Id = 5,
                Type = "XboxONE"
            });

            #endregion

            #region GamePlatformType

            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 1,
                GameId = 1,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 2,
                GameId = 1,
                PlatformTypeId = 2
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 3,
                GameId = 2,
                PlatformTypeId = 2
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 4,
                GameId = 2,
                PlatformTypeId = 3
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 5,
                GameId = 3,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 6,
                GameId = 3,
                PlatformTypeId = 3
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 7,
                GameId = 4,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 8,
                GameId = 4,
                PlatformTypeId = 2
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 9,
                GameId = 4,
                PlatformTypeId = 3
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 10,
                GameId = 4,
                PlatformTypeId = 4
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 11,
                GameId = 5,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 12,
                GameId = 6,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 13,
                GameId = 6,
                PlatformTypeId = 2
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 14,
                GameId = 7,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 15,
                GameId = 7,
                PlatformTypeId = 2
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 16,
                GameId = 8,
                PlatformTypeId = 3
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 17,
                GameId = 8,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 18,
                GameId = 9,
                PlatformTypeId = 3
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 19,
                GameId = 9,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 20,
                GameId = 9,
                PlatformTypeId = 2
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 21,
                GameId = 10,
                PlatformTypeId = 3
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 22,
                GameId = 11,
                PlatformTypeId = 4
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 23,
                GameId = 11,
                PlatformTypeId = 1
            });
            modelBuilder.Entity<GamePlatformType>().HasData(new GamePlatformType
            {
                Id = 24,
                GameId = 12,
                PlatformTypeId = 1
            });

            #endregion

        }
    }
}