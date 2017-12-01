using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleTetrisTanki
{
    public class SplashReader
    {
        private int x; 
        public SplashReader()
        {
            x = 5;
        }

        public BGField[] Read(String filename)
        {
            List<BGField> frames = new List<BGField>();
            
            string[] lines = File.ReadAllLines(filename);

            int framesCount = lines.Length / 20;

            for (int i = 0; i < framesCount; i++)
            {
                int[][] array = new int[10][];

                for (int j = 0; j < 10; j++)
                {
                    array[j] = new int[20];
                }

                for (int j = i * 20; j < (i + 1) * 20; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        array[k][j % 20] = int.Parse(lines[j][k].ToString());
                    }
                }
                
                frames.Add(new BGField(array));
            }

            return frames.ToArray();
        }
    }
}