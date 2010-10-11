/*
 * Whitespace Interpreter
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
    public class Whitespace : IInterpreter, IDisassembler
    {
        public event PrintCharEventHandler   PrintChar   = Console.Write;
        public event PrintStringEventHandler PrintString = Console.WriteLine;
        public event ReadCharEventHandler    ReadChar    = () => Console.ReadKey().KeyChar;
        public event ReadStringEventHandler  ReadString  = Console.ReadLine;

        private readonly List<string> _cmds = new List<string>
            { "  ", " \n ", " \n\t", " \n\n", "\t   ", "\t  \t", "\t  \n", "\t \t ", "\t \t\t", "\t\t ", "\t\t\t", "\n  ", "\n \t", "\n \n", "\n\t ", "\n\t\t", "\n\t\n", "\n\n\n", "\t\n  ", "\t\n \t", "\t\n\t ", "\t\n\t\t" };
        private readonly Dictionary<string, bool> _params = new Dictionary<string, bool>
            { // This is a list of the commands that take a parameter, and whether they're signed.
                {"  ", true},
                {"\n  ", false},
                {"\n \t", false},
                {"\n \n", false},
                {"\n\t ", false},
                {"\n\t\t", false}
            };
        private readonly Dictionary<string, string> _tr = new Dictionary<string, string>
            {
                {"  ", "push"},
                {" \n ", "dup"},
                {" \n\t", "swap"},
                {" \n\n", "pop"},
                {"\t   ", "add"},
                {"\t  \t", "sub"},
                {"\t  \n", "mul"},
                {"\t \t ", "div"},
                {"\t \t\t", "mod"},
                {"\t\t ", "store"},
                {"\t\t\t", "retr"},
                {"\n  ", "label"},
                {"\n \t", "call"},
                {"\n \n", "jmp"},
                {"\n\t ", "jz"},
                {"\n\t\t", "jn"},
                {"\n\t\n", "ret"},
                {"\n\n\n", "exit"},
                {"\t\n  ", "outchar"},
                {"\t\n \t", "outnum"},
                {"\t\n\t ", "readchar"},
                {"\t\n\t\t", "readnum"}
            };

        private StackList<long> _stack;
        private StackList<int> _callstack;
        private Dictionary<long, long> _heap;
        private Dictionary<string, int> _labels;

        private enum State
        {
            Command,
            Parameter
        }

        /// <summary>
        /// Loops through the specified Whitespace code and runs it.
        /// </summary>
        /// <param name="src">The Whitespace code.</param>
        /// <returns>Returns the results of the code run.</returns>
        public unsafe void Run(string src)
        {
            var length = src.Length;
            string cmd = String.Empty, param = String.Empty;
            var ls = State.Command;

            _stack      = new StackList<long>();
            _callstack  = new StackList<int>();
            _heap       = new Dictionary<long, long>();
            _labels     = new Dictionary<string, int>();

            fixed (char* app = src)
            {
                for (var i = 0; i != length; i++)
                {
                    if (app[i] != ' ' && app[i] != '\t' && app[i] != '\n')
                    {
                        continue;
                    }

                    if (ls == State.Command)
                    {
                        cmd += app[i];

                        if (_cmds.Contains(cmd))
                        {
                            if (_params.ContainsKey(cmd))
                            {
                                ls = State.Parameter;
                            }
                            else
                            {
                                cmd = param = String.Empty;
                            }
                        }

                        continue;
                    }

                    if (ls == State.Parameter)
                    {
                        if (app[i] != '\n')
                        {
                            param += app[i];
                        }
                        else
                        {
                            if(cmd == "\n  ")
                            {
                                _labels[param] = i;
                            }

                            cmd = param = String.Empty;
                            ls = State.Command;
                        }

                        continue;
                    }
                }

                for (var i = 0; i != length; i++)
                {
                    if (app[i] != ' ' && app[i] != '\t' && app[i] != '\n')
                    {
                        continue;
                    }

                    if (ls == State.Command)
                    {
                        cmd += app[i];

                        if (_cmds.Contains(cmd))
                        {
                            if (_params.ContainsKey(cmd))
                            {
                                ls = State.Parameter;
                            }
                            else
                            {
                                var res = RunInstruction(cmd, i);

                                if (res != null)
                                {
                                    if (res.Type == ResultType.Jump)
                                    {
                                        i = res.Value;
                                    }
                                    if (res.Type == ResultType.End)
                                    {
                                        break;
                                    }
                                }

                                cmd = param = String.Empty;
                            }
                        }

                        continue;
                    }

                    if (ls == State.Parameter)
                    {
                        if (app[i] != '\n')
                        {
                            param += app[i];
                        }
                        else
                        {
                            var res = _params[cmd] ? RunInstruction(cmd, i, ParseNumber(param)) : RunInstruction(cmd, i, param);

                            if (res != null)
                            {
                                if (res.Type == ResultType.Jump)
                                {
                                    i = res.Value;
                                }
                                if (res.Type == ResultType.End)
                                {
                                    break;
                                }
                            }

                            cmd = param = String.Empty;
                            ls = State.Command;
                        }

                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Disassembles the specified Whitespace code into an assembly-like language.
        /// </summary>
        /// <param name="src">The Whitespace code.</param>
        /// <returns>Returns the disassembled code.</returns>
        public unsafe string Disassemble(string src)
        {
            var length = src.Length;
            string cmd = String.Empty, param = String.Empty;
            var ls = State.Command;
            var sb = new StringBuilder();

            fixed (char* app = src)
            {
                for (var i = 0; i != length; i++)
                {
                    if (app[i] != ' ' && app[i] != '\t' && app[i] != '\n')
                    {
                        continue;
                    }

                    if (ls == State.Command)
                    {
                        cmd += app[i];

                        if (_cmds.Contains(cmd))
                        {
                            if (_params.ContainsKey(cmd))
                            {
                                ls = State.Parameter;
                            }
                            else
                            {
                                sb.AppendLine("[" + i.ToString("0000") + "] " + _tr[cmd] + " ");
                                cmd = param = String.Empty;
                            }
                        }

                        continue;
                    }

                    if (ls == State.Parameter)
                    {
                        if (app[i] != '\n')
                        {
                            param += app[i];
                        }
                        else
                        {
                            if (_params[cmd])
                            {
                                var tmp = ParseNumber(param);
                                sb.AppendLine("[" + i.ToString("0000") + "] " + _tr[cmd] + " " + tmp + (!char.IsControl((char)tmp) ? " ; ascii: " + (char)tmp : String.Empty));
                            }
                            else
                            {
                                sb.AppendLine("[" + i.ToString("0000") + "] " + _tr[cmd] + " " + param.Replace(' ', '0').Replace('\t', '1').Replace('\n', 'n').Substring(0, param.Length < 60 ? param.Length : 60) + (param.Length > 60 ? "..." : String.Empty));
                            }
                            
                            cmd = param = String.Empty;
                            ls = State.Command;
                        }

                        continue;
                    }
                }
            }

            return sb.ToString();
        }

        private long ParseNumber(string param)
        {
            var bin = String.Empty;

            for (var i = 1; i != param.Length; i++)
            {
                if (param[i] == ' ')
                {
                    bin += "0";
                }
                if (param[i] == '\t')
                {
                    bin += "1";
                }
                if (param[i] == '\n')
                {
                    break;
                }
            }

            var num = Convert.ToInt64(bin, 2);

            if (param[0] == '\t')
            {
                return num * -1;
            }

            return num;
        }

        private Result RunInstruction(string cmd, int location, dynamic param = null)
        {
            switch (cmd)
            {
                // -- Stack Manipulation --
                // Push
                case "  ":
                    _stack.Push(param);
                    break;

                // Duplicate
                case " \n ":
                    _stack.Push(_stack.Peek());
                    break;

                // Copy n-th to top
                case " \t ":
                    _stack.Push(_stack[(int)param]);
                    break;

                // Swap
                case " \n\t":
                    var s1 = _stack.Pop();
                    var s2 = _stack.Pop();

                    _stack.Push(s1);
                    _stack.Push(s2);
                    break;

                // Discard
                case " \n\n":
                    _stack.Pop();
                    break;

                // Discard n, keep top
                case " \t\n":
                    var dt = _stack.Pop();

                    for (var i = 0; i != param; i++)
                    {
                        _stack.Pop();
                    }

                    _stack.Push(dt);
                    break;

                // -- Arithmetic --
                // +
                case "\t   ":
                    _stack.Push(_stack.Pop() + _stack.Pop());
                    break;

                // -
                case "\t  \t":
                    _stack.Push(_stack.Pop() - _stack.Pop());
                    break;

                // *
                case "\t  \n":
                    _stack.Push(_stack.Pop() * _stack.Pop());
                    break;

                // /
                case "\t \t ":
                    _stack.Push(_stack.Pop() / _stack.Pop());
                    break;

                // %
                case "\t \t\t":
                    _stack.Push(_stack.Pop() % _stack.Pop());
                    break;

                // -- Heap Access --

                // Store
                case "\t\t ":
                    var hsVal = _stack.Pop();
                    var hsAdr = _stack.Pop();

                    _heap[hsAdr] = hsVal;
                    break;

                // Retrieve
                case "\t\t\t":
                    _stack.Push(_heap[_stack.Pop()]);
                    break;

                // -- Flow Control --
                // Mark
                case "\n  ":
                    _labels[param] = location;
                    break;

                // Call
                case "\n \t":
                    _callstack.Push(location);
                    return new Result(ResultType.Jump, _labels[param]);

                // Jump
                case "\n \n":
                    return new Result(ResultType.Jump, _labels[param]);

                // Jump if zero
                case "\n\t ":
                    if (_stack.Pop() == 0)
                    {
                        return new Result(ResultType.Jump, _labels[param]);
                    }
                    break;

                // Jump if negative
                case "\n\t\t":
                    if (_stack.Pop() < 0)
                    {
                        return new Result(ResultType.Jump, _labels[param]);
                    }
                    break;

                // Exit func
                case "\n\t\n":
                    return new Result(ResultType.Jump, _callstack.Pop());

                // Exit app
                case "\n\n\n":
                    return new Result(ResultType.End);

                // -- I/O --
                // Print character
                case "\t\n  ":
                    PrintChar((char) _stack.Pop());
                    break;

                // Print number
                case "\t\n \t":
                    PrintChar(_stack.Pop());
                    break;

                // Read character
                case "\t\n\t ":
                    _heap[_stack.Pop()] = ReadChar();
                    break;

                // Read number
                case "\t\n\t\t":
                    _heap[_stack.Pop()] = int.Parse(ReadString());
                    break;
            }

            return null;
        }

        private class Result
        {
            public ResultType Type { get; private set; }
            public int Value { get; private set; }

            public Result(ResultType type)
            {
                Type = type;
            }

            public Result(ResultType type, int value)
            {
                Type  = type;
                Value = value;
            }
        }

        private enum ResultType
        {
            Jump,
            End
        }
    }
}