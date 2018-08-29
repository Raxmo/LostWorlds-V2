using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace LostWorldsV2
{
	[Serializable]
	public class Entity
	{
		// Declaration of classes for use with Entities
		[Serializable]
		public class Stats
		{
			/* Stat levels
			 * 
			  	0%      N/A         N/A
				1%     -2.3263     65.1055
				5%     -1.6449     75.3265
				10%    -1.2816     80.776
				20%    -0.8416     87.376
				30%    -0.5244     92.134
				40%    -0.2533     96.2005
				60%     0.2533    103.7995
				70%     0.5244    107.866
				80%     0.8416    112.624
				90%     1.2816    119.224
				95%     1.6449    124.6735
				99%     2.3263    134.8945
				100%     else       else
			 *
			 */

			public double Strength;
			public double PainTolerance;
			public double Reflex;
			public double FineMoter;
			public double Flexibility;
			public double Analysis;

			public double Intelegence;
			public double Focus;
			public double Knowledge;

			public void Roll()
			{
				Strength		= Consts.nrand(100, 15);
				PainTolerance	= Consts.nrand(100, 15);
				Reflex			= Consts.nrand(100, 15);
				FineMoter		= Consts.nrand(100, 15);
				Flexibility		= Consts.nrand(100, 15);
				Analysis		= Consts.nrand(100, 15);
				Intelegence		= Consts.nrand(100, 15);
				Focus			= Consts.nrand(100, 15);
				Knowledge		= Consts.nrand(100, 15);
			}

			public static Stats operator + (Stats a, Stats b)
			{
				Stats c = new Stats
				{
					Strength		= a.Strength + b.Strength,
					PainTolerance	= a.PainTolerance + b.PainTolerance,
					Reflex			= a.Reflex + b.Reflex,
					FineMoter		= a.FineMoter + b.FineMoter,
					Flexibility		= a.Flexibility + b.Flexibility,
					Analysis		= a.Analysis + b.Analysis,
					Intelegence		= a.Intelegence + b.Intelegence,
					Focus			= a.Focus + b.Focus,
					Knowledge		= a.Knowledge + b.Knowledge
				};

				return c;
			}
		}

		[Serializable]
		public class EGender
		{
			public string Name;
			public Stats Stats;
		}

		[Serializable]
		public class Genders
		{
			public static EGender Male => new EGender()
			{
				Name = "Male",
				Stats = new Stats()
				{
					Strength = 7.5,
					PainTolerance = -7.5,
					FineMoter = 7.5,
					Flexibility = -7.5,
					Reflex = 7.5,
					Analysis = -7.5,

					Intelegence = 0,
					Focus = 0,
					Knowledge = 0
				}
			};

			public static EGender Female => new EGender()
			{
				Name = "Female",
				Stats = new Stats()
				{
					Strength = -7.5,
					PainTolerance = 7.5,
					FineMoter = -7.5,
					Flexibility = 7.5,
					Reflex = -7.5,
					Analysis = 7.5,

					Intelegence = 0,
					Focus = 0,
					Knowledge = 0
				}
			};

			public static EGender None => new EGender()
			{
				Name = "None",
				Stats = new Stats()
			};
		}
		
		public class CombatDescriptions
		{
			public List<string> atkhit;
			public List<string> atkmiss;
			public List<string> atkdamage;
			public List<string> atknodamage;
			public List<string> death;

			public string PickAtkhit()
			{
				return atkhit[Consts.rand.Next(atkhit.Count)];
			}

			public string PickAtkmiss()
			{
				return atkmiss[Consts.rand.Next(atkmiss.Count)];
			}

			public string PickAtkdamage()
			{
				return atkdamage[Consts.rand.Next(atkdamage.Count)];
			}

			public string PickAtknodamage()
			{
				return atknodamage[Consts.rand.Next(atknodamage.Count)];
			}

			public string PickDeath()
			{
				return death[Consts.rand.Next(death.Count)];
			}
		}

		// declaration of the things that Entities need
		public string Name;

		public Stats StatBlock;

		public EGender Gender;

		[NonSerialized()]
		public CombatDescriptions CBTDesc;

		public double damage;

		public bool isalive = true;

		// Declaration of methods and such
		public string Death()
		{
			string dtext = "";
			
			dtext += (isalive = Consts.nrand(StatBlock.PainTolerance, 15) > damage) ? "" : CBTDesc.PickDeath();

			return dtext;
		}		

		public string Attack(Entity target)
		{
			string attext = "";
			
			double acc = 0;

			if((acc = Consts.nrand(StatBlock.FineMoter - target.StatBlock.Flexibility, 15)) > 0)
			{
				attext += CBTDesc.PickAtkhit();

				if (target is Characters.Character)
				{
					double boost = Math.Pow(acc, 1 / Math.E);

					target.StatBlock.Flexibility += boost;
				}

				double dmg = 0;

				if ((dmg = acc + Consts.nrand(StatBlock.Strength - target.StatBlock.PainTolerance, 15)) > 0)
				{
					attext += CBTDesc.PickAtkdamage();
					target.damage += dmg;

					if (target is Characters.Character)
					{
						double boost = Math.Pow(dmg, 1 / Math.E);

						target.StatBlock.PainTolerance += boost;
					}
				}
				else
				{
					attext += CBTDesc.PickAtknodamage();

					if (this is Characters.Character)
					{
						double boost = Math.Pow(-dmg, 1 / Math.E);

						StatBlock.Strength += boost;
					}
				}
			}
			else
			{
				attext += CBTDesc.PickAtkmiss();

				if(this is Characters.Character)
				{
					double boost = Math.Pow(-acc, 1 / Math.E);

					StatBlock.FineMoter += boost;
				}
			}

			return attext + target.Death();
		}
	}
}
