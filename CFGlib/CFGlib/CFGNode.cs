using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace CFGlib
{
    public class CFGNode
    {


        public int Id
        {
            get; set;
        }



        public CFGNode ( int id )
        {
            Id = id;
        }


        public override string ToString ()
        {
            return Id.ToString();
        }


    }

}
