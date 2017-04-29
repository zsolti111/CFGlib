using CFGlib;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

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

        public uint ILOffset
        {
            get; set;
        }

        public enum NodeShape
        {
            Ellipse,
            Rectangle
        }


        public enum NodeBorder
        {
            Single,
            Double
        }

        public enum NodeColor
        {
            White,
            Green,
            Orange,
            Red,
            Indigo,
            Blue,
            None
        }

        public CFGNode ( int id, Instruction footer, Instruction header, ControlFlowBlockType type, uint iLOffset )
        {
            Id = id;
            Footer = footer;
            Header = header;
            Type = type;
            ILOffset = iLOffset;
        }


        public override string ToString ()
        {
            return Id.ToString();
        }


    }

}
