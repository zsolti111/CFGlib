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


        [Category("Node details")]
        [DisplayName("Method name")]
        [Description("Fully qualified name of the method that contains this node.")]
        [ReadOnly(true)]
        [XmlAttribute("method")]
        public string MethodName
        {
            get; set;
        }


        [Category("Node appearance")]
        [DisplayName("Shape (solver calls)")]
        [Description("Shape of the node determines if the node triggered a constraint solver call. If yes, the shape is an ellipse, otherwise it is a rectangle.")]
        [ReadOnly(true)]
        public NodeShape Shape
        {
            get; set;
        }

        [Category("Node details")]
        [DisplayName("Path condition")]
        [Description("Full form of the path condition that contains all constraints from the start.")]
        [ReadOnly(true)]
        [XmlAttribute("pc")]
        public string PathCondition
        {
            get; set;
        }

        [Category("Node details")]
        [DisplayName("Incremental path condition")]
        [Description("Incremental form of the path condition, compared to its parent node.")]
        [ReadOnly(true)]
        [XmlAttribute("ipc")]
        public string IncrementalPathCondition
        {
            get; set;
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
