//
// AssemblyInfo.cs
//
// Author:
//       Zach Deibert <zachdeibert@gmail.com>
//
// Copyright (c) 2016 Latipium
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

[assembly: AssemblyTitle("Com.Latipium.Daemon")]
[assembly: AssemblyDescription("The daemon the website will connect to in order to launch Latipium")]
[assembly: AssemblyCompany("Latipium")]
[assembly: AssemblyProduct("Latipium Launcher Daemon")]
[assembly: AssemblyCopyright("Latipium")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif BETA
[assembly: AssemblyConfiguration("Release (Beta)")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AllowPartiallyTrustedCallers]
[assembly: CLSCompliant(true)]
[assembly: SecurityRules(SecurityRuleSet.Level1)]
