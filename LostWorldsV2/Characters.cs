using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace LostWorldsV2
{
	public static class Characters
	{
		public static double erest = 75; //watts
		public static double ehike = 100; //watts

		public static double wrest = 25; // microliters per sec
		public static double whike = 33.33333; // microliters per second

		[Serializable]
		public class Character : Entity
		{
			public Races.Race Race;
			public Inventory inventory = new Inventory();

			public double drymass = 24800;

			public double energy = 182700;
			public double water = 37200;
			public double capacity = 1000;

			public double Hydration => water / (drymass + water);

			public Stomach stomach = new Stomach()
			{
				fluids = 400,
				solids = 400,
				energy = 3000
			};
			
			[Serializable]
			public struct Stomach
			{
				public double fluids;
				public double solids;
				public double energy;

				public double denergy;
				public double dwat;

				public double volume
				{
					get => fluids + solids;
					set
					{
						solids = (solids / volume) * value;
						fluids = (fluids / volume) * value;
					}
				}
				public double edensity => energy / volume;

				public void Update()
				{					
					double srate = 1000.0 / (MainWindow.Time.minute * 120.0);

					var tdensity = edensity;
					var twater = fluids;

					solids = Math.Max(0, solids - (srate * MainWindow.Time.delta));
					fluids *= Math.Pow(10, -(MainWindow.Time.delta / MainWindow.Time.hour));

					denergy = (edensity - tdensity) * volume;

					energy -= denergy;
					dwat = twater - fluids;
				}
				
				public void Eat(Foods.Food food, double volume)
				{
					var tvolume = Math.Min(volume, 1000 - this.volume);
					fluids += tvolume * (food.water / 1000);
					solids += tvolume * (1 - (food.water / 1000));
					energy += tvolume * (food.energy / 1000);
				}

				public void Drink(double volume)
				{
					var tvolume = Math.Min(volume, 1000 - this.volume);
					fluids += tvolume;
				}
			}
			
			public double erate = 75; // Watts
			
			public double waterrate = 25; // Microliters per second
						
			public void Init()
			{
				StatBlock.Roll();
				StatBlock += Race.Stats + Gender.Stats;
			}

			public void Update()
			{
				energy -= (erate * MainWindow.Time.delta / 1000);
				water -= (waterrate * MainWindow.Time.delta / 1000);
				stomach.Update();
				energy += stomach.denergy;
				water += stomach.dwat;

				damage *= Math.Exp(-MainWindow.Time.delta / (20 * MainWindow.Time.day));

				if(energy <= 0 || Hydration <= 0.345)
				{
					Events.OOCDeath.Load();
				}
				
				MainWindow.App.ColorizeLabels();
			}
		}

		public static void Update()
		{
			Player.Update();
		}

		public static Character Player = new Character()
		{
			Name = "Player",
			Gender = Entity.Genders.Female,
			StatBlock = new Entity.Stats(),
			Race = Races.Human,
			CBTDesc = new Entity.CombatDescriptions()
			{
				atkhit = new List<string>()
				{
					"You square up and lash out, striking your target, ",
					"You strike your opponent with some force, ",
					"You lash out, hitting the target, "
				},

				atkmiss = new List<string>()
				{
					"You lash out, hitting nothing but air. ",
					"You attack your opponent, but you end up missing. ",
					"You go to attack, but miss entirely. "
				},

				atkdamage = new List<string>()
				{
					"dealing some damage. ",
					"and you manage to draw some blood. ",
					"hurting the target. "
				},

				atknodamage = new List<string>()
				{
					"but it seems to be ineffective. ",
					"your attack glancing off. ",
					"seemingly doing absolutely nothing. "
				},

				death = new List<string>()
				{
					"The pain fills your mind, and the world fades to nothing as you pass out from the pain. ",
					"You groan in pain and colapse to the ground, passing out from the pain. ",
					"You collapse, unable to continue the fight as the world around you spins and fades to nothing. "
				}
			}			
		};
	}
}
