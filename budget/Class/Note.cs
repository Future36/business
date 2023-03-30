using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Class
{
    internal class Note
    {
        private string NameNote;
        private string Type;
        private double Money;
        private bool StatusMoney;
        public Note(string nameNote, string type, double money, string date)
        {
            this.NameNote = nameNote;
            this.Type = type;
            this.Money = money;
            this.date = date;
            if (money > 0)
            {
                StatusMoney = true;
            }
            else
            {
                StatusMoney = false;
            }
        }
        public string nameNote
        {
            get { return NameNote; }
            set { NameNote = value; }
        }
        public bool status_money
        {
            get { return StatusMoney; }
            set { StatusMoney = value; }
        }

        public string date { get; set; }

        public double money
        {
            get { return Money; }
            private set
            {
                if (value < 0)
                {
                    money = -1 * value;
                }
            }
        }

        public string type
        {
            get { return Type; }
            set { Type = value; }
        }
    }
}
