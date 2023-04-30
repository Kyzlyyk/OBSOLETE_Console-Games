using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw
{
    public class PaintBrush
    {
        public PaintBrush(char pixel = '█')
        {
            _pixel = pixel; 
        }

        private char _pixel = '■';

        public void DrawLine(int x, int y, int end, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.SetCursorPosition(x, y);

            for (int i = x; i <= end; i++)
            {
                Console.Write(_pixel);
            }
        }
    }
}
