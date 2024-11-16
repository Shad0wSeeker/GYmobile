﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYmobile.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string ChatName { get; set; }
        public string Message { get; set; }

        public string SenderName { get; set; }
    }
}
