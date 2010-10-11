/*
 * Byter Interpreter
 * Copyright (C) 2010 RoliSoft
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace RoliSoft.Interpreters
{
    public class Byter : IInterpreter
    {
        public event PrintCharEventHandler   PrintChar   = Console.Write;
        public event PrintStringEventHandler PrintString = Console.WriteLine;
        public event ReadCharEventHandler    ReadChar    = () => Console.ReadKey().KeyChar;
        public event ReadStringEventHandler  ReadString  = Console.ReadLine;

        private unsafe char* _matrix;

        /// <summary>
        /// Builds a 16x16 matrix from the Byter code and runs it.
        /// </summary>
        /// <param name="src">The Byter code.</param>
        /// <returns>Returns the results of the code run.</returns>
        public unsafe void Run(string src)
        {
            fixed (char* ptr = new char[256])
            {
                var i = 0;
                foreach (var c in src)
                {
                    if (c == '0' || c == '<' || c == '>' || c == 'V' || c == 'A' || c == '{' || c == '}' || c == '+' || c == '-' || c == '$' || c == '#')
                    {
                        ptr[i++] = c;
                    }
                }

                if (i != 256)
                {
                    throw new Exception("Code must be exactly 256 characters!");
                }

                _matrix = ptr;
            }

            Step(0, 0);
        }

        private unsafe void Step(int x, int y)
        {
            if (x > 15 || y > 15)
            {
                throw new Exception(x + "x" + y + " is not a valid point in the matrix.");
            }

            switch (_matrix[x * 16 + y])
            {
                case '0':
                    Step(x, --y);
                    break;

                case '<':
                    _matrix[x * 16 + y] = '>';
                    Step(x, --y);
                    break;

                case '>':
                    _matrix[x * 16 + y] = '<';
                    Step(x, ++y);
                    break;

                case 'V':
                    _matrix[x * 16 + y] = 'A';
                    Step(++x, y);
                    break;

                case 'A':
                    _matrix[x * 16 + y] = 'V';
                    Step(--x, y);
                    break;

                case '{':
                    PrintChar((char)(x * 16 + y));
                    Step(x, --y);
                    break;

                case '}':
                    PrintChar((char)(x * 16 + y));
                    Step(x, ++y);
                    break;

                case '+':
                    PrintChar((char)(x * 16 + y));
                    Step(--x, y);
                    break;

                case '-':
                    PrintChar((char)(x * 16 + y));
                    Step(++x, y);
                    break;

                case '$':
                    PrintChar((char)(x * 16 + y));
                    Step(0, 0);
                    break;

                case '#':
                    return;
            }
        }
    }
}