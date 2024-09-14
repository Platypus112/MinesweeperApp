using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class Tile
    {
        public int Value { get; private set; }
        public bool Unvailed { get; private set; }//-1 is bomb
        public bool Flagged {  get; private set; }
        public int? x { get; private set; }
        public int? y { get; private set; }
        public Tile(int Value_)
        {
            Value = Value_;
            Unvailed = false;
            Flagged = false;
        }
        public Tile(int Value_,bool Unvailed_) : this(Value_)
        {
            Unvailed = Unvailed_;

        }
        public Tile(int Value_, int x_,int y_) : this(Value_)
        {
            x = x_;
            y = y_;
        }
        public bool Dig()//returns true when the move doesn't kill you
        {
            if (Unvailed||Flagged) return true;
            Unvailed = true;
            if (Value == -1) return false;
            return true;
        }
        public bool Flag()//if the tile is flagged by this action this will return true
        {
            if (Flagged) Flagged = false;
            else Flagged = true;
            return Flagged;
        }
        
        public void AddBomb()
        {
            Value+=1;
        }
        public override string ToString()
        {
            if (Flagged) return "@";
            if (Unvailed)
            {
                if(Value!=-1) return Value.ToString();
                return "*";
            }
            return "x";
        }
        //public Tile[] Adjacants { get; private set; }//adjacant tiles
        //public int Value { get;set; }//goes from 0-9, -1 value is a bomb
        //public bool Vailed {  get; set; }//if the tile is vailed this will be true

        //public Tile()
        //{
        //    Adjacants = new Tile[8];
        //}
        //public Tile(int value,bool vailed)
        //{
        //    Adjacants = new Tile[8];
        //    this.Value = value;
        //    this.Vailed = vailed;
        //}
        ///// <summary>
        ///// produces a tile with a given value and a place near the given title
        ///// </summary>
        ///// <param name="tile">an adjacant tile</param>
        ///// <param name="val">the value of the new tile</param>
        ///// <param name="place">the place in which the given tile will be placed in the new tile</param>
        //public Tile(Tile tile,int place)
        //{
        //    this.Vailed = true;
        //    this.Adjacants= new Tile[8];
        //    LinkTile(place,tile);
        //}
        //public Tile(int height,int width,int bombs)
        //{
        //    this.Adjacants = new Tile[8];
        //    List<int> bombNum = bombNums(height,width,bombs);
        //    Tile left = this;
        //    for(int i = 0;i<height;i++)
        //    {
        //        Tile t = left;
        //        for(int j = 0; j < width; j++)
        //        {
        //            if (bombNum.Count>0&&bombNum[0] == i*width +j) t.Value = -1;
        //            else t.Value = 0;
        //            t.Vailed= true;
        //            if (bombNum.Count > 0)bombNum.RemoveAt(0);
        //            t.Adjacants[2] = new Tile(t,6);
        //            t = t.Adjacants[2];
        //        }
        //        left.Adjacants[4] = new Tile(left, 0);
        //        left = left.Adjacants[4];
        //    }

        //}
        //private List<int> bombNums(int height, int width, int bombs)
        //{
        //    List<int> result = new List<int>();
        //    for(int i=0;i<bombs;i++)
        //    {
        //        Random rnd = new Random();
        //        int add= rnd.Next(0,height*width);
        //        while (result.Any(x => x==add)) add = rnd.Next(0, height * width);
        //        result.Add(add);
        //    }
        //    result.OrderDescending();
        //    return result;
        //}
        //private void LinkTile(int place,Tile? t)
        //{
        //    if (t == null) return;
        //    this.Adjacants[place] = t;
        //    t.Adjacants[FlipPlace(place)] = this;
        //    Tile nextTile=null;
        //    switch (place)
        //    {
        //        case 0: { if (t.Adjacants[2] != null) nextTile = t.Adjacants[2]; break; }
        //        case 1: { if (t.Adjacants[4] != null) nextTile = t.Adjacants[4]; break; }
        //        case 2: { if (t.Adjacants[4] != null) nextTile = t.Adjacants[4]; break; }
        //        case 3: { if (t.Adjacants[6] != null) nextTile = t.Adjacants[6]; break; }
        //        case 4: { if (t.Adjacants[6] != null) nextTile = t.Adjacants[6]; break; }
        //        case 5: { if (t.Adjacants[0] != null) nextTile = t.Adjacants[0]; break; }
        //        case 6: { if (t.Adjacants[0] != null) nextTile = t.Adjacants[0]; break; }
        //        case 7: { if (t.Adjacants[2] != null) nextTile = t.Adjacants[2]; break; }
        //    }
        //    LinkTile((place+1)%8, nextTile);
        //}
        //private int FlipPlace(int place)
        //{

        //    return (place+4)%8;
        //}
        //public override string ToString()
        //{
        //    Tile t = this;
        //    while(t!=null&&(t.Adjacants[1] != null && t.Adjacants[2] != null && t.Adjacants[3] != null && t.Adjacants[4] != null && t.Adjacants[5] != null))
        //    {
        //        t = t.Adjacants[6];
        //    }
        //    while (t != null && (t.Adjacants[7] != null && t.Adjacants[0] != null && t.Adjacants[1] != null && t.Adjacants[2] != null && t.Adjacants[3] != null))
        //    {
        //        t = t.Adjacants[0];
        //    }
        //    string result = "";
        //    Tile left = t;
        //    while (left != null)
        //    {
        //        while (t != null)
        //        {
        //            result += t.Value + " ";
        //            t = t.Adjacants[2];
        //        }
        //        result+= "\n";
        //        left = left.Adjacants[4];
        //        t = left;
        //    }
        //    return result;
        //}
    }
}
