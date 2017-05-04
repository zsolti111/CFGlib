using dnlib.DotNet;
using dnlib.DotNet.Emit;
using QuickGraph;
using QuickGraph.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CFGlib
{
    public class CFGCreator
    {


        // Lista melyben a cúcsokat tároljuk
        public static List<CFGNode> nodes = new List<CFGNode>();

        // Lista melyben az éleket tároljuk
        public static List<CFGEdge> edges = new List<CFGEdge>();
        // gráf
        public static SoftMutableBidirectionalGraph<CFGNode, CFGEdge> graph = new SoftMutableBidirectionalGraph<CFGNode, CFGEdge>();

        public static int edgeId = 0;




        public static void Serialize ( SoftMutableBidirectionalGraph<CFGNode, CFGEdge> graph, string path )
        {

            var ser = new GraphMLSerializer<CFGNode, CFGEdge, SoftMutableBidirectionalGraph<CFGNode, CFGEdge>>();
            using (var writer = XmlWriter.Create(path + "temp.graphml", new XmlWriterSettings { Indent = true, WriteEndDocumentOnClose = false }))
            {
                ser.Serialize(writer, graph, v => v.Id.ToString(), e => e.Id.ToString());
            }
        }

        public static SoftMutableBidirectionalGraph<CFGNode, CFGEdge> Deserialize ( string path )
        {
            var deser = new GraphMLDeserializer<CFGNode, CFGEdge, SoftMutableBidirectionalGraph<CFGNode, CFGEdge>>();
            var graph = new SoftMutableBidirectionalGraph<CFGNode, CFGEdge>();
            var ivf = new IdentifiableVertexFactory<CFGNode>(CFGNode.Factory);
            var ief = new IdentifiableEdgeFactory<CFGNode, CFGEdge>(CFGEdge.Factory);
            using (var reader = XmlReader.Create(path))
            {
                deser.Deserialize(reader, graph, ivf, ief);
            }
            ReplaceLineBreaks(graph, false);
            return graph;
        }

        private static void ReplaceLineBreaks ( SoftMutableBidirectionalGraph<CFGNode, CFGEdge> graph, bool serialize )
        {
            if (serialize)
            {
                foreach (var v in graph.Vertices)
                {
                    v.PathCondition = v.PathCondition.Replace(Environment.NewLine, "[LB]");
                    v.IncrementalPathCondition = v.IncrementalPathCondition.Replace(Environment.NewLine, "[LB]");
                }
            }
            else
            {
                foreach (var v in graph.Vertices)
                {
                    v.PathCondition = v.PathCondition.Replace("[LB]", Environment.NewLine);
                    v.IncrementalPathCondition = v.IncrementalPathCondition.Replace("[LB]", Environment.NewLine);
                }
            }
        }



        public void Create ( string path, string functionName )
        {


            string filename = path;


            ModuleDefMD mod = ModuleDefMD.Load(filename);



            foreach (TypeDef type in mod.GetTypes())
            {

                //// Type-on belül methodok ////

                //type.Methods.Where(m => m.Name == functionName).FirstOrDefault();

                foreach (var method in type.Methods)
                {

                    // -----------------------------------
                    // A Foo-ra szedünk ki értékeket csak
                    // -----------------------------------

                    if (method.Name == functionName)
                    {


                        ControlFlowGraph graph = ControlFlowGraph.Construct(method.Body);



                        // Először feltöltjük a csúcsokat
                        // propertyk tárolása footer, header type

                        foreach (var block in graph.GetAllBlocks())
                        {
                            nodes.Add(new CFGNode(block.Id, block.Footer, block.Header, block.Type, block.Footer.Offset, false));

                        }



                        // Kiíratás + élek feltöltése
                        foreach (var block in graph.GetAllBlocks())
                        {

                            foreach (var item in block.Sources)
                            {

                            }

                            // Az adott blokk forrás blokkja

                            foreach (var source in block.Sources)
                            {

                            }

                            // Az adott blokk cél blokkja
                            // itt hozunk létre élt 
                            foreach (var target in block.Targets)
                            {
                                var tempEdge = new CFGEdge(edgeId, nodes.Where(x => x.Id == block.Id).FirstOrDefault(), nodes.Where(x => x.Id == target.Id).FirstOrDefault());
                                edgeId++;
                                edges.Add(tempEdge);
                            }


                        }

                    }
                }
            }

            // A gráfhoz hozzáadjuk a csúcsokat
            graph.AddVertexRange(nodes);
            graph.AddEdgeRange(edges);
            var temppath = @"C:\Users\Zsolti\Desktop\önlab";
            Serialize(graph, temppath);

        }
    }
}
