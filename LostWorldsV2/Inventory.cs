using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostWorldsV2
{
	public class Inventory
	{
		public List<Entry> inventory = new List<Entry>();

		public struct Entry
		{
			public MResources.Resource resource;
			public double ammount;
		}

		public void AddResource(MResources.Resource resource, double ammount)
		{
			bool isInInventory = false;
			int resourceIndex = 0;
			int index = 0;
			foreach (Entry e in inventory)
			{
				isInInventory = (isInInventory || e.resource.name == resource.name);
				if (e.resource.name == resource.name)
				{
					resourceIndex = index;
				}
				index++;
			}

			if (isInInventory)
			{
				inventory[resourceIndex] = new Entry() { resource = inventory[resourceIndex].resource, ammount = inventory[resourceIndex].ammount + ammount };
			}
			else
			{
				inventory.Add(new Entry() { resource = resource, ammount = ammount });
			}
		}

		public void RemoveResource(MResources.Resource resource, double ammount)
		{
			bool isInInventory = false;
			int resourceIndex = 0;
			int index = 0;
			foreach (Entry e in inventory)
			{
				isInInventory = (isInInventory || e.resource.name == resource.name);
				if (e.resource.name == resource.name)
				{
					resourceIndex = index;
				}
				index++;
			}

			if (isInInventory)
			{
				inventory[resourceIndex] = new Entry() { resource = inventory[resourceIndex].resource, ammount = Math.Max(0, inventory[resourceIndex].ammount - ammount) };

				if (inventory[resourceIndex].ammount <= 0)
				{
					inventory.RemoveAt(resourceIndex);
				}
			}
			else
			{
				Console.WriteLine("The inventory does not have {0}.", resource.name);
			}
		}
	}
}
