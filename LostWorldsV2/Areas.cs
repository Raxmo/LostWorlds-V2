using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utils;

namespace LostWorldsV2
{
	public static class Areas
	{
		public static double TotalDistance = 0;

		public class Area
		{
			public byte BiomeID;
			public string Description;
			public DataMatrix EventList;
			public double MeanDist;
			public double SigmoidDeviation;
			public List<Option> options;

			public void Load()
			{
				MainWindow.MapInfo.CanDrag = true;
				MainWindow.App.MainText.SelectAll();
				MainWindow.App.MainText.Selection.Text = Description;
				MainWindow.App.OptionsContainer.Children.Clear();

				if (options != null)
				{
					int index = 0;
					foreach (Option o in options)
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
				}

				if (Consts.rand.NextDouble() < Consts.SigmoidN(TotalDistance, MeanDist, SigmoidDeviation, 1.0))
				{
					EventList?.Pick()?.Load();
					TotalDistance = 0;
				}
			}

			public class Option
			{
				public string name;
				public Events.Event option;

				public void Load(object sender, EventArgs e)
				{
					option.Load();
				}
			}
		}

		public static void Load(int bid)
		{
			AreaArray[bid].Load();
		}

		private static Area[] AreaArray = new Area[8]
		{
			Badlands,	// 0
			Mountain,	// 1
			Swamp,		// 2
			River,		// 3
			Forest,		// 4
			Ruins,		// 5
			Desert,		// 6
			Plains		// 7
		};

		// Special class for stuffs
		public static class Special
		{
			private static Random spcrand = new Random();
			
			public static string Badlands()
			{
				string output = "";
				byte[] bytes = new byte[200];
				spcrand.NextBytes(bytes);
				output += Encoding.ASCII.GetString(bytes);
				return output;
			}
		}

		// areas
		public static Area Badlands => new Area()
		{
			BiomeID = 0,
			Description = "The badlands are a hostile place, full of unpredictable baddies and you should generally not spend any amount of time in this area. The " + Special.Badlands() + " GET OUT!"
		};

		public static Area Mountain => new Area()
		{
			BiomeID = 1,
			Description = "The mountains are full of rocks and sparce with plants. You'll be able to find stone, and even metals in the mountains, and you'll be able to find some animals to hunt here and there, but foraiging will be dificult.",
			options = new List<Area.Option>()
			{
				new Area.Option()
				{
					name = "Sleep",
					option = Events.MountainSleep
				},
				new Area.Option()
				{
					name = "Forage",
					option = Events.MountainForage
				},
				new Area.Option()
				{
					name = "Drink",
					option = Events.MountainDrink
				}
			},
			MeanDist = 250,
			SigmoidDeviation = 15,
			EventList = new DataMatrix { { Events.Couger, 1 } }
		};

		public static Area Swamp => new Area()
		{
			BiomeID = 2,
			Description = "The swamps are full of nasty water, it'll be hard to find good, clean water here, but you'll be able to find plenty of mushrooms and aquatic wildlife.",
			options = new List<Area.Option>()
			{
				new Area.Option()
				{
					name = "Sleep",
					option = Events.SwampSleep
				},
				new Area.Option()
				{
					name = "Forage",
					option = new Events.Event()
					{
						Text = "You look around the swamp and are able to find some eatable mushrooms to consume. It isn't much, but it'll stave you off for now.",
						Special = new Action(() =>
						{
							Characters.Player.stomach.Eat(Foods.Mushroom, 200);
							MainWindow.Time.delta = 30 * MainWindow.Time.minute;
							MainWindow.Update();
						})
					}
				}
			},
			MeanDist = 250,
			SigmoidDeviation = 15,
			EventList = new DataMatrix { { Events.Aligator, 1 } }
		};

		public static Area River => new Area()
		{
			BiomeID = 3,
			Description = "The rivers are full of fish and fresh water. You'll be wet, nothing you can avoid, but you will have plenty of opportunities to fish and drink clean water.",
			options = new List<Area.Option>()
			{
				new Area.Option()
				{
					name = "Sleep",
					option = Events.RiverSLeep
				},
				new Area.Option()
				{
					name = "Fish",
					option = Events.RiverFish
				},
				new Area.Option()
				{
					name = "Drink",
					option = Events.RiverDrink
				}
			}
		};

		public static Area Forest => new Area()
		{
			BiomeID = 4,
			MeanDist = 250,
			SigmoidDeviation = 10,
			Description = "The forest is full of wildlife, and plenty of eatable plants. Water shouldn't be too much of an issue here either.",
			EventList = new DataMatrix() { { Events.Boar, 0.25 }, { Events.Wolf, 0.75 } },
			options = new List<Area.Option>()
			{
				ForestSleep,
				new Area.Option()
				{
					name = "Drink",
					option = Events.ForestDrink
				},
				new Area.Option()
				{
					name = "Forage",
					option = Events.ForestForage
				},
				new Area.Option()
				{
					name = "Hunt",
					option = Events.ForestHunt
				}
			}
		};

		public static Area Ruins => new Area()
		{
			BiomeID = 5,
			Description = "The ruins are just a mass of decrepid buildings, deralict and left to rot. You will likely have a hard time finding any decent shealter here, or even usefull supplies.",
			options = new List<Area.Option>()
			{
				new Area.Option()
				{
					name = "Sleep",
					option = Events.RuinsSleep
				}
			}
		};

		public static Area Desert => new Area()
		{
			BiomeID = 6,
			Description = "The deserts are hot, and baren. You'll have a very hard time finding water, unless you are a bit more crafty with how you go about finding it.",
			options = new List<Area.Option>()
			{
				new Area.Option()
				{
					name = "Sleep",
					option = Events.DesertSleep
				}
			}
		};

		public static Area Plains => new Area()
		{
			BiomeID = 7,
			Description = "The plains are full of grain plants, and grazing animals. Food will not be an issue.",
			options = new List<Area.Option>()
			{
				new Area.Option()
				{
					name = "Sleep",
					option = Events.PlainsSleep
				},
				new Area.Option()
				{
					name = "Forage",
					option = Events.PlainsForage
				}
			},
			MeanDist = 250,
			SigmoidDeviation = 15,
			EventList = new DataMatrix() { { Events.Boar, 1 } }
		};

		// Base options
		public static Area.Option ForestSleep => new Area.Option()
		{
			name = "Sleep",
			option = Events.ForestSleep
		};
	}
}
