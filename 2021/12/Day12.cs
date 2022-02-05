using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace AdventOfCode2021
{
    // Deklariert die Klasse für die Knoten des Graphen
    class Node
    {
        public int index;
        public string value = "";
        public List<Node> adjacentNodes = new List<Node>(); // Liste der Nachbarknoten
    }

    // Deklariert die Klasse für den gerichteten Graphen
    class DirectedGraph
    {
        // Diese Methode verbindet die Knoten startNode und targetNode miteinander.
        public void ConnectNodes(Node startNode, Node targetNode)
        {
            startNode.adjacentNodes.Add(targetNode);
        }
    }

    class Day12 : TaskBase
    {
        int PathCount { get; set; } = 0;

        Dictionary<string, string[]> Connections = new Dictionary<string, string[]>();

        // Diese Methode gibt die Liste der Knoten in der Form A, B, C, ... als Text zurück.
        public string ToString(List<Node> traversedNodes)
        {
            string text = "";
            for (int i = 0; i < traversedNodes.Count; i++) // for-Schleife, die die Knoten durchläuft
            {
                text += traversedNodes[i].value + ", ";
            }
            text = text.Substring(0, text.Length - 2);
            return text;
        }

        // Diese Methode durchläuft die Knoten des gerichteten Graphen mit einer Tiefensuche
        public int DepthFirstSearch(Node startNode, List<Node> traversedNodes)
        {
            if (startNode.value == "end")
            {
                return 1;
            }

            var res = 0;
            foreach (Node node in startNode.adjacentNodes) // foreach-Schleife, die alle benachbarten Knoten des Knotens durchläuft
            {
                var isBigCave = node.value.ToUpper() == node.value;
                var seen = traversedNodes.Contains(node);
                if (!seen || isBigCave)
                {
                    if (!isBigCave) traversedNodes.Add(node);

                    res += DepthFirstSearch(node, traversedNodes); // Rekursiver Aufruf der Methode mit dem Nachbarknoten als Startknoten
                }
            }
            return res;
        }
        public List<Node> BreadthFirstSearch(Node startNode)
        {
            List<Node> traversedNodes = new List<Node>(); // Liste der Knoten für die Breitensuche
            traversedNodes.Add(startNode); // Fügt den Startenknoten der Liste hinzu
            HashSet<Node> visitedNodes = new HashSet<Node>(); // Menge der markierten Knoten
            visitedNodes.Add(startNode); // Fügt den Startenknoten der Menge der markierten Knoten hinzu
            LinkedList<Node> queue = new LinkedList<Node>(); // Warteschlange für die Breitensuche
            queue.AddLast(startNode); // Fügt den Startenknoten der Warteschlange hinzu
            while (queue.Count != 0) // So lange die Warteschlange nicht leer ist
            {
                startNode = queue.First.Value; // Erster Knoten der Warteschlange
                queue.RemoveFirst(); // Entfernt den ersten Knoten aus der Warteschlange
                foreach (Node node in startNode.adjacentNodes) // foreach-Schleife, die alle benachbarten Knoten des Knotens durchläuft
                {
                    if (!visitedNodes.Contains(node)) // Wenn der Knoten noch nicht markiert wurde
                    {
                        traversedNodes.Add(node); // Fügt den Knoten der Liste hinzu
                        visitedNodes.Add(node); // Markiert den Knoten
                        queue.AddLast(node); // Fügt den Knoten der Warteschlange für die Breitensuche hinzu
                    }
                }
            }
            return traversedNodes; // Gibt die Liste der Knoten zurück
        }
        public Day12(bool demo = false)
        {
            this.Demo = demo;
            base.ReadInput(12, Demo);
        }

        internal void GetResults()
        {
            GetResultPart1();
            GetResultPart2();
        }

        private new void GetResultPart1()
        {
            List<Node> nodes = new List<Node>();
            DirectedGraph directedGraph = new DirectedGraph();
            foreach (string input in InputAsString)
            {
                var caves = input.Split("-");
                var caveA = caves[0];
                var caveB = caves[1];

                var nodeA = nodes.Where(x => x.value == caveA).FirstOrDefault();

                if (nodeA == null)
                {
                    nodeA = new Node { index = nodes.Count(), value = caveA };
                    nodes.Add(nodeA);
                }

                var nodeB = nodes.Where(x => x.value == caveB).FirstOrDefault();
                if (nodeB == null)
                {
                    nodeB = new Node { index = nodes.Count(), value = caveB };
                    nodes.Add(nodeB);
                }

                directedGraph.ConnectNodes(nodeA, nodeB);
                // directedGraph.ConnectNodes(nodeB, nodeA);

            }

            //  List<Node> traversedNodes = new List<Node>(); // Liste der Knoten für die Tiefensuche
            var startNodes = nodes.Where(x => x.value == "start");

            foreach (var item in startNodes)
            {
                List<Node> traversedNodes = new List<Node>();
                PathCount += DepthFirstSearch(item, traversedNodes); // Aufruf der Methode
            }

            //List<Node> traversedNodes2 = BreadthFirstSearch(nodes.Where(x => x.value == "start").First()); // Aufruf der Methode
            // Console.WriteLine(ToString(traversedNodes)); // Ausgabe auf der Konsole

            var answer = PathCount;
            Log.Information("Part 1 answer: {0}", answer);
            var expectedResult = Demo ? 19 : 0;
            Assert(answer, expectedResult);
        }
        private new void GetResultPart2()
        {
            var answer = 0;
            Log.Information("Part 2 answer: {0}", answer);
            var expectedResult = Demo ? 1 : 0;
            Assert(answer, expectedResult);
        }
    }
}
