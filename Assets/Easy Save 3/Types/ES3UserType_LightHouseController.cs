using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("ammountBigFire", "needSurvivor", "isLightHouseOpen", "isLevelCompleted")]
	public class ES3UserType_LightHouseController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_LightHouseController() : base(typeof(LightHouseController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (LightHouseController)obj;
			
			writer.WritePrivateField("ammountBigFire", instance);
			writer.WriteProperty("needSurvivor", instance.needSurvivor, ES3Type_int.Instance);
			writer.WriteProperty("isLightHouseOpen", instance.isLightHouseOpen, ES3Type_bool.Instance);
			writer.WriteProperty("isLevelCompleted", instance.isLevelCompleted, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (LightHouseController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ammountBigFire":
					reader.SetPrivateField("ammountBigFire", reader.Read<System.Int32>(), instance);
					break;
					case "needSurvivor":
						instance.needSurvivor = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "isLightHouseOpen":
						instance.isLightHouseOpen = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isLevelCompleted":
						instance.isLevelCompleted = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_LightHouseControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_LightHouseControllerArray() : base(typeof(LightHouseController[]), ES3UserType_LightHouseController.Instance)
		{
			Instance = this;
		}
	}
}