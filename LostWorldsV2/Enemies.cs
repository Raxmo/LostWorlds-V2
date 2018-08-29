using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostWorldsV2
{
	public static class Enemies
	{
		public class Enemy : Entity
		{
			public Stats BaseStats;

			public void Init()
			{
				StatBlock = BaseStats + Gender.Stats;
				damage = 0;
				isalive = true;
			}
		}

		public static Enemy Aligator => new Enemy()
		{
			Name = "Aligator",
			Gender = Entity.Genders.None,
			BaseStats = new Entity.Stats()
			{
				Strength = 120,
				PainTolerance = 135,
				FineMoter = 90,
				Flexibility = 80,
				Reflex = 80,
				Analysis = 85,

				Intelegence = 85,
				Focus = 85,
				Knowledge = 85
			},
			CBTDesc = new Entity.CombatDescriptions()
			{
				atkhit = new List<string>()
				{
					"The aligator growls at you and lashes out at you, striking you with it's tail, ",
					"The aligator snaps it's jaws at you, "
				},

				atkmiss = new List<string>()
				{
					"The aligator lashes out at you, swinging in the air to no effect. ",
					"The aligator snaps it's jaws in the air, doing nothing. "
				},

				atkdamage = new List<string>()
				{
					"hitting hard, drawing blood. ",
					"lacerating your flesh. "
				},

				atknodamage = new List<string>()
				{
					"grazing you, leaving only a slight scrape. ",
					"but you are able to turn out of the way, avoiding any damage delt to you. "
				},

				death = new List<string>()
				{
					"The aligator slumps to the ground, lifeless. ",
					"The aligator cries out  one last time, breathing it's final breath. "
				}
			}
		};

		public static Enemy Couger => new Enemy()
		{
			Name = "Couger",
			Gender = Entity.Genders.None,
			BaseStats = new Entity.Stats()
			{
				Strength = 120,
				PainTolerance = 115,
				FineMoter = 120,
				Flexibility = 115,
				Reflex = 100,
				Analysis = 85,

				Intelegence = 85,
				Focus = 85,
				Knowledge = 85
			},
			CBTDesc = new Entity.CombatDescriptions()
			{
				atkhit = new List<string>()
				{
					"The couger eyes you down, swiping at you, ",
					"The couger lunges out at you snapping it's jaws at you, "
				},

				atkmiss = new List<string>()
				{
					"The couger swipes at the air, hurting the air sevearly. ",
					"The couger snaps it's jaws at the air. "
				},

				atkdamage = new List<string>()
				{
					"sinking into your flesh, drawing blood. ",
					"connecting hard, making you wince in pain. "
				},

				atknodamage = new List<string>()
				{
					"but you are able to move out of the way just before it is able to deal any damage. ",
					"only grazing you without damaging you. "
				},

				death = new List<string>()
				{
					"The couger yelps in pain, and collapses, breathing it's final breath. ",
					"The couger collapses, going limp and lifeless. "
				}
			}
		};

		public static Enemy Boar => new Enemy()
		{
			Name = "Bore",
			Gender = Entity.Genders.None,
			BaseStats = new Entity.Stats()
			{
				Strength = 120,
				PainTolerance = 130,
				FineMoter = 85,
				Flexibility = 80,
				Reflex = 20,
				Analysis = 85,

				Intelegence = 85,
				Focus = 85,
				Knowledge = 85
			},
			CBTDesc = new Entity.CombatDescriptions()
			{
				atkhit = new List<string>()
				{
					"The boar charges at you, landing a hit ",
					"The boar cries out and starts to charge at you, hitting you ",
					"The boar scratches the ground before charging you "
				},

				atkmiss = new List<string>()
				{
					"The boar charges, but misses you entirely. ",
					"The boar gets ready to charge, but you are able to get out of the way before it lands it's hit. ",
					"The boar charges at you, missing."
				},

				atkdamage = new List<string>()
				{
					"landing a solid hit, sending pain through your body. ",
					"hitting you squarely, drawing blood. ",
					"gouging you with it's tusks. "
				},

				atknodamage = new List<string>()
				{
					"only grazing you, dealing no damage. ",
					"as you spin out of the way, avoiding any damage dealt to you. ",
					"and you jump back with the hit, avoiding damage. "
				},

				death = new List<string>()
				{
					"The boar limps a bit before collapsing, lying limp on the ground. ",
					"The boar cries out one last time, before it's life slips away. ",
					"The boar starts to charge at you, before falling to the ground, lifeless. "
				}
			}
		};

		public static Enemy Wolf => new Enemy()
		{
			Name = "Wolf",
			Gender = Entity.Genders.None,
			BaseStats = new Entity.Stats()
			{
				Strength = 115,
				PainTolerance = 100,
				FineMoter = 100,
				Flexibility = 85,
				Reflex = 85,
				Analysis = 85,

				Intelegence = 85,
				Focus = 85,
				Knowledge = 85
			},
			CBTDesc = new Entity.CombatDescriptions()
			{
				atkhit = new List<string>()
				{
					"The wolf snaps at you, ",
					"The wolf lashes out at you, ",
					"The wolf growls and lunges at you "
				},

				atkmiss = new List<string>()
				{
					"The wolf strikes out, hitting absolutely nothing, it's jaws snapping shut in the air. ",
					"The wolf lunges at you, but you are able to get out of the way in time. "
				},

				atkdamage = new List<string>()
				{
					"growling deeply as it sinks it's teath into you. ",
					"scratching you, drawing blood. "
				},

				atknodamage = new List<string>()
				{
					"but mearly grazes you, dealing no real damage to you at all ",
					"while the wolf lands it's attack, you spin out of the way, escaping any damage. "
				},

				death = new List<string>()
				{
					"The wolf winces and whines, shivering as it collapses to the ground, lifeless. ",
					"The wolf cries out one last time before falling limp. "
				}
			}
		};

		public static Enemy testEnemy => new Enemy()
		{
			Name = "Tester",
			Gender = Entity.Genders.Male,
			BaseStats = new Entity.Stats()
			{
				Strength = 85,
				PainTolerance = 85,
				FineMoter = 150,
				Flexibility = 85,
				Reflex = 85,
				Analysis = 85,

				Intelegence = 85,
				Focus = 85,
				Knowledge = 85
			},
			CBTDesc = new Entity.CombatDescriptions()
			{
				atkhit = new List<string>()
				{
					"The thing hits you! ",
					"The thing manages to land a hit! ",
					"The thing hits squarely! "
				},

				atkmiss = new List<string>()
				{
					"The thing simply whiffs! ",
					"The thing hits nothing but air! "
				},

				atkdamage = new List<string>()
				{
					"The thing manages to damage you! ",
					"It deals damage! "
				},

				atknodamage = new List<string>()
				{
					"The thing's attack simply bounces off of you! ",
					"The thing ends up doing nothing! ",
					"You don't feel a thing! "
				},

				death = new List<string>()
				{
					"The test thing croaks and dies! ",
					"The thing dies! ",
					"It's dead Jim! "
				}
			}
		};
	}
}
