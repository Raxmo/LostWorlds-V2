using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostWorldsV2
{
	public static class Foods
	{
		public struct Food
		{
			/* foodstuffs notes and things
			 * 
			 * All numbers are in terms of per liter
			 */

			public double energy;
			public double water;
		}

		public static Food Pork => new Food()
		{
			energy = 6979.52,
			water = 312.7
		};

		public static Food Rye => new Food()
		{
			energy = 10101.87,
			water = 75.72
		};

		public static Food Mushroom => new Food()
		{
			energy = 275.89,
			water = 273.53
		};

		public static Food SweetPotato => new Food()
		{
			energy = 2022.78,
			water = 434.44
		};

		public static Food Cheyote => new Food()
		{
			energy = 447.96,
			water = 525
		};

		public static Food Duck => new Food()
		{
			energy = 8343.66,
			water = 306
		};

		public static Food Fish => new Food()
		{
			energy = 3089.56,
			water = 528
		};
	}
}
