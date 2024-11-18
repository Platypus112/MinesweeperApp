﻿using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MersenneTwister;

namespace MinesweeperApp.Models
{
    public class Game
    {
        private readonly Tile[,] Board;
        private int UnVailedTiles;
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }   
        public bool IsPlaying { get; private set; }
        public bool? HasWon { get; private set; }

        public int Bombs {  get; private set; }
        private readonly int OBombs;
        
        public Game(int width,int height,int Bombs_,int? xPos,int? yPos)
        {
            StartTime=DateTime.Now;
            EndTime=DateTime.Now;
            IsPlaying = true;
            Board = new Tile[width,height];
            Bombs= Bombs_;
            OBombs = Bombs_;
            UnVailedTiles = width * height;
            FillBoard(xPos,yPos);
        }
        
        public void FillBoard(int? xPos,int? yPos)
        {
            if (Board == null) return;
            List<int> heights=new List<int>();
            List<int> widths=new List<int>();
            for (int i = 0; i < Bombs; i++)//getting the locarions of bombs
            {
                int width = MersenneTwister.Randoms.Next(Board.GetLength(0)-3);
                int height = MersenneTwister.Randoms.Next(Board.GetLength(1)-3);
                bool within = false;
                if (xPos != null && yPos != null)
                {
                    if (width >= xPos - 1) width += 3;
                    if (height >= yPos - 1) height += 3;
                    within= (width>=xPos-1&&width<=xPos+1)&&(height>=yPos-1&&height<=yPos+1);
                }
                bool exists = false;
                if((widths.Contains(width) && heights.Contains(height)))
                {
                    exists = width == widths[heights.IndexOf(height)];
                }
                if (exists||within) i--;
                else
                {
                    widths.Add(width);
                    heights.Add(height);
                }
            }
            for(int i = 0; i < Board.GetLength(0); i++)//width
            {
                for(int j = 0; j < Board.GetLength(1); j++)//height
                {
                    Board[i,j] = new Tile(0,i,j);
                }
            }
            for(int i = 0;i < heights.Count; i++)//filling an empty board with tiles with bombs
            {
                Board[widths[i], heights[i]] = new Tile(-1, widths[i], heights[i]);
                for (int k = -1; k <= 1; k++)
                {
                    for( int j = -1;j <= 1; j++)
                    {
                        if (k!=0|| j!=0)
                        {
                            bool withinBounds = 
                                (widths[i] + k >= 0 && widths[i] + k < Board.GetLength(0))//checks if the k value gives a tile that exists in the board array
                                &&
                                (heights[i] + j >= 0 && heights[i] + j < Board.GetLength(1));//checks if the j value gives a tile that exists in the board array
                            if (withinBounds) 
                            {
                                if(Board[widths[i] + k, heights[i] + j].Value!=-1)Board[widths[i] + k, heights[i] + j].AddBomb();
                            }
                        }
                    }
                }
            }

        }
        public async Task<bool> EasyDig(int x, int y)
        {
            if ((x < 0 || y < 0 || x >= Board.GetLength(0) || y >= Board.GetLength(1)) || Board[x, y] == null || !Board[x, y].Unvailed)
            {
                Console.WriteLine("illegal move");
                return false;
            }
            if (Board[x, y].Flagged) return false;
            if(!(await CountAdjBy(x,y, (xPos, yPos) => Board[xPos, yPos].Flagged) >= Board[x,y].Value)) return false;
            if (await CountAdjBy(x, y, (xPos, yPos) => UnvailTile(xPos, yPos).Result) > 0) return true;
            return false;
            
        }
        public async Task<bool> UnvailTile(int x,int y)//returns true when the game is done
        {
            if ((x < 0 || y < 0 || x >= Board.GetLength(0) || y >= Board.GetLength(1))|| Board[x, y] == null || Board[x, y].Unvailed)
            {
                Console.WriteLine("illegal move");
                return false;
            }
            if (Board[x,y].Flagged)return false;
            //if (Board[x, y].Unvailed) 
            //{
                
            //    if (Board[x, y].Value != 0 && Board[x, y].FlagCount == Board[x,y].Value)
            //    {
            //        for (int k = -1; k <= 1; k++)
            //        {
            //            for (int j = -1; j <= 1; j++)
            //            {

            //                if (k != 0 || j != 0)
            //                {
            //                    bool withinBounds =
            //                        (x + k >= 0 && x + k < Board.GetLength(0))//checks if the k value gives a tile that exists in the board array
            //                        &&
            //                        (y + j >= 0 && y + j < Board.GetLength(1));//checks if the j value gives a tile that exists in the board array
            //                    if (withinBounds)
            //                    {
            //                        if (Board[x + k, y + j].Value == 0)
            //                        {
            //                            await UnvailTile(x + k, y + j);
            //                        }
            //                        else
            //                        {
            //                            if (!Board[x + k, y + j].Unvailed) UnVailedTiles--;
            //                            if (!await Board[x + k, y + j].Dig())
            //                            {
            //                                GameLost();
            //                                return true;
            //                            }
            //                            else if (UnVailedTiles <= OBombs)
            //                            {
            //                                GameWon();
            //                                return true;
            //                            }
            //                        }
            //                        //await Task.Delay(20);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    EndTime = DateTime.Now;
            //    if (UnVailedTiles == OBombs)
            //    {
            //        GameWon();
            //        return true;
            //    }
            //    return false;
            //}
            UnVailedTiles--;
            if(!await Board[x, y].Dig())
            {
                GameLost();
                return true;
            }
            if (Board[x, y].Value == 0)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int j = -1; j <= 1; j++)
                    {

                        if (k != 0 || j != 0)
                        {
                            bool withinBounds =
                                (x + k >= 0 && x + k < Board.GetLength(0))//checks if the k value gives a tile th
                                                                          //at exists in the board array
                                &&
                                (y + j >= 0 && y + j < Board.GetLength(1));//checks if the j value gives a tile that exists in the board array
                            if (withinBounds)
                            {
                                await UnvailTile(x + k, y + j);
                            }
                            //await Task.Delay(20);
                        }
                    }
                }
            }
            EndTime = DateTime.Now;
            if (UnVailedTiles == OBombs)
            {
                GameWon();
                return true;
            }
            return false;
        }
        public void FlagTile(int x, int y)
        {
            if ((x < 0 || y < 0 || x >= Board.GetLength(0) || y >= Board.GetLength(1)) || Board[x, y] == null)
            {
                Console.WriteLine("illegal move");
                return;
            }
            if (Board[x, y].Unvailed) return;
            if (Board[x, y].Flag())
            {
                Bombs--;
                CountAdjBy(x, y, (int X, int Y) => { Board[X, Y].AddFlag(); return true; });
            }
            else
            {
                Bombs++;
                CountAdjBy(x, y, (int X, int Y) => { Board[X, Y].RemoveFlag(); return true; });
            }
            EndTime = DateTime.Now;
        }
        private Task<int> CountAdjBy(int x, int y, Func<int, int, bool> func)
        {
            int count = 0;
            for (int k = -1; k <= 1; k++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (k != 0 || j != 0)
                    {
                        bool withinBounds =
                                (x + k >= 0 && x + k < Board.GetLength(0))//checks if the k value gives a tile that exists in the board array
                                &&
                                (y + j >= 0 && y + j < Board.GetLength(1));//checks if the j value gives a tile that exists in the board array
                        if (withinBounds&&func(x+k, y+j)) count++;
                    }
                }
            }
            return Task<int>.FromResult(count);
        }
        public void GameLost()
        {
            for (int i = 0; i < Board.GetLength(0); i++)//width
            {
                for (int j = 0; j < Board.GetLength(1); j++)//height
                {
                    Board[i, j].Dig();
                }
            }
            Bombs = 0;
            EndTime = DateTime.Now;
            HasWon = false;
            IsPlaying = false;
        }
        public void GameWon()
        {
            EndTime = DateTime.Now;
            IsPlaying = false;
            HasWon = true;
        }
        public List<Tile> GetBoardStateList()
        {
            return Board.ToList<Tile>().ToList();
        }
        public void Play()
        {
            while(IsPlaying)
            {
                Console.Clear();
                Console.WriteLine(this);
                Console.WriteLine("what action do you wish to do\n" + "1-dig. 2-flag tile. 3-quit game");
                int input = int.Parse(Console.ReadLine());
                if (input==1)
                {
                    Console.WriteLine("write the x value of the spot you want to dig");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine("write the y value of the spot you want to dig");
                    int y = int.Parse(Console.ReadLine());
                    this.UnvailTile(x, y);
                }
                else if(input==2)
                {
                    Console.WriteLine("write the x value of the spot you want to flag");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine("write the y value of the spot you want to flag");
                    int y = int.Parse(Console.ReadLine());
                    this.FlagTile(x, y);
                }
                else if(input== 3)
                {
                    GameLost();
                }
            }
        }
        public override string ToString()
        {
            string result = string.Empty;
            result += "@: "+Bombs+"\n";
            result += "Time: " + (EndTime - StartTime ).Hours + ":" + (EndTime - StartTime ).Minutes + ":" + (EndTime - StartTime ).Seconds + "\n";
            result += "  ";
            for (int j = -1; j < Board.GetLength(1); j++)//width
            {
                if (j != -1) result += j % 10 + " ";
                for (int i = 0; i < Board.GetLength(0); i++)//height
                {
                    if (j == -1) result += (i%10)+" ";
                    else result += Board[i,j] + " ";
                }
                result += "\n";
            }
            return result;
        }
    }
}
