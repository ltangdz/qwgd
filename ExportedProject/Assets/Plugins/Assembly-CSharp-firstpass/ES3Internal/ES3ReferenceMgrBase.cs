using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ES3Internal
{
	[Serializable]
	[DisallowMultipleComponent]
	public abstract class ES3ReferenceMgrBase : MonoBehaviour
	{
		private object _lock = new object();

		public const string referencePropertyName = "_ES3Ref";

		private static ES3ReferenceMgrBase _current;

		private static System.Random rng;

		[HideInInspector]
		public bool openPrefabs;

		public List<ES3Prefab> prefabs = new List<ES3Prefab>();

		[SerializeField]
		public ES3IdRefDictionary idRef = new ES3IdRefDictionary();

		private ES3RefIdDictionary _refId;

		public static ES3ReferenceMgrBase Current
		{
			get
			{
				if (_current == null || (_current.gameObject.scene.buildIndex != -1 && _current.gameObject.scene != SceneManager.GetActiveScene()))
				{
					GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
					GameObject[] array = rootGameObjects;
					foreach (GameObject gameObject in array)
					{
						if (gameObject.name == "Easy Save 3 Manager")
						{
							return _current = gameObject.GetComponent<ES3ReferenceMgr>();
						}
					}
					array = rootGameObjects;
					for (int i = 0; i < array.Length; i++)
					{
						if ((_current = array[i].GetComponentInChildren<ES3ReferenceMgr>()) != null)
						{
							return _current;
						}
					}
				}
				return _current;
			}
		}

		public bool IsInitialised => idRef.Count > 0;

		public ES3RefIdDictionary refId
		{
			get
			{
				if (_refId == null)
				{
					_refId = new ES3RefIdDictionary();
					foreach (KeyValuePair<long, UnityEngine.Object> item in idRef)
					{
						if (item.Value != null)
						{
							_refId[item.Value] = item.Key;
						}
					}
				}
				return _refId;
			}
			set
			{
				_refId = value;
			}
		}

		public ES3GlobalReferences GlobalReferences => ES3GlobalReferences.Instance;

		public void Awake()
		{
			if (_current != null && _current != this)
			{
				ES3ReferenceMgrBase current = _current;
				if (Current != null)
				{
					current.Merge(this);
					if (base.gameObject.name.Contains("Easy Save 3 Manager"))
					{
						UnityEngine.Object.Destroy(base.gameObject);
					}
					else
					{
						UnityEngine.Object.Destroy(this);
					}
					_current = current;
				}
			}
			else
			{
				_current = this;
			}
		}

		public void Merge(ES3ReferenceMgrBase otherMgr)
		{
			foreach (KeyValuePair<long, UnityEngine.Object> item in otherMgr.idRef)
			{
				Add(item.Value, item.Key);
			}
		}

		public long Get(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return -1L;
			}
			if (!refId.TryGetValue(obj, out var value))
			{
				return -1L;
			}
			return value;
		}

		internal UnityEngine.Object Get(long id, Type type)
		{
			if (id == -1)
			{
				return null;
			}
			if (!idRef.TryGetValue(id, out var value))
			{
				if (GlobalReferences != null)
				{
					UnityEngine.Object obj = GlobalReferences.Get(id);
					if (obj != null)
					{
						return obj;
					}
				}
				ES3Debug.LogWarning(string.Concat("Reference for ", type, " with ID ", id, " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene. If you are loading objects dynamically, this warning is expected and can be ignored."), this);
				return null;
			}
			if (value == null)
			{
				return null;
			}
			return value;
		}

		public UnityEngine.Object Get(long id, bool suppressWarnings = false)
		{
			if (id == -1)
			{
				return null;
			}
			if (!idRef.TryGetValue(id, out var value))
			{
				if (GlobalReferences != null)
				{
					UnityEngine.Object obj = GlobalReferences.Get(id);
					if (obj != null)
					{
						return obj;
					}
				}
				if (!suppressWarnings)
				{
					ES3Debug.LogWarning("Reference for property ID " + id + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene. If you are loading objects dynamically, this warning is expected and can be ignored.", this);
				}
				return null;
			}
			if (value == null)
			{
				return null;
			}
			return value;
		}

		public ES3Prefab GetPrefab(long id, bool suppressWarnings = false)
		{
			for (int i = 0; i < prefabs.Count; i++)
			{
				if (prefabs[i] != null && prefabs[i].prefabId == id)
				{
					return prefabs[i];
				}
			}
			if (!suppressWarnings)
			{
				ES3Debug.LogWarning("Prefab with ID " + id + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene.", this);
			}
			return null;
		}

		public long GetPrefab(ES3Prefab prefab, bool suppressWarnings = false)
		{
			for (int i = 0; i < prefabs.Count; i++)
			{
				if (prefabs[i] == prefab)
				{
					return prefabs[i].prefabId;
				}
			}
			if (!suppressWarnings)
			{
				ES3Debug.LogWarning("Prefab with name " + prefab.name + " could not be found in Easy Save's reference manager. Try pressing the Refresh References button on the ES3ReferenceMgr Component of the Easy Save 3 Manager in your scene.", prefab);
			}
			return -1L;
		}

		public long Add(UnityEngine.Object obj)
		{
			if (refId.TryGetValue(obj, out var value))
			{
				return value;
			}
			if (GlobalReferences != null)
			{
				value = GlobalReferences.GetOrAdd(obj);
				if (value != -1)
				{
					Add(obj, value);
					return value;
				}
			}
			lock (_lock)
			{
				value = GetNewRefID();
				return Add(obj, value);
			}
		}

		public long Add(UnityEngine.Object obj, long id)
		{
			if (!CanBeSaved(obj))
			{
				return -1L;
			}
			if (id == -1)
			{
				id = GetNewRefID();
			}
			lock (_lock)
			{
				idRef[id] = obj;
				refId[obj] = id;
				return id;
			}
		}

		public bool AddPrefab(ES3Prefab prefab)
		{
			if (!prefabs.Contains(prefab))
			{
				prefabs.Add(prefab);
				return true;
			}
			return false;
		}

		public void Remove(UnityEngine.Object obj)
		{
			lock (_lock)
			{
				refId.Remove(obj);
				foreach (KeyValuePair<long, UnityEngine.Object> item in idRef.Where((KeyValuePair<long, UnityEngine.Object> kvp) => kvp.Value == obj).ToList())
				{
					idRef.Remove(item.Key);
				}
			}
		}

		public void Remove(long referenceID)
		{
			lock (_lock)
			{
				idRef.Remove(referenceID);
				foreach (KeyValuePair<UnityEngine.Object, long> item in refId.Where((KeyValuePair<UnityEngine.Object, long> kvp) => kvp.Value == referenceID).ToList())
				{
					refId.Remove(item.Key);
				}
			}
		}

		public void RemoveNullValues()
		{
			foreach (long item in (from pair in idRef
				where pair.Value == null
				select pair.Key).ToList())
			{
				idRef.Remove(item);
			}
			if (GlobalReferences != null)
			{
				GlobalReferences.RemoveInvalidKeys();
			}
		}

		public void Clear()
		{
			lock (_lock)
			{
				refId.Clear();
				idRef.Clear();
			}
		}

		public bool Contains(UnityEngine.Object obj)
		{
			return refId.ContainsKey(obj);
		}

		public bool Contains(long referenceID)
		{
			return idRef.ContainsKey(referenceID);
		}

		public void ChangeId(long oldId, long newId)
		{
			idRef.ChangeKey(oldId, newId);
			refId = null;
		}

		internal static long GetNewRefID()
		{
			if (rng == null)
			{
				rng = new System.Random();
			}
			byte[] array = new byte[8];
			rng.NextBytes(array);
			return Math.Abs(BitConverter.ToInt64(array, 0) % long.MaxValue);
		}

		internal static bool CanBeSaved(UnityEngine.Object obj)
		{
			return true;
		}
	}
}
