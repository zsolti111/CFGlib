using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace CFGlib
{
    public class CFGEdge : Edge<CFGNode>
    {

        public int Id
        {
            get; set;
        }


        public CFGEdge ( int id, CFGNode source, CFGNode target ) : base(source, target)
        {
            Id = id;

        }

    }

}