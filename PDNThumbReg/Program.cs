/*
 * MIT License
 * 
 * Copyright (c) 2023 Yonder
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PDNThumbReg
{
    public static class Program
    {
        private const string cliName = "PDNThumbReg";
        private const string regName = "Paint.NET Project File Thumbnail Provider";
        private const string regAssemblyName = "PDNThumb.dll";
        private static readonly Assembly regAssembly = typeof(PDNThumb.ThumbnailHandler).Assembly;

        public static int Main(string[] args)
        {
            bool help = false;
            bool silent = false;
            bool assumeYes = false;
            bool? forceIsRegistered = null;
            if (args.Length != 0)
            {
                int i = 0, l = 5;
                foreach (char c in args[0])
                {
                    if (i + 1 == l)
                        break;

                    if (!silent && c == 'h')
                        help = true;
                    if (!silent && c == 's')
                        silent = true;
                    if (!assumeYes && c == 'y')
                        assumeYes = true;

                    if (forceIsRegistered != null)
                    {
                        if (c == 'i')
                            forceIsRegistered = false;
                        if (c == 'u')
                            forceIsRegistered = true;
                    }

                    i++;
                }
            }

            if (help)
            {
                Console.Out.WriteLine(
                    "USAGE: {0} [hsyiu]" +
                    "\r\n h = Print this message" +
                    "\r\n s = Be completely silent (no console output; implies [y])" +
                    "\r\n y = Assume yes to all \"ask yes/no\" prompts and \"press any key ...\" prompts" +
                    "\r\n i = Force register (override checking if registered AKA. regasm behavior" +
                    "\r\n u = Force unregister (override checking if registered AKA. regasm behavior" +
                    "\r\nNOTE: These options all go within one argument. Each option is a single character. The " +
                    "\r\nlimit of what characters are counted for is 5 characters. Options can be repeated but there" +
                    "\r\nis no usage in that.",
                cliName);
                return 1;
            }

            bool isRegistered;
            if (forceIsRegistered == null)
            {
                try
                {
                    isRegistered = ShellReg.IsAssemblyRegistered(regAssembly);
                }
                catch (Exception e)
                {
                    if (!silent)
                    {
                        Console.Error.WriteLine("ERROR: Failed to determine if {0} is already registed. If this " +
                            "keeps happening, you can force register/unregister with a single argument of i/u. {1}",
                            regAssemblyName, e);
                        if (!assumeYes)
                            PAUSE();
                    }
                    return 1;
                }
            }
            else
                isRegistered = forceIsRegistered.Value;

            if (isRegistered)
            {
                if (!silent && !assumeYes)
                {
                    if (AskYN($"Do you want to unregister \"{regName}\"?", silent))
                    {
                        Console.Error.WriteLine("Not unregistering \"{0}\"", regName);
                        if (!assumeYes)
                            PAUSE();
                        return 1;
                    }
                }

                if (!silent)
                    Console.Out.WriteLine("Ungistering {0} . . .", regAssemblyName);

                try
                {
                    if (!ShellReg.UnregisterAssembly(regAssembly))
                    {
                        if (!silent)
                        {
                            Console.Error.WriteLine("ERROR: Failed to unregister {0}. Most likely, there is no type " +
                                "with a [COMVisible(true)] attribute found in the assembly.", regAssemblyName);
                            if (!assumeYes)
                                PAUSE();
                        }
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    if (!silent)
                    {
                        Console.Error.WriteLine("ERROR: Failed to unregister {0}. {1}", regAssemblyName, e);
                        if (!assumeYes)
                            PAUSE();
                    }
                    return 1;
                }

                if (!silent)
                    Console.Out.WriteLine("Successfully unregistered {0}.", regAssemblyName);
            }
            else
            {
                if (!silent && !assumeYes)
                {
                    if (AskYN($"Do you want to register \"{regName}\"?", silent))
                    {
                        Console.Error.WriteLine("Not registering \"{0}\"", regName);
                        if (!assumeYes)
                            PAUSE();
                        return 1;
                    }
                }

                if (!silent)
                    Console.Out.WriteLine("Registering {0} . . .", regAssemblyName);

                try
                {
                    if (!ShellReg.RegisterAssembly(regAssembly, AssemblyRegistrationFlags.SetCodeBase))
                    {
                        if (!silent)
                        {
                            Console.Error.WriteLine("ERROR: Failed to register {0}. Most likely, there is no type " +
                                "with a [COMVisible(true)] attribute found in the assembly.", regAssemblyName);
                            if (!assumeYes)
                                PAUSE();
                        }
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    if (!silent)
                    {
                        Console.Error.WriteLine("ERROR: Failed to register {0}. {1}", regAssemblyName, e);
                        if (!assumeYes)
                            PAUSE();
                    }
                    return 1;
                }

                if (!silent)
                    Console.Out.WriteLine("Successfully registered {0}.", regAssemblyName);
            }

            if (!silent && !assumeYes)
                PAUSE();
            return 0;
        }

        private static void PAUSE()
        {
            Console.Out.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        internal static bool AskYN(string message, bool silent = false)
        {
            if (!silent)
            {
                Console.Out.Write(message);
                Console.Out.Write(' ');
            }
            ConsoleKey key = Console.ReadKey(silent).Key;
            if (!silent)
                Console.Out.WriteLine();
            return key != ConsoleKey.Y;
        }
    }
}
