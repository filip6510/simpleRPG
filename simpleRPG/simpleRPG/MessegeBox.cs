using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simpleRPG
{
    class MessageBox
    {
        private string[] messages;
        private Label textDisplayer;
        public MessageBox(Label label)
        {
            textDisplayer = label;
            messages = new string[6];
            for (int i = 0; i < 6; i++)
                messages[i] = "";
        }
        public void SendMsg(string text)
        {
            for (int i = 5; i >= 1; i--)
                messages[i] = messages[i -1];
            messages[0] = text;
            textDisplayer.Text = GetText();
        }
        public string GetText()
        {
            string result = messages[5];
            for (int i = 4; i >= 0; i--)
                result = messages[i] + "\n" + result;
            return result;
        }
        
    }
}
