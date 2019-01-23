// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreeterServerDotNet;
using Grpc.Core;
using Helloworld;

namespace GreeterServer
{
  
  class GreeterImpl : Greeter.GreeterBase
  {
    
    
    static DataBaseManager db = new DataBaseManager();

    public override Task<Reply> listEvent(Username request, ServerCallContext context)
    {
      string userek = "";
      List<User> f = db.Select();
      foreach (User f1 in f)
        userek += f1.Username +" "+ f1.Clicks + Environment.NewLine;
      return Task.FromResult(new Reply { Message = userek });
    }

    public override Task<Clicks> ClickBetoltEvent(Username request, ServerCallContext context)
    {
      string clicks = db.ClickBetolt(request.Name);
      return Task.FromResult(new Clicks { Ertek = clicks });
    }

    public override Task<Success> ClickEvent(Username name, ServerCallContext context)
    {
      db.Update(name.Name.ToString());
      return Task.FromResult(new Success { Sikerult = true });
    }
    public override Task<Success> ResetEvent(Username name, ServerCallContext context)
    {
      db.Reset(name.Name.ToString());
      return Task.FromResult(new Success { Sikerult = true });
    }
    public override Task<Success> AddEvent(Usercred cred, ServerCallContext context)
    {
      string username = cred.Name;
      string password = cred.Password;
      bool temp = db.Register(username, password);
      return Task.FromResult(new Success { Sikerult = temp });
    }

    public override Task<Success> LoginEvent(Usercred cred, ServerCallContext context)
    {
      string username = cred.Name;
      string password = cred.Password;
      bool vanilyen = db.Login(username, password);
      return Task.FromResult(new Success { Sikerult = vanilyen });
    }

  }
  

  class Program
  {
    const int Port = 50051;
    public static void Main(string[] args)
    {
      

      Server server = new Server
      {
        Services = { Greeter.BindService(new GreeterImpl()) },
        Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
      };
      server.Start();

      Console.WriteLine("Server listening on port " + Port);
      Console.WriteLine("Press any key to stop the server...");


      Console.ReadKey();

      server.ShutdownAsync().Wait();
    }
  }
}
