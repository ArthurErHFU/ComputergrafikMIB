using Fusee.Math.Core;
namespace FuseeApp
{
    public static class ArrayConverter
    {
        public static float3[] To1DArray(float3[,] input)
        {
            // Step 1: get total size of 2D array, and allocate 1D array.
            int size = input.Length;
            float3[] result = new float3[size];

            // Step 2: copy 2D array elements float a 1D array.
            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            // Step 3: return the new array.
            return result;
        }
        public static ushort[] To1DArray(ushort[,] input)
        {
            // Step 1: get total size of 2D array, and allocate 1D array.
            int size = input.Length;
            ushort[] result = new ushort[size];

            // Step 2: copy 2D array elements ushort a 1D array.
            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            // Step 3: return the new array.
            return result;
        }
    }
}