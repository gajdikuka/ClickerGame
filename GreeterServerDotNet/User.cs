using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeterServerDotNet
{
  class User
  {
    private string username;
    private int clicks;
    private string password;

    public string Username
    {
      get { return username; }
      set
      {
        if (value == null)
          throw new NullReferenceException();
        if (value == string.Empty)
          throw new ArgumentOutOfRangeException();
        username = value;
      }
    }

    public int Clicks
    {
      get { return clicks; }
      set
      {
        clicks = value;
      }
    }

    public string Password
    {
      get { return password; }
      set
      {
        if (value == null)
          throw new NullReferenceException();
        if (value == string.Empty)
          throw new ArgumentOutOfRangeException();
        password = value;
      }
    }
  }
}
