using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practic
{
    public class Graphs
    {
        public Graph gr;
        public List<Anomaly> anomalyList;
        int counter_for_levels;
        private int counter_for_weights;
        public void ownGraph(int countOfLevels)
        {
            gr = new Graph();
            gr.levels = countOfLevels;
            gr.countLevels = new List<int> { };
            gr.knots = new List<Knot> { };
            gr.arcs = new List<Arc> { };
            counter_for_levels = 0;
            counter_for_weights = 0;
        }
        public int addLevel(int countOnLevel)
        {
            gr.countLevels.Add(countOnLevel);
            counter_for_levels++;
            return counter_for_levels;
        }
        public void addKnots()
        {
            int n = 0;
            for (int i = 0; i < gr.levels; i++)
            {
                for (int j = 0; j < gr.countLevels[i]; j++)
                {
                    gr.knots.Add(new Knot(n, i));
                    n++;
                }
            }
        }
        public void addArc(int outKN, int inKN)
        {
            gr.arcs.Add(new Arc(gr.knots[outKN], gr.knots[inKN]));
        }
        public void fillPriorityList_L()
        {
            gr.L_priorityList = new List<int>();
            for (int i = 0; i < gr.knots.Count; i++)
            {
                gr.L_priorityList.Add(i);
            }
        }
        public void changePositionInListL(int knot_num, int position)
        {
            gr.L_priorityList.Remove(knot_num);
            gr.L_priorityList.Insert(position, knot_num);
        }
        public int addWeight(int weight)
        {
            gr.knots[counter_for_weights].weight = weight;
            counter_for_weights++;
            return counter_for_weights;
        }
        public void findAnomalies(Graph gr)
        {
            gr.fillMatrixOfAdjacency();
            gr.findOrder();

            anomalyList = new List<Anomaly> { };
            Graph graph = gr.Clone();
            int minWeight = 100;
            string comment = "";

            for (int i = 0; i < graph.knots.Count; i++)
            {
                if (minWeight > graph.knots[i].weight)
                {
                    minWeight = graph.knots[i].weight;
                }          
            }
            minWeight -= 1;
            graph.fillMatrixOfAdjacency();            
            for (int i = 0; minWeight > 0; minWeight--, i++)
            { 
                for (int j = 0; j < graph.knots.Count; j++)
                {
                    graph.knots[j].weight--;
                }
                graph.knotsAreNotProceed();
                graph.findOrder();
                if (graph.l > gr.l)
                {
                    comment = "Вага вершин зменшилась на " + (i + 1);
                    anomalyList.Add(new Anomaly(graph.Clone(), 3, graph.l - gr.l, comment));
                }
            }
            graph = gr.Clone();
            graph.fillMatrixOfAdjacency();
            for (int i = 0; graph.h < graph.knots.Count; i++)
            {
                graph.h++;
                graph.knotsAreNotProceed();
                graph.findOrder();
                if (graph.l > gr.l)
                {
                    comment = "Кількість процесорів збільшилась на " + (i + 1);
                    anomalyList.Add(new Anomaly(graph.Clone(), 4, graph.l - gr.l, comment));
                }
            }
            List<int> arcsForRemove = new List<int>();
            if (gr.arcs.Count != 0)
            {
                arcsForRemove.Add(0);                

                while(!AllCombinationsAreProcessed(arcsForRemove, gr.arcs.Count))
                {
                    graph = gr.Clone();
                    graph.knotsAreNotProceed();
                    arcsForRemove = findNextComb(arcsForRemove, gr.arcs.Count);
                    if (arcsForRemove.Count <= gr.arcs.Count)
                    {
                        for (int i = arcsForRemove.Count - 1; i >= 0; i--)
                        {
                            graph.arcs.RemoveAt(arcsForRemove[i]);
                        }
                        //graph.arcs.RemoveAt(i);

                        graph.fillMatrixOfAdjacency();
                        graph.findOrder();

                        if (graph.l > gr.l)
                        {
                            comment = "Було видалено дуги: ";  //gr.arcs[i].knotOut.number + 1) + " у вершину " + (gr.arcs[i].knotIn.number + 1);
                            for (int i = 0; i < arcsForRemove.Count; i++)
                            {
                                comment += " (" + (gr.arcs[arcsForRemove[i]].knotOut.number + 1) + ";" + (gr.arcs[arcsForRemove[i]].knotIn.number + 1) + "),";
                            }
                            comment = comment.Remove(comment.Length - 1);
                            anomalyList.Add(new Anomaly(graph.Clone(), 2, graph.l - gr.l, comment));
                        }
                    }
                }
            }
        }
        bool AllCombinationsAreProcessed(List<int> list, int count)
        {
            bool allProcessed = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != (count - list.Count + (i + 1)))
                {
                    allProcessed = false;
                    break;
                }
            }
            return allProcessed;
        }
        List<int> findNextComb(List<int> list, int count)
        {
            bool addNewArc = true;
            int position = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] < count - (list.Count - i))
                {
                    position = i;
                    addNewArc = false;
                    break;
                }
            }
            if (addNewArc)
            {
                list.Add(0);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = i;
                }
            }
            else
            {
                list[position]++;
                if (position != list.Count - 1)
                {
                    int counter = 1;
                    for (int i = position + 1; i < list.Count; i++)
                    {
                        list[i] = list[position] + counter;
                        counter++;
                    }
                }
            }
            return list;
        }
        public void craeteGraph1_1(Graph tr)
        {
            tr.levels = 2;
            tr.h = 2;

            tr.L_priorityList = new List<int> {0, 1, 2, 3, 4, 5};

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(4);
            tr.countLevels.Add(2);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[5]));

            tr.knots[0].weight = 2;
            tr.knots[1].weight = 4;
            tr.knots[2].weight = 3;
            tr.knots[3].weight = 1;
            tr.knots[4].weight = 5;
            tr.knots[5].weight = 10;
        }
        public void craeteGraph1_2(Graph tr)
        {
            tr.levels = 2;
            tr.h = 2;

            tr.L_priorityList = new List<int> { 0, 1, 2, 4, 3, 5 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(4);
            tr.countLevels.Add(2);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[5]));

            tr.knots[0].weight = 2;
            tr.knots[1].weight = 4;
            tr.knots[2].weight = 3;
            tr.knots[3].weight = 1;
            tr.knots[4].weight = 5;
            tr.knots[5].weight = 10;
        }
        public void craeteGraph2_1(Graph tr)
        {
            tr.levels = 2;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 4, 5 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(3);
            tr.countLevels.Add(3);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 2));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[3]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[5]));

            tr.knots[0].weight = 5;
            tr.knots[1].weight = 6;
            tr.knots[2].weight = 7;
            tr.knots[3].weight = 5;
            tr.knots[4].weight = 4;
            tr.knots[5].weight = 6;
        }
        public void craeteGraph2_2(Graph tr)
        {
            tr.levels = 2;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 4, 5 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(3);
            tr.countLevels.Add(3);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 2));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[3]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[5]));

            tr.knots[0].weight = 5;
            tr.knots[1].weight = 6;
            tr.knots[2].weight = 7;
            tr.knots[3].weight = 5;
            tr.knots[4].weight = 4;
            tr.knots[5].weight = 6;
        }
        public void craeteGraph3_1(Graph tr)
        {
            tr.levels = 2;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 4, 6, 7, 8, 9, 5 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(5);
            tr.countLevels.Add(5);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 1));
            tr.knots.Add(new Knot(5, 2));
            tr.knots.Add(new Knot(6, 2));
            tr.knots.Add(new Knot(7, 2));
            tr.knots.Add(new Knot(8, 2));
            tr.knots.Add(new Knot(9, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[4], tr.knots[7]));
            tr.arcs.Add(new Arc(tr.knots[4], tr.knots[8]));
            tr.arcs.Add(new Arc(tr.knots[4], tr.knots[9]));

            tr.knots[0].weight = 8;
            tr.knots[1].weight = 5;
            tr.knots[2].weight = 5;
            tr.knots[3].weight = 5;
            tr.knots[4].weight = 5;
            tr.knots[5].weight = 16;
            tr.knots[6].weight = 7;
            tr.knots[7].weight = 7;
            tr.knots[8].weight = 7;
            tr.knots[9].weight = 7;
        }
        public void craeteGraph3_2(Graph tr)
        {
            tr.levels = 2;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 4, 6, 7, 8, 9, 5 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(5);
            tr.countLevels.Add(5);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 1));
            tr.knots.Add(new Knot(5, 2));
            tr.knots.Add(new Knot(6, 2));
            tr.knots.Add(new Knot(7, 2));
            tr.knots.Add(new Knot(8, 2));
            tr.knots.Add(new Knot(9, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[4], tr.knots[7]));
            tr.arcs.Add(new Arc(tr.knots[4], tr.knots[8]));
            tr.arcs.Add(new Arc(tr.knots[4], tr.knots[9]));

            tr.knots[0].weight = 6;
            tr.knots[1].weight = 3;
            tr.knots[2].weight = 3;
            tr.knots[3].weight = 3;
            tr.knots[4].weight = 3;
            tr.knots[5].weight = 14;
            tr.knots[6].weight = 5;
            tr.knots[7].weight = 5;
            tr.knots[8].weight = 5;
            tr.knots[9].weight = 5;
        }
        public void craeteGraph4_1(Graph tr)
        {
            tr.levels = 2;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 5, 6, 7, 8, 4 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(4);
            tr.countLevels.Add(5);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));
            tr.knots.Add(new Knot(6, 2));
            tr.knots.Add(new Knot(7, 2));
            tr.knots.Add(new Knot(8, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[7]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[8]));


            tr.knots[0].weight = 4;
            tr.knots[1].weight = 3;
            tr.knots[2].weight = 3;
            tr.knots[3].weight = 4;
            tr.knots[4].weight = 12;
            tr.knots[5].weight = 4;
            tr.knots[6].weight = 5;
            tr.knots[7].weight = 9;
            tr.knots[8].weight = 4;
        }
        public void craeteGraph4_2(Graph tr)
        {
            tr.levels = 2;
            tr.h = 4;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 5, 6, 7, 8, 4 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(4);
            tr.countLevels.Add(5);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));
            tr.knots.Add(new Knot(6, 2));
            tr.knots.Add(new Knot(7, 2));
            tr.knots.Add(new Knot(8, 2));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[7]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[8]));


            tr.knots[0].weight = 4;
            tr.knots[1].weight = 3;
            tr.knots[2].weight = 3;
            tr.knots[3].weight = 4;
            tr.knots[4].weight = 12;
            tr.knots[5].weight = 4;
            tr.knots[6].weight = 5;
            tr.knots[7].weight = 9;
            tr.knots[8].weight = 4;
        }
        public void craeteGraph5(Graph tr)
        {
            tr.levels = 3;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(4);
            tr.countLevels.Add(3);
            tr.countLevels.Add(3);

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));
            tr.knots.Add(new Knot(6, 2));
            tr.knots.Add(new Knot(7, 3));
            tr.knots.Add(new Knot(8, 3));
            tr.knots.Add(new Knot(9, 3));

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[1], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[2], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[5], tr.knots[7]));
            tr.arcs.Add(new Arc(tr.knots[5], tr.knots[8]));
            tr.arcs.Add(new Arc(tr.knots[6], tr.knots[8]));
            tr.arcs.Add(new Arc(tr.knots[6], tr.knots[9]));

            tr.knots[0].weight = 2;
            tr.knots[1].weight = 1;
            tr.knots[2].weight = 2;
            tr.knots[3].weight = 1;
            tr.knots[4].weight = 3;
            tr.knots[5].weight = 4;
            tr.knots[6].weight = 3;
            tr.knots[7].weight = 8;
            tr.knots[8].weight = 6;
            tr.knots[9].weight = 9;
        }
        public void craeteGraph6(Graph tr)
        {
            tr.levels = 2;
            tr.h = 3;

            tr.L_priorityList = new List<int> { 0, 1, 2, 3, 5, 6, 7, 8, 4};

            tr.countLevels = new List<int> { };

            tr.countLevels.Add(4);
            tr.countLevels.Add(5);            

            tr.knots = new List<Knot> { };

            tr.knots.Add(new Knot(0, 1));
            tr.knots.Add(new Knot(1, 1));
            tr.knots.Add(new Knot(2, 1));
            tr.knots.Add(new Knot(3, 1));
            tr.knots.Add(new Knot(4, 2));
            tr.knots.Add(new Knot(5, 2));
            tr.knots.Add(new Knot(6, 2));
            tr.knots.Add(new Knot(7, 2));
            tr.knots.Add(new Knot(8, 2));            

            tr.arcs = new List<Arc> { };

            tr.arcs.Add(new Arc(tr.knots[0], tr.knots[4]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[5]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[6]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[7]));
            tr.arcs.Add(new Arc(tr.knots[3], tr.knots[8]));

            tr.knots[0].weight = 3;
            tr.knots[1].weight = 2;
            tr.knots[2].weight = 2;
            tr.knots[3].weight = 2;
            tr.knots[4].weight = 9;
            tr.knots[5].weight = 4;
            tr.knots[6].weight = 4;
            tr.knots[7].weight = 4;
            tr.knots[8].weight = 4;
        }
    }
}
