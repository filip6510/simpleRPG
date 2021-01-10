using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    interface IMoveAble
    {
        void Move(IEnumerable<Texture> textures);
    }
}
