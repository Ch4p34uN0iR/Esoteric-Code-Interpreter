/*
 * Befunge-93 Interpreter
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
using System.IO;

namespace RoliSoft.Interpreters
{
    public class Befunge93 : IInterpreter
    {
        public event PrintCharEventHandler   PrintChar   = Console.Write;
        public event PrintStringEventHandler PrintString = Console.WriteLine;
        public event ReadCharEventHandler    ReadChar    = () => Console.ReadKey().KeyChar;
        public event ReadStringEventHandler  ReadString  = Console.ReadLine;

        private unsafe char* _matrix;
        private StackList<int> _stack;
        private int _rowNr, _colNr, _loc;
        private Direction _dir;
        private bool _stringMode, _trampoline, _done;

        private enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }

        /// <summary>
        /// Builds a matrix from the Befunge-93 code and runs it.
        /// </summary>
        /// <param name="src">The Befunge-93 code.</param>
        public unsafe void Run(string src)
        {
            _stack      = new StackList<int>();
            _dir        = Direction.Right;
            _stringMode = _trampoline = _done = false;
            _loc        = 0;

            using (var sr = new StringReader(src))
            {
                string ln;
                while((ln = sr.ReadLine()) != null)
                {
                    _rowNr++;

                    if (_colNr < ln.Length)
                    {
                        _colNr = ln.Length;
                    }
                }
            }

            fixed (char* ptr = new char[_rowNr * _colNr])
            {
                var i = 0;
                foreach (var c in src)
                {
                    if (c != '\r' && c != '\n')
                    {
                        ptr[i++] = c;
                    }
                }

                _matrix = ptr;
            }

            do
            {
                RunInstruction();
            } while (!_done);
        }

        private unsafe void RunInstruction()
        {
            if (!_stringMode && !_trampoline)
            {
                switch (_matrix[_loc])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        _stack.Push(int.Parse(_matrix[_loc].ToString()));
                        break;

                    case '+':
                        _stack.Push(_stack.PopZero() + _stack.PopZero());
                        break;

                    case '-':
                        _stack.Push(_stack.PopZero() - _stack.PopZero());
                        break;

                    case '*':
                        _stack.Push(_stack.PopZero() * _stack.PopZero());
                        break;

                    case '/':
                        var da = _stack.PopZero();
                        var db = _stack.PopZero();
                        _stack.Push(db / da);
                        break;

                    case '%':
                        var ma = _stack.PopZero();
                        var mb = _stack.PopZero();

                        _stack.Push(mb % ma);
                        break;

                    case '!':
                        _stack.Push(_stack.PopZero() == 0 ? 1 : 0);
                        break;

                    case '`':
                        _stack.Push(_stack.PopZero() < _stack.PopZero() ? 1 : 0);
                        break;

                    case '>':
                        _dir = Direction.Right;
                        break;

                    case '<':
                        _dir = Direction.Left;
                        break;

                    case '^':
                        _dir = Direction.Up;
                        break;

                    case 'v':
                        _dir = Direction.Down;
                        break;

                    case '?':
                        _dir = Utils.RandomEnum<Direction>();
                        break;

                    case '_':
                        _dir = _stack.PopZero() == 0 ? Direction.Right : Direction.Left;
                        break;

                    case '|':
                        _dir = _stack.PopZero() == 0 ? Direction.Down : Direction.Up;
                        break;

                    case '"':
                        _stringMode = true;
                        break;

                    case ':':
                        _stack.Push(_stack.PeekZero());
                        break;

                    case '\\':
                        var s1 = _stack.PopZero();
                        var s2 = _stack.PopZero();

                        _stack.Push(s1);
                        _stack.Push(s2);
                        break;

                    case '$':
                        _stack.PopZero();
                        break;

                    case '.':
                        PrintChar(_stack.PopZero() + " ");
                        break;

                    case ',':
                        PrintChar((char) _stack.PopZero());
                        break;

                    case '#':
                        _trampoline = true;
                        break;

                    case 'p':
                        var py = _stack.PopZero();
                        var px = _stack.PopZero();
                        var pv = _stack.PopZero();

                        _matrix[px + py * _colNr] = (char)pv;
                        break;

                    case 'g':
                        var gy = _stack.PopZero();
                        var gx = _stack.PopZero();

                        _stack.Push(_matrix[gx + gy * _colNr]);
                        break;

                    case '&':
                        _stack.Push(int.Parse(ReadString()));
                        break;

                    case '~':
                        _stack.Push(ReadChar());
                        break;

                    case '@':
                        Console.WriteLine("FUCK");
                        _done = true;
                        return;
                        
                    case ' ':
                        // noop
                        break;

                    default:
                        Reverse();
                        break;
                }
            }
            else if (_stringMode)
            {
                if (_matrix[_loc] != '"')
                {
                    _stack.Push(_matrix[_loc]);
                }
                else
                {
                    _stringMode = false;
                }
            }
            else if (_trampoline)
            {
                _trampoline = false;
            }

            switch(_dir)
            {
                case Direction.Up:
                    _loc -= _colNr;

                    if (_loc < 0)
                    {
                        _loc += _rowNr * _colNr;
                    }
                    break;

                case Direction.Down:
                    _loc += _colNr;

                    if (_loc >= _rowNr * _colNr)
                    {
                        _loc -= _rowNr * _colNr;
                    }
                    break;

                case Direction.Right:
                    if (_loc % _colNr == _colNr - 2)
                    {
                        _loc -= _colNr - 2;
                    }
                    else
                    {
                        ++_loc;
                    }
                    break;

                case Direction.Left:
                    if (_loc % _colNr == 0)
                    {
                        _loc += _colNr - 2;
                    }
                    else
                    {
                        --_loc;
                    }
                    break;
            }
        }

        private void Reverse()
        {
            switch(_dir)
            {
                case Direction.Up:
                    _dir = Direction.Down;
                    break;

                case Direction.Down:
                    _dir = Direction.Up;
                    break;

                case Direction.Right:
                    _dir = Direction.Left;
                    break;

                case Direction.Left:
                    _dir = Direction.Right;
                    break;
            }
        }
    }
}