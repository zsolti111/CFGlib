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

        [Browsable(false)]
        [ReadOnly(true)]
        public NodeColor Color
        {
            get; set;
        }

        [Category("Node details")]
        [DisplayName("Generated test code")]
        [Description("The generated source code if it is a leaf node and a test was generated (e.g., not a duplicate).")]
        [ReadOnly(true)]
        [XmlAttribute("gtc")]
        public string GenerateTestCode
        {
            get; set;
        }

        [Category("Node details")]
        [DisplayName("Node status")]
        [Description("Status of the node indicating whether there are remaining uncovered branches starting from this node.")]
        [ReadOnly(true)]
        [XmlAttribute("status")]
        public string Status
        {
            get; set;
        }

        [Category("Node details")]
        [DisplayName("Execution runs")]
        [Description("The list of executions that this node was involved in.")]
        [ReadOnly(true)]
        [XmlAttribute("runs")]
        public string Runs
        {
            get; set;
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

        [Category("Node details")]
        [DisplayName("Source code mapping")]
        [Description("Aprroximation of the place in the source code.")]
        [ReadOnly(true)]
        [XmlAttribute("scm")]
        public string SourceCodeMappingString
        {
            get; set;
        }

        [Category("Node appearance")]
        [DisplayName("Border (source mapping)")]
        [Description("Border of the node determines if the node has exact source code location mapping. If yes, the border is doubled, otherwise the border is single.")]
        [ReadOnly(true)]
        public NodeBorder Border
        {
            get; set;
        }

        private Stack<NodeColor> originalColors = new Stack<NodeColor>();

        public bool IsSelected
        {
            get; set;
        }

        public void Deselect ()
        {
            IsSelected = false;
            RevertToOriginalColor();
        }


        public void RevertToOriginalColor ()
        {
            NodeColor o;
            if (originalColors.Count == 1)
                o = originalColors.Peek();
            else
                o = originalColors.Pop();
            if (o != Color)
            {
                Color = o;
            }
        }

        public bool IsCollapsed
        {
            get; set;
        }

        public HashSet<CFGNode> CollapsedSubtreeNodes
        {
            get; private set;
        }


        public HashSet<CFGEdge> CollapsedSubtreeEdges
        {
            get; private set;
        }

        public CFGNode ( int id, Instruction footer, Instruction header, ControlFlowBlockType type, uint iLOffset, bool solverCall )
        {
            Id = id;
            Footer = footer;
            Header = header;
            Type = type;
            ILOffset = iLOffset;
            Shape = ( solverCall ) ? NodeShape.Ellipse : NodeShape.Rectangle;
        }


        public CFGNode ( int id, bool solverCall )
        {
            Id = id;
            Shape = ( solverCall ) ? NodeShape.Ellipse : NodeShape.Rectangle;

        }


        public override string ToString ()
        {
            return Id.ToString();
        }

        public static CFGNode Factory ( string id )
        {
            return new CFGNode(int.Parse(id), false);
        }

        public void Select ()
        {
            IsSelected = true;
            NodeColor o = originalColors.Peek();
            //else o = originalColors.Pop();
            if (o != NodeColor.Blue)
            {
                originalColors.Push(Color);
                Color = NodeColor.Blue;
            }
        }

        public void Collapse ()
        {
            NodeColor o;
            if (originalColors.Count == 1)
                o = originalColors.Peek();
            else
                o = originalColors.Pop();
            if (o != NodeColor.Indigo)
            {
                originalColors.Push(Color);
                Color = NodeColor.Indigo;
            }
        }

        public void Expand ()
        {
            if (IsSelected)
            {
                originalColors.Pop();
            }
            RevertToOriginalColor();
        }


    }

}
