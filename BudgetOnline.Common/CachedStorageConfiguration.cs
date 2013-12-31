using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetOnline.Common
{
	public class CachedStorageConfiguration
	{
		public CachedStorageConfiguration()
		{
			DefaultSlotTimeout = 5000;
			NumberOfSlots = 10;
		}

		public int NumberOfSlots { get; set; }
		public int DefaultSlotTimeout { get; set; }

		//public int MaxSizeOfSot { get; set; }
		//public int MaxSizeOfAllSots { get; set; }
	}
}
