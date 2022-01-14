using System;
using Random = UnityEngine.Random;

public static class ArrayExtensions
{
    public static T RandomIndex<T>(this T[] array)
    {
        if (array.Length == 0)
            throw new IndexOutOfRangeException();
        
        return array[Random.Range(0, array.Length)];
    }

    public static T[] PickUnique<T>(this T[] array, int count)
    {
        if (array.Length == 0)
            throw new IndexOutOfRangeException();
        
        var result = new T[count];
        
        int numToChoose = count;

        for (int numLeft = array.Length; numLeft > 0; numLeft--)
        {
            float prob = (float) numToChoose / numLeft;

            if (Random.value <= prob) {
                numToChoose--;
                result[numToChoose] = array[numLeft - 1];

                if (numToChoose == 0) {
                    break;
                }
            }
        }

        result.Shuffle();
        
        return result;
    }
    
    public static void Shuffle<T> (this T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
