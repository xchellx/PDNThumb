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

using Microsoft.Win32;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PDNThumbReg
{
    internal static class ShellReg
    {
        private static readonly RegistrationServices regSvc = new RegistrationServices();

        /// <summary>
        /// Register all COM visible <see cref="Type"/>s in <paramref name="assembly"/> for COM.<br/>
        /// This uses <see cref="RegistrationServices"/> to register for COM, which reads/writes to the registry (in
        /// <br/>particular: <see cref="Registry.ClassesRoot"/>).<br/>
        /// Reading/writing to <see cref="Registry.ClassesRoot"/> requires administrator permissions, so an application
        /// <br/>that utilizes this must ask for UAC to give such permissions.<br/>
        /// The most likey reason for this failing is because there is no COM visible <see cref="Type"/>s in
        /// <paramref name="assembly"/>.<br/>Make sure that the <see cref="Type"/>s you want to register have a
        /// [<see cref="ComVisibleAttribute"/>(<see langword="true"/>)] attribute.<br/>
        /// Further more, this class has a capabilitity to tell if a <see cref="Type"/> was registered for COM. Howver,
        /// <br/>a consistent value has to be used to look for later on. Hence, <paramref name="assembly"/> and any
        /// <br/>COM visible <see cref="Type"/> in <paramref name="assembly"/> to be registered here must have a
        /// <see cref="GuidAttribute"/> attribute.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to register <see cref="Type"/>s for COM.
        /// </param>
        /// <param name="flags">
        /// <inheritdoc cref="RegistrationServices.RegisterAssembly" path="/param[@name='flags']"/>
        /// </param>
        /// <returns>
        /// <see langword="true"/> if successfully registered, else <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="assembly"/> and/or a <see cref="Type"/> in <paramref name="assembly"/> is missing a
        /// <see cref="GuidAttribute"/> attribute.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Missing UAC permissions to read from/write to registry (<see cref="Registry.ClassesRoot"/>).
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// A value in the registry that was read was marked for deletion.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// An attempt to write to a registry key/value that was open as readonly (or user does not have permission to
        /// do so) was made.
        /// </exception>
        internal static bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
        {
            CheckForAssemblyGUID(assembly);
            CheckForTypeGUIDs(assembly);
            return regSvc.RegisterAssembly(assembly, flags);
        }

        /// <summary>
        /// Unregister all COM visible <see cref="Type"/>s in <paramref name="assembly"/> from COM.<br/>
        /// This uses <see cref="RegistrationServices"/> to unregister from COM, which reads/writes to the registry (in
        /// <br/>particular: <see cref="Registry.ClassesRoot"/>).<br/>
        /// Reading/riting to <see cref="Registry.ClassesRoot"/> requires administrator permissions, so an application
        /// <br/>that utilizes this must ask for UAC to give such permissions.<br/>
        /// The most likey reason for this failing is because there is no COM visible <see cref="Type"/>s in
        /// <paramref name="assembly"/>.<br/>Make sure that the <see cref="Type"/>s you want to unregister have a
        /// [<see cref="ComVisibleAttribute"/>(<see langword="true"/>)] attribute.<br/>
        /// Further more, this class has a capabilitity to tell if a <see cref="Type"/> was registered for COM. Howver,
        /// <br/>a consistent value has to be used to look for later on. Hence, <paramref name="assembly"/> and any
        /// <br/>COM visible <see cref="Type"/> in <paramref name="assembly"/> to be unregistered here must have a
        /// <see cref="GuidAttribute"/> attribute.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to unregister <see cref="Type"/>s from COM.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if successfully unregistered, else <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="assembly"/> and/or a <see cref="Type"/> in <paramref name="assembly"/> is missing a
        /// <see cref="GuidAttribute"/> attribute.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Missing UAC permissions to read from/write to registry (<see cref="Registry.ClassesRoot"/>).
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// A value in the registry that was read was marked for deletion.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// An attempt to write to a registry key/value that was open as readonly (or user does not have permission to
        /// do so) was made.
        /// </exception>
        internal static bool UnregisterAssembly(Assembly assembly)
        {
            CheckForAssemblyGUID(assembly);
            CheckForTypeGUIDs(assembly);
            return regSvc.UnregisterAssembly(assembly);
        }

        /// <summary>
        /// Checks if any <see cref="Type"/>s in <paramref name="assembly"/> were registered for COM already or not.
        /// <br/>This uses <see cref="RegistrationServices"/> to check for COM registerable <see cref="Type"/>s, which
        /// reads/writes to<br/>the registry (in particular: <see cref="Registry.ClassesRoot"/>).<br/>
        /// Reading/riting to <see cref="Registry.ClassesRoot"/> requires administrator permissions, so an application
        /// <br/>that utilizes this must ask for UAC to give such permissions.<br/>
        /// This may return <see langword="false"/> if there is no COM visible <see cref="Type"/>s in
        /// <paramref name="assembly"/>.<br/>Make sure that the <see cref="Type"/>s you want to check if registered
        /// have a [<see cref="ComVisibleAttribute"/>(<see langword="true"/>)]<br/>attribute.<br/>
        /// <paramref name="assembly"/> and any COM visible <see cref="Type"/> in <paramref name="assembly"/>
        /// to be checked for here must have a<br/><see cref="GuidAttribute"/> attribute.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to cehck if any <see cref="Type"/>s were already registered for COM.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the types in <paramref name="assembly"/> were already registerd for COM, else
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="assembly"/> and/or a <see cref="Type"/> in <paramref name="assembly"/> is missing a
        /// <see cref="GuidAttribute"/> attribute.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Missing UAC permissions to read from/write to registry (<see cref="Registry.ClassesRoot"/>).
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// A value in the registry that was read was marked for deletion.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// An attempt to write to a registry key/value that was open as readonly (or user does not have permission to
        /// do so) was made.
        /// </exception>
        internal static bool IsAssemblyRegistered(Assembly assembly)
        {
            string asmGuid = GetAssemblyRegGUID(assembly);
            Type[] types = regSvc.GetRegistrableTypesInAssembly(assembly);
            foreach (Type type in types)
            {
                string guid = GetTypeRegGUID(type);
                if (type.IsValueType)
                {
                    using (RegistryKey recKey = Registry.ClassesRoot.OpenSubKey("Record"))
                    {
                        if (recKey != null)
                        {
                            using (RegistryKey guidKey = recKey.OpenSubKey(guid))
                            {
                                if (guidKey != null)
                                    return true;
                            }
                        }
                    }
                }
                else if (regSvc.TypeRepresentsComType(type))
                {
                    using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                    {
                        if (clsidKey != null)
                        {
                            using (RegistryKey guidKey = clsidKey.OpenSubKey(guid))
                            {
                                if (guidKey != null)
                                    return true;
                            }
                        }
                    }
                }
                else
                {
                    string prgId = regSvc.GetProgIdForType(type);
                    if (prgId != null && prgId != string.Empty)
                    {
                        using (RegistryKey prgIdKey = Registry.ClassesRoot.OpenSubKey(prgId))
                        {
                            if (prgIdKey != null)
                            {
                                using (RegistryKey clsidKey = prgIdKey.OpenSubKey("CLSID"))
                                {
                                    if (clsidKey != null)
                                    {
                                        object regVal = clsidKey.GetValue("", string.Empty);
                                        if (regVal is string regGuid && regGuid == guid)
                                            return true;
                                    }
                                }
                            }
                        }
                    }

                    using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                    {
                        if (clsidKey != null)
                        {
                            using (RegistryKey guidKey = clsidKey.OpenSubKey(guid))
                            {
                                if (guidKey != null)
                                {
                                    if (prgId != null && prgId != string.Empty)
                                    {
                                        using (RegistryKey prgIdKey = guidKey.OpenSubKey("ProgId"))
                                        {
                                            if (prgIdKey != null)
                                            {
                                                object regVal = prgIdKey.GetValue("", string.Empty);
                                                if (regVal is string regProgId && regProgId == prgId)
                                                    return true;
                                            }
                                        }
                                    }
                                    else
                                        return true;
                                }
                            }
                        }
                    }
                }
            }

            using (RegistryKey tlKey = Registry.ClassesRoot.OpenSubKey("TypeLib"))
            {
                if (tlKey != null)
                {
                    using (RegistryKey guidKey = tlKey.OpenSubKey(asmGuid))
                    {
                        if (guidKey != null)
                            return true;
                    }
                }
            }

            return false;
        }

        private static void CheckForAssemblyGUID(Assembly assembly) => _ = GetAssemblyGUID(assembly);

        private static string GetAssemblyRegGUID(Assembly assembly)
            => $"{{{GetAssemblyGUID(assembly).Value.ToString().ToUpper(CultureInfo.InvariantCulture)}}}";

        private static GuidAttribute GetAssemblyGUID(Assembly assembly)
        {
            GuidAttribute guidAttr = assembly.GetCustomAttribute<GuidAttribute>();
            if (guidAttr != null)
                return guidAttr;
            else
                throw new InvalidOperationException("Assembly to register must have a GUID attribute to ensure no " +
                    "collision and to determine if registered. Marshal.GetTypeLibGuidForAssembly(...) is not " +
                    "reliable without this.");
        }

        private static void CheckForTypeGUIDs(Assembly assembly)
        {
            Type[] types = regSvc.GetRegistrableTypesInAssembly(assembly);
            foreach (Type type in types)
                CheckForTypeGUID(type);
        }

        private static void CheckForTypeGUID(Type type) => _ = GetTypeGUID(type);

        private static string GetTypeRegGUID(Type type)
            => $"{{{GetTypeGUID(type).Value.ToString().ToUpper(CultureInfo.InvariantCulture)}}}";

        private static GuidAttribute GetTypeGUID(Type type)
        {
            GuidAttribute guidAttr = type.GetCustomAttribute<GuidAttribute>();
            if (guidAttr != null)
                return guidAttr;
            else
                throw new InvalidOperationException("Type to register must have a GUID attribute to ensure no " +
                    "collision and to determine if registered.\r\nMarshal.GenerateGuidForType(...) and " +
                    "typeof(...).GUID are not reliable without this.");
        }
    }
}
