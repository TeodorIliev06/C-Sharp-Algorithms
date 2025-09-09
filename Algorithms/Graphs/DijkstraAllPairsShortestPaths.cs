/***
 * Computes Dijkstra's shortest paths for all pairs of vertices in a graph. 
 * This is a wrapper class that applies single-source dijkstra shortest paths (DijkstraShortestPaths<TG, TV>) 
 * to all vertices of a graph and saves the results in a dictionary index by the vertices.
 */

namespace Algorithms.Graphs
{
    using System;
    using System.Collections.Generic;

    using DataStructures.Graphs;

    public class DijkstraAllPairsShortestPaths<TGraph, TVertex>
        where TGraph : IGraph<TVertex>, IWeightedGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        readonly Dictionary<TVertex, DijkstraShortestPaths<TGraph, TVertex>> _allPairsDijkstra;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public DijkstraAllPairsShortestPaths(TGraph Graph)
        {
            if (Graph == null)
                throw new ArgumentNullException();

            // Initialize the all pairs dictionary
            _allPairsDijkstra = new Dictionary<TVertex, DijkstraShortestPaths<TGraph, TVertex>>();

            var vertices = Graph.Vertices;

            foreach (var vertex in vertices)
            {
                var dijkstra = new DijkstraShortestPaths<TGraph, TVertex>(Graph, vertex);
                _allPairsDijkstra.Add(vertex, dijkstra);
            }
        }


        /// <summary>
        /// Determines whether there is a path from source vertex to destination vertex.
        /// </summary>
        public bool HasPath(TVertex source, TVertex destination)
        {
            if (!_allPairsDijkstra.ContainsKey(source) || !_allPairsDijkstra.ContainsKey(destination))
                throw new Exception("Either one of the vertices or both of them don't belong to Graph.");

            return _allPairsDijkstra[source].HasPathTo(destination);
        }

        /// <summary>
        /// Returns the distance between source vertex and destination vertex.
        /// </summary>
        public long PathDistance(TVertex source, TVertex destination)
        {
            if (!_allPairsDijkstra.ContainsKey(source) || !_allPairsDijkstra.ContainsKey(destination))
                throw new Exception("Either one of the vertices or both of them don't belong to Graph.");

            return _allPairsDijkstra[source].DistanceTo(destination);
        }

        /// <summary>
        /// Returns an enumerable collection of nodes that specify the shortest path from source vertex to destination vertex.
        /// </summary>
        public IEnumerable<TVertex> ShortestPath(TVertex source, TVertex destination)
        {
            if (!_allPairsDijkstra.ContainsKey(source) || !_allPairsDijkstra.ContainsKey(destination))
                throw new Exception("Either one of the vertices or both of them don't belong to Graph.");

            return _allPairsDijkstra[source].ShortestPathTo(destination);
        }

    }

}
