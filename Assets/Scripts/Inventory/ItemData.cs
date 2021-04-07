using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemData{
	public int ID{ get; set; }
	public int Num{ get; set; }
    public string Name{ get; set; }
	public string Desp { get; set; }
	public Sprite Sprite{ get; set; }
	

	public ItemData(int _id, string _name, string _des, string spriteName)
    {
		this.ID = _id;
		this.Name = _name;
		this.Desp = _des;
		this.Sprite = Resources.Load<Sprite> ("Sprites/" + spriteName);	
	}

	public ItemData()
    {
		this.ID = -1;
	}

	public void Reset()
    {
		ID = -1;
		Name = null;
		Desp = null;
		Sprite = null;
    }
}

public class Goods {
	public  int iD;
	public string Name;
	public string Desp;
	public Sprite Sprite;
	public string SpriteName;

}
public class Rock : Goods
{
	public Rock( )
    {
		iD = 0;
		Name = "Rock";
		Desp = "You can use this to create weapon";
		SpriteName = "rock";
	}
}
public class Wood : Goods
{
	public Wood( )
	{
		iD = 1;
		Name = "Wood";
		Desp = "You can use this to create weapon";
		SpriteName = "wood";
	}
}
public class Apple : Goods
{
    public Apple()
    {
        iD = 2;
        Name = "Apple";
        Desp = "Delicous apple Hp: + 10";
        SpriteName = "apple";
    }
}

public class Sword : Goods
{
    public Sword()
    {
        iD = 3;
        Name = "Sword";
        Desp = "this is a sword";
        SpriteName = "sword";
    }
}

public class Axe : Goods
{
        public Axe()
        {
            iD = 4;
            Name = "Axe";
            Desp = "this is an axe";
            SpriteName = "axe";
        }
}

