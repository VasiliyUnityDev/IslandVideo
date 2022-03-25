using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("needToOpen", "isBridgeOpen")]
	public class ES3UserType_BridgeController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BridgeController() : base(typeof(BridgeController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BridgeController)obj;
			
			writer.WritePrivateField("needToOpen", instance);
			writer.WriteProperty("isBridgeOpen", instance.isBridgeOpen, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BridgeController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "needToOpen":
					reader.SetPrivateField("needToOpen", reader.Read<System.Single>(), instance);
					break;
					case "isBridgeOpen":
						instance.isBridgeOpen = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BridgeControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BridgeControllerArray() : base(typeof(BridgeController[]), ES3UserType_BridgeController.Instance)
		{
			Instance = this;
		}
	}
}