using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practic
{
    public class Graph
    {
        public List<Knot> knots;
        public List<Arc> arcs;
        public int h; //count of processors      
        public int levels;
        public List<int> countLevels;
        public List<int> L_priorityList;

        public int l; //max time
        public int[,] matrixOfAdjacency;
        public List<List<int>> S;
        public int[] lenInS;

        public Graph Clone()
        { 
            Graph clone = new Graph();

            clone.h = h;
            clone.l = l;
            clone.levels = levels;
            clone.countLevels = new List<int> { };
            clone.L_priorityList = new List<int> { };
            clone.knots = new List<Knot> { };
            clone.arcs = new List<Arc> { };
            clone.S = new List<List<int>>();

            for (int i = 0; i < countLevels.Count; i++)
            {
                clone.countLevels.Add(countLevels[i]);
            }
            for (int i = 0; i < L_priorityList.Count; i++)
            {
                clone.L_priorityList.Add(L_priorityList[i]);
            }
            for (int i = 0; i < knots.Count; i++)
            {
                clone.knots.Add((Knot)knots[i].Clone());
            }
            for (int i = 0; i < arcs.Count; i++)
            {
                clone.arcs.Add(new Arc(clone.knots[arcs[i].knotOut.number], clone.knots[arcs[i].knotIn.number]));
            }
            for (int i = 0; i < h; i++)
            {
                clone.S.Add(new List<int> ());
                for (int j = 0; j < l; j++)
                {
                    clone.S[i].Add(S[i][j]);
                }
            }
            return clone;
        }
        public void knotsAreNotProceed()
        {
            for (int i = 0; i < knots.Count; i++)
            {
                knots[i].processed = false;
                knots[i].avaible = false;
            }            
        }
        public void fillMatrixOfAdjacency()
        {
            matrixOfAdjacency = new int[knots.Count, knots.Count];
            for (int i = 0; i < knots.Count; i++)
            {
                for (int j = 0; j < knots.Count; j++)
                {
                    matrixOfAdjacency[i, j] = 0;
                }
            }
            for (int i = 0; i < arcs.Count; i++)
            {
                matrixOfAdjacency[arcs[i].knotOut.number, arcs[i].knotIn.number] = 1;
            }
        }
        public bool deleteAllTranzArcs()
        {
            bool deleted = false;
            for (int i = 0; i < knots.Count; i++)
            {
                if (knots[i].level > 1)
                {
                    if (!deleted)
                    {
                        deleted = findANDDeleteTranzitiveArcs(i);
                    }
                    else
                    {
                        findANDDeleteTranzitiveArcs(i);
                    }
                }
            }
            return deleted;
        }
        public bool ifArcExist(int knotOut, int knotIn)
        {
            bool exist = false;
            for (int i = 0; i < arcs.Count; i++)
            {
                if (arcs[i].knotOut.number == knotOut && arcs[i].knotIn.number == knotIn)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }       
        List<int> findParents(int num)
        {
            List<int> parents = new List<int> { };
            for (int i = 0; i < knots.Count; i++)
            {
                if (matrixOfAdjacency[i, num] == 1)
                {
                    parents.Add(i);
                }
            }
            return parents;
        }
        List<int> findChildren(int num) // находит всех непосредственных потомков вершины
        {
            List<int> children = new List<int> { };
            for (int i = 0; i < knots.Count; i++)
            {
                if (matrixOfAdjacency[num, i] == 1)
                {
                    children.Add(i);
                }
            }
            return children;
        }
        List<int> addlist2to1(List<int> a, List<int> b)
        {
            for (int i = 0; i < b.Count; i++)
            {
                a.Add(b[i]);
            }
            return a;
        }
        void removeArc(int OutKN, int InKN)
        {
            for (int i = 0; i < arcs.Count; i++)
            {
                if (arcs[i].knotOut.number == OutKN && arcs[i].knotIn.number == InKN)
                {
                    arcs.RemoveAt(i);
                    break;
                }
            }
        }
        public bool findANDDeleteTranzitiveArcs(int num)
        {
            bool deleted = false;
            List<int> parentsNum = findParents(num);
            List<int> candidates = new List<int> { };
            candidates = addlist2to1(parentsNum, candidates);
            List<int> activeParents = new List<int> { };
            int active = 0;

            while (candidates.Count != 0)
            {
                active = candidates[candidates.Count - 1];
                candidates.RemoveAt(candidates.Count - 1);
                activeParents = findParents(active);
                for (int i = 0; i < activeParents.Count; i++)
                {
                    if (parentsNum.Contains(activeParents[i]))
                    {
                        matrixOfAdjacency[activeParents[i], num] = 0;
                        removeArc(activeParents[i], num);
                        deleted = true;
                    }
                    else
                    {
                        candidates.Add(activeParents[i]);
                    }
                }
            }
            return deleted;
        }

        public bool allKnotsAreProcessed()
        {
            bool allProcessed = true;
            for (int i = 0; i < knots.Count; i++)
            {
                if (knots[i].processed == false)
                {
                    allProcessed = false;
                    break;
                }
            }
            return allProcessed;
        }

        public void setAvaible()
        {
            bool isAvaible;
            List<int> parentsNum;
            for (int i = 0; i < knots.Count; i++)
            {
                isAvaible = true;
                if (knots[i].avaible == false)
                {
                    parentsNum = findParents(i);
                    for (int j = 0; j < parentsNum.Count; j++)
                    {
                        if (knots[parentsNum[j]].processed == false)
                        {
                            isAvaible = false; break;
                        }
                    }
                    knots[i].avaible = isAvaible;
                }
            }
        }


        public void findOrder()
        {
            S = new List<List<int>>();
            lenInS = new int[h];
            int[] inProcess = new int[h];

            for (int i = 0; i < h; i++)
            {
                lenInS[i] = 0;
                inProcess[i] = -1;
                S.Add(new List<int>());
            }
            int step = 0;

            while (!allKnotsAreProcessed())
            {                
                for (int i = 0; i < h; i++)
                {
                    if (step >= lenInS[i])
                    {
                        if (inProcess[i] != -1)
                        {
                            knots[inProcess[i]].processed = true;
                        }

                        inProcess[i] = -1;

                    }
                }
                setAvaible();
                for (int i = 0; i < h; i++)
                {
                    if (step >= lenInS[i])
                    {
                        for (int j = 0; j < knots.Count; j++)
                        {
                            if (knots[L_priorityList[j]].avaible == true && knots[L_priorityList[j]].processed == false && !inProcess.Contains(knots[L_priorityList[j]].number))
                            {
                                inProcess[i] = knots[L_priorityList[j]].number;
                                lenInS[i] += knots[L_priorityList[j]].weight;
                                break;
                            }
                        }
                    }
                    S[i].Add(inProcess[i]);
                    if (inProcess[i] == -1)
                    {
                        lenInS[i] += 1;
                    }
                }
                step++;
            }
            for (int i = 0; i < h; i++)
            {
                S[i].RemoveAt(S[i].Count - 1);
                lenInS[i] -= 1;
            }              
            l = 0;
            for (int i = 0; i < h; i++)
            {
                if (lenInS[i] > l)
                {
                    l = lenInS[i];
                }
            }
        }

        
    }
}
