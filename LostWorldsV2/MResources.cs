using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostWorldsV2
{
	public static class MResources
	{
		public class Resource
		{
			public string name;
			public double density;      // g per ml
		}

		public static Resource Limestone => new Resource()
		{
			name = "Limestone",
			density = 2.560
		};

		public static Resource Clay => new Resource()
		{
			name = "Clay",
			density = 1.826
		};

		public static Resource Wood => new Resource()
		{
			name = "Wood",
			density = 0.5
		};

		public static Resource Bamboo => new Resource()
		{
			name = "Bamboo",
			density = 0.16
		};

		public static Resource Charcoal => new Resource()
		{
			name = "Charcoal",
			density = 0.2
		};
	}
}
