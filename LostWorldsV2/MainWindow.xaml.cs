using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Drawing;
//using System.Drawing.Drawing2D;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utils;
using MapFile;

namespace LostWorldsV2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	
	public class MapInfo
	{
		public static GlobalData Globe = new GlobalData("world.gbd");
		public static Vec position = new Vec() { 0, 0 };
		public static Vec chunkPos = new Vec() { (int)(Globe.Width / 2), (int)(Globe.Height / 4) };
		public static Vec origin = new Vec() { 300, 300 };
		public static byte[,] Biomes = new byte[3, 3];
		public static byte CurrBiome = 0;
		public static Polygon[,] Cells = new Polygon[3, 3];
		public static bool DoDrag = false;
		public static bool CanDrag = true;
		public static Vec OldMousePos = new Vec() { 0, 0 };
		public static Vec OldPosition = new Vec() { 0, 0 };

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
				River,		// 5
				Ruins,		// 6
				Plains		// 7
			};

		}

		public static void VCell(Vec location)
		{
			var index = chunkPos + location;
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
				
				MainWindow.App.MainText.SelectAll();
				MainWindow.App.MainText.Selection.Text = "Current Biome ID : " + CurrBiome;
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

	public partial class MainWindow : Window
	{
		public static MainWindow App;

		public MainWindow()
		{
			InitializeComponent();
			App = this;
			MapInfo.Update();
		}

		private void MapMouseDrag(object sender, MouseEventArgs e)
		{
			if (MapInfo.DoDrag)
			{
				/* Steps for dragging the map around
				 * 
				 * - 
				 */

				Vec deltamouse = MapInfo.OldMousePos - new Vec() { e.GetPosition(map).X, e.GetPosition(map).Y };

				MapInfo.OldPosition += deltamouse;
				MapInfo.position = MapInfo.OldPosition;

				bool doupdate = false;

				if (MapInfo.position[0] < -100)
				{
					MapInfo.OldPosition[0] += 200;
					MapInfo.chunkPos[0] -= 1;
					doupdate = true;
				}
				if (MapInfo.position[0] > 100)
				{
					MapInfo.OldPosition[0] -= 200;
					MapInfo.chunkPos[0] += 1;
					doupdate = true;
				}
				if (MapInfo.position[1] < -100)
				{
					MapInfo.OldPosition[1] += 200;
					MapInfo.chunkPos[1] -= 1;
					doupdate = true;
				}
				if (MapInfo.position[1] > 100)
				{
					MapInfo.OldPosition[1] -= 200;
					MapInfo.chunkPos[1] += 1;
					doupdate = true;
				}
				if (doupdate)
				{
					MapInfo.Update();
				}


				Console.WriteLine(MapInfo.chunkPos.Print());

				//Console.WriteLine(Map.position.Print());

				MapInfo.MoveUpdate();

				TransformGroup group = new TransformGroup();
				group.Children.Add(new TranslateTransform(-MapInfo.position[0], -MapInfo.position[1]));

				map.RenderTransform = group;
			}
		}
		
		private void MapMouseLeave(object sender, MouseEventArgs e)
		{
			MapInfo.DoDrag = false;
		}

		private void MapMouseDown(object sender, MouseButtonEventArgs e)
		{
			MapInfo.DoDrag = true && MapInfo.CanDrag;
			MapInfo.OldPosition = MapInfo.position;
			MapInfo.OldMousePos = new Vec() { e.GetPosition(map).X, e.GetPosition(map).Y };
		}

		private void MapMouseUp(object sender, MouseButtonEventArgs e)
		{
			MapInfo.DoDrag = false;
		}
	}
}
