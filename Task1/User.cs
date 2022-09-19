using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
  internal class User
  {
    public string Name;
    public int Point;
    public User(string name, int point)
    {
      this.Name = name;
      this.Point = point;
    }

    public bool Equals(User other)
    {
      return Name != other.Name ? false : true;
    }
    public override bool Equals(object obj)
    {
      if (obj is not User) return false;
      if (obj == null) return false;
      return Equals(obj as User);
    }
    public override int GetHashCode()
    {
      if (Name == null) return 0;
      return Name.GetHashCode();
    }
  }
}
