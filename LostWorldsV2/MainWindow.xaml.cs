using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using Utils;
using MapFile;

namespace LostWorldsV2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	

	public partial class MainWindow : Window
	{
		// test area
		public void Test()
		{

		}
		// end test area
		
		public static MainWindow App;

		public void ColorizeLabels()
		{
			/* dehydration notes:
			 * 
			 * 53% hydration is normal
			 * 46% is dehydration onset								black
			 * 43.7% getting thirsty								white
			 * 41.4% dizzyness										yellow
			 * 39.1% severe dehydration, mental deterioration		red
			 * 34.5% death											black
			 */

			if (Characters.Player.Hydration > 0.46)
			{
				Thirsty.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(0, 0, 0)
				};
			}
			else if (Characters.Player.Hydration > 0.437)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.Hydration - 0.437;
				t = t / (0.46 - 0.437);
				t = 1 - t;

				value = (byte)(255 * t);

				Thirsty.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(value, value, value)
				};
			}
			else if (Characters.Player.Hydration > 0.414)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.Hydration - 0.414;
				t = t / (0.437 - 0.414);

				value = (byte)(255 * t);

				Thirsty.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(255, 255, value)
				};
			}
			else if (Characters.Player.Hydration > 0.391)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.Hydration - 0.391;
				t = t / (0.414 - 0.391);

				value = (byte)(255 * t);

				Thirsty.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(255, value, 0)
				};
			}
			else if (Characters.Player.Hydration > 0.345)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.Hydration - 0.345;
				t = t / (0.391 - 0.345);

				value = (byte)(255 * t);

				Thirsty.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(value, 0, 0)
				};
			}
			else
			{
				Thirsty.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(0, 0, 0)
				};
			}

			/* Energy notes
			 * 
			 * - 182700 average
			 * - 121800 starvation onset
			 * - 60900  extream starvation
			 * - 0      death
			 */

			if (Characters.Player.energy > 182700)
			{
				Energy.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(0, 0, 0)
				};
			}
			else if (Characters.Player.energy > 121800)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.energy - 121800;
				t = t / 60900;
				t = 1 - t;

				value = (byte)(255 * t);

				Energy.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(value, value, value)
				};
			}
			else if (Characters.Player.energy > 60900)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.energy - 60900;
				t = t / 60900;

				value = (byte)(255 * t);

				Energy.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(255, value, value)
				};
			}
			else if (Characters.Player.energy > 0)
			{
				byte value = 0;
				double t = 0;

				t = Characters.Player.energy;
				t = t / 60900;

				value = (byte)(255 * t);

				Energy.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(value, 0, 0)
				};
			}
			else
			{
				Energy.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(0, 0, 0)
				};
			}

			/* Damage Notes:
			 * 
			 * tanh the damage and modify it to work the way neccessary
			 * do the do with the do [INSERT MATH HERE]
			 */

			double ldamage = (Math.Tanh((Characters.Player.damage - Characters.Player.StatBlock.PainTolerance) / 30) * 510);

			if (ldamage < -255)
			{
				double sdmg = ldamage + 510;
				byte value = (byte)sdmg;

				Damage.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(value, value, value)
				};
			}
			else if (ldamage < 0)
			{
				byte value = (byte)(-ldamage);

				Damage.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(255, 255, value)
				};
			}
			else if (ldamage < 255)
			{
				byte value = (byte)(255 - ldamage);

				Damage.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(255, value, 0)
				};
			}
			else
			{
				byte value = (byte)(510 - ldamage);

				Damage.Foreground = new SolidColorBrush()
				{
					Color = Color.FromRgb(value, 0, 0)
				};
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			App = this;
			MapInfo.Update();
			Sun.Draw();
			Events.Start.Load();
			Test();
		}

		// class declarations and such
		public static class Time
		{
			public static double			delta	= 0;
			public static UInt32			time	= 0;
			public static readonly UInt32	minute	= 60;
			public static readonly UInt32	hour	= 60	* minute;
			public static readonly UInt32	day		= 24	* hour;
			public static readonly UInt32	year	= 365	* day;
		}

		public static class MapInfo
		{
			public static GlobalData Globe = new GlobalData("world.gbd");
			public static Vec position = new Vec() { 0, 0 };
			public static Vec chunkpos = new Vec() { (int)(Globe.Width / 2), (int)(Globe.Height / 4) };
			public static Vec origin = new Vec() { 300, 300 };
			public static byte[,] Biomes = new byte[3, 3];
			public static byte CurrBiome = 0;
			public static Polygon[,] Cells = new Polygon[3, 3];
			public static bool DoDrag = false;
			public static bool CanDrag = true;
			public static Vec OldMousePos = new Vec() { 0, 0 };
			public static double MoveRate = Time.hour / 500;

			public static class BColors
			{
				public static Color Badlands = Colors.Firebrick;
				public static Color Forest = Colors.ForestGreen;
				public static Color Plains = Colors.Lime;
				public static Color Swamp = Colors.Olive;
				public static Color Desert = Colors.Yellow;
				public static Color Mountain = Colors.LightSlateGray;
				public static Color Ruins = Colors.Bisque;
				public static Color River = Colors.DodgerBlue;

				public static Color[] BCList = new Color[8]
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

			}

			public static void VCell(Vec location)
			{
				Vec index = chunkpos + location;
				Color cellcolor = BColors.BCList[Globe.GetChunk(index).BiomeID];

				Point cellcenter = new Point(Globe.GetChunk(index).BiomeCenter[0], Globe.GetChunk(index).BiomeCenter[1]);

				Biomes[(int)location[0] + 1, (int)location[1] + 1] = Globe.GetChunk(index).BiomeID;

				RadialGradientBrush cellbrush = new RadialGradientBrush()
				{
					GradientOrigin = new Point(0.5, 0.5),
					Center = new Point(0.5, 0.5),
					RadiusX = 0.5,
					RadiusY = 0.5
				};

				cellbrush.GradientStops.Add(new GradientStop(cellcolor, 0.0));
				cellbrush.GradientStops.Add(new GradientStop(Color.Multiply(cellcolor, (float)0.75), 1.0));

				cellbrush.Freeze();

				Polygon cell = new Polygon()
				{
					Fill = cellbrush
				};

				Cells[(int)location[0] + 1, (int)location[1] + 1] = cell;

				// voronoi from triangulation algorithm

				Vec[] t = new Vec[4];
				Vec p;
				Vec q;

				q = new Vec() { -1, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[0] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { -1, -1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[1] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 0, -1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[2] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 0, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[3] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);

				if (!Consts.IsInCirc(t))
				{
					Vec v1 = Consts.Center(t[0], t[1], t[3]);
					Vec v2 = Consts.Center(t[1], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);
					Point p2 = new Point(v2[0], v2[1]);

					cell.Points.Add(p1);
					cell.Points.Add(p2);
				}
				else
				{
					Vec v1 = Consts.Center(t[0], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);

					cell.Points.Add(p1);
				}

				q = new Vec() { 0, -1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[0] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 1, -1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[1] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 1, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[2] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 0, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[3] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);

				if (!Consts.IsInCirc(t))
				{
					Vec v1 = Consts.Center(t[0], t[1], t[3]);
					Vec v2 = Consts.Center(t[1], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);
					Point p2 = new Point(v2[0], v2[1]);

					cell.Points.Add(p1);
					cell.Points.Add(p2);
				}
				else
				{
					Vec v1 = Consts.Center(t[0], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);

					cell.Points.Add(p1);
				}

				q = new Vec() { 1, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[0] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 1, 1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[1] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 0, 1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[2] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 0, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[3] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);

				if (!Consts.IsInCirc(t))
				{
					Vec v1 = Consts.Center(t[0], t[1], t[3]);
					Vec v2 = Consts.Center(t[1], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);
					Point p2 = new Point(v2[0], v2[1]);

					cell.Points.Add(p1);
					cell.Points.Add(p2);
				}
				else
				{
					Vec v1 = Consts.Center(t[0], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);

					cell.Points.Add(p1);
				}

				q = new Vec() { 0, 1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[0] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { -1, 1 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[1] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { -1, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[2] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);
				q = new Vec() { 0, 0 };
				p = Globe.GetChunk(index + q).BiomeCenter;
				t[3] = (Vec)(new Polar(p[0], p[1] / 64)) + origin + (q * 200) + (location * 200);

				if (!Consts.IsInCirc(t))
				{
					Vec v1 = Consts.Center(t[0], t[1], t[3]);
					Vec v2 = Consts.Center(t[1], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);
					Point p2 = new Point(v2[0], v2[1]);

					cell.Points.Add(p1);
					cell.Points.Add(p2);
				}
				else
				{
					Vec v1 = Consts.Center(t[0], t[2], t[3]);

					Point p1 = new Point(v1[0], v1[1]);

					cell.Points.Add(p1);
				}

				Cells[(int)location[0] + 1, (int)location[1] + 1] = cell;
				MainWindow.App.map.Children.Add(cell);
			}

			public static void MoveUpdate()
			{
				if (Consts.IsInPoly(Cells[1,1], position + origin))
				{
					CurrBiome = Biomes[1, 1];				
				}
				else if (Consts.IsInPoly(Cells[0, 1], position + origin))
				{
					CurrBiome = Biomes[0, 1];
				}
				else if (Consts.IsInPoly(Cells[1,0], position + origin))
				{
					CurrBiome = Biomes[1, 0];
				}
				else if (Consts.IsInPoly(Cells[2, 1], position + origin))
				{
					CurrBiome = Biomes[2, 1];
				}
				else if (Consts.IsInPoly(Cells[1, 2], position + origin))
				{
					CurrBiome = Biomes[1, 2];
				}
				else if (Consts.IsInPoly(Cells[0, 0], position + origin))
				{
					CurrBiome = Biomes[0, 0];
				}
				else if (Consts.IsInPoly(Cells[2, 0], position + origin))
				{
					CurrBiome = Biomes[2, 0];
				}
				else if (Consts.IsInPoly(Cells[2, 2], position + origin))
				{
					CurrBiome = Biomes[2, 2];
				}
				else if (Consts.IsInPoly(Cells[0, 2], position + origin))
				{
					CurrBiome = Biomes[0, 2];
				}
			}

			public static void Update()
			{
				/* Drawing the map stuffs
				 * 
				 * - grab the chunk data from the chunk you are currently in
				 * - draw all the voronois around the player's location
				 */

				MainWindow.App.map.Children.Clear();

				for (int i = -1; i <= 1; i++)
				{
					for (int j = -1; j <= 1; j++)
					{
						VCell(new Vec() { i, j });
					}
				}

				MoveUpdate();
			}
		}

		public static class Sun
		{
			public static double latitude		=	45.0 * Math.PI / 180.0;
			public static double axistilt		=	23.0 * Math.PI / 180.0;
			public static double axisrot		=	90.0 * Math.PI / 180.0;
			public static double eccentricity	=	0.02;

			/* Notes for solar tracking
			 * - solar offset = atan2(ecc * sin(theta), 1 - (ecc * cos(theta)))
			 */

			public static void Draw()
			{
				// variable declaration
				Quaterneon pos		=	new Quaterneon(0, 0, 0, 1);
				double yearpos		=	(double)Time.time / Time.year;
				double yearangle	=	yearpos * Math.PI * 2.0;
				double daypos		=	(double)Time.time / Time.day;
				double dayangle		=	daypos * Math.PI * 2.0;
				double drift		=	Math.Atan2(eccentricity * Math.Sin(yearangle), 1 - (eccentricity * Math.Cos(yearangle)));

				// rotational declaration
				var qlat		=	Quaterneon.Rotation (latitude,				new Vec() { -1,  0,  0 });
				var qaxtilt		=	Quaterneon.Rotation (axistilt,				new Vec() {  1,  0,  0 });
				var qaxisrot	=	Quaterneon.Rotation (yearangle + axisrot,	new Vec() {  0, -1,  0 });
				var qdrot		=	Quaterneon.Rotation (dayangle - drift,		new Vec() {  0, -1,  0 });

				// rotational logic
				var ntilt	=	qaxtilt	^	qaxisrot;
					pos		=	pos		^	ntilt;
					pos		=	pos		^	qlat		^	qdrot;

				// squash quaternion into a polar representation
				var fpos = (Vec)(new Polar
					(
						Math.Acos(pos.k / pos.Abs) * (200.0 / Math.PI),
						Math.Atan2((pos.j * 100), (pos.i * 100))
					));
			
				Canvas.SetLeft(App.sunpos, fpos[0] + 95);
				Canvas.SetTop(App.sunpos, fpos[1] + 95);
			}
		}

		public static void Update()
		{
			Time.time += (uint)Time.delta;
			Characters.Update();
			Time.delta = 0;
			Sun.Draw();
			App.ColorizeLabels();
		}

		// interactive stuffs
		private void MapMouseDrag(object sender, MouseEventArgs e)
		{
			if (MapInfo.DoDrag && MapInfo.CanDrag)
			{
				bool doupdate = false;

				Vec deltamouse = MapInfo.OldMousePos - new Vec() { e.GetPosition(map).X, e.GetPosition(map).Y };

				Areas.TotalDistance += deltamouse.Mag;
				Time.delta = (deltamouse.Mag * MapInfo.MoveRate);
				Time.time += (uint)Time.delta;

				MapInfo.position = MapInfo.position + deltamouse;

				if (MapInfo.position[0] < -100)
				{
					MapInfo.position[0] += 200;
					MapInfo.chunkpos[0] -= 1;
					doupdate = true;
				}
				if (MapInfo.position[0] >  100)
				{
					MapInfo.position[0] -= 200;
					MapInfo.chunkpos[0] += 1;
					doupdate = true;
				}
				if (MapInfo.position[1] < -100)
				{
					MapInfo.position[1] += 200;
					MapInfo.chunkpos[1] -= 1;
					doupdate = true;
				}
				if (MapInfo.position[1] >  100)
				{
					MapInfo.position[1] -= 200;
					MapInfo.chunkpos[1] += 1;
					doupdate = true;
				}
				if (doupdate)
				{
					MapInfo.DoDrag = false;
					MapInfo.Update();
				}

				MapInfo.MoveUpdate();
				Areas.Load(MapInfo.CurrBiome);

				TransformGroup group = new TransformGroup();
				group.Children.Add(new TranslateTransform(-MapInfo.position[0], -MapInfo.position[1]));
				map.RenderTransform = group;

				Characters.Update();

				Sun.Draw();
			}
		}
		
		private void MapMouseLeave(object sender, MouseEventArgs e)
		{
			MapInfo.DoDrag = false;
		}

		private void MapMouseDown(object sender, MouseButtonEventArgs e)
		{
			MapInfo.DoDrag = true && MapInfo.CanDrag;
			MapInfo.OldMousePos = new Vec() { e.GetPosition(map).X, e.GetPosition(map).Y };
		}

		private void MapMouseUp(object sender, MouseButtonEventArgs e)
		{
			MapInfo.DoDrag = false;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Time.time += Time.day * 14;
			Sun.Draw();
		}
		
		private void LoadCLick(object sender, RoutedEventArgs e)
		{
			SaveData.Load("save.dat");
		}
	}
}
