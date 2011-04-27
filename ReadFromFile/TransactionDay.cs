using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadFromFile
{
    public class TransactionDay
    {
        public TransactionDay(DateTime date, decimal first, decimal last, decimal high, decimal low, decimal value, long volume)
        {
            Date = date;
            First = first;
            Last = last;
            High = high;
            Low = low;
            Value = value;
            Volume = volume;
            Average = Value / Volume;
        }

        public TransactionDay(DateTime date, decimal first, decimal last, decimal high, decimal low)
        {
            Date = date;
            First = first;
            Last = last;
            High = high;
            Low = low;
            Value = 0;
            Volume = 0;
            Average = 0;
        }

        public DateTime Date { get; private set; }
        public decimal First { get; private set; }
        public decimal Last { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public long Volume { get; private set; }
        public decimal Value { get; private set; }
        public decimal Average { get; private set; }
    }
}
