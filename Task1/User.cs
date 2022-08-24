using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
  internal class User
  {
    public string name;
    public User(string name) { this.name = name; }

    public bool Equals(User other)
    {
      return name != other.name ? false : true;
    }
    public override bool Equals(object obj)
    {
      if (obj is not User) return false;
      if (obj == null) return false;
      return Equals(obj as User);
    }
    public override int GetHashCode()
    {
      if (name == null) return 0;
      return name.GetHashCode();
    }
  }
}
