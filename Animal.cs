using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecosystem
{
    internal class Animal
    {
        protected char icon;
        protected int index;

        public int move()
        {
            Random rnd = new Random();
            int position = rnd.Next(-1, 2);
            return position;
        }
    }
    class Bear : Animal
    {
        public Bear()
        {
            this.icon = 'B';
        }
        public Char Icon
        {
            get
            {
                return icon;
            }
        }
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
    }
    class Fish : Animal
    {
        public Fish()
        {
            this.icon = 'F';
        }
        public Char Icon
        {
            get
            {
                return icon;
            }
        }
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
    }
}
