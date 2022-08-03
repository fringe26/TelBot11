using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelBot11.Services
{
    public class SentenceSizeService
    {
        public int GetLenth(string message)
        {
            return message.Length;
        }
    }
}
