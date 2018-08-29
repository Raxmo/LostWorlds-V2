using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LostWorldsV2
{
	public static class Events
	{
		public class Event
		{
			/* What do we need in an event?
			 * 
			 * - text
			 * - valid actions
			 */
			/* what do we know all events would do?
			* 
			* 
			* -hault the ability to drag the map
			*/

			public string Text;
			public List<Option> OptionList = new List<Option>();
			public Action Special;
			public Encounters.Encounter encounter;

			public void Load()
			{
				MainWindow.MapInfo.CanDrag = false;

				Special?.Invoke();

				MainWindow.App.MainText.SelectAll();
				MainWindow.App.MainText.Selection.Text = Text;

				MainWindow.App.OptionsContainer.Children.Clear();
				int index = 0;
				foreach(Option o in OptionList)
				{

					var btn = new Button()
					{
						Content = o.name,
						Name = "b" + index,
						Foreground = Brushes.White,
						Background = Brushes.Black,
						BorderBrush = Brushes.White,
					};
					btn.Click += new RoutedEventHandler(o.Load);

					MainWindow.App.OptionsContainer.Children.Add(btn);
					Grid.SetColumn(btn, (index % 2));
					Grid.SetRow(btn, index / 2);

					index++;
				}

				encounter?.Load();
			}

			public class Option
			{
				public string name;
				public Action opClick;

				public void Load(object sender, EventArgs e)
				{
					opClick();
				}
			}
		}

		public static Event Start => new Event()
		{
			Text = "So, you wana get in and walk around eh? Just hit 'continue' to do so, my dude",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Continue",
					opClick = new Action(() =>
					{
						PlayerGender.Load();
					})
				}
			}
		};

		public static Event PlayerGender => new Event()
		{
			Text = "What is your gender?",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Male",
					opClick = new Action(() =>
					{
						Characters.Player.Gender = Entity.Genders.Male;
						PlayerRace.Load();
					})
				},

				new Event.Option()
				{
					name = "Female",
					opClick = new Action(() =>
					{
						Characters.Player.Gender = Entity.Genders.Female;
						PlayerRace.Load();
					})
				}
			}
		};

		public static Event PlayerRace => new Event()
		{
			Text = "What is your race?",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Human",
					opClick = new Action(() =>
					{
						Characters.Player.Race = Races.Human;
						GeneratePlayer.Load();
					})
				},

				new Event.Option()
				{
					name = "Wolf",
					opClick = new Action(() =>
					{
						Characters.Player.Race = Races.Wolf;
						GeneratePlayer.Load();
					})
				},

				new Event.Option()
				{
					name = "Fox",
					opClick = new Action(() =>
					{
						Characters.Player.Race = Races.Fox;
						GeneratePlayer.Load();
					})
				},

				new Event.Option()
				{
					name = "Cat",
					opClick = new Action(() =>
					{
						Characters.Player.Race = Races.Cat;
						GeneratePlayer.Load();
					})
				}
			}
		};

		public static Event GeneratePlayer = new Event()
		{
			Text = "Time to generate your character, even though you'll never see this text, it's here for teh lulz. Remind me to NEVER say that shit ever again.",
			Special = new Action(() =>
			{
				string outtext = "";

				Characters.Player.Init();

				//Strength
				outtext +=
				(Characters.Player.StatBlock.Strength < 65.1055) ?		"Your lack of strength is legendary. " :
				(Characters.Player.StatBlock.Strength < 75.3265) ?		"Your strength is incredibly low. " :
				(Characters.Player.StatBlock.Strength < 80.776) ?		"Your strength is far lower than average. " :
				(Characters.Player.StatBlock.Strength < 87.376) ?		"Your strength is very low. " :
				(Characters.Player.StatBlock.Strength < 92.134) ?		"Your strength is lower than average. " :
				(Characters.Player.StatBlock.Strength < 96.2005) ?		"Your strength is slightly lower than average. " :
				(Characters.Player.StatBlock.Strength < 103.7995) ?		"Your strength is about average. " :
				(Characters.Player.StatBlock.Strength < 107.866) ?		"Your strength is slightly higher than average. " :
				(Characters.Player.StatBlock.Strength < 112.624) ?		"Your strength is higher than average. " :
				(Characters.Player.StatBlock.Strength < 119.224) ?		"Your strength is very high. " :
				(Characters.Player.StatBlock.Strength < 124.6735) ?		"Your strength is far higher than average. " :
				(Characters.Player.StatBlock.Strength < 134.8945) ?		"Your strength is incredibly high. " :
																		"Your strength is a thing of legend. ";

				// Pain tolerance
				outtext +=
				(Characters.Player.StatBlock.PainTolerance < 65.1055) ?		"Your lack of pain tolerance is legendary. " :
				(Characters.Player.StatBlock.PainTolerance < 75.3265) ?		"Your pain tolerance is incredibly low. " :
				(Characters.Player.StatBlock.PainTolerance < 80.776) ?		"Your pain tolerance is far lower than average. " :
				(Characters.Player.StatBlock.PainTolerance < 87.376) ?		"Your pain tolerance is very low. " :
				(Characters.Player.StatBlock.PainTolerance < 92.134) ?		"Your pain tolerance is lower than average. " :
				(Characters.Player.StatBlock.PainTolerance < 96.2005) ?		"Your pain tolerance is slightly lower than average. " :
				(Characters.Player.StatBlock.PainTolerance < 103.7995) ?	"Your pain tolerance is about average. " :
				(Characters.Player.StatBlock.PainTolerance < 107.866) ?		"Your pain tolerance is slightly higher than average. " :
				(Characters.Player.StatBlock.PainTolerance < 112.624) ?		"Your pain tolerance is higher than average. " :
				(Characters.Player.StatBlock.PainTolerance < 119.224) ?		"Your pain tolerance is very high. " :
				(Characters.Player.StatBlock.PainTolerance < 124.6735) ?	"Your pain tolerance is far higher than average. " :
				(Characters.Player.StatBlock.PainTolerance < 134.8945) ?	"Your pain tolerance is incredibly high. " :
																			"Your pain tolerance is a thing of legend. ";

				// reflexes
				outtext +=
				(Characters.Player.StatBlock.Reflex < 65.1055) ?	"Your lack of reflexes is legendary. " :
				(Characters.Player.StatBlock.Reflex < 75.3265) ?	"Your reflexes are incredibly low. " :
				(Characters.Player.StatBlock.Reflex < 80.776) ?		"Your reflexes are far lower than average. " :
				(Characters.Player.StatBlock.Reflex < 87.376) ?		"Your reflexes are very low. " :
				(Characters.Player.StatBlock.Reflex < 92.134) ?		"Your reflexes are lower than average. " :
				(Characters.Player.StatBlock.Reflex < 96.2005) ?	"Your reflexes are slightly lower than average. " :
				(Characters.Player.StatBlock.Reflex < 103.7995) ?	"Your reflexes are about average. " :
				(Characters.Player.StatBlock.Reflex < 107.866) ?	"Your reflexes are slightly higher than average. " :
				(Characters.Player.StatBlock.Reflex < 112.624) ?	"Your reflexes are higher than average. " :
				(Characters.Player.StatBlock.Reflex < 119.224) ?	"Your reflexes are very high. " :
				(Characters.Player.StatBlock.Reflex < 124.6735) ?	"Your reflexes are far higher than average. " :
				(Characters.Player.StatBlock.Reflex < 134.8945) ?	"Your reflexes are incredibly high. " :
																	"Your reflexes are a thing of legend. ";

				// Fine Moter
				outtext +=
				(Characters.Player.StatBlock.FineMoter < 65.1055) ?		"Your lack of fine moter skills is legendary. " :
				(Characters.Player.StatBlock.FineMoter < 75.3265) ?		"Your fine moter skills are incredibly low. " :
				(Characters.Player.StatBlock.FineMoter < 80.776) ?		"Your fine moter skills are far lower than average. " :
				(Characters.Player.StatBlock.FineMoter < 87.376) ?		"Your fine moter skills are very low. " :
				(Characters.Player.StatBlock.FineMoter < 92.134) ?		"Your fine moter skills are lower than average. " :
				(Characters.Player.StatBlock.FineMoter < 96.2005) ?		"Your fine moter skills are slightly lower than average. " :
				(Characters.Player.StatBlock.FineMoter < 103.7995) ?	"Your fine moter skills are about average. " :
				(Characters.Player.StatBlock.FineMoter < 107.866) ?		"Your fine moter skills are slightly higher than average. " :
				(Characters.Player.StatBlock.FineMoter < 112.624) ?		"Your fine moter skills are higher than average. " :
				(Characters.Player.StatBlock.FineMoter < 119.224) ?		"Your fine moter skills are very high. " :
				(Characters.Player.StatBlock.FineMoter < 124.6735) ?	"Your fine moter skills are far higher than average. " :
				(Characters.Player.StatBlock.FineMoter < 134.8945) ?	"Your fine moter skills are incredibly high. " :
																		"Your fine moter skills are a thing of legend. ";

				// Flexibility
				outtext +=
				(Characters.Player.StatBlock.Flexibility < 65.1055) ?	"Your lack of flexibility is legendary. " :
				(Characters.Player.StatBlock.Flexibility < 75.3265) ?	"Your flexibility is incredibly low. " :
				(Characters.Player.StatBlock.Flexibility < 80.776) ?	"Your flexibility is far lower than average. " :
				(Characters.Player.StatBlock.Flexibility < 87.376) ?	"Your flexibility is very low. " :
				(Characters.Player.StatBlock.Flexibility < 92.134) ?	"Your flexibility is lower than average. " :
				(Characters.Player.StatBlock.Flexibility < 96.2005) ?	"Your flexibility is slightly lower than average. " :
				(Characters.Player.StatBlock.Flexibility < 103.7995) ?	"Your flexibility is about average. " :
				(Characters.Player.StatBlock.Flexibility < 107.866) ?	"Your flexibility is slightly higher than average. " :
				(Characters.Player.StatBlock.Flexibility < 112.624) ?	"Your flexibility is higher than average. " :
				(Characters.Player.StatBlock.Flexibility < 119.224) ?	"Your flexibility is very high. " :
				(Characters.Player.StatBlock.Flexibility < 124.6735) ?	"Your flexibility is far higher than average. " :
				(Characters.Player.StatBlock.Flexibility < 134.8945) ?	"Your flexibility is incredibly high. " :
																		"Your flexibility is a thing of legend. ";

				// Analysis
				outtext +=
				(Characters.Player.StatBlock.Analysis < 65.1055) ?	"Your lack of analysis is legendary. " :
				(Characters.Player.StatBlock.Analysis < 75.3265) ?	"Your analysis is incredibly low. " :
				(Characters.Player.StatBlock.Analysis < 80.776) ?	"Your analysis is far lower than average. " :
				(Characters.Player.StatBlock.Analysis < 87.376) ?	"Your analysis is very low. " :
				(Characters.Player.StatBlock.Analysis < 92.134) ?	"Your analysis is lower than average. " :
				(Characters.Player.StatBlock.Analysis < 96.2005) ?	"Your analysis is slightly lower than average. " :
				(Characters.Player.StatBlock.Analysis < 103.7995) ? "Your analysis is about average. " :
				(Characters.Player.StatBlock.Analysis < 107.866) ?	"Your analysis is slightly higher than average. " :
				(Characters.Player.StatBlock.Analysis < 112.624) ?	"Your analysis is higher than average. " :
				(Characters.Player.StatBlock.Analysis < 119.224) ?	"Your analysis is very high. " :
				(Characters.Player.StatBlock.Analysis < 124.6735) ? "Your analysis is far higher than average. " :
				(Characters.Player.StatBlock.Analysis < 134.8945) ? "Your analysis is incredibly high. " :
																	"Your analysis is a thing of legend. ";

				// Intelegence
				outtext +=
				(Characters.Player.StatBlock.Intelegence < 65.1055) ?	"Your lack of intelegence is legendary. " :
				(Characters.Player.StatBlock.Intelegence < 75.3265) ?	"Your intelegence is incredibly low. " :
				(Characters.Player.StatBlock.Intelegence < 80.776) ?	"Your intelegence is far lower than average. " :
				(Characters.Player.StatBlock.Intelegence < 87.376) ?	"Your intelegence is very low. " :
				(Characters.Player.StatBlock.Intelegence < 92.134) ?	"Your intelegence is lower than average. " :
				(Characters.Player.StatBlock.Intelegence < 96.2005) ?	"Your intelegence is slightly lower than average. " :
				(Characters.Player.StatBlock.Intelegence < 103.7995) ?	"Your intelegence is about average. " :
				(Characters.Player.StatBlock.Intelegence < 107.866) ?	"Your intelegence is slightly higher than average. " :
				(Characters.Player.StatBlock.Intelegence < 112.624) ?	"Your intelegence is higher than average. " :
				(Characters.Player.StatBlock.Intelegence < 119.224) ?	"Your intelegence is very high. " :
				(Characters.Player.StatBlock.Intelegence < 124.6735) ?	"Your intelegence is far higher than average. " :
				(Characters.Player.StatBlock.Intelegence < 134.8945) ?	"Your intelegence is incredibly high. " :
																		"Your intelegence is a thing of legend. ";

				// focus
				outtext +=
				(Characters.Player.StatBlock.Focus < 65.1055) ?		"Your lack of focus is legendary. " :
				(Characters.Player.StatBlock.Focus < 75.3265) ?		"Your focus is incredibly low. " :
				(Characters.Player.StatBlock.Focus < 80.776) ?		"Your focus is far lower than average. " :
				(Characters.Player.StatBlock.Focus < 87.376) ?		"Your focus is very low. " :
				(Characters.Player.StatBlock.Focus < 92.134) ?		"Your focus is lower than average. " :
				(Characters.Player.StatBlock.Focus < 96.2005) ?		"Your focus is slightly lower than average. " :
				(Characters.Player.StatBlock.Focus < 103.7995) ?	"Your focus is about average. " :
				(Characters.Player.StatBlock.Focus < 107.866) ?		"Your focus is slightly higher than average. " :
				(Characters.Player.StatBlock.Focus < 112.624) ?		"Your focus is higher than average. " :
				(Characters.Player.StatBlock.Focus < 119.224) ?		"Your focus is very high. " :
				(Characters.Player.StatBlock.Focus < 124.6735) ?	"Your focus is far higher than average. " :
				(Characters.Player.StatBlock.Focus < 134.8945) ?	"Your focus is incredibly high. " :
																	"Your focus is a thing of legend. ";

				// Knowledge
				outtext +=
				(Characters.Player.StatBlock.Knowledge < 65.1055) ?		"Your lack of knowledge is legendary. " :
				(Characters.Player.StatBlock.Knowledge < 75.3265) ?		"Your knowledge is incredibly low. " :
				(Characters.Player.StatBlock.Knowledge < 80.776) ?		"Your knowledge is far lower than average. " :
				(Characters.Player.StatBlock.Knowledge < 87.376) ?		"Your knowledge is very low. " :
				(Characters.Player.StatBlock.Knowledge < 92.134) ?		"Your knowledge is lower than average. " :
				(Characters.Player.StatBlock.Knowledge < 96.2005) ?		"Your knowledge is slightly lower than average. " :
				(Characters.Player.StatBlock.Knowledge < 103.7995) ?	"Your knowledge is about average. " :
				(Characters.Player.StatBlock.Knowledge < 107.866) ?		"Your knowledge is slightly higher than average. " :
				(Characters.Player.StatBlock.Knowledge < 112.624) ?		"Your knowledge is higher than average. " :
				(Characters.Player.StatBlock.Knowledge < 119.224) ?		"Your knowledge is very high. " :
				(Characters.Player.StatBlock.Knowledge < 124.6735) ?	"Your knowledge is far higher than average. " :
				(Characters.Player.StatBlock.Knowledge < 134.8945) ?	"Your knowledge is incredibly high. " :
																		"Your knowledge is a thing of legend. ";

				outtext += "Are you happy with how you turned out?";
				GeneratePlayer.Text = outtext;
			}),
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Go",
					opClick = new Action(() =>
					{
						Areas.Load(MainWindow.MapInfo.CurrBiome);
						MainWindow.App.ColorizeLabels();
						SaveData.SleepLoc.Save();
						SaveData.Save("save.dat");
					})
				},
				 new Event.Option()
				 {
					 name = "Re-roll",
					 opClick = new Action(() =>
					 {
						 GeneratePlayer.Load();
					 })
				 }
			}
		};

		public static Event CombatDeath => new Event()
		{
			Text = "Youcompletely sucomb to the pain and loos conciouseness. You wakeup however, back at where you were asleep last, still a little sore, but you do feel quite a bit better. You should be more careful next time.",
			Special = new Action(() =>
			{
				Characters.Player.damage = 0;
				Characters.Player.isalive = true;
				SaveData.SleepLoc.Load();
			}),
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Continue...",
					opClick = new Action(() =>
					{
						Areas.Load(MainWindow.MapInfo.CurrBiome);
					})
				}
			}
		};

		// Special Encounter stuffs
		public static Event OnBoarDeath => new Event()
		{
			Text = "After killing the boar, you look over the body of the boar. Will you butcher it and have yourself a bite to eat, or will you leave it be for some other animal to come by and eat it?",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Leave",
					opClick = new Action(() =>
					{
						Areas.Load(MainWindow.MapInfo.CurrBiome);
					})
				},
				new Event.Option()
				{
					name = "Eat",
					opClick = new Action(() =>
					{
						Events.EatBoar.Load();
					})
				}
			}
		};

		// encounters:
		public static Event Aligator => new Event()
		{
			Text = "Container for the aligator encounter.",
			encounter = Encounters.Aligator,
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event Couger => new Event()
		{
			Text = "Container for couger encounter",
			encounter = Encounters.Couger,
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event Boar => new Event()
		{
			Text = "This is the boar event container for the boar encounter",
			encounter = Encounters.Boar,
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event Wolf => new Event()
		{
			Text = "This is the event that holds the information about the Wolf encounter.",
			encounter = Encounters.Wolf,
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event test => new Event()
		{
			Text = "Yay! you found the testing event! Congradulations, if you are reading this while not on the development team at Dark Sigma Studios, then you've found a bug! You should let us know about it with a detailed explaination of how you actually got to this point of a broken game that is now in front of you.",
			encounter = Encounters.TesterEncounter,
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Back!",
					opClick = new Action(() =>
					{
						Areas.Load(MainWindow.MapInfo.CurrBiome);
					})
				}
			}
		};

		//Encounter eating events
		public static Event EatBoar => new Event()
		{
			Text = "You look over the body of the boar before you and decide to take the opportunity to have yourself a meal.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.Pork, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		// Common options
		public static Event.Option Continue => new Event.Option()
		{
			name = "Continue",
			opClick = new Action(() =>
			{
				Areas.Load(MainWindow.MapInfo.CurrBiome);
			})
		};

		// Other Events:
		public static Event OOCDeath => new Event()
		{
			Text = "Your vision fades as you grow far too week to continue, and you collapse to the ground, fading out to nothing.",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Continue",
					opClick = new Action(() =>
					{
						Characters.Player.energy = 100000;
						Characters.Player.water = Characters.Player.drymass * 0.8;
						SaveData.SleepLoc.Load();

						Areas.Load(MainWindow.MapInfo.CurrBiome);
					})
				}
			}
		};

		// Area Events:
		// Area sleep events:
		public static Event MountainSleep => new Event()
		{
			Text = "You look around the rocky, mountainous area and find a grouping of rocks that'll provide a bit of cover for you to sleep relatively well enough.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 6 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event SwampSleep => new Event()
		{
			Text = "You look around, the swamp, the stench filling the air. You'll likely not be sleeping very well, but you will be able to sleep. You find a tree to climb up into and manage to sleep for a few hours.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 4 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event RiverSLeep => new Event()
		{
			Text = "You look around the winding rivers running everywhere in sight, and you spot a nice, sturdy tree to climb up into and nestle yourself down to catch some sleep, the sound of running water sooths your mind and you quickly fall asleep.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 8 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event RuinsSleep => new Event()
		{
			Text = "You look around the ruins of an old city, you can find a few run-down buildings. You slip into one that was mostly intact, and you manage to even find a soft spot to lay down on. Granted, it doesn't small the best, but you'll be safe for the time being.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 8 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event DesertSleep => new Event()
		{
			Text = "You look around the scorched desert before you and find not much in the ways of places to rest your head. You do find a rock with a void in the sand behind it, big enough to curl up into.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 2 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event PlainsSleep => new Event()
		{
			Text = "You look around the plains, finding not much to rest your eyes, but you do find a spot that you could close your eyes for a few hours.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 2 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event ForestSleep => new Event()
		{
			Text = "You look around the forest and you find a space under a tree, amongst the roots. You crawl in and curl up to catch a little sleep.",
			Special = new Action(() =>
			{
				SaveData.SleepLoc.Save();

				MainWindow.Time.delta = 8 * MainWindow.Time.hour;
				MainWindow.Update();

				SaveData.Save("save.dat");
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		// Hub Events:
		public static Event ForestForage => new Event()
		{
			Text = "You decide that it would be good to forage for some food. you know that there are a few sources in the forest, so, what would you like to forage for?",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Cheyote",
					opClick = new Action(() =>
					{
						ForestCheyote.Load();
					})
				},
				new Event.Option()
				{
					name = "S.Potato",
					opClick = new Action(() =>
					{
						ForestSweetPotato.Load();
					})
				}
			}
		};

		public static Event ForestHunt => new Event()
		{
			Text = "You think to yourself and come to the idea of going on a little hunt, you hear some ducks and you know you could catch a fish or two in a reasonable time, what would you like to do?",
			OptionList = new List<Event.Option>()
			{
				new Event.Option()
				{
					name = "Fish",
					opClick = new Action(() =>
					{
						ForestFish.Load();
					})
				},
				new Event.Option()
				{
					name = "Duck",
					opClick = new Action(() =>
					{
						ForestDuck.Load();
					})
				}
			}
		};

		// Consumption events
		public static Event MountainForage => new Event()
		{
			Text = "You look around the rocky area and you notice that there are some sweet potato plants growing out between some rocks. You decide to dig them up and have a bite to eat.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.SweetPotato, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event RiverFish => new Event()
		{
			Text = "You look around the area and you notice plenty of fish swimming in the rivers, so you decide to catch a few to eat.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.Fish, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event PlainsForage => new Event()
		{
			Text = "You look around the plains around you and you find some rye plants that you can harvest, so you decide to harvest a good amount and eat the grain.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.Rye, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};
		
		public static Event ForestSweetPotato => new Event()
		{
			Text = "You  look around and find a few sweet potato plants, digging them up and clean them off before eating them.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.SweetPotato, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event ForestCheyote => new Event()
		{
			Text = "You look around for some ripe Cheyote and find a few. You manage to grab one or two and have yourself a little bite to eat.",
			Special = new Action(() =>
			{
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				Characters.Player.stomach.Eat(Foods.Cheyote, 500);
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};
		
		public static Event ForestDuck => new Event()
		{
			Text = "You hear a duck nearby and you stalk your prey for a little while, before managing to catch it, making quick work of it.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.Duck, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event ForestFish => new Event()
		{
			Text = "You hear a stream nearby and decide to catch a fish or two. After some time, you manage to catch a few fish",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Eat(Foods.Fish, 500);
				MainWindow.Time.delta = 30 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		// Drinking events
		public static Event MountainDrink => new Event()
		{
			Text = "You hear a small trickling stream nearby, so you go over to it to find a small stream and have a drink from it.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Drink(500);
				MainWindow.Time.delta = 5 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event ForestDrink => new Event()
		{
			Text = "You hear a nearby stream and you soon find it. You decide to take a nice drink of refreshing, cool water.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Drink(500);
				MainWindow.Time.delta = 15 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};

		public static Event RiverDrink => new Event()
		{
			Text = "You look around at all the flowing water around you and you find a nice, clear stream to drink from, taking a moment to drink up.",
			Special = new Action(() =>
			{
				Characters.Player.stomach.Drink(500);
				MainWindow.Time.delta = 5 * MainWindow.Time.minute;
				MainWindow.Update();
			}),
			OptionList = new List<Event.Option>()
			{
				Continue
			}
		};
	}
}
