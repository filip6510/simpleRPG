using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    interface IFightAble
    {
        bool IsAlive();
        int ReciveDemage(int value);// decrement health of object 
        int Attack();// return value of demage that attack deal
    }
}
