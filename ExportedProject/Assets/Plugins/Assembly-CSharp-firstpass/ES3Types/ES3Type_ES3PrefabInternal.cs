using System.Collections.Generic;
using ES3Internal;
using UnityEngine;

namespace ES3Types
{
	public class ES3Type_ES3PrefabInternal : ES3Type
	{
		public static ES3Type Instance;

		public ES3Type_ES3PrefabInternal()
			: base(typeof(ES3Type_ES3PrefabInternal))
		{
			Instance = this;
		}

		public override void Write(object obj, ES3Writer writer)
		{
			ES3Prefab eS3Prefab = (ES3Prefab)obj;
			writer.WriteProperty("prefabId", eS3Prefab.prefabId.ToString(), ES3Type_string.Instance);
			writer.WriteProperty("refs", eS3Prefab.GetReferences());
		}

		public override object Read<T>(ES3Reader reader)
		{
			long num = reader.ReadRefProperty();
			Dictionary<ES3Ref, ES3Ref> dictionary = reader.ReadProperty<Dictionary<ES3Ref, ES3Ref>>();
			Dictionary<long, long> dictionary2 = new Dictionary<long, long>();
			foreach (KeyValuePair<ES3Ref, ES3Ref> item in dictionary)
			{
				dictionary2.Add(item.Key.id, item.Value.id);
			}
			if (ES3ReferenceMgrBase.Current == null)
			{
				return null;
			}
			ES3Prefab prefab = ES3ReferenceMgrBase.Current.GetPrefab(num);
			if (prefab == null)
			{
				throw new MissingReferenceException("Prefab with ID " + num + " could not be found.\nPress the 'Refresh References' button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in the scene to refresh prefabs.");
			}
			ES3Prefab component = Object.Instantiate(prefab.gameObject).GetComponent<ES3Prefab>();
			if (component == null)
			{
				throw new MissingReferenceException("Prefab with ID " + num + " was found, but it does not have an ES3Prefab component attached.");
			}
			component.ApplyReferences(dictionary2);
			return component.gameObject;
		}
	}
}
