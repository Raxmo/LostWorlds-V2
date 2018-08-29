using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utils;
using System.Runtime.Serialization.Formatters.Soap;
using System.Reflection;
using System.Windows.Media;

namespace LostWorldsV2
{
	public static class SaveData
	{
		public static bool Save(string filename)
		{
			// store relevant data to the data class
			sleepchunkx = SleepLoc.chunkpos[0];
			sleepchunky = SleepLoc.chunkpos[1];

			sleepposx = SleepLoc.pos[0];
			sleepposy = SleepLoc.pos[1];

			time = MainWindow.Time.time;

			// save all the data to the file
			try
			{
				FieldInfo[] fields = typeof(SaveData).GetFields(BindingFlags.Static | BindingFlags.Public);
				object[,] a = new object[fields.Length, 2];
				int i = 0;
				foreach (FieldInfo field in fields)
				{
					a[i, 0] = field.Name;
					a[i, 1] = field.GetValue(null);
					i++;
				};
				Stream f = File.Open(filename, FileMode.Create);
				SoapFormatter formatter = new SoapFormatter();
				formatter.Serialize(f, a);
				f.Close();
				
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool Load(string filename)
		{
			// load in data from file
			try
			{
				FieldInfo[] fields = typeof(SaveData).GetFields(BindingFlags.Static | BindingFlags.Public);
				object[,] a;
				Stream f = File.Open(filename, FileMode.Open);
				SoapFormatter formatter = new SoapFormatter();
				a = formatter.Deserialize(f) as object[,];
				f.Close();
				if (a.GetLength(0) != fields.Length) return false;
				int i = 0;
				foreach (FieldInfo field in fields)
				{
					if (field.Name == (a[i, 0] as string))
					{
						field.SetValue(null, a[i, 1]);
					}
					i++;
				};

				// set data to relevent things
				SleepLoc.pos = new Vec() { sleepposx, sleepposy };

				SleepLoc.chunkpos = new Vec() { sleepchunkx, sleepchunky };

				MainWindow.Time.time = time;
				MainWindow.Sun.Draw();

				SleepLoc.Load();

				var temp = Characters.Player.CBTDesc;

				Characters.Player = player;

				Characters.Player.CBTDesc = temp;

				MainWindow.MapInfo.Update();

				TransformGroup group = new TransformGroup();
				group.Children.Add(new TranslateTransform(-MainWindow.MapInfo.position[0], -MainWindow.MapInfo.position[1]));
				MainWindow.App.map.RenderTransform = group;

				Areas.Load(MainWindow.MapInfo.CurrBiome);

				Console.WriteLine("Load successfull...");

				return true;

			}
			catch
			{
				Console.WriteLine("Load failed...");

				return false;
			}			
		}

		/* Holder space for things to have thing be better placed and what have you
		 */
		public static double sleepposx = 0;
		public static double sleepposy = 0;

		public static double sleepchunkx = 0;
		public static double sleepchunky = 0;

		public static Characters.Character player = Characters.Player;

		public static uint time = 0;

		/* things that will be saved
		 * 
		 * last sleep location
		 * 
		 */
		public static class SleepLoc
		{
			public static Vec pos = new Vec() { 0, 0 };
			public static Vec chunkpos = new Vec() { 0, 0 };

			public static void Save()
			{
				pos[0] = MainWindow.MapInfo.position[0];
				pos[1] = MainWindow.MapInfo.position[1];

				chunkpos[0] = MainWindow.MapInfo.chunkpos[0];
				chunkpos[1] = MainWindow.MapInfo.chunkpos[1];
			}

			public static void Load()
			{
				MainWindow.MapInfo.position[0] = pos[0];
				MainWindow.MapInfo.position[1] = pos[1];

				MainWindow.MapInfo.chunkpos[0] = chunkpos[0];
				MainWindow.MapInfo.chunkpos[1] = chunkpos[1];

				MainWindow.MapInfo.Update();

				MainWindow.App.ColorizeLabels();

				TransformGroup group = new TransformGroup();
				group.Children.Add(new TranslateTransform(-MainWindow.MapInfo.position[0], -MainWindow.MapInfo.position[1]));
				MainWindow.App.map.RenderTransform = group;
			}
		}
	}
}
