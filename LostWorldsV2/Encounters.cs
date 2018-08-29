using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Utils;


/*
 * 
			Strength;			Damage
			PainTolerance;		resistance
			Reflex;				speed
			FineMoter;			Accuracy
			Flexibility;		Dodging
			Analysis;			

			Intelegence;		
			Focus;				
			Knowledge;			
 */

namespace LostWorldsV2
{
	public static class Encounters
	{
		public class Encounter
		{
			public Enemies.Enemy Enemy;
			public Events.Event OnDeath;
			public string Description;

			public void Attack(object sender, EventArgs e)
			{
				MainWindow.App.MainText.SelectAll();
				MainWindow.App.MainText.Selection.Text = "";
				Combat(Characters.Player, Enemy);
				MainWindow.App.ColorizeLabels();
			}

			public void Run(object sender, EventArgs e)
			{
				Areas.Load(MainWindow.MapInfo.CurrBiome);
				if (!Characters.Player.isalive)
				{
					Events.CombatDeath.Load();
				}
			}

			public void LoadOnDeath(object sender, EventArgs e)
			{
				OnDeath.Load();
			}
			
			public void Load()
			{
				Enemy.Init();

				MainWindow.App.OptionsContainer.Children.Clear();

				Button btn = new Button()
				{
					Content = "Attack",
					Name = "attackbtn",

					Foreground = Brushes.White,
					Background = Brushes.Black,
					BorderBrush = Brushes.White,
				};
				btn.Click += new System.Windows.RoutedEventHandler(Attack);

				MainWindow.App.OptionsContainer.Children.Add(btn);

				Button btn2 = new Button()
				{
					Content = "Run",
					Name = "runbtn",

					Foreground = Brushes.White,
					Background = Brushes.Black,
					BorderBrush = Brushes.White,
				};
				btn2.Click += new System.Windows.RoutedEventHandler(Run);

				MainWindow.App.OptionsContainer.Children.Add(btn2);

				Grid.SetColumn(btn, 0);
				Grid.SetColumn(btn2, 1);

				MainWindow.App.MainText.SelectAll();
				MainWindow.App.MainText.Selection.Text = Description;
			}

			double aspot = 0;
			double bspot = 0;

			public void Combat(Entity a, Entity b)
			{
				/* Combat steps:
				 * 
				 * - check to see who's turn it is
				 * - current entity's turn attacks
				 *   - first see if they hit
				 *   - calculate damage
				 */

				if (a.isalive && b.isalive)
				{

					string output = "";

					Entity active;
					Entity target;
					Entity next;

					if (aspot == 0 && bspot == 0)
					{
						aspot = Consts.nrand(a.StatBlock.Reflex, 15);
						bspot = Consts.nrand(b.StatBlock.Reflex, 15);
					}

					if (aspot > bspot)
					{
						active = a;
						target = b;

						aspot -= bspot;
					}
					else
					{
						active = b;
						target = a;

						bspot -= aspot;
					}

					aspot += Consts.nrand(a.StatBlock.Reflex, 15);
					bspot += Consts.nrand(b.StatBlock.Reflex, 15);

					next = (aspot > bspot) ? a : b;

					output += active.Attack(target);

					MainWindow.App.MainText.AppendText(output);

					if (next != Characters.Player)
					{
						Combat(a, b);
					}

					if (!(a.isalive && b.isalive))
					{
						MainWindow.App.OptionsContainer.Children.Clear();

						Entity nme = (a == Characters.Player) ? b : a;

						if (!nme.isalive && OnDeath != null)
						{
							Button btn2 = new Button()
							{
								Content = "Continue",
								Name = "cntbtn",

								Foreground = Brushes.White,
								Background = Brushes.Black,
								BorderBrush = Brushes.White,
							};
							btn2.Click += new System.Windows.RoutedEventHandler(LoadOnDeath);

							MainWindow.App.OptionsContainer.Children.Add(btn2);
						}
						else
						{
							Button btn2 = new Button()
							{
								Content = "Continue",
								Name = "cntbtn",

								Foreground = Brushes.White,
								Background = Brushes.Black,
								BorderBrush = Brushes.White,
							};
							btn2.Click += new System.Windows.RoutedEventHandler(Run);

							MainWindow.App.OptionsContainer.Children.Add(btn2);
						}
					}
				}
			}
		}

		//Encounters:
		public static Encounter Aligator => new Encounter()
		{
			Enemy = Enemies.Aligator,
			Description = "You spot an aligator sulking in the shadows of the waters in the swamp."
		};

		public static Encounter Couger => new Encounter()
		{
			Enemy = Enemies.Couger,
			Description = "You notice a couger stalking you, if you play your cards correctly, you should be able to get away before it decides to attack you."
		};

		public static Encounter TesterEncounter => new Encounter()
		{
			Enemy = Enemies.testEnemy,
			Description = "You see a weird testing enemy in front of you, you should be able to take it on easily."
		};

		public static Encounter Wolf => new Encounter()
		{
			Enemy = Enemies.Wolf,
			Description = "You spot a wolf lurking towards you, growling low as it prepares to attack."
		};

		public static Encounter Boar => new Encounter()
		{
			Enemy = Enemies.Boar,
			Description = "You spot a wild boar nearby, you could get away before it sposts you, or, you could attack it and try and get some good meat from it.",
			OnDeath = Events.OnBoarDeath
		};
	}
}
