﻿//
// DirectoryController.cs
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
using System.IO;
using System.Web.Http;
using Com.Latipium.Daemon.Model;

namespace Com.Latipium.Daemon.Controllers {
    /// <summary>
    /// Directory controller.
    /// </summary>
    public class DirectoryController : ApiController {
        /// <summary>
        /// Performs the get request.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public DirectoryObject Get(string id) {
            Request.Check();
            return new DirectoryObject(id.ExpandParameter());
        }

        /// <summary>
        /// Performs the put request.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public DirectoryObject Put(string id) {
            Request.Check();
            Directory.CreateDirectory(id.ExpandParameter());
            return Get(id);
        }

        /// <summary>
        /// Performs the delete request.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public DirectoryObject Delete(string id) {
            Request.Check();
            Directory.Delete(id.ExpandParameter());
            return Get(id);
        }
    }
}
