using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("needToOpen", "rangeFire", "isBonfireOpen")]
	public class ES3UserType_BonfireController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BonfireController() : base(typeof(BonfireController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BonfireController)obj;
			
			writer.WritePrivateField("needToOpen", instance);
			writer.WritePrivateField("rangeFire", instance);
			writer.WriteProperty("isBonfireOpen", instance.isBonfireOpen, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BonfireController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "needToOpen":
					reader.SetPrivateField("needToOpen", reader.Read<System.Single>(), instance);
					break;
					case "rangeFire":
					reader.SetPrivateField("rangeFire", reader.Read<System.Single>(), instance);
					break;
					case "isBonfireOpen":
						instance.isBonfireOpen = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BonfireControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BonfireControllerArray() : base(typeof(BonfireController[]), ES3UserType_BonfireController.Instance)
		{
			Instance = this;
		}
	}
}