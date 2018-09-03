using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Utils;

namespace LostWorldsV2
{
	public static class Bases
	{
		public static List<Base>[,] BaseList = new List<Base>[MainWindow.MapInfo.Globe.Width, MainWindow.MapInfo.Globe.Height];

		public static Base NearestBase;
		public static Base SelectedBase;

		public static void Draw(Vec chunk_position, Vec offset)
		{
			try
			{
				foreach (Base _base in BaseList[(int)chunk_position[0], (int)chunk_position[1]])
				{
					try
					{
						_base.Draw(offset);
					}
					catch
					{

					}
				}
			}
			catch
			{

			}
		}

		public class Base
		{
			public Inventory BaseInventory = new Inventory();
			public Vec chunkPos = new Vec();
			public Vec pos = new Vec();

			public double distance_to
			{
				get
				{
					Vec delta_chunk = chunkPos - MainWindow.MapInfo.chunkpos;
					Vec delta_pos = pos - (MainWindow.MapInfo.position + new Vec() { 100, 100 });

					Vec chunk_offset = delta_chunk * 200;

					Vec delta = chunk_offset + delta_pos;

					return delta.Mag;
				}
			}

			public bool is_selected => this == SelectedBase;

			public Base()
			{
				BaseInventory = new Inventory();
				chunkPos = MainWindow.MapInfo.chunkpos;
				pos = MainWindow.MapInfo.position;

				try
				{
					BaseList[(int)chunkPos[0], (int)chunkPos[1]].Add(this);
				}
				catch
				{
					BaseList[(int)chunkPos[0], (int)chunkPos[1]] = new List<Base>{this};
				}
			}

			public void Draw(Vec offset)
			{
				Color fill = (is_selected) ? Colors.SlateGray : Colors.DarkSlateGray;

				Ellipse e = new Ellipse()
				{
					Width = 10,
					Height = 10,
					Fill = new SolidColorBrush(Colors.DarkSlateGray)
				};

				MainWindow.App.map.Children.Add(e);
				Canvas.SetLeft(e, pos[0] + (200 * (offset[0] + 1)));
				Canvas.SetTop(e, pos[1] + (200 * (offset[1] + 1)));
			}
		}
	}
}
