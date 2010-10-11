/*
 * Spoon Interpreter
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
using System.Collections.Generic;
using System.Text;

namespace RoliSoft.Interpreters
{
    public class Spoon : Brainfuck
    {
        private Dictionary<string, char> _dic = new Dictionary<string, char>
                                                    {
                                                        {       "1", '+' },
                                                        {     "000", '-' },
                                                        {     "010", '>' },
                                                        {     "011", '<' },
                                                        {    "0011", ']' },
                                                        {   "00100", '[' },
                                                        {  "001010", '.' },
                                                        { "0010110", ',' },
                                                        {"00101111", '\0'},
                                                    };

        /// <summary>
        /// Loops through the specified Spoon code, translates it to Brainfuck and runs it.
        /// </summary>
        /// <param name="src">The Spoon code.</param>
        /// <returns>Returns the results of the code run.</returns>
        public unsafe new void Run(string src)
        {
            var sb = new StringBuilder();
            var last = String.Empty;
            var length = src.Length;

            fixed (char* app = src)
            {
                for (var i = 0; i != length; i++)
                {
                    last += app[i];

                    if (_dic.ContainsKey(last))
                    {
                        sb.Append(_dic[last]);
                        last = String.Empty;
                    }
                }
            }

            base.Run(sb.ToString());
        }
    }
}