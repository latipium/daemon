﻿//
// LaunchedProcess.cs
//
// Author:
//       Zach Deibert <zachdeibert@gmail.com>
//
// Copyright (c) 2016 Zach Deibert
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Com.Latipium.Daemon.Model;

namespace Com.Latipium.Daemon.Controllers {
    internal class LaunchedProcess {
        private Process Proc;
        private Stream StdIn;
        internal StreamReader StdOut;
        internal StreamReader StdErr;
        internal List<string> StdOutLines;
        internal List<string> StdErrLines;
        internal Task<string> StdOutTask;
        internal Task<string> StdErrTask;

        public ProcessData Data {
            get {
                return GetData(true);
            }
        }

        private ProcessData GetData(bool canWait) {
            string[] stdOut;
            if (StdOutLines.Count > 0) {
                lock (StdOutLines) {
                    stdOut = StdOutLines.ToArray();
                    StdOutLines.Clear();
                }
            } else {
                stdOut = new string[0];
            }
            string[] stdErr;
            if (StdErrLines.Count > 0) {
                lock (StdErrLines) {
                    stdErr = StdErrLines.ToArray();
                    StdErrLines.Clear();
                }
            } else {
                stdErr = new string[0];
            }
            bool isRunning = !Proc.HasExited;
            if (canWait && isRunning && stdOut == null && stdErr == null) {
                Task.WaitAny(new [] { StdOutTask, StdErrTask }, 4750);
                Thread.Sleep(250);
                return GetData(false);
            }
            int exitCode;
            if (isRunning) {
                exitCode = -65537;
            } else {
                exitCode = Proc.ExitCode;
            }
            return new ProcessData(stdOut, stdErr, isRunning, exitCode);
        }

        public void Kill() {
            StdIn.Close();
            StdOut.Close();
            StdErr.Close();
            StdIn.Dispose();
            StdOut.Dispose();
            StdErr.Dispose();
            if (!Proc.HasExited) {
                Proc.Kill();
            }
            Proc.Dispose();
        }

        public void SupplyStdIn(Stream stream) {
            stream.CopyTo(StdIn);
            StdIn.WriteByte((byte) '\n');
        }

        public LaunchedProcess(ProcessInformation info) {
            Console.WriteLine("{0}@{1}:{2}$ {3} {4}", Environment.UserName, Environment.UserDomainName, info.WorkingDirectory, info.FileName, info.Arguments);
            Proc = Process.Start(info);
            StdIn = Proc.StandardInput.BaseStream;
            StdOut = Proc.StandardOutput;
            StdErr = Proc.StandardError;
            StdOutLines = new List<string>();
            StdErrLines = new List<string>();
            StdOutTask = StdOut.ReadLineAsync();
            StdErrTask = StdErr.ReadLineAsync();
        }
    }
}
