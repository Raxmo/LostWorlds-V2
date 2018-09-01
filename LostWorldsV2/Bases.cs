using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace LostWorldsV2
{
	public static class Bases
	{
		public static List<Base>[,] ActiveBases = new List<Base>[MainWindow.MapInfo.Globe.Width, MainWindow.MapInfo.Globe.Height];

		public class Base
		{
			public Inventory BaseInventory = new Inventory();
			public Vec chunkPos = new Vec();
			public Vec pos = new Vec();

			public Base()
			{
				BaseInventory = new Inventory();
				chunkPos = MainWindow.MapInfo.chunkpos;
				pos = MainWindow.MapInfo.position;

				ActiveBases[(int)chunkPos[0], (int)chunkPos[1]].Add(this);
			}

			public void Draw()
			{

			}
		}
	}
}
