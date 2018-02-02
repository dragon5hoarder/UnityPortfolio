using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//declares each direction in a 9 space grid
public enum ActionType { SW = 0, S, SE, W, STAY, E, NW, N, NE};
//declares each piece type
public enum PieceType { SIMPLE = 0, STRATEGIC, FOOD, ADVANTAGE, INACCESSIBLE, SELF, EMPTY };

//an array that holds the surroundings of each piece
public struct Surroundings
{
     public PieceType[] array;
}

