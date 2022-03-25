using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("currencyCrushResource")]
	public class ES3UserType_ResourceSaveController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ResourceSaveController() : base(typeof(ResourceSaveController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ResourceSaveController)obj;
			
			writer.WriteProperty("currencyCrushResource", instance.currencyCrushResource, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<Resource>)));
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ResourceSaveController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "currencyCrushResource":
						instance.currencyCrushResource = reader.Read<System.Collections.Generic.List<Resource>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ResourceSaveControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ResourceSaveControllerArray() : base(typeof(ResourceSaveController[]), ES3UserType_ResourceSaveController.Instance)
		{
			Instance = this;
		}
	}
}