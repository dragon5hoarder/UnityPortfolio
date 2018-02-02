using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

     //each piece
     public Transform strategicPiece, simplePiece, advantagePiece, foodPiece;

     //the buttons used to control the game
     public Button roundButton, newButton;
     public Transform board;

     //the board grid, uses xy coordinates
     public List<Transform> grid;

     
     //game initializers
     const int _initAgentFactor = 4, _initResourceFactor = 2;
     public const int _minWidth = 3, _minheight = 3;
     public const double _startingAgentEnergy = 20, _startingResourceCapacity = 10;

     int numInitAgents, numInitResources, roundNum = 0;
     
     //game statuses
     public enum Status { NOT_STARTED, PLAYING, OVER };
     //initially not started
     Status status = Status.NOT_STARTED;

     //can only go from 3 to 10
     //less than 3 and the game doesnt function well
     //more than 10 and the game takes too long
     [Range(3, 10)]
     public int width, height;     

     //sets the new round button to false on startup
     private void Awake()
     {
          roundButton.interactable = false;
     }

     //creates a new game
     public void NewGame () {
          //moves the game object and board so the pieces are sitting in the center of the screen
          this.transform.position = new Vector3(100 - (width / 2), 0, -(height / 2));
          board.localScale = new Vector3(width + 1, -0.1f, height + 1);
          board.localPosition = new Vector3((width / 2), 0, (height / 2));

          //each new game is not started
          status = Status.NOT_STARTED;
          

          
          //initial number of pieces
          numInitAgents = (width * height) / _initAgentFactor;
          numInitResources = (width * height) / _initResourceFactor;

          play();
          

          
	}
	
	
     //sets all the pieces
     public void play()
     {
          //Debug.Log("grid.Count: " + grid.Count);
          //Debug.Log("grid.Capacity: " + grid.Capacity);

          //clears the previous board
          for (int i = 0; i < grid.Count; i++)
          {
               if (grid[i] != null)
                    Destroy(grid[i].gameObject);
          }
          grid.Clear();

          //initializes the grid list
          //each empty space needs to be null
          grid = new List<Transform>(width * height);
          for (int i = 0; i < width * height; i++)
          {
               grid.Add(null);
          }

          //initializes number of specific pieces
          int numStrategic = numInitAgents / 2;
          int numSimple = numInitAgents - numStrategic;
          int numAdvantages = numInitResources / 4;
          int numFood = numInitResources - numAdvantages;

          //randomly places strategic pieces
          while (numStrategic > 0)
          {
               int i = (int)Random.Range(0, (width * height));
               if (grid[i] == null)
               {
                    grid[i] = Instantiate(strategicPiece, this.transform);
                    grid[i].GetComponent<Strategic>().energy = _startingAgentEnergy;
                    //grid[i].GetComponent<Piece>().id = idGen++;
                    grid[i].transform.localPosition = new Vector3((i % width), 0, (i / width));
                    numStrategic--;
               }

          }
          //randomly places simple pieces
          while (numSimple > 0)
          {
               int i = (int)Random.Range(0, (width * height));
               if (grid[i] == null)
               {
                    grid[i] = Instantiate(simplePiece, this.transform);
                    grid[i].GetComponent<Simple>().energy = _startingAgentEnergy;
                    //grid[i].GetComponent<Piece>().id = idGen++;
                    grid[i].transform.localPosition = new Vector3((i % width), 0, (i / width));
                    numSimple--;
               }
          }
          //randomly places advantages
          while (numAdvantages > 0)
          {
               int i = (int)Random.Range(0, (width * height));
               if (grid[i] == null)
               {
                    grid[i] = Instantiate(advantagePiece, this.transform);
                    grid[i].GetComponent<Advantage>().capacity = _startingResourceCapacity;
                    //grid[i].GetComponent<Piece>().id = idGen++;
                    grid[i].transform.localPosition = new Vector3((i % width), 0, (i / width));
                    numAdvantages--;
               }
          }
          //randomly places food
          while (numFood > 0)
          {
               int i = (int)Random.Range(0, (width * height));
               if (grid[i] == null)
               {
                    grid[i] = Instantiate(foodPiece, this.transform);
                    grid[i].GetComponent<Food>().capacity = _startingResourceCapacity;
                    //grid[i].GetComponent<Piece>().id = idGen++;
                    grid[i].transform.localPosition = new Vector3((i % width), 0, (i / width));
                    numFood--;
               }
          }
          //Debug.Log("grid.Count: " + grid.Count);
          //Debug.Log("grid.Capacity: " + grid.Capacity);

          //makes the buttons usable
          roundButton.interactable = true;
          newButton.interactable = true;


     }

     //plays a new round as long as the game isnt over
     public void playRound()
     {
          roundButton.interactable = false;
          newButton.interactable = false;

          if (status != Status.OVER)
               StartCoroutine(Round());

          else
               newButton.interactable = true;



     }

     //returns amount of resources
     int GetNumResources()
     {
          int pieces = 0;
          for (int i = 0; i < grid.Count; i++)
          {
               if (grid[i] != null && (grid[i].GetComponent<Piece>().GetType() ==
                    PieceType.FOOD || grid[i].GetComponent<Piece>().GetType() ==
                    PieceType.ADVANTAGE))
                    pieces++;
          }
          return pieces;
     }

     //returns amount of agents
     int GetNumAgents()
     {
          int pieces = 0;
          for (int i = 0; i < grid.Count; i++)
          {
               if (grid[i] != null && (grid[i].GetComponent<Piece>().GetType() ==
                    PieceType.SIMPLE || grid[i].GetComponent<Piece>().GetType() ==
                    PieceType.STRATEGIC))
                    pieces++;
          }
          return pieces;
     }

     //takes the current coordinates and reurns its surroundings
     public Surroundings GetSurroundings(int posX, int posZ)
     {
          Surroundings temp;
          temp.array = new PieceType[9];
          for (int i = 0, y = -1; y < 2; y++)
          {
               for (int x = -1; x < 2; x++, i++)
               {
                    //the space is inaccessible if it is beyond the boundries of the board
                    if ((posX + x) < 0 || (posZ + y) < 0 || (posX + x) > (width - 1) ||
                         (posZ + y) > (height - 1))
                         temp.array[i] = PieceType.INACCESSIBLE;
                    else if (y == 0 && x == 0)
                         temp.array[i] = PieceType.SELF;
                    else if (grid[(posZ + y) * width + (posX + x)] == null)
                         temp.array[i] = PieceType.EMPTY;
                    else
                         temp.array[i] = grid[(posZ + y) * width + (posX + x)].
                              GetComponent<Piece>().GetType();
               }
          }
          //temp.array[9] = PieceType.INACCESSIBLE;
          return temp;
     }

     //takes the pieces action and coordinates and returns if the piece is able to take that action
     bool IsLegal(ref ActionType action, int posX, int posZ)
     {
          Surroundings surr = GetSurroundings(posX, posZ);
          int direction = (int)action;
          
          if (surr.array[direction] != PieceType.INACCESSIBLE)
               return true;
          return false;
     }

     //takes the piece and its action and returns its new coordinates
     Vector3 move(Transform piece, ActionType ac)
     {
          Vector3 p = new Vector3(piece.localPosition.x, 0, piece.localPosition.z);
          switch (ac)
          {
               case ActionType.N:
                    p.z += 1;
                    break;
               case ActionType.NE:
                    p.z += 1;
                    p.x += 1;
                    break;
               case ActionType.NW:
                    p.z += 1;
                    p.x -= 1;
                    break;
               case ActionType.E:
                    p.x += 1;
                    break;
               case ActionType.W:
                    p.x -= 1;
                    break;
               case ActionType.SE:
                    p.z -= 1;
                    p.x += 1;
                    break;
               case ActionType.SW:
                    p.z -= 1;
                    p.x -= 1;
                    break;
               case ActionType.S:
                    p.z -= 1;
                    break;
          }
          return p;
     }

     //uses an enumerator so that all the pieces dont move on the screen at the same time
     //plays a round for each piece
     private IEnumerator Round()
     {
          roundNum++;
          ActionType direction;
          Vector3 newPos = new Vector3(0, 0, 0);

          //changes status
          if (status == Status.NOT_STARTED)
               status = Status.PLAYING;
          //goes through each piece
          for (int i = 0; i < grid.Count; i++)
          {
               bool defeated = false;
               bool win = false;
               //not an empty space
               if(grid[i] != null)
               {
                    //only need to call it once
                    Piece current = grid[i].GetComponent<Piece>();

                    //hasnt taken a turn yet
                    if (!current.turned)
                    {
                         //gets the pieces surroundings and decides on a direction to move
                         direction = current.TakeTurn(GetSurroundings
                              ((int)grid[i].transform.localPosition.x, 
                              (int)grid[i].transform.localPosition.z));
                         current.turned = true;
                         //if the piece can move in its intended direction then
                         //the new position will be stored in newPos
                         if (IsLegal(ref direction, (int)grid[i].transform.localPosition.x,
                              (int)grid[i].transform.localPosition.z))
                         {
                              newPos = move(grid[i], direction);
                         }
                         //new position isnt empty and the piece is moving
                         if (grid[(int)newPos.z * width + (int)newPos.x] != null && 
                              direction != ActionType.STAY)
                         {
                              //interacts with an agent
                              if (grid[(int)newPos.z * width + (int)newPos.x].GetComponent<Agent>())
                              {
                                   current.Interact(grid[(int)newPos.z *
                                        width + (int)newPos.x].GetComponent<Agent>());
                              }
                              //interacts with a resource
                              else if (grid[(int)newPos.z * width + (int)newPos.x].GetComponent<Resource>())
                              {
                                   current.Interact(grid[(int)newPos.z *
                                        width + (int)newPos.x].GetComponent<Resource>());
                              }
                              //removes the current piece if it was killed
                              if (!current.IsViable())
                              {
                                   Destroy(grid[i].gameObject);
                                   grid[i] = null;
                                   defeated = true;
                                   //yield return new WaitForSeconds(0.5f);
                              }
                              //removes the piece that was interacted with, if it was killed
                              if (!grid[(int)newPos.z * width + (int)newPos.x].
                                   GetComponent<Piece>().IsViable())
                              {
                                   Destroy(grid[(int)newPos.z * width + (int)newPos.x].
                                        gameObject);
                                   grid[(int)newPos.z * width + (int)newPos.x] = null;
                                   win = true;
                              }
                         }
                         //redundant
                         if (defeated && win)
                         {

                              /*Destroy(grid[(int)newPos.y * width + (int)newPos.x].gameObject);
                              grid[(int)newPos.y * width + (int)newPos.x] = null;*/
                         }
                         //if the current piece wasnt destroyed it will move to the new position
                         if (!defeated)
                         {
                              grid[(int)newPos.z * width + (int)newPos.x] = grid[i];
                              
                         }
                         //moves the gameObject of the current piece, if it were to move
                         if (direction != ActionType.STAY && !defeated)
                         {
                              
                              grid[i].GetComponent<Agent>().startMovement(newPos);
                              //Destroy(grid[i].gameObject);
                              grid[i] = null;
                              yield return new WaitForSeconds(1.2f);
                         }
                         

                    }

               }
          }

          //resets each piece, cleans up any dead ones, and ages them
          for (int i = 0; i < grid.Count; i++)
          {
               if (grid[i] != null)
               {
                    grid[i].GetComponent<Piece>().Age();
                    grid[i].GetComponent<Piece>().turned = false;
                    if (!grid[i].GetComponent<Piece>().IsViable())
                    {
                         Destroy(grid[i].gameObject);
                         grid[i] = null;
                    }

               }
          }

          //the game is over if there are no more resources or agents
          if (GetNumResources() == 0 || GetNumAgents() == 0)
          {
               status = Status.OVER;
          }

          roundButton.interactable = true;
          newButton.interactable = true;

     }
}
