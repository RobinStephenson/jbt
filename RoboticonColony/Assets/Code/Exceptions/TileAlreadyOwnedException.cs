using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Exceptions
{
    class TileAlreadyOwnedException: Exception
    {
        public TileAlreadyOwnedException()
        {
        }

        public TileAlreadyOwnedException(string message) : base(message)
        {
        }

        public TileAlreadyOwnedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
