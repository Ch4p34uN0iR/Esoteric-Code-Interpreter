/*
 * Brainfuck Interpreter
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
    public class Brainfuck : IInterpreter
    {
        public event PrintCharEventHandler   PrintChar   = Console.Write;
        public event PrintStringEventHandler PrintString = Console.WriteLine;
        public event ReadCharEventHandler    ReadChar    = () => Console.ReadKey().KeyChar;
        public event ReadStringEventHandler  ReadString  = Console.ReadLine;

        /// <summary>
        /// Loops through the specified Brainfuck code and runs it.
        /// </summary>
        /// <param name="src">The Brainfuck code.</param>
        /// <returns>Returns the results of the code run.</returns>
        public unsafe void Run(string src)
        {
            int pointer = 0, loop = 0, length = src.Length;

            fixed (char* app = src)
            fixed (char* mem = new char[short.MaxValue])
            {
                for (var i = 0; i != length; i++)
                {
                    switch (app[i])
                    {
                        case '>':
                            pointer++;
                            break;

                        case '<':
                            pointer--;
                            break;

                        case '+':
                            mem[pointer]++;
                            break;

                        case '-':
                            mem[pointer]--;
                            break;

                        case '.':
                            PrintChar(mem[pointer]);
                            break;

                        case ',':
                            mem[pointer] = ReadChar();
                            break;

                        case '[':
                            if (mem[pointer] == 0)
                            {
                                for (i++; loop > 0 || app[i] != ']'; i++)
                                {
                                    if (app[i] == '[')
                                        loop++;

                                    if (app[i] == ']')
                                        loop--;
                                }
                            }
                            break;

                        case ']':
                            for (i--; loop > 0 || app[i] != '['; i--)
                            {
                                if (app[i] == ']')
                                    loop++;

                                if (app[i] == '[')
                                    loop--;
                            }

                            i--;
                            break;

                        case '\0':
                            return;
                    }
                }
            }
        }
    }
}