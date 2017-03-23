using CFGlib;
using dnlib.DotNet.Emit;
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

        public Instruction Footer
        {
            get; set;
        }

        public Instruction Header
        {
            get; set;
        }

        public ControlFlowBlockType Type
        {
            get; set;
        }

        public CFGNode ( int id, Instruction footer, Instruction header, ControlFlowBlockType type )
        {
            Id = id;
            Footer = footer;
            Header = header;
            Type = type;
        }


        public override string ToString ()
        {
            return Id.ToString();
        }


    }

}
