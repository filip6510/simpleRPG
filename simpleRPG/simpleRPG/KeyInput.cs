using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simpleRPG
{
    class KeyInput
    {
        private static Dictionary<Keys, bool> keyTable = new Dictionary<Keys, bool>();
        public static void SetKeyState(Keys key, bool state)
        {
            if (keyTable.ContainsKey(key))
                keyTable[key] = state;
            else
                keyTable.Add(key, state);
        }
        static public bool UseKey (Keys key)
        {
            if (IsPressed(key))
            {
                SetKeyState(key, false);
                return true;
            }
            return false;
        }
        public static bool IsPressed (Keys key)
        {

            if (keyTable.ContainsKey(key))
            {
                return keyTable[key];
                /*if( keyTable[key])
                {
                    SetKeyState(key,false);
                    return true;
                }
                return false;*/
            }
            return false;
        }
    }
}
