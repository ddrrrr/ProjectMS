using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectMS.Core.DataSet
{
    /// <summary>
    /// 保存在内存的单个信号序列
    /// 有限定长度，读写锁定
    /// </summary>
    class SignalDataInRam
    {
        public int Length { get; }
        private ReaderWriterLockSlim datalock;
        public event Action<List<DateTime>, List<double>> UpdateEvent;
        private List<DateTime> _time;
        private List<double> _value;
        public List<DateTime> time
        {
            set
            {
                _time = value;
            }
            get
            {
                var r = new List<DateTime>();
                datalock.EnterReadLock();
                _time.ForEach((x) => r.Add(x));
                datalock.ExitReadLock();
                return r;
            }
        }
        public List<double> value
        {
            set
            {
                _value = value;
            }
            get
            {
                var r = new List<double>();
                datalock.EnterReadLock();
                _value.ForEach((x) => r.Add(x));
                datalock.ExitReadLock();
                return r;
            }
        }

        public SignalDataInRam(string name, int L)
        {
            Length = L;
            datalock = new ReaderWriterLockSlim();
            _time = new List<DateTime>();
            _value = new List<double>();
        }
        public void Add(DateTime t, double v)
        {
            if (datalock.TryEnterWriteLock(new TimeSpan(1)))
            {
                _time.Add(t);
                _value.Add(v);
                int popnum = 0;
                if (_time.Count > this.Length)
                {
                    popnum = _time.Count - this.Length;
                    _time.RemoveRange(0, popnum);
                    _value.RemoveRange(0, popnum);
                }
                datalock.ExitWriteLock();
                UpdateEvent?.Invoke(new List<DateTime>() { t }, new List<double>() { v });
            }
        }
        public void AddRange(List<DateTime> t, List<double> v)
        {
            if (datalock.TryEnterWriteLock(new TimeSpan(1)))
            {
                _time.AddRange(t);
                _value.AddRange(v);

                int popnum = 0;
                if (_time.Count > this.Length)
                {
                    popnum = _time.Count - this.Length;
                    _time.RemoveRange(0, popnum);
                    _value.RemoveRange(0, popnum);
                }

                datalock.ExitWriteLock();
                UpdateEvent?.Invoke(t, v);
            }
        }

        public double Weight;

        public void ClearUpdateEvent()
        {
            foreach (var x in UpdateEvent?.GetInvocationList())
                UpdateEvent -= (Action<List<DateTime>, List<double>>)x;
        }
    }
}
