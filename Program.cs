/*
 * Interactive Esoteric Code Interpreter
 * Copyright (C) 2010 RoliSoft
 * 
 * This program is free software. It comes without any warranty, to
 * the extent permitted by applicable law. You can redistribute it
 * and/or modify it under the terms of the Do What The Fuck You Want
 * To Public License, Version 2, as published by Sam Hocevar. See
 * http://sam.zoy.org/wtfpl/COPYING for more details.
 */

using System;
using System.IO;
using System.Reflection;
using System.Text;
using RoliSoft.Interpreters;

namespace RoliSoft
{
    class InteractiveInterpreter
    {
        static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        static void Main(string[] args)
        {
            SetTitle();
            Console.WriteLine("Interactive Esoteric Code Interpreter v" + Version + " -- (c) " + DateTime.Now.Year + " RoliSoft");

            while (true)
            {
                //try
                {
                    Console.Write("> ");
                    var input = Console.ReadLine().Trim().Split(' ');

                    if (input[0] == "exit")
                    {
                        return;
                    }
                    if (input[0] == "load")
                    {
                        var parser = CreateInterpreter(input[1]);
                        var code   = File.ReadAllText(input[2]);

                        Console.Title = "Running " + parser.GetType().Name + " app: " + new FileInfo(input[2]).Name;

                        parser.Run(code);

                        SetTitle();

                        if (Console.CursorLeft != 0)
                        {
                            Console.WriteLine();
                        }

                        continue;
                    }
                    if (input[0] == "disasm")
                    {
                        var parser = CreateDisassembler(input[1]);
                        var code   = File.ReadAllText(input[2]);

                        Console.Title = "Disassembling " + parser.GetType().Name + " app: " + new FileInfo(input[2]).Name;

                        var asm = parser.Disassemble(code);

                        Console.WriteLine(asm.TrimEnd());

                        SetTitle();
                        continue;
                    }
                    if (input[0] == "start")
                    {
                        var parser = CreateInterpreter(input[1]);

                        Console.Title = "Enter " + parser.GetType().Name + " code to interpret...";

                        var sb = new StringBuilder();

                        while (true)
                        {
                            var ln = Console.ReadLine();

                            if (ln != "end")
                            {
                                sb.AppendLine(ln);
                            }
                            else
                            {
                                break;
                            }
                        }

                        Console.Title = "Running entered " + parser.GetType().Name + " code...";

                        parser.Run(sb.ToString());

                        SetTitle();

                        if (Console.CursorLeft != 0)
                        {
                            Console.WriteLine();
                        }

                        continue;
                    }
                    if (input[0] == "help")
                    {
                        Console.WriteLine(@"To interpret a code enter `start <lang>` and start writing the code. End it with `end`.
To load a file enter `load <lang> <file>`. To exit enter `exit`.
Languages with interpreter interface implemented:

  Brainfuck  -> bf
  Ook!       -> ook
  Spoon      -> sp
  Byter      -> bt
  Whitespace -> ws
  Befunge-93 -> b93

To disassemble an esoteric code into a more understandable assembly-like language enter `disasm <lang> <file>`.
Languages with disassembler interface implemented:

  Whitespace -> ws
");
                        continue;
                    }
                    if (input[0] == "clear")
                    {
                        Console.Clear();
                        continue;
                    }
                    if (input[0].Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Unknown command. See `help` for usage information.");
                    }
                }
                /*catch (Exception ex)
                {
                    SetTitle();
                    Console.WriteLine("Error: " + ex.Message);
                }*/
            }
        }

        static IInterpreter CreateInterpreter(string code)
        {
            switch(code)
            {
                case "bf":
                    return new Brainfuck();

                case "ook":
                    return new Ook();

                case "sp":
                    return new Spoon();

                case "bt":
                    return new Byter();

                case "ws":
                    return new Whitespace();

                case "b93":
                    return new Befunge93();

                default:
                    throw new Exception(code + " is not supported.");
            }
        }

        static IDisassembler CreateDisassembler(string code)
        {
            switch (code)
            {
                case "ws":
                    return new Whitespace();

                default:
                    throw new Exception(code + " is not supported.");
            }
        }

        static void SetTitle()
        {
            Console.Title = "Interactive Esoteric Code Interpreter v" + Version;
        }
    }
}