﻿//
// StartApi.cs
//
// Author:
//       Zach Deibert <zachdeibert@gmail.com>
//
// Copyright (c) 2017 Zach Deibert
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
using Com.Latipium.Daemon.Api.Model;
using Com.Latipium.Daemon.Model;
using Com.Latipium.Daemon.Platform;

namespace Com.Latipium.Daemon.Apis {
    public class StartApi : AbstractApi<object, StartSessionResponse> {
        public override StartSessionResponse Handle(object req, ApiClient client) {
            if (client == null) {
                client = Server.RegisterClient();
            }
            DisplayDetectData display = PlatformFactory.Proxy.DetectDisplay(client.Id.ToString());
            if (display.Detected) {
                client.Display = display;
            }
            client.Type = ClientType.Launcher;
            return new StartSessionResponse() {
                Display = display,
                ClientId = client.Id
            };
        }

        public StartApi() : base("/session/start") {
        }
    }
}

