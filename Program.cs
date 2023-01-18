using System;
using System.Collections.Generic;
namespace Ecosystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            int numfish = 0;
            bool isBear = true;
            bool areEquals = true;
            bool notFinished = true;
            List<Bear> bears = new List<Bear>();
            List<Fish> fishes = new List<Fish>();
            List<Bear> btemp = new List<Bear>();
            List<Fish> ftemp = new List<Fish>();
            List<char> river = new List<char>();
            int indexB1 = 0;
            int indexF1 = 0;
            int indexB2 = 0;
            int indexF2 = 0;
            int[] indexes = new int[4] { indexB1, indexF1, indexB2, indexF2 };
            for (int i = 0; i < 11; i++)
            {
                river.Add('^'); //Filling the river with ""water""
            }
            while (notFinished == true)
            {
                Random rnd = new Random();
                while (areEquals == true)
                {
                    areEquals = false;
                    indexB1 = rnd.Next(0, 9);
                    indexF1 = rnd.Next(0, 9);
                    indexB2 = rnd.Next(0, 9);
                    indexF2 = rnd.Next(0, 9);
                    indexes[0] = indexB1;
                    indexes[1] = indexB2;
                    indexes[2] = indexF1;
                    indexes[3] = indexF2;
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (i != j && indexes[i] == indexes[j])
                            {
                                areEquals = true;
                            }
                        }
                    }
                }
                if (counter < 2)
                {
                    bears = newBearObject(bears); //initializing 2 bears and 2 fishes
                    fishes = newFishObject(fishes);
                    river.RemoveAt(indexes[counter]);
                    river.Insert(indexes[counter], bears[counter].Icon);
                    river.RemoveAt(indexes[counter + 2]);
                    river.Insert(indexes[counter + 2], fishes[counter].Icon);
                    bears[counter].Index = indexes[counter];
                    fishes[counter].Index = indexes[counter + 2];
                    counter++;
                }
                else
                {
                    int indexB;
                    List<int> delF = new List<int>();
                    int indexF;
                    bool isSame = false;
                    isBear = true;
                    try
                    {
                        foreach (Bear items in bears)
                        {
                            indexB = items.Index + items.move();
                            indexB = avoidLimits(indexB, river);
                            if (indexB == items.Index)
                            {
                                isSame = true;
                            }
                            Tuple<List<char>, Bear, Fish, int> tuple = collission(river, indexB, items, null, isBear, isSame, bears, fishes);
                            river = tuple.Item1;
                            btemp.Add(tuple.Item2);
                            delF.Add(tuple.Item4);
                            btemp.Clear();
                        }
                        foreach (Bear items in btemp)
                        {
                            bears.Add(items);
                        }
                        foreach (int items in delF)
                        {
                            if (items >= 0)
                            {
                                fishes.RemoveAt(items);
                            }
                        }
                        delF.Clear();
                        foreach (Fish items in fishes)
                        {
                            indexF = items.Index + items.move();
                            indexF = avoidLimits(indexF, river);
                            if (indexF == items.Index)
                            {
                                isSame = true;
                            }
                            Tuple<List<char>, Bear, Fish, int> tuple = collission(river, indexF, null, items, false, isSame, bears, fishes);
                            river = tuple.Item1;
                            ftemp.Add(tuple.Item3);
                            delF.Add(tuple.Item4);
                            ftemp.Clear();
                        }
                        foreach (Fish items in ftemp)
                        {
                            fishes.Add(items);
                        }
                        foreach (int items in delF)
                        {
                            if (items >= 0)
                            {
                                fishes.RemoveAt(items);
                            }
                        }
                        delF.Clear();

                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (river.Exists(x => x == '^') == true || river.Exists(x => x == 'F') == true)
                {
                    notFinished = true;
                }
                else
                {
                    Console.WriteLine("End!");
                    notFinished = false;
                }
                foreach (char items in river) //Displaying river
                {
                    Console.Write(items);
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }
        static Tuple<List<char>, Bear, Fish, int> collission(List<char> riv, int i, Bear b, Fish f, bool isBear, bool isSame, List<Bear> bears, List<Fish> fishes)
        {
            int tempindx = -1;
            Bear btemp = new Bear();
            Random rnd = new Random();
            Fish ftemp = new Fish();
            bool lookForAnotherOne = true;
            if (((riv[i] == 'B' && isBear == true) || (riv[i] == 'F' && isBear == false)) && isSame == false)//collission with the same animal type
            {
                while (lookForAnotherOne == true)
                {
                    int index = rnd.Next(0, 11);
                    if (riv[index] == '^' || (isBear != false && riv[index] == 'F')) //Creating new instance of the animal
                    {
                        if (isBear == true)
                        {
                            riv[index] = btemp.Icon;
                            btemp.Index = index;
                            lookForAnotherOne = false;
                        }
                        else
                        {
                            if (isBear == false)
                            {
                                riv[index] = ftemp.Icon;
                                ftemp.Index = index;
                                lookForAnotherOne = false;
                            }
                        }
                    }
                    if (riv.Exists(x => x == '^') == false)
                    {
                        lookForAnotherOne = false;
                    }
                }
            }
            else
            {
                if (riv[i] == '^') //move to a null
                {
                    if (isBear == true)
                    {
                        (riv, tempindx) = bearEatFish(riv, fishes, b, i);
                    }
                    else
                    {
                        riv[i] = f.Icon;
                        riv[f.Index] = '^';
                        f.Index = i;
                    }
                }
                else
                {
                    if (isSame == false)
                    {
                        if (isBear == true)
                        {
                            (riv, tempindx) = bearEatFish(riv, fishes, b, i);
                        }
                        else
                        {
                            riv[f.Index] = '^';
                        }
                    }
                }
            }
            return Tuple.Create(riv, btemp, ftemp, tempindx);
        }
        static (List<char>, int) bearEatFish(List<char> riv, List<Fish> fishes, Bear b, int i)
        {
            int index = -1;
            riv[i] = b.Icon;
            riv[b.Index] = '^';
            for (int indx = 0; indx < fishes.Count; indx++)
            {
                if (fishes[indx].Index == i)
                {
                    index = indx;
                }
            }
            b.Index = i;
            return (riv, index);
        }
        static List<Bear> newBearObject(List<Bear> bears)
        {
            Bear b = new Bear();
            bears.Add(b);
            return bears;
        }
        static List<Fish> newFishObject(List<Fish> fishes)
        {
            Fish f = new Fish();
            fishes.Add(f);
            return fishes;
        }
        static int avoidLimits(int index, List<char> river)
        {
            if (index < 0)
            {
                index += 1;
            }
            if (index >= river.Count)
            {
                index -= 1;
            }
            return index;
        }
    }
}

