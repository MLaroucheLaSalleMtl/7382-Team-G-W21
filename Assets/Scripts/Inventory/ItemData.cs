using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//wrote by iris
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
        Desp = "Delicous apple Hp: + 5 Hunger: + 5";
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

public class Pear : Goods
{
    public Pear()
    {
        iD = 5;
        Name = "Pear";
        Desp = "Yummy pear Hp + 3 Hunger +3";
        SpriteName = "pear";
    }
}


public class Bow : Goods
{
    public Bow()
    {
        iD = 6;
        Name = "Bow";
        Desp = "Can be used for long distance attack";
        SpriteName = "bow";
    }
}

public class Shield : Goods
{
    public Shield()
    {
        iD = 7;
        Name = "Shield";
        Desp = "Protect yourself from attacks";
        SpriteName = "shield";
    }
}

public class DeerMeat : Goods
{
    public DeerMeat()
    {
        iD = 8;
        Name = "DeerMeat";
        Desp = "Delicous deer meat Hunger: +20";
        SpriteName = "deermeat";
    }
}

public class WolfMeat : Goods
{
    public WolfMeat()
    {
        iD = 9;
        Name = "WolfMeat";
        Desp = "Cooked wolf steak Hunger: +30";
        SpriteName = "wolfmeat";
    }
}

public class BearMeat : Goods
{
    public BearMeat()
    {
        iD = 10;
        Name = "BearMeat";
        Desp = "Raw bear meat Hunger: +45";
        SpriteName = "bearmeat";
    }
}