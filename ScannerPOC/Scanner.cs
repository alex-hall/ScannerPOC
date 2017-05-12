using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreScanner;

namespace ScannerPOC
{
	class Scanner
	{
		public void bootstrapScangun()
		{
			short[] scannerTypes = new short[1];
			scannerTypes[0] = 1; //all scanner types
			short lengthOfTypes = 1;
			int status;
			short numScanners;

			int[] scannerList = new int[255];
			string xml;

			CCoreScannerClass scannerInstance = new CCoreScannerClass();

			scannerInstance.PNPEvent += new _ICoreScannerEvents_PNPEventEventHandler(OnPNPEvent);
			scannerInstance.BarcodeEvent += new _ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);

			scannerInstance.Open(0, scannerTypes, lengthOfTypes, out status);
			scannerInstance.GetScanners(out numScanners, scannerList, out xml, out status);

			beginListeningForEvents(scannerInstance);
		}

		private static void beginListeningForEvents(CCoreScannerClass scannerInstance)
		{
			int status;
			int listenForEventsOpcode = 1001;
			string outXML;

			int numberOfEventsToListenFor = 2;
			string barcodeScanEvent = "1";
			string plugAndPlayEvent = "16";

			string inXML = "<inArgs>" +
								"<cmdArgs>" +
									"<arg-int>" + numberOfEventsToListenFor + "</arg-int>" +
									"<arg-int>" + barcodeScanEvent + "," + plugAndPlayEvent + "</arg-int>" +
								"</cmdArgs>" +
							"</inArgs>";

			scannerInstance.ExecCommand(listenForEventsOpcode, ref inXML, out outXML, out status);
		}

		void OnPNPEvent(short eventType, ref string ppnpData)
		{
			Console.Write("Scan gun event: " + ppnpData);
		}

		void OnBarcodeEvent(short eventType, ref string pscanData)
		{
			Console.Write("Scan: " + pscanData);
		}

	}
}
