using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Dal.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialCreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentGenreId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsClosedForEdit = table.Column<bool>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    TotalSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlatformTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 40, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HomePage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    UnitInStock = table.Column<short>(nullable: false),
                    Discontinued = table.Column<bool>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentCommentId = table.Column<int>(nullable: true),
                    GameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGenres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameGenres_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatformTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    PlatformTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatformTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePlatformTypes_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatformTypes_PlatformTypes_PlatformTypeId",
                        column: x => x.PlatformTypeId,
                        principalTable: "PlatformTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<short>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    SumWithoutDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumWithDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { 1, false, "Strategy", null },
                    { 17, false, "Misc.", null },
                    { 16, false, "Puzzle & Skill", null },
                    { 15, false, "Adventure", null },
                    { 12, false, "FPS", 11 },
                    { 11, false, "Action", null },
                    { 10, false, "Off-road", 6 },
                    { 9, false, "Formula", 6 },
                    { 13, false, "TPS", 11 },
                    { 7, false, "Rally", 6 },
                    { 6, false, "Races", null },
                    { 5, false, "Sports", null },
                    { 4, false, "RPG", null },
                    { 3, false, "TBS", 1 },
                    { 2, false, "RTS", 1 },
                    { 8, false, "Arcade", 6 }
                });

            migrationBuilder.InsertData(
                table: "PlatformTypes",
                columns: new[] { "Id", "IsDeleted", "Type" },
                values: new object[,]
                {
                    { 1, false, "PC" },
                    { 2, false, "PS3" },
                    { 3, false, "PS4" },
                    { 4, false, "Xbox360" },
                    { 5, false, "XboxONE" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "Description", "HomePage", "IsDeleted" },
                values: new object[,]
                {
                    { 3, "EA Mobile", "EA Mobile Inc. is an American video game development studio of the publisher Electronic Arts (EA) for mobile platforms. ", "ea.com", false },
                    { 1, "Rockstar", "Rockstar Games, Inc. is an American video game publisher based in New York City. The company was established in December 1998 as a subsidiary of Take-Two Interactive, using the assets Take-Two had previously acquired from BMG Interactive. Founding members of the company were Sam and Dan Houser, Terry Donovan and Jamie King, who worked for Take-Two at the time, and of which the Houser brothers were previously executives at BMG Interactive. Sam Houser heads the studio as president.", "rockstar.com", false },
                    { 2, "Blizzard", "Blizzard Entertainment is a PC, console, and mobile game developer known for its epic multiplayer titles including the Warcraft, Diablo, StarCraft, and Overwatch ...", "blizzard.com", false },
                    { 4, "Wargaming", "Wargaming.net is a private company, offshore company, publisher and developer of computer games, mainly free-to-play MMO genre and near-game services for various platforms. The headquarters is located in Nicosia, Republic of Cyprus, the development centers are in Minsk (main), Kiev, St. Petersburg, Seattle, Chicago, Baltimore, Sydney, Helsinki, Austin and Prague.", "wargaming.com", false }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Discontinued", "Discount", "IsDeleted", "Key", "Name", "Price", "PublisherId", "UnitInStock" },
                values: new object[,]
                {
                    { 1, @"The Witcher: Wild Hunt is a story-driven open world RPG set in a visually stunning fantasy universe full of meaningful choices and impactful consequences. In The Witcher, you play as professional monster hunter Geralt of Rivia tasked with finding a child of prophecy in a vast open world rich with merchant cities, pirate islands, dangerous mountain passes, and forgotten caverns to explore. 

                KEY FEATURES: 
                PLAY AS A HIGHLY TRAINED MONSTER SLAYER FOR HIRE 
                Trained from early childhood and mutated to gain superhuman skills, strength and reflexes, witchers are a counterbalance to the monster-infested world in which they live. 

                	Gruesomely destroy foes as a professional monster hunter armed with a range of upgradeable weapons, mutating potions and combat magic. 
                	Hunt down a wide range of exotic monsters — from savage beasts prowling the mountain passes, to cunning supernatural predators lurking in the shadows of densely populated towns. 
                	Invest your rewards to upgrade your weaponry and buy custom armour, or spend them away in horse races, card games, fist fighting, and other pleasures the night brings.", false, 7f, false, "The Witcher 3: Wild Hunt", "The Witcher 3: Wild Hunt", 12.00m, 1, (short)10 },
                    { 2, @"Indebted to the wrong people,with his life on the line,veteran of the U.S.Cavalry and now hired gun,Booker DeWitt has only one opportunity to wipe his slate clean.He must rescue Elizabeth,a mysterious girl imprisoned since childhood and locked up in the flying city of Columbia.Forced to trust one another,Booker and Elizabeth form a powerful bond during their daring escape. Together,they learn to harness an expanding arsenal of weapons and abilities, as they fight on zeppelins in the clouds,along high - speed Sky - Lines,and down in the streets of Columbia,all while surviving the threats of the air - city and uncovering its dark secret. 

                Key Features
                	The City in the Sky – Leave the depths of Rapture to soar among the clouds of Columbia. A technological marvel, the flying city is a beautiful and vibrant world that holds a very dark secret. 
                	Unlikely Mission – Set in 1912, hired gun Booker DeWitt must rescue a mysterious girl from the sky-city of Columbia or never leave it alive. 
                	Whip, Zip, and Kill – Turn the city’s Sky-Lines into weaponized roller coasters as you zip through the flying city and dish out fatal hands-on punishment.
                	Tear Through Time – Open Tears in time and space to shape the battlefield and turn the tide in combat by pulling weapons, turrets, and other resources out of thin air.
                	Vigorous Powers – Throw explosive fireballs, shoot lightning, and release murders of crows as devastatingly powerful Vigors surge through your body to be unleashed against all that oppose you.
                	Custom Combat Experience – With deadly weapons in one hand, powerful Vigors in the other, and the ability to open Tears in time and space, fight your own way through the floating city of Columbia to rescue Elizabeth and reach freedom.
                	1999 Mode – Upon finishing BioShock Infinite, the player can unlock a game mode called “1999 Mode” that gives experienced players a taste of the kind of design and balance that hardcore gamers enjoyed back in the 20th century.", false, 6f, false, "BioShock Infinite", "BioShock Infinite", 12.01m, 1, (short)11 },
                    { 3, "The forest of Nibel is dying. After a powerful storm sets a series of devastating events in motion, an unlikely hero must journey to find his courage and confront a dark nemesis to save his home. “Ori and the Blind Forest” tells the tale of a young orphan destined for heroics, through a visually stunning action-platformer crafted by Moon Studios for PC. Featuring hand-painted artwork, meticulously animated character performance, and a fully orchestrated score, “Ori and the Blind Forest” explores a deeply emotional story about love and sacrifice, and the hope that exists in us all. ", false, 2f, false, "Ori and the Blind Forest: Definitive Edition", "Ori and the Blind Forest: Definitive Edition", 11.40m, 1, (short)1 },
                    { 4, @"RimWorld is a sci-fi colony sim driven by an intelligent AI storyteller. Inspired by Dwarf Fortress, Firefly, and Dune. 

                You begin with three survivors of a shipwreck on a distant world.
                	Manage colonists' moods, needs, wounds, illnesses and addictions.
                	Build in the forest, desert, jungle, tundra, and more.
                	Watch colonists develop and break relationships with family members, lovers, and spouses.
                	Replace wounded limbs and organs with prosthetics, bionics, or biological parts harvested from others.
                	Fight pirates, tribes, mad animals, giant insects and ancient killing machines.
                	Craft structures, weapons, and apparel from metal, wood, stone, cloth, and futuristic materials.
                	Tame and train cute pets, productive farm animals, and deadly attack beasts.
                	Trade with passing ships and caravans.
                	Form caravans to complete quests, trade, attack other factions, or migrate your whole colony.
                	Dig through snow, weather storms, and fight fires.
                	Capture refugees or prisoners and turn them to your side or sell them into slavery.
                	Discover a new generated world each time you play.
                	Explore hundreds of wild and interesting mods on the Steam Workshop.
                	Learn to play easily with the help of an intelligent and unobtrusive AI tutor.

                RimWorld is a story generator.It’s designed to co-author tragic, twisted, and triumphant stories about imprisoned pirates, desperate colonists, starvation and survival. It works by controlling the “random” events that the world throws at you.Every thunderstorm, pirate raid, and traveling salesman is a card dealt into your story by the AI Storyteller. There are several storytellers to choose from. Randy Random does crazy stuff, Cassandra Classic goes for rising tension, and Phoebe Chillax likes to relax.

                Your colonists are not professional settlers – they’re crash - landed survivors from a passenger liner destroyed in orbit. You can end up with a nobleman, an accountant, and a housewife. You’ll acquire more colonists by capturing them in combat and turning them to your side, buying them from slave traders, or taking in refugees. So your colony will always be a motley crew.

                Each person’s background is tracked and affects how they play. A nobleman will be great at social skills(recruiting prisoners, negotiating trade prices), but refuse to do physical work. A farm oaf knows how to grow food by long experience, but cannot do research. A nerdy scientist is great at research, but cannot do social tasks at all. A genetically engineered assassin can do nothing but kill – but he does that very well.Colonists develop - and destroy - relationships. Each has an opinion of the others, which determines whether they'll become lovers, marry, cheat, or fight. Perhaps your two best colonists are happily married - until one of them falls for the dashing surgeon who saved her from a gunshot wound.The game generates a whole planet from pole to equator. You choose whether to land your crash pods in a cold northern tundra, a parched desert flat, a temperate forest, or a steaming equatorial jungle. Different areas have different animals, plants, diseases, temperatures, rainfall, mineral resources, and terrain. These challenges of surviving in a disease-infested, choking jungle are very different from those in a parched desert wasteland or a frozen tundra with a two - month growing season. 

                Travel across the planet. You're not stuck in one place. You can form a caravan of people, animals, and prisoners. Rescue kidnapped former allies from pirate outposts, attend peace talks, trade with other factions, attack enemy colonies, and complete other quests. You can even pack up your entire colony and move to a new place. You can use rocket-powered transport pods to travel faster. 

                You can tame and train animals. Lovable pets will cheer up sad colonists. Farm animals can be worked, milked, and sheared. Attack beasts can be released upon your enemies. There are many animals - cats, labrador retrievers, grizzly bears, camels, cougars, chinchillas, chickens, and exotic alien - like lifeforms. 

                People in RimWorld constantly observe their situation and surroundings in order to decide how to feel at any given moment. They respond to hunger and fatigue, witnessing death, disrespectfully unburied corpses, being wounded, being left in darkness, getting packed into cramped environments, sleeping outside or in the same room as others, and many other situations. If they're too stressed, they might lash out or break down.

                Wounds, infections, prosthetics, and chronic conditions are tracked on each body part and affect characters' capacities. Eye injuries make it hard to shoot or do surgery. Wounded legs slow people down. Hands, brain, mouth, heart, liver, kidneys, stomach, feet, fingers, toes, and more can all be wounded, diseased, or missing, and all have logical in-game effects. And other species have their own body layouts - take off a deer's leg, and it can still hobble on the other three. Take off a rhino's horn, and it's much less dangerous.

                You can repair body parts with prosthetics ranging from primitive to transcendent. A peg leg will get Joe Colonist walking after an unfortunate incident with a rhinoceros, but he'll still be quite slow. Buy an expensive bionic leg from a trader the next year, and Joe becomes a superhuman runner. You can even extract, sell, buy, and transplant internal organs.", false, 3f, false, "RimWorld", "RimWorld", 12.11m, 1, (short)3 },
                    { 5, @"Brave the Depths of a Forgotten Kingdom
                Beneath the fading town of Dirtmouth sleeps an ancient,ruined kingdom. Many are drawn below the surface, searching for riches, or glory, or answers to old secrets.

                Hollow Knight is a classically styled 2D action adventure across a vast interconnected world. Explore twisting caverns, ancient cities and deadly wastes; battle tainted creatures and befriend bizarre bugs; and solve ancient mysteries at the kingdom's heart.

                Game Features
                	Classic side-scrolling action, with all the modern trimmings.
                	Tightly tuned 2D controls. Dodge, dash and slash your way through even the most deadly adversaries.
                	Explore a vast interconnected world of forgotten highways, overgrown wilds and ruined cities.
                	Forge your own path!The world of Hallownest is expansive and open. Choose which paths you take, which enemies you face and find your own way forward. 
                	Evolve with powerful new skills and abilities! Gain spells, strength and speed. Leap to new heights on ethereal wings. Dash forward in a blazing flash. Blast foes with fiery Soul!
                	Equip Charms! Ancient relics that offer bizarre new powers and abilities. Choose your favourites and make your journey unique!
                	An enormous cast of cute and creepy characters all brought to life with traditional 2D frame - by - frame animation.
                	Over 130 enemies! 30 epic bosses! Face ferocious beasts and vanquish ancient knights on your quest through the kingdom. Track down every last twisted foe and add them to your Hunter's Journal!
                	Leap into minds with the Dream Nail. Uncover a whole other side to the characters you meet and the enemies you face.
                	Beautiful painted landscapes, with extravagant parallax, give a unique sense of depth to a side - on world.
                	Chart your journey with extensive mapping tools. Buy compasses, quills, maps and pins to enhance your understanding of Hollow Knight’s many twisting landscapes.
                	A haunting, intimate score accompanies the player on their journey, composed by Christopher Larkin. The score echoes the majesty and sadness of a civilisation brought to ruin.
                	Complete Hollow Knight to unlock Steel Soul Mode, the ultimate challenge!

                An Evocative Hand-Crafted World
                The world of Hollow Knight is brought to life in vivid, moody detail, its caverns alive with bizarre and terrifying creatures, each animated by hand in a traditional 2D style. Every new area you’ll discover is beautifully unique and strange, teeming with new creatures and characters. Take in the sights and uncover new wonders hidden off of the beaten path.If you like classic gameplay, cute but creepy characters, epic adventure and beautiful, gothic worlds, then Hollow Knight awaits!", false, 4f, false, "Hollow Knight", "Hollow Knight", 1.20m, 1, (short)102 },
                    { 6, @"Portal 2 draws from the award-winning formula of innovative gameplay, story, and music that earned the original Portal over 70 industry accolades and created a cult following. 
                The single - player portion of Portal 2 introduces a cast of dynamic new characters,a host of fresh puzzle elements, and a much larger set of devious test chambers. Players will explore never - before - seen areas of the Aperture Science Labs and be reunited with GLaDOS, the occasionally murderous computer companion who guided them through the original game.
                The game’s two - player cooperative mode features its own entirely separate campaign with a unique story,test chambers, and two new player characters. This new mode forces players to reconsider everything they thought they knew about portals. Success will require them to not just act cooperatively, but to think cooperatively. 

                Product Features 
                	Extensive single player: Featuring next generation gameplay and a wildly - engrossing story.
                	Complete two - person co - op: Multiplayer game featuring its own dedicated story, characters, and gameplay.
                	Advanced physics: Allows for the creation of a whole new range of interesting challenges, producing a much larger but not harder game.
                	Original music.
                	Massive sequel: The original Portal was named 2007's Game of the Year by over 30 publications worldwide.
                	Editing Tools: Portal 2 editing tools will be included.", false, 70f, false, "Portal 2", "Portal 2", 21.02m, 1, (short)13 },
                    { 7, @"Congratulations.
                The October labor lottery is complete.Your name was pulled. For immediate placement, report to the Ministry of Admission at Grestin Border Checkpoint. An apartment will be provided for you and your family in East Grestin. Expect a Class - 8 dwelling.
                 Glory to Arstotzka

                The communist state of Arstotzka has just ended a 6 - year war with neighboring Kolechia and reclaimed its rightful half of the border town, Grestin.Your job as immigration inspector is to control the flow of people entering the Arstotzkan side of Grestin from Kolechia. Among the throngs of immigrants and visitors looking for work are hidden smugglers, spies, and terrorists. Using only the documents provided by travelers and the Ministry of Admission's primitive inspect, search, and fingerprint systems you must decide who can enter Arstotzka and who will be turned away or arrested.", false, 9f, false, "Papers, Please", "Papers, Please", 12.10m, 1, (short)19 },
                    { 8, @"Batman: Arkham City builds upon the intense, atmospheric foundation of Batman: Arkham Asylum, sending players flying through the expansive Arkham City - five times larger than the game world in Batman: Arkham Asylum - the new maximum security ""home"" for all of Gotham City's thugs, gangsters and insane criminal masterminds. Featuring an incredible Rogues Gallery of Gotham City's most dangerous criminals including Catwoman, The Joker, The Riddler, Two-Face, Harley Quinn, The Penguin, Mr. Freeze and many others, the game allows players to genuinely experience what it feels like to be The Dark Knight delivering justice on the streets of Gotham City.
                Batman: Arkham City - Game of the Year Edition includes the following DLC:
                	Catwoman Pack
                	Nightwing Bundle Pack
                	Robin Bundle Pack
                	Harley Quinn’s Revenge
                	Challenge Map Pack
                	Arkham City Skins Pack

                Batman: Arkham City - Game of the Year Edition packages new gameplay content, seven maps, three playable characters, and 12 skins beyond the original retail release: 
                	Maps: Wayne Manor, Main Hall, Freight Train, Black Mask, The Joker's Carnival, Iceberg Long, and Batcave
                	Playable Characters: Catwoman, Robin and Nightwing
                	Skins: 1970s Batsuit, Year One Batman, The Dark Knight Returns, Earth One Batman, Batman Beyond Batman, Animated Batman, Sinestro Corps Batman, Long Halloween Catwoman, Animated Catwoman, Animated Robin, Red Robin and Animated Nightwing", false, 14f, false, "Batman: Arkham City - Game of the Year Edition", "Batman: Arkham City - Game of the Year Edition", 34.10m, 1, (short)12 },
                    { 9, @"Penny Arcade’s “On the Rain-Slick Precipice of Darkness 4” concludes the saga of Tycho Brahe, Scion of The Long Project, and his almost certainly human partner Jonathan Gabriel. The world has been destroyed, but existence extends beyond this mortal plane in the gruesome Underhell! The new game expands considerably on its predecessor in size, scope and gameplay. Create the ultimate fighting force by recruiting bizarre, occasionally disgusting monsters and teaming them up with the best trainer for the job! 

                Key Features
                	Old - School RPG style mixed with modern design sensibilities!
                	Bizarre and humorous story written by Penny Arcade & Zeboyd Games!
                	Over twice as many area maps as the previous game!
                	Explore the Underhell world(in all its traditional RPG glory) and discover its many secrets.
                	No random battles!
                	Music created by Hyperduck Soundworks(Dust: An Elysian Tail, Mojang's Scrolls)!
                	Recruit bizarre & powerful monsters like a sentient vending machine, an evil ice cream cone, a bug - crow... thing, and more!
                	Disrupt enemy attacks with powerful interrupt abilities!
                	Rechargeable MP & items! Unleash your full power in every battle!", false, 2f, false, "Penny Arcade's On the Rain-Slick Precipice of Darkness 4", "Penny Arcade's On the Rain-Slick Precipice of Darkness 4", 22.30m, 1, (short)54 },
                    { 10, @"From the creators of Bastion, Transistor is a sci-fi themed action RPG that invites you to wield an extraordinary weapon of unknown origin as you fight through a stunning futuristic city. Transistor seamlessly integrates thoughtful strategic planning into a fast-paced action experience, melding responsive gameplay and rich atmospheric storytelling. During the course of the adventure, you will piece together the Transistor's mysteries as you pursue its former owners. 

                Key Features:	An all - new world from the team that created Bastion
                	Configure the powerful Transistor with thousands of possible Function combinations
                	Action - packed real - time combat fused with a robust strategic planning mode
                	Vibrant hand - painted artwork in full 1080p resolution
                	Original soundtrack changes dynamically as the action unfolds
                	Hours of reactive voiceover create a deep and atmospheric story
                	'Recursion' option introduces procedural battles after finishing the story
                	Fully customizable controls custom - tailored for PC", false, 8f, false, "Transistor", "Transistor", 23.00m, 1, (short)89 },
                    { 11, @"The lord of insanity, Cthulhu was all set to plunge the world into insanity and destruction when his powers were sealed by a mysterious sorcerer. The only way for him to break the curse is to become a true hero. Save the world to destroy it in an epic parody RPG journey of redemption, romance, and insanity!

                Key features:
                	Old school RPG style mixed with modern design sensibilities!
                	Inflict insanity upon your opponents for fun and profit!
                	6 - 10 hour quest with unlockable game modes & difficulty levels for increased replay value.
                	Highlander mode – XP gains are quadrupled, but only one character can be brought into battle at a time!
                	Score Attack mode – Gain points by defeating bosses at the lowest LV possible!
                	Overkill – Jump to LV40 in a single battle! Perfect for replays and experimentation!
                	Cthulhu's Angels mode – Remix mode with new playable characters, new dialogue, new bosses, and more!
                	All of the great features players know and love from Breath of Death VII: The Beginning have returned – fast - paced gameplay, combo system, random encounter limits, branching LV - Ups, and more!", false, 6f, false, "Cthulhu Saves the World", "Cthulhu Saves the World", 34.29m, 1, (short)13 },
                    { 12, @"The Flagship Turn-Based Strategy Game Returns
                Become Ruler of the World by establishing and leading a civilization from the dawn of man into the space age: Wage war, conduct diplomacy, discover new technologies, go head - to - head with some of history’s greatest leaders and build the most powerful empire the world has ever known.

                	INVITING PRESENTATION: Jump right in and play at your own pace with an intuitive interface that eases new players into the game. Veterans will appreciate the depth, detail and control that are highlights of the series.
                	BELIEVABLE WORLD: Ultra realistic graphics showcase lush landscapes for you to explore, battle over and claim as your own.
                	COMMUNITY & MULTIPLAYER: Compete with players all over the world or locally in LAN matches, mod the game in unprecedented ways, and install mods directly from an in-game community hub without ever leaving the game.
                	WIDE SYSTEM COMPATIBILITY: Civilization V operates on many different systems, from high end desktops to many laptops.
                	ALL NEW FEATURES: A new hex-based gameplay grid opens up exciting new combat and build strategies. City States become a new resource in your diplomatic battleground. An improved diplomacy system allows you to negotiate with fully interactive leaders.", false, 78f, false, "Sid Meier's Civilization® V", "Sid Meier's Civilization® V", 123.89m, 1, (short)438 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Body", "GameId", "IsDeleted", "Name", "ParentCommentId", "Time" },
                values: new object[,]
                {
                    { 1, "Good game!", 1, false, "Peter", 0, new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local) },
                    { 2, "Peter, Thanks a lot)", 1, false, "Adam", 1, new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local) },
                    { 3, "Peter, cool!", 1, false, "Bogdan from Bolt", 1, new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local) },
                    { 4, "Adam, yep))0)", 1, false, "Kelvin", 2, new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local) },
                    { 5, "Adam++", 1, false, "Martin", 2, new DateTime(2020, 7, 20, 12, 22, 28, 73, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "Id", "GameId", "GenreId", "IsDeleted" },
                values: new object[,]
                {
                    { 20, 10, 6, false },
                    { 9, 5, 16, false },
                    { 10, 5, 17, false },
                    { 11, 5, 7, false },
                    { 25, 12, 1, false },
                    { 19, 9, 16, false },
                    { 13, 6, 7, false },
                    { 21, 10, 16, false },
                    { 14, 7, 9, false },
                    { 15, 7, 16, false },
                    { 18, 9, 3, false },
                    { 12, 6, 8, false },
                    { 16, 8, 7, false },
                    { 8, 4, 9, false },
                    { 6, 4, 1, false },
                    { 1, 1, 3, false },
                    { 2, 1, 7, false },
                    { 3, 2, 7, false },
                    { 7, 4, 16, false },
                    { 24, 11, 3, false },
                    { 4, 2, 13, false },
                    { 5, 3, 6, false },
                    { 22, 10, 3, false },
                    { 23, 11, 16, false },
                    { 17, 8, 6, false }
                });

            migrationBuilder.InsertData(
                table: "GamePlatformTypes",
                columns: new[] { "Id", "GameId", "IsDeleted", "PlatformTypeId" },
                values: new object[,]
                {
                    { 21, 10, false, 3 },
                    { 19, 9, false, 1 },
                    { 18, 9, false, 3 },
                    { 22, 11, false, 4 },
                    { 23, 11, false, 1 },
                    { 20, 9, false, 2 },
                    { 17, 8, false, 1 },
                    { 11, 5, false, 1 },
                    { 15, 7, false, 2 },
                    { 14, 7, false, 1 },
                    { 13, 6, false, 2 },
                    { 12, 6, false, 1 },
                    { 10, 4, false, 4 },
                    { 9, 4, false, 3 },
                    { 8, 4, false, 2 },
                    { 7, 4, false, 1 },
                    { 6, 3, false, 3 },
                    { 5, 3, false, 1 },
                    { 4, 2, false, 3 },
                    { 3, 2, false, 2 },
                    { 2, 1, false, 2 },
                    { 1, 1, false, 1 },
                    { 16, 8, false, 3 },
                    { 24, 12, false, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GameId",
                table: "Comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GameId",
                table: "GameGenres",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenreId",
                table: "GameGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatformTypes_GameId",
                table: "GamePlatformTypes",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatformTypes_PlatformTypeId",
                table: "GamePlatformTypes",
                column: "PlatformTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Key",
                table: "Games",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GameId",
                table: "OrderItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformTypes_Type",
                table: "PlatformTypes",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_CompanyName",
                table: "Publishers",
                column: "CompanyName",
                unique: true,
                filter: "[CompanyName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "GamePlatformTypes");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "PlatformTypes");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
