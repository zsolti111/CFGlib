using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace CFGlib
{
    public class CFGEdge : Edge<CFGNode>
    {
        public enum EdgeColor
        {
            Black,
            Red,
            Green
        }
        public int Id
        {
            get; set;
        }

        public EdgeColor Color
        {
            get; set;
        }


        public CFGEdge ( int id, CFGNode source, CFGNode target ) : base(source, target)
        {
            Id = id;

        }


    }

}