using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GameSettings))]
public class Inventory : MonoBehaviour
{
	List<Item> items;
	GameSettings settings;

	public Inventory()
	{
		items = new List<Item>();
	}

	void Start()
	{
		settings = this.GetComponent<GameSettings> ();

		//
		Item i = CreateItem (ItemType.Special, "ExampleItemShape", new Vector3 (425F, 495F, 0F));

		GameObject obj = i.GetGameObject ();

		obj.GetComponent<MeshRenderer> ().enabled = true;

		AddItem (obj);
		//AddItem (obj);
	}

	public Item CreateItem(ItemType type, string baseShape)
	{
		GameObject obj = Item.CreateItemObject(GameObject.Find ("ExampleItemShape"));
		return obj.GetComponent<Item> ();
	}

	public Item CreateItem(ItemType type, string baseShape, Vector3 startposition)
	{
		GameObject obj = Item.CreateItemObject(GameObject.Find ("ExampleItemShape"));
		obj.transform.position = startposition;
		return obj.GetComponent<Item> ();
	}

	public void LoadItems()
	{
		if (!LoadItemsFromMemory ())
		{
			LoadItemsFromSettings();
		}
	}

	// 
	void LoadItemsFromSettings()
	{
		List<Item> items = new List<Item> ();

		//playersettings something

		foreach (Item i in items)
		{
			AddItem (Item.CopyAndAttachObject(i));
		}
	}

	bool LoadItemsFromMemory()
	{
		bool already_loaded = false;

		List<Item> items = Item.GetAllItemsList ();

		if (items.Count > 0)
		{
			already_loaded = true;

			foreach(Item i in items)
			{
				AddItem (i);
			}
		}

		return already_loaded;
	}

	public void AddItem(GameObject item)
	{
		Item i = item.GetComponent<Item> ();
		if (i != null)
		{
			AddItem (i);
		}
	}

	public void AddItem(Item item)
	{
		if (item.GetGameObject () == null)
		{
			item = Item.CopyAndAttachObject(item);
		}

		items.Add (item);
	}

	public List<Item> GetItems()
	{
		return items;
	}

	public bool Contains(Item item)
	{
		return items.Contains (item);
	}

	public int NumberOfItem(Item item)
	{
		int count = 0;

		foreach (Item i in items)
		{
			if(i.Compare(item))
			{
				count++;
			}
		}

		return count;
	}
}