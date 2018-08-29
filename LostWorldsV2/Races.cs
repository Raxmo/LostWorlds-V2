using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostWorldsV2
{
	public class Races
	{
		[Serializable]
		public class Race
		{
			public string Name;

			public Entity.Stats Stats = new Entity.Stats();
		}

		public static Race Human => new Race()
		{
			Name = "Human",
			Stats = new Entity.Stats()
			{
				Strength = 0,
				PainTolerance = 0,
				Reflex = 0,
				FineMoter = 0,
				Flexibility = 0,
				Analysis = 0,

				Intelegence = 0,
				Knowledge = 0,
				Focus = 0,
			}
		};

		public static Race Wolf => new Race()
		{
			Name = "Wolf",
			Stats = new Entity.Stats()
			{
				Strength = 7.5,
				PainTolerance = 7.5,
				Reflex = -7.5,
				FineMoter = -7.5,
				Flexibility = 0,
				Analysis = 0,

				Intelegence = 0,
				Knowledge = 0,
				Focus = 0,
			}
		};

		public static Race Fox => new Race()
		{
			Name = "Fox",
			Stats = new Entity.Stats()
			{
				Strength = -7.5,
				PainTolerance = -7.5,
				Reflex = 7.5,
				FineMoter = 7.5,
				Flexibility = 7.5,
				Analysis = 0,

				Intelegence = 0,
				Knowledge = 0,
				Focus = -7.5,
			}
		};

		public static Race Cat => new Race()
		{
			Name = "Cat",
			Stats = new Entity.Stats()
			{
				Strength = 0,
				PainTolerance = 0,
				Reflex = 7.5,
				FineMoter = -7.5,
				Flexibility = 7.5,
				Analysis = 0,

				Intelegence = 0,
				Knowledge = 0,
				Focus = -7.5,
			}
		};
	}
}
