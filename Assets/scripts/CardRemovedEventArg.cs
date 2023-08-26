using UnityEngine;
using System;


public class CardEventArg:EventArgs{
	public int cardIndex{ get; private set;}
	public CardEventArg(int i){
		cardIndex = i;

	}
	
	
};

public class StackEventArg : EventArgs
{
    public int cardIndex { get; private set; }
    public Vector3 pos { get; private set; }
    public StackEventArg(int i,Vector3 p)
    {
        cardIndex = i;
        pos = p;

    }


};