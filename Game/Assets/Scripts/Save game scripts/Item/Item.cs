using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType
{
	None,
	Special,

	Key,
	Medal,
	Painting,
	Sculpture,
	Recordings,//Tapes, discs, voice recorders

}

[RequireComponent(typeof(MeshRenderer))]
public class Item : MonoBehaviour
{
	//De naam van het voorwerp
	public string Name = "Onbekend voorwerp";
	public ItemType Type = ItemType.None;
	public GameObject Shape = null;

	public bool UsesQuantity = false;

	//Quantity wordt gebruikt voor wanneer een voorwerp opstapelbaar is (Zoals pijltjes, pagina's uit een boek)
	public int Quantity = 1;

	//Elk item heeft een uniek nummer (voor challenges)
	static long uniqueItemIDCounter = 0;
	long uniqueItemID;

	static List<Item> items = new List<Item>();

	public Item()
	{
		uniqueItemID = ++uniqueItemIDCounter;

		items.Add (this);
	}

	~Item()
	{
		items.Remove (this);
	}

	void Awake()
	{
		DontDestroyOnLoad (this);
	}

	void Start()
	{

	}

	public Item(string name) : base()
	{
		Name = name;
	}

	static public List<Item> GetAllItemsList()
	{
		return items;
	}

	public long GetUniqueID()
	{
		return uniqueItemID;
	}

	//Kijk of dit voorwerp exact overeenkomt met een ander bestand object
	public bool CompareUnique(Item item)
	{
		return (item.uniqueItemID == uniqueItemID);
	}

	public bool Compare(Item item)
	{
		return (item.Type == Type); 
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	static public GameObject CreateItemObject(GameObject shape)
	{
		if (shape == null)
		{
			Debug.Log ("ERROR in Item:CreateObjectFromItem: Tried to create object from itemdata but reference shape GameObject is null");
			return null;
		}
		
		GameObject obj = Instantiate (shape);
		
		obj.AddComponent<Item> ();
		obj.GetComponent<Item>().Shape = shape;

		return obj;
	}

	/*
	 * 	Creates a copy of given Item, attached to an object	
	 */
	static public Item CopyAndAttachObject(Item item)
	{
		switch (item.Type)
		{
			case ItemType.None:
			{
				Debug.Log ("ERROR in Item:CreateObjectFromItem: Tried to create object from itemdata but type isn't set");
				return null;
			}
		}

		GameObject obj = CreateItemObject (item.Shape);

		Item i = null;

		if(obj != null)
		{
			i = obj.GetComponent<Item> ();
			i.ApplyProperties(item);
		}

		return i;
	}

	public void ApplyProperties(Item item)
	{

	}
}
