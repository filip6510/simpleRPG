using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{

    class FriendlyCharacter : GameObject
    {
        public NpcDialogPart Dialog { get; private set; }
        public FriendlyCharacter (int x, int y, string name, NpcDialogPart dialog)
        {
            ObjectPosition = new System.Drawing.Rectangle(x, y, Global.CharacterSize, Global.CharacterSize);
            Name = name;
            Dialog = dialog;
        }
    }
}
