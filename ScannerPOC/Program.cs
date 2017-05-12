using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreScanner;

namespace ScannerPOC
{
	class Program
	{
		static void Main(string[] args)
		{

			Scanner scannerInstance = new Scanner();

			scannerInstance.bootstrapScangun();

			while (true)
			{
				System.Threading.Thread.Sleep(10);
			}
		}
	}
}