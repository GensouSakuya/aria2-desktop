using System;
using System.Collections.Generic;
using System.Linq;

namespace GensouSakuya.Aria2.Desktop.Model
{
    public class DownloadStatusInfoList
    {
        private List<DownloadTask> _list = new List<DownloadTask>();
        private volatile object locker = new object();

        public List<DownloadTask> List
        {
            get
            {
                var tempList = new List<DownloadTask>();
                lock (locker)
                {
                    _list.ForEach(item => tempList.Add(item));
                }
                return tempList;
            }
            set
            {
                lock (locker)
                {
                    _list = value;
                }
            }
        }

        public List<DownloadTask> GetWithCondition(Func<DownloadTask, bool> condition)
        {
            var tempList = new List<DownloadTask>();
            lock (locker)
            {
                tempList = _list.Where(condition).ToList();
            }
            return tempList;
        }

        public List<DownloadTask> RemoveAll(Predicate<DownloadTask> condition)
        {
            var tempList = new List<DownloadTask>();
            lock (locker)
            {
                _list.RemoveAll(condition);
                _list.ForEach(item => tempList.Add(item));
            }
            return tempList;
        }

        public List<DownloadTask> AddRange(IEnumerable<DownloadTask> list)
        {
            var tempList = new List<DownloadTask>();
            lock (locker)
            {
                _list.AddRange(list);
                _list.ForEach(item => tempList.Add(item));
            }
            return tempList;
        }

        public DownloadStatusInfoList() { }

        public DownloadStatusInfoList(IEnumerable<DownloadTask> list)
        {
            _list = list.ToList();
        }
    }
}
